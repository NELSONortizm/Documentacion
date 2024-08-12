using System;
using System.IO;
using System.ComponentModel;
using Microsoft.BizTalk.Message.Interop;
using Microsoft.BizTalk.Component.Interop;
using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;
using MobileTec.BizTalk.Utilities.Configuration.Helper;
using MobileTec.BizTalk.Utilities.Configuration.Helper.DTO;

namespace MobileTec.BizTalk.Cad.PipelineComponents
{
    /// <summary>
    /// Esta clase clona el mensaje recibido generando un nuevo mensaje para cada destino según la configuración.
    /// </summary>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("a378cd79-6b8e-45a7-a4a8-fcda0f6cff10")]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    public class EventMessageDuplicator : Microsoft.BizTalk.Component.Interop.IDisassemblerComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        public EventMessageDuplicator()
        {
        }

        #region Variables 
        /// <summary>
        /// Todos los mensajes que se generan durante el proceso de clonado.
        /// </summary>
        private System.Collections.Queue _msgs = new System.Collections.Queue();
        /// <summary>
        /// Indica si ya se inició el trace al entregar los mensajes clonados.
        /// </summary>
        bool TraceInitiated = false;

        System.Guid GcallToken = System.Guid.NewGuid();
        #endregion

        #region Propiedades

        private string _XpathAgencyID;

        /// <summary>
        /// Expresión Xpath para obtener AgencyID del mensaje recibido
        /// </summary>
        [Description("Xpath expression to obtain the AgencyID from the received message")]
        public string XpathAgencyID
        {
            get { return _XpathAgencyID; }
            set { _XpathAgencyID = value; }
        }

        private string _XpathDepartment;

        /// <summary>
        /// Expresión Xpath para obtener el identificador del Departmento
        /// </summary>
        public string XpathDepartment
        {
            get { return _XpathDepartment; }
            set { _XpathDepartment = value; }
        }

        private string _XpathHeaderTarget;

        /// <summary>
        /// Expresión Xpath para obtener el nodo con el destino del mensaje
        /// </summary>
        public string XpathHeaderTarget
        {
            get { return _XpathHeaderTarget; }
            set { _XpathHeaderTarget = value; }
        }

        private string _XpathHeaderAgencyName;

        /// <summary>
        /// Expresión Xpath para obtener el nodo con la agencia del mensaje
        /// </summary>
        public string XpathHeaderAgencyName
        {
            get { return _XpathHeaderAgencyName; }
            set { _XpathHeaderAgencyName = value; }
        }

        private string _XpathHeaderDepartmentName;

        /// <summary>
        /// Expresión Xpath para obtener el nodo del encabezado con el departamento al que pertenece la agencia
        /// </summary>
        public string XpathHeaderDepartmentName
        {
            get { return _XpathHeaderDepartmentName; }
            set { _XpathHeaderDepartmentName = value; }
        }

        private string _XpahtHeaderEndPoint;

        /// <summary>
        /// Expresión Xpath para obtener el nodo del encabezado con el ID del EndPoint al que se envía el mensaje
        /// </summary>
        public string XpathHeaderEndPoint
        {
            get { return _XpahtHeaderEndPoint; }
            set { _XpahtHeaderEndPoint = value; }
        }

        private string _DefaultDepartment;

        /// <summary>
        /// Indica el nombre del departamento por defecto que se utiliza para configurar destinos que no requieren una
        /// departamento configurado
        /// </summary>
        public string DefaultDepartment
        {
            get { return _DefaultDepartment; }
            set { _DefaultDepartment = value; }
        }

        private string _DefaultAgency;

        /// <summary>
        /// Indica el nombre de la agencia por defecto que se utiliza para configurar destinos que no requieren una
        /// agencia configurada
        /// </summary>
        public string DefaultAgency
        {
            get { return _DefaultAgency; }
            set { _DefaultAgency = value; }
        }

        private string _XPathAdditionalParameters;

        /// <summary>
        /// Indica la ruta Xpath donde se encuentra el nodo con los parámetros adicionales
        /// </summary>
        public string XPathAdditionalParameters
        {
            get { return _XPathAdditionalParameters; }
            set { _XPathAdditionalParameters = value; }
        }

        private string _XPathNodeToInsertAdditionalParameters;

        /// <summary>
        /// Indica la ruta Xpath del nodo al que se le deben insertar los parámetros adicionales
        /// </summary>
        public string XPathNodeToInsertAdditionalParameters
        {
            get { return _XPathNodeToInsertAdditionalParameters; }
            set { _XPathNodeToInsertAdditionalParameters = value; }
        }

        #endregion

        #region IBaseComponent members
        /// <summary>
        /// Nombre del componente
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return "Event Message Duplicator";
            }
        }
        
        /// <summary>
        /// Versión del componente
        /// </summary>
        [Browsable(false)]
        public string Version
        {
            get
            {
                return "1.0";
            }
        }
        
        /// <summary>
        /// Descripción del componente
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return "Clone message for each destination based on configuration";
            }
        }
        #endregion
        
        #region IPersistPropertyBag members
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classID">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out System.Guid classID)
        {
            classID = new System.Guid("a378cd79-6b8e-45a7-a4a8-fcda0f6cff10");
        }
        
        /// <summary>
        /// initialize a new instance
        /// </summary>
        public void InitNew()
        {
            XpathAgencyID = "";
            XpathDepartment = "";
            XpathHeaderTarget = "";
            XpathHeaderAgencyName = "";
            XpathHeaderDepartmentName = "";
            XpathHeaderEndPoint = "";
            DefaultDepartment = "";
            DefaultAgency = "";
            XPathAdditionalParameters = "";
            XPathNodeToInsertAdditionalParameters = "";
        }

        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="errorLog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, int errorLog)
        {
            try
            {
                object valor;

                valor = ReadPropertyBag(propertyBag, "XpathAgencyID", errorLog);
                if (valor != null)
                    XpathAgencyID = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XpathDepartment", errorLog);
                if (valor != null)
                    XpathDepartment = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XpathHeaderTarget", errorLog);
                if (valor != null)
                    XpathHeaderTarget = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XpathHeaderAgencyName", errorLog);
                if (valor != null)
                    XpathHeaderAgencyName = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XpathHeaderDepartmentName", errorLog);
                if (valor != null)
                    XpathHeaderDepartmentName = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XpathHeaderEndPoint", errorLog);
                if (valor != null)
                    XpathHeaderEndPoint = (string)valor;

                valor = ReadPropertyBag(propertyBag, "DefaultDepartment", errorLog);
                if (valor != null)
                    DefaultDepartment = (string)valor;

                valor = ReadPropertyBag(propertyBag, "DefaultAgency", errorLog);
                if (valor != null)
                    DefaultAgency = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XPathNodeToInsertAdditionalParameters", errorLog);
                if (valor != null)
                    XPathNodeToInsertAdditionalParameters = (string)valor;

                valor = ReadPropertyBag(propertyBag, "XPathAdditionalParameters", errorLog);
                if (valor != null)
                    XPathAdditionalParameters = (string)valor;
            }
            catch
            { }
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="clearDirty">not used</param>
        /// <param name="saveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            WritePropertyBag(propertyBag, "XpathAgencyID", (object)XpathAgencyID);
            WritePropertyBag(propertyBag, "XpathDepartment", (object)XpathDepartment);
            WritePropertyBag(propertyBag, "XpathHeaderTarget", (object)XpathHeaderTarget);
            WritePropertyBag(propertyBag, "XpathHeaderAgencyName", (object)XpathHeaderAgencyName);
            WritePropertyBag(propertyBag, "XpathHeaderDepartmentName", (object)XpathHeaderDepartmentName);
            WritePropertyBag(propertyBag, "XpathHeaderEndPoint", (object)XpathHeaderEndPoint);
            WritePropertyBag(propertyBag, "DefaultDepartment", (object)DefaultDepartment);
            WritePropertyBag(propertyBag, "DefaultAgency", (object)DefaultAgency);
            WritePropertyBag(propertyBag, "XPathAdditionalParameters", (object)XPathAdditionalParameters);
            WritePropertyBag(propertyBag, "XPathNodeToInsertAdditionalParameters", (object)XPathNodeToInsertAdditionalParameters);
        }

        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="propertyBag">Property bag</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, string propertyName, int errorLog)
        {
            object val = null;
            try
            {
                propertyBag.Read(propertyName, out val, errorLog);
            }
            catch (System.ArgumentException )
            {
                return val;
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("Error reading pipeline property: [" + propertyName + "] " + ex.Message + ex.StackTrace);
            }
            return val;
        }
        
        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="propertyBag">Property bag.</param>
        /// <param name="propertyName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, string propertyName, object val)
        {
            try
            {
                propertyBag.Write(propertyName, ref val);
            }
            catch (System.Exception ex)
            {
                throw new ApplicationException("Error writing pipeline property: [" + propertyName + "] " + ex.Message + ex.StackTrace);
            }
        }
        #endregion
        #endregion
        
        #region IComponentUI members
        /// <summary>
        /// Component icon to use in BizTalk Editor
        /// </summary>
        [Browsable(false)]
        public IntPtr Icon
        {
            get
            {
                return Properties.Resources.DuplicateMessage.Handle;
            }
        }
        
        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="projectSystem">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public System.Collections.IEnumerator Validate(object projectSystem)
        {
            // example implementation:
            // yield return "This is a compiler error";
            // yield break;
            return null;
        }
        #endregion

        #region IDisassemblerComponent members
        /// <summary>
        /// called by the messaging engine until returned null, after disassemble has been called
        /// </summary>
        /// <param name="pContext">the pipeline context</param>
        /// <returns>an IBaseMessage instance representing the message created</returns>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage GetNext(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext)
        {
            // get the next message from the Queue and return it
            IBaseMessage Respuesta = null;

            if (TraceInitiated == false)
            {
                TraceInitiated = true;
                GcallToken = TraceManager.PipelineComponent.TraceIn("EventMessageDuplicator_GetNext");
            }

            if (_msgs.Count > 0)
            {
                Respuesta = (IBaseMessage)_msgs.Dequeue();
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator_GetNext - Mensaje entregado: {0}", Respuesta.MessageID.ToString());
            }
            else
            {
                TraceManager.PipelineComponent.TraceOut(GcallToken);
            }

            return Respuesta;
        }
        
        /// <summary>
        /// called by the messaging engine when a new message arrives
        /// </summary>
        /// <param name="pContext">the pipeline context</param>
        /// <param name="pInMsg">the actual message</param>
        public void Disassemble(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
        {
            System.Guid callToken = TraceManager.PipelineComponent.TraceIn("EventMessageDuplicator");
            try
            {
                string ScopeName = "EventMessageDuplicator_Disassemble";
                long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

                XmlDocument Mensaje;

                //Se configura un XmlDocument con el Body del mensaje recibido
                Mensaje = ObtenerMensaje(pInMsg);

                //Se promueve la propiedad MessageType
                string MessageType = PromoverMessageType(pInMsg, Mensaje);

                //Se agregan los parámetros adicionales (si se configuraron)
                AgregarParametrosAdicionales(Mensaje);

                GenerarMensajesPorDestino(pContext, pInMsg, Mensaje, MessageType);

                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                TraceManager.PipelineComponent.TraceOut(callToken);
            }
            catch (System.Exception ex)
            {
                TraceManager.PipelineComponent.TraceError(ex, true, callToken);
                TraceManager.PipelineComponent.TraceOut(callToken);
                throw ex;
            }
        }

        private void AgregarParametrosAdicionales(XmlDocument mensaje)
        {
            XmlNode additionalParametersNode;
            XmlNode additionalParametersParent;
            string[] additionalParameters;
            string ScopeName = "EventMessageDuplicator_AgregarParametrosAdicionales";

            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            if (!string.IsNullOrWhiteSpace(XPathAdditionalParameters) && !string.IsNullOrWhiteSpace(XPathNodeToInsertAdditionalParameters))
            {
                additionalParametersNode = mensaje.SelectSingleNode(XPathAdditionalParameters);
                if (additionalParametersNode != null)
                {
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Valor nodo parametros adicionales: {0}", additionalParametersNode.InnerText);

                    additionalParameters = additionalParametersNode.InnerText.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (additionalParameters.Length > 0)
                    {
                        additionalParametersParent = mensaje.SelectSingleNode(XPathNodeToInsertAdditionalParameters);
                        if (additionalParametersParent != null)
                        {
                            foreach (string KeyValue in additionalParameters)
                            {
                                string[] KeyValueArray = KeyValue.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                if (KeyValueArray.Length > 0)
                                {
                                    XmlNode parameterNode = additionalParametersParent.AppendChild(mensaje.CreateNode(XmlNodeType.Element, KeyValueArray[0], additionalParametersParent.NamespaceURI));
                                    if (KeyValueArray.Length > 1)
                                        parameterNode.InnerText = KeyValueArray[1];

                                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Agregado nodo: {0} - Valor: {1}", parameterNode.Name, parameterNode.InnerText);
                                }
                            }
                        }
                        else
                            TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - No existe el nodo padre para parametros adicionales.");

                    }
                    else
                        TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - El nodo parametros adicionales no tiene datos.");

                }
                else
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - No se encontró nodo de parametros adicionales.");

            }
            else
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - No se configuraron rutas Xpath para parametros adicionales.");

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

        }
        #endregion

        #region Funciones Personalizadas

        private void GenerarMensajesPorDestino(IPipelineContext pContext, IBaseMessage pInMsg, XmlDocument Mensaje, string MessageType)
        {
            XmlNodeList agencias;
            XmlNodeList departamentos = null;

            string departmentName;
            string agencyID;
            
            string ScopeName = "EventMessageDuplicator_ObtenerDestinos";

            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            XmlNode dest = Mensaje.SelectSingleNode(XpathHeaderTarget);

            if (dest != null && string.IsNullOrWhiteSpace(dest.InnerText) == false)
            {
                _msgs.Enqueue(pInMsg);
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - El mensaje ya tiene destino {0}", dest.InnerText);
                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                return;
            }

            if (string.IsNullOrWhiteSpace(XpathAgencyID) && string.IsNullOrWhiteSpace(DefaultAgency))
            {
                _msgs.Enqueue(pInMsg);
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - NO se configuro XPathAgencyID ni DefaultAgency, se conserva el mismo mensaje.");
                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                return;
            }

            if (string.IsNullOrWhiteSpace(XpathAgencyID) == false)
            {
                agencias = Mensaje.SelectNodes(XpathAgencyID);

                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Agencias encontradas {0}", agencias.Count);

                if (string.IsNullOrWhiteSpace(XpathDepartment) == false)
                {
                    departamentos = Mensaje.SelectNodes(XpathDepartment);
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Departamentos encontrados {0}", departamentos.Count);
                }

                if (agencias.Count > 0)
                {
                    for (int index = 0; index < agencias.Count; index++)
                    {
                        XmlNode agencia = agencias.Item(index);

                        agencyID = agencia.InnerText;
                        departmentName = "";

                        if (departamentos != null && departamentos.Count > 0)
                        {
                            departmentName = departamentos.Item(index).InnerText;
                        }

                        EvaluarDestinos(pContext, pInMsg, Mensaje, agencyID, departmentName, MessageType);
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(DefaultAgency) == false)
            {
                agencyID = DefaultAgency;
                departmentName = "";

                if (string.IsNullOrWhiteSpace(DefaultDepartment) == false)
                    departmentName = DefaultDepartment;

                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Se encontro agencia por defecto: Agencia {0} - Departamento {1}", agencyID, departmentName);

                EvaluarDestinos(pContext, pInMsg, Mensaje, agencyID, departmentName, MessageType);

            }

            if (_msgs.Count == 0)
            {
                _msgs.Enqueue(pInMsg);
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - NO se encontraron destinos para el mensaje, se conserva el mismo mensaje.");
            }

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

        }

        private void EvaluarDestinos(IPipelineContext pContext, IBaseMessage pInMsg, XmlDocument Mensaje, string agencyID, string departmentName, string MessageType)
        {
            List<Destino> destinos = ConfigReaderStatic.ObtenerDestinos(agencyID, departmentName, MessageType);

            TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Evaluando destinos Agencia: {0} - Departamento: {1} - Destinos encontrados: {2}", agencyID, departmentName, destinos.Count);

            destinos.ForEach(destino =>
            {
                if (EsDestinoValido(Mensaje, destino, agencyID, departmentName))
                {
                    ActualizarEncabezado(Mensaje, destino, agencyID, departmentName);

                    IBaseMessage MensajeNuevo = DuplicarMensaje(Mensaje, pInMsg, pContext);
                    MensajeNuevo.BodyPart.Data.Seek(0, SeekOrigin.Begin); //Se inicia el apuntar del body

                    _msgs.Enqueue(MensajeNuevo); //Se escribe el mensaje nuevo en la cola de respuesta

                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Mensaje generado para Agencia: {0} - Departamento: {1} - Destino: {2}", agencyID, departmentName, destino.DestinoID);
                }
                else
                {
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - NO se cumple el criterio para Agencia: {0} - Departamento: {1} - Destino: {2}", agencyID, departmentName, destino.DestinoID);
                }
            });
        }

        private XmlDocument ObtenerMensaje(IBaseMessage pInMsg)
        {
            string ScopeName = "EventMessageDuplicator_ObtenerMensaje";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            XmlDocument Mensaje = new XmlDocument();
            Mensaje.Load(pInMsg.BodyPart.Data);
            pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);

            int linea = 0;
            int cantidad = 5000;
            int inicio;

            do
            {
                inicio = linea * cantidad;

                if (Mensaje.OuterXml.Length < (inicio + cantidad))
                    cantidad = Mensaje.OuterXml.Length - inicio;

                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Linea {0}: {1}", linea + 1, Mensaje.OuterXml.Substring(inicio, cantidad));
                linea += 1;
            } while ((inicio + cantidad) < Mensaje.OuterXml.Length);

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

            return Mensaje;
        }

        private string PromoverMessageType(IBaseMessage pInMsg, XmlDocument Mensaje)
        {
            string ScopeName = "EventMessageDuplicator_PromoverMessageType";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);
            string MessageType = (string)pInMsg.Context.Read("MessageType", "http://schemas.microsoft.com/BizTalk/2003/system-properties");

            if (string.IsNullOrWhiteSpace(MessageType))
            {
                MessageType = Mensaje.DocumentElement.NamespaceURI + "#" + Mensaje.DocumentElement.LocalName;
                pInMsg.Context.Promote("MessageType", "http://schemas.microsoft.com/BizTalk/2003/system-properties", MessageType);
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - MessageType promovido: {0}", MessageType);
            }
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            return MessageType;
        }

        /// <summary>
        /// Valida que el destino cumpla la condición según el Xpath definido
        /// </summary>
        /// <param name="mensaje">Mensaje a evaluar</param>
        /// <param name="destino">DTO.Destino que contiene la información del destino a validar</param>
        /// <param name="AgencyID">Id de la agencia que se está evaluando</param>
        /// <param name="departmentName">Id del departamento que se está evaluando</param>
        /// <returns>Verdadero si se cumple la condición del Xpath</returns>

        private bool EsDestinoValido(XmlDocument mensaje, Destino destino, string AgencyID, string departmentName)
        {
            XPathNavigator nav = mensaje.CreateNavigator();
            string xpath;
            bool resultadoCondicion = true;
            bool resultadoUltimaUnidad = true;

            if (string.IsNullOrEmpty(destino.XpathCondicion) && string.IsNullOrEmpty(destino.XpathUltimaUnidadModificada) && string.IsNullOrEmpty(destino.XpathCondicionUltimaUnidad))
                return true;

            if (string.IsNullOrEmpty(destino.XpathCondicion) == false)
            {
                xpath = destino.XpathCondicion.Replace("**AgencyID**", AgencyID).Replace("**DepartmentName**", departmentName);
                resultadoCondicion = (bool)nav.Evaluate(xpath);
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Validar destino: Agencia {0} - Departamento {1} - Destino {2} - Condición mensaje {3} - Resultado {4}", AgencyID, departmentName, destino.DestinoID, xpath, resultadoCondicion);
            }

            if (string.IsNullOrEmpty(destino.XpathUltimaUnidadModificada) == false && string.IsNullOrEmpty(destino.XpathCondicionUltimaUnidad) == false)
            {
                xpath = destino.XpathUltimaUnidadModificada.Replace("**AgencyID**", AgencyID).Replace("**DepartmentName**", departmentName);
                
                XmlNodeList unidades = mensaje.SelectNodes(xpath);
                string maxdate = "";
                XmlNode maxstatus = null;
                foreach (XmlNode node in unidades)
                {
                    if (maxdate.CompareTo(node.InnerText) < 0)
                    {
                        maxdate = node.InnerText;
                        maxstatus = node;
                    }
                }

                if (maxstatus != null)
                {

                    xpath = destino.XpathCondicionUltimaUnidad.Replace("**AgencyID**", AgencyID).Replace("**DepartmentName**", departmentName);

                    resultadoUltimaUnidad = (bool)maxstatus.ParentNode.CreateNavigator().Evaluate(xpath);
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Validar destino: Agencia {0} - Departamento {1} - Destino {2] - Condición unidad {3} - Resultado {4}", AgencyID, departmentName, destino.DestinoID, xpath, resultadoUltimaUnidad);
                }
                else
                {
                    resultadoUltimaUnidad = false;
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Validar destino: Agencia {0} - Departamento {1} - Destino {2] - Condición unidad {3} - No se pudo evaluar condición, el xpath no arrojó resultados.", AgencyID, departmentName, destino.DestinoID, xpath);
                }
            }

            return (resultadoCondicion && resultadoUltimaUnidad);
        }

        /// <summary>
        /// Actualiza el encabezado del mensaje
        /// </summary>
        /// <param name="mensaje">Mensaje a actualizar</param>
        /// <param name="destino">DTO.Destino que contiene la información del destino</param>
        /// <param name="AgencyID">Id de la agencia que se está actualizando</param>
        /// <param name="departmentName">Id del departamento que se está actualizando</param>
        //
        private void ActualizarEncabezado(XmlDocument mensaje, Destino destino, string AgencyID, string departmentName)
        {
            XmlNode dest = mensaje.SelectSingleNode(XpathHeaderTarget);
            if (dest != null)
                dest.InnerText = destino.DestinoID;

            XmlNode agencia = mensaje.SelectSingleNode(XpathHeaderAgencyName);
            if (agencia != null)
                agencia.InnerText = AgencyID;

            XmlNode departamento = mensaje.SelectSingleNode(XpathHeaderDepartmentName);
            if (departamento != null)
                departamento.InnerText = departmentName;

            XmlNode endPoint = mensaje.SelectSingleNode(XpathHeaderEndPoint);
            if (endPoint != null)
                endPoint.InnerText = destino.EndPointID;
        }

        /// <summary>
        /// Este método crea un mensaje basado en un documento xml y le asocia el contexto recibido.
        /// </summary>
        /// <param name="xmlMsg">Documento xml con el que se va a generar el mensaje.</param>
        /// <param name="pInMsg">Mensaje original recibido.</param>
        /// <param name="pContext">Contexto original del mensaje recibido.</param>
        /// <returns>Un nuevo mensaje con el mismo contexto del mensaje original.</returns>
        IBaseMessage DuplicarMensaje(XmlDocument xmlMsg, IBaseMessage pInMsg, IPipelineContext pContext)
        {
            IBaseMessage outMsg = null;

            //Se genera un mensaje nuevo y como Body se le asigna el documento xml
            outMsg = pContext.GetMessageFactory().CreateMessage(); //Mensaje de salida
            //Se agrega el cuerpo al nuevo mensaje
            outMsg.AddPart(pInMsg.BodyPartName, pContext.GetMessageFactory().CreateMessagePart(), true);
            outMsg.Context = PipelineUtil.CloneMessageContext(pInMsg.Context); //Se conserva el contexto del mensaje recibido;
            outMsg.BodyPart.Data = new MemoryStream();
            xmlMsg.Save(outMsg.BodyPart.Data); //Se escribe el documento xml en el body del mensaje

            return outMsg;
        }
        #endregion
    }
}
