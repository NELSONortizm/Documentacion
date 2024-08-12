//-------------------------------------------------------------------------------
// File: ContextAdderProperties.cs
// 
// Summary: Clase encargada de construir un pipeline component
//
// ===============================================================================
// Desarrollado Por      : Juan Esteban Angulo Uribw
// Producto              : MobileTec.Routing.PipelineComponents
// Version Framework     : 4.5
// Empresa               : MobileTec Inc
// ===============================================================================
// Copyright (C) 2016 MobileTec Inc
// ===============================================================================


namespace MobileTec.Routing.PipelineComponents
{
    using System;
    using System.IO;
    using System.Text;
    using System.Drawing;
    using System.Resources;
    using System.Reflection;
    using System.Diagnostics;
    using System.Collections;
    using System.ComponentModel;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Messaging;
    using Microsoft.BizTalk.Streaming;
    using System.Linq;
    using System.Collections.Generic;
    using System.Xml;
    using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;
    using System.Net;


    /// <summary>
    /// 
    /// </summary>
    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("58A676EB-EF6A-47AE-842A-20E7F9829A81")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class ControlSpecialCharacters : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {

        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("MobileTec.Routing.PipelineComponents.ContextAdderProperties", Assembly.GetExecutingAssembly());

        private bool _replace;

        public bool Replace
        {
            get
            {
                return _replace;
            }
            set
            {
                _replace = value;
            }
        }

        private string _newchar;

        public string Newchar
        {
            get
            {
                return _newchar;
            }
            set
            {
                _newchar = value;
            }
        }

        private string _encodefieldsxpath;
        public string Encodefieldsxpath
        {
            get
            {
                return _encodefieldsxpath;
            }
            set
            {
                _encodefieldsxpath = value;
            }
        }

        #region IBaseComponent members
        /// <summary>
        /// Name of the component
        /// </summary>
        [Browsable(false)]
        public string Name
        {
            get
            {
                return "ControlSpecialCharacters";
            }
        }

        /// <summary>
        /// Version of the component
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
        /// Description of the component
        /// </summary>
        [Browsable(false)]
        public string Description
        {
            get
            {
                return "This component replace the not valid characters coming inside the xml";
            }
        }
        #endregion

        #region IPersistPropertyBag members
        /// <summary>
        /// Gets class ID of component for usage from unmanaged code.
        /// </summary>
        /// <param name="classid">
        /// Class ID of the component
        /// </param>
        public void GetClassID(out System.Guid classid)
        {
            classid = new System.Guid("58A676EB-EF6A-47AE-842A-20E7F9829A81");
        }

        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
            this._replace = false;
            this._newchar = "";
            this._encodefieldsxpath = "";
        }

        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            object val = null;
            val = this.ReadPropertyBag(pb, "Replace");
            if ((val != null))
            {
                this._replace = ((bool)(val));
            }
            val = this.ReadPropertyBag(pb, "Newchar");
            if ((val != null))
            {
                this._newchar = ((string)(val));
            }
            val = this.ReadPropertyBag(pb, "Encodefieldsxpath");
            if ((val != null))
            {
                this._encodefieldsxpath = ((string)(val));
            }
        }

        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
            this.WritePropertyBag(pb, "Replace", this.Replace);
            this.WritePropertyBag(pb, "Newchar", this.Newchar);
            this.WritePropertyBag(pb, "Encodefieldsxpath", this.Encodefieldsxpath);
        }

        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="pb">Property bag</param>
        /// <param name="propName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName)
        {
            object val = null;
            try
            {
                pb.Read(propName, out val, 0);
            }
            catch (System.ArgumentException)
            {
                return val;
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
            }
            return val;
        }

        /// <summary>
        /// Writes property values into a property bag.
        /// </summary>
        /// <param name="pb">Property bag.</param>
        /// <param name="propName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, string propName, object val)
        {
            try
            {
                pb.Write(propName, ref val);
            }
            catch (System.Exception e)
            {
                throw new System.ApplicationException(e.Message);
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
                return Properties.Resources.ControlSpecialCharacters.Handle;
            }
        }

        /// <summary>
        /// The Validate method is called by the BizTalk Editor during the build 
        /// of a BizTalk project.
        /// </summary>
        /// <param name="obj">An Object containing the configuration properties.</param>
        /// <returns>The IEnumerator enables the caller to enumerate through a collection of strings containing error messages. These error messages appear as compiler error messages. To report successful property validation, the method should return an empty enumerator.</returns>
        public System.Collections.IEnumerator Validate(object obj)
        {
            // example implementation:
            // ArrayList errorList = new ArrayList();
            // errorList.Add("This is a compiler error");
            // return errorList.GetEnumerator();
            return null;
        }
        #endregion

        #region IComponent members
        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pc">Pipeline context</param>
        /// <param name="inmsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(Microsoft.BizTalk.Component.Interop.IPipelineContext pc, Microsoft.BizTalk.Message.Interop.IBaseMessage inmsg)
        {
            System.Guid callToken = TraceManager.PipelineComponent.TraceIn("ControlSpecialCharacters");
            try
            {
                string ScopeName = "ControlSpecialCharacters Execute";
                long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

                string Mensaje = ObtenerMensaje(inmsg);
                string Respuesta = ValidateSpecialCharaters(Mensaje);
                Respuesta = ValidateEncondeFields(Respuesta);
                byte[] newmessage = Encoding.UTF8.GetBytes(Respuesta);
                inmsg.BodyPart.Data = new MemoryStream(newmessage);
                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                TraceManager.PipelineComponent.TraceOut(callToken);

                return inmsg;
            }//try
            catch (System.Exception ex)
            {
                TraceManager.PipelineComponent.TraceError(ex, true, callToken);
                TraceManager.PipelineComponent.TraceOut(callToken);
                throw ex;
            }//catch

        }

        private string ValidateEncondeFields(string Mensaje)
        {
            string ScopeName = "ControlSpecialCharacters ValidateEncondeFields";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            string Respuesta="";
            if (string.IsNullOrWhiteSpace(this._encodefieldsxpath))
            {
                TraceManager.PipelineComponent.TraceInfo("No encodedfielsd configured");
                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                return Mensaje;
            }
            string[] arrayxpath = this._encodefieldsxpath.Split('|');
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Mensaje);
            foreach (string xpath in arrayxpath)
            {
                TraceManager.PipelineComponent.TraceInfo(string.Format("Encondedfieldxpath: {0}",xpath));
                XmlNodeList nodes = xml.SelectNodes(xpath);
                if (nodes != null)
                {
                    TraceManager.PipelineComponent.TraceInfo(string.Format("Encondednodescount: {0}", nodes.Count));
                    foreach (XmlNode node in nodes)
                    {
                        string nodetext = WebUtility.HtmlDecode(node.InnerXml);                        
                        nodetext = ValidateSpecialCharaters(nodetext);
                        nodetext = WebUtility.HtmlEncode(nodetext);
                        node.InnerXml = nodetext;
                        TraceManager.PipelineComponent.TraceInfo(string.Format("Nodetext: {0}", nodetext));
                    }
                }
            }
            Respuesta = xml.OuterXml;
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            return Respuesta;
        }

        private string ValidateSpecialCharaters(string Mensaje)
        {
            string ScopeName = "ControlSpecialCharacters ValidateSpecialCharaters";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);
            string Respuesta = "";

            foreach (char ch in Mensaje)
            {

                if (System.Xml.XmlConvert.IsXmlChar(ch))
                {
                    Respuesta += ch;
                }
                else
                {

                    TraceManager.PipelineComponent.TraceInfo(string.Format("Special Character Found:  {0}", (int)ch));
                    if (this._replace && !string.IsNullOrWhiteSpace(this._newchar) && System.Xml.XmlConvert.IsXmlChar(this._newchar.ToCharArray()[0]))
                    {
                        Respuesta += this._newchar;
                        TraceManager.PipelineComponent.TraceInfo(string.Format("Special Character Replaced:  {0}", this._newchar));
                    }
                }

            }
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            return Respuesta;
        }
        #endregion
        private string GetDecodeValueNode(string xmltext)
        {
            string xml = WebUtility.HtmlDecode(xmltext);             
            return xml;
        }

        private String ObtenerMensaje(IBaseMessage pInMsg)
        {
            string ScopeName = "ControlSpecialCharacters ObtenerMensaje";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);
            StreamReader st = new StreamReader(pInMsg.BodyPart.Data);
            string Mensaje = st.ReadToEnd();          
            pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);

            int linea = 0;
            int cantidad = 5000;
            int inicio;

            do
            {
                inicio = linea * cantidad;

                if (Mensaje.Length < (inicio + cantidad))
                    cantidad = Mensaje.Length - inicio;

                TraceManager.PipelineComponent.TraceInfo("ControlSpecialCharacters ObtenerMensaje- Linea {0}: {1}", linea + 1, Mensaje.Substring(inicio, cantidad));
                linea += 1;
            } while ((inicio + cantidad) < Mensaje.Length);

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

            return Mensaje;
        }
    }
}
