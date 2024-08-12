namespace SetContextPropertyNameSpace
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
    using System.Xml;
    using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("37a3c263-8f1b-430b-a271-da9af8c3396f")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class SetContextProperty : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("SetContextPropertyNameSpace.SetContextProperty", Assembly.GetExecutingAssembly());
        
        private string _promotedproperties;
        private string _operation;

        public string Promotedproperties
        {

            get
            {
                return _promotedproperties;
            }
            set
            {
                _promotedproperties = value;
            }
        }

        public string Operation
        {

            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
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
                return resourceManager.GetString("COMPONENTNAME", System.Globalization.CultureInfo.InvariantCulture);
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
                return resourceManager.GetString("COMPONENTVERSION", System.Globalization.CultureInfo.InvariantCulture);
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
                return resourceManager.GetString("COMPONENTDESCRIPTION", System.Globalization.CultureInfo.InvariantCulture);
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
            classid = new System.Guid("37a3c263-8f1b-430b-a271-da9af8c3396f");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
        }
        
        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            object val = null;
            val = this.ReadPropertyBag(pb, "Promotedproperties");
            if ((val != null))
            {
                this._promotedproperties = ((string)(val));
            }

            val = this.ReadPropertyBag(pb, "Operation");
            if ((val != null))
            {
                this._operation = ((string)(val));
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
           // this.WritePropertyBag(pb, "Promotedproperties", this._promotedproperties);
            this.WritePropertyBag(pb, "Promotedproperties", this.Promotedproperties);
            this.WritePropertyBag(pb, "Operation", this.Operation);

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
            catch (System.ArgumentException )
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
                return ((System.Drawing.Bitmap)(this.resourceManager.GetObject("COMPONENTICON", System.Globalization.CultureInfo.InvariantCulture))).GetHicon();
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
            System.Guid callToken = TraceManager.PipelineComponent.TraceIn("Pipeline Component SetContextProperty");
            string ScopeName = "SetContextProperty";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);
            string valueText;

            if (!string.IsNullOrEmpty(this._promotedproperties))
            {
                XmlDocument Mensaje;
                //Se configura un XmlDocument con el Body del mensaje recibido
                Mensaje = ObtenerMensaje(inmsg);

                string[] properties = this._promotedproperties.Split('│');

                foreach (string property in properties)
                {
                    string[] propertiesXpath = property.Split(';');
                    string propertynamespace = propertiesXpath[0];
                    string propertyxpath = propertiesXpath[1];

                    string[] propertiesnamespace = propertynamespace.Split('#');

                    XmlNode value = Mensaje.SelectSingleNode(propertyxpath);
                    if (value != null)
                    {
                        valueText = value.InnerText;
                        if (valueText.Length > 256)
                        {
                            valueText = valueText.Substring(0, 256);
                        }
                        inmsg.Context.Promote(propertiesnamespace[1], propertiesnamespace[0], valueText);
                        TraceManager.PipelineComponent.TraceInfo("SetContextProperty  - Propiedad Promovida {0} : valor {1}", propertynamespace, valueText);
                    }

                }
            }//

            if (!string.IsNullOrEmpty(this._operation))
            {
                inmsg.Context.Promote("Operation", "http://schemas.microsoft.com/BizTalk/2003/system-properties", this._operation);
                TraceManager.PipelineComponent.TraceInfo(string.Format("SetContextProperty  - Propiedad Promovida Operation {0} ", this._operation));

            }

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            TraceManager.PipelineComponent.TraceOut(callToken);
            return inmsg;
        }
        #endregion

        #region private functions
        private XmlDocument ObtenerMensaje(IBaseMessage pInMsg)
        {
            string ScopeName = "ContextProperty_ObtenerMensaje";
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

                TraceManager.PipelineComponent.TraceInfo("ContextProperty_ObtenerMensaje - Linea {0}: {1}", linea + 1, Mensaje.OuterXml.Substring(inicio, cantidad));
                linea += 1;
            } while ((inicio + cantidad) < Mensaje.OuterXml.Length);

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

            return Mensaje;
        }

        #endregion
    }
}
