using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BeginReceive
{
    public class StateObject
    {
        public List<Socket> workSockets = new List<Socket>();
        public const int BUFFER_SIZE = 16;
        public byte[] buffer = new byte[BUFFER_SIZE];
        public StringBuilder sb = new StringBuilder();

        public int SendInterval;
        public string MessageToSend;
        public int EndChar = 3;

        public IPEndPoint ipEndPoint;
        public string IpAddress;
        public int TcpPortNumber;
        public System.Net.Sockets.TcpListener SocketTcpListener;

        private List<System.Timers.Timer> sendTimers = new List<System.Timers.Timer>();

        public StateObject()
        {
            this.IpAddress = "127.0.0.1";
            this.TcpPortNumber = 11000;
            this.SendInterval = -1;
            this.MessageToSend = "";
        }
        public StateObject(string ip, int portnumber)
        {
            this.IpAddress = ip;
            this.TcpPortNumber = portnumber;
            this.SendInterval = -1;
            this.MessageToSend = "";
        }//

        public StateObject(string ip, int portnumber, int sendInterval, string messageToSend)
        {
            this.IpAddress = ip;
            this.TcpPortNumber = portnumber;
            this.SendInterval = sendInterval;
            this.MessageToSend = messageToSend + (char)EndChar;
        }//


        public void Start()
        {
            try
            {
                IPAddress ip;
                if (IPAddress.TryParse(IpAddress, out ip))
                {
                    ipEndPoint = new IPEndPoint(ip, TcpPortNumber);
                    SocketTcpListener = new System.Net.Sockets.TcpListener(ipEndPoint);
                    SocketTcpListener.ExclusiveAddressUse = false;
                    SocketTcpListener.Start();
                    SocketTcpListener.BeginAcceptSocket(Conexion_recibida, SocketTcpListener);
                }
                else
                    throw new Exception(string.Format("The Ip Address {0} is not valid.", ip));
            }
            catch (Exception e)
            {
                //TraceManager.CustomComponent.TraceError(e, true);
                Console.WriteLine("exception. " + e);
            }
            Console.WriteLine("Conected to Tcplistener server at: " + ipEndPoint + " port :" + TcpPortNumber);
        }//start
        public void Conexion_recibida(IAsyncResult ar)
        {

            try
            {

                if (SocketTcpListener == null)
                    return;
                Socket workSocket = SocketTcpListener.EndAcceptSocket(ar);
                workSockets.Add(workSocket);
                Console.WriteLine("Conection accepted from: {0} at {1}", workSocket.RemoteEndPoint.ToString(), DateTime.Now);               
                workSocket.BeginReceive(buffer, 0, BUFFER_SIZE, 0, new AsyncCallback(LecturaConexion_recibida), workSocket);

                if (SendInterval > 0)
                {
                    System.Timers.Timer sendTimer = new System.Timers.Timer(SendInterval);
                    sendTimers.Add(sendTimer);
                    sendTimer.Elapsed += SendTimer_Elapsed;
                    sendTimer.Start();
                }

                SocketTcpListener.BeginAcceptSocket(Conexion_recibida, SocketTcpListener);

            }
            finally
            {
                // this.controlledTermination.Leave();
            }
        }

        private void SendTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int socketPosition = 0;
            System.Timers.Timer sendTimer = (System.Timers.Timer)sender;

            for (int i = 0; i < sendTimers.Count; i++)
                if (sendTimers.ToArray()[i].Equals(sendTimer))
                {
                    socketPosition = i;
                    break;
                }

            if (socketPosition < sendTimers.Count)
            {
                Socket workSocket = workSockets.ToArray()[socketPosition];
                if (workSocket.Connected)
                {
                    byte[] sendBuffer = Encoding.ASCII.GetBytes(MessageToSend);

                    workSocket.BeginSend(sendBuffer, 0, sendBuffer.Length, SocketFlags.None, DataSent, workSocket);
                }
            }
        }

        private void DataSent(IAsyncResult ar)
        {
            Socket workSocket = (Socket)ar.AsyncState;
            int sent = workSocket.EndSend(ar);
            Console.WriteLine("{0} bytes sent to {1} at {2}", sent, workSocket.RemoteEndPoint.ToString(), DateTime.Now);
        }

        public void LecturaConexion_recibida(IAsyncResult ar)
        {
            try
            {
                char endC = (char)EndChar;
                //StateObject so = (StateObject)ar.AsyncState;
                // Socket s = SocketTcpListener.EndAcceptSocket(ar);
                Socket workSocket = (Socket)ar.AsyncState;

                int read = workSocket.EndReceive(ar);

                if (read > 0)
                {
                    sb.Append(Encoding.ASCII.GetString(buffer, 0, read));

                }


                if (read == 0 || sb.Length > 1)
                {
                    if (sb.ToString().Contains(endC.ToString()))
                    {
                        //All of the data has been read, so displays it to the console
                        string strContent;
                        strContent = sb.ToString();
                        strContent = strContent.Substring(0, strContent.IndexOf(endC) - 1);
                        Console.WriteLine(String.Format("Read {0} bytes from {3}. Data = {1} at {2}", strContent.Length, strContent, DateTime.Now, workSocket.RemoteEndPoint.ToString()));
                        sb.Clear();
                    }
                }


                workSocket.BeginReceive(buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(LecturaConexion_recibida), workSocket);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // this.controlledTermination.Leave();
            }
        }
    }//class
}
