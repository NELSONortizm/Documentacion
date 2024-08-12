namespace MobileTec.BizTalk.Cad.PipelineComponents
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
    using System.Globalization;
    using Microsoft.BizTalk.Message.Interop;
    using Microsoft.BizTalk.Component.Interop;
    using Microsoft.BizTalk.Component;
    using Microsoft.BizTalk.Messaging;
    using Microsoft.BizTalk.Streaming;
    using System.Xml;
    using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;
    using MobileTec.BizTalk.Utilities.Configuration.Helper;

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("74472c5f-6781-4f16-b74d-24c05ac6f8f0")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class SetContextPropertyFromParameter : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        
        private System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("MobileTec.BizTalk.Cad.PipelineComponents.SetContextPropertyFromParameter", Assembly.GetExecutingAssembly());
        
        private string _targetXpath;
        
        public string TargetXpath
        {
            get
            {
                return _targetXpath;
            }
            set
            {
                _targetXpath = value;
            }
        }
        
        private string _agencyXpath;
        
        public string AgencyXpath
        {
            get
            {
                return _agencyXpath;
            }
            set
            {
                _agencyXpath = value;
            }
        }
        
        private string _departmentXpath;
        
        public string DepartmentXpath
        {
            get
            {
                return _departmentXpath;
            }
            set
            {
                _departmentXpath = value;
            }
        }
        
        private string _promotedProperties;
        
        public string PromotedProperties
        {
            get
            {
                return _promotedProperties;
            }
            set
            {
                _promotedProperties = value;
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
                return _resourceManager.GetString("COMPONENTNAME", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("COMPONENTVERSION", CultureInfo.InvariantCulture);
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
                return _resourceManager.GetString("COMPONENTDESCRIPTION", CultureInfo.InvariantCulture);
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
            classID = new System.Guid("74472c5f-6781-4f16-b74d-24c05ac6f8f0");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
            this.AgencyXpath = "";
            this.DepartmentXpath = "";
            this.TargetXpath = "";
            this.PromotedProperties = "";
        }
        
        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="errorLog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, int errorLog)
        {
            object val = null;
            val = this.ReadPropertyBag(propertyBag, "TargetXpath");
            if ((val != null))
            {
                this._targetXpath = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "AgencyXpath");
            if ((val != null))
            {
                this._agencyXpath = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "DepartmentXpath");
            if ((val != null))
            {
                this._departmentXpath = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "PromotedProperties");
            if ((val != null))
            {
                this._promotedProperties = ((string)(val));
            }
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="clearDirty">not used</param>
        /// <param name="saveAllProperties">not used</param>
        public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            this.WritePropertyBag(propertyBag, "TargetXpath", this.TargetXpath);
            this.WritePropertyBag(propertyBag, "AgencyXpath", this.AgencyXpath);
            this.WritePropertyBag(propertyBag, "DepartmentXpath", this.DepartmentXpath);
            this.WritePropertyBag(propertyBag, "PromotedProperties", this.PromotedProperties);
        }
        
        #region utility functionality
        /// <summary>
        /// Reads property value from property bag
        /// </summary>
        /// <param name="propertyBag">Property bag</param>
        /// <param name="propertyName">Name of property</param>
        /// <returns>Value of the property</returns>
        private object ReadPropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, string propertyName)
        {
            object val = null;
            try
            {
                propertyBag.Read(propertyName, out val, 0);
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
        /// <param name="propertyBag">Property bag.</param>
        /// <param name="propertyName">Name of property.</param>
        /// <param name="val">Value of property.</param>
        private void WritePropertyBag(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, string propertyName, object val)
        {
            try
            {
                propertyBag.Write(propertyName, ref val);
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
                return ((System.Drawing.Bitmap)(this._resourceManager.GetObject("COMPONENTICON", CultureInfo.InvariantCulture))).GetHicon();
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
        
        #region IComponent members
        /// <summary>
        /// Implements IComponent.Execute method.
        /// </summary>
        /// <param name="pContext">Pipeline context</param>
        /// <param name="pInMsg">Input message</param>
        /// <returns>Original input message</returns>
        /// <remarks>
        /// IComponent.Execute method is used to initiate
        /// the processing of the message in this pipeline component.
        /// </remarks>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage Execute(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
        {
            System.Guid callToken = TraceManager.PipelineComponent.TraceIn("SetContextPropertiesFromParameter");
            try
            {
                string ScopeName = "SetContextPropertiesFromParameter_Execute";
                long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

                if (!string.IsNullOrWhiteSpace(this.PromotedProperties))
                {
                    if (!string.IsNullOrWhiteSpace(this.AgencyXpath) && !string.IsNullOrWhiteSpace(this.DepartmentXpath) && !string.IsNullOrWhiteSpace(this.TargetXpath))
                    {
                        XmlDocument Mensaje;

                        //Se configura un XmlDocument con el Body del mensaje recibido
                        Mensaje = ObtenerMensaje(pInMsg);

                        //Se promueven las propiedades según configuración
                        PromoverPropiedades(pInMsg, Mensaje);
                    }
                    else
                        TraceManager.PipelineComponent.TraceInfo("{0} - No se configuraron correctamente los valores Xpath. AgencyXpath: {1} - DepartmentXpath: {2} - TargetXpath: {3}", ScopeName, this.AgencyXpath, this.DepartmentXpath, this.TargetXpath);

                }
                else
                    TraceManager.PipelineComponent.TraceInfo(string.Format("{0} - No se configuraron propiedades para promover.", ScopeName));

                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                TraceManager.PipelineComponent.TraceOut(callToken);

                return pInMsg;
            }
            catch (System.Exception ex)
            {
                TraceManager.PipelineComponent.TraceError(ex, true, callToken);
                TraceManager.PipelineComponent.TraceOut(callToken);
                throw ex;
            }
        }

        #endregion

        #region Private methods

        private void PromoverPropiedades(IBaseMessage pInMsg, XmlDocument mensaje)
        {
            string ScopeName = "SetContextPropertiesFromParameter_ObtenerMensaje";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            string target = mensaje.SelectSingleNode(this.TargetXpath).InnerText;
            string agency = mensaje.SelectSingleNode(this.AgencyXpath).InnerText;
            string department = mensaje.SelectSingleNode(this.DepartmentXpath).InnerText;

            //Obtener las propiedades a promover
            string[] properties = this.PromotedProperties.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string property in properties)
            {
                string[] propertyConfiguration = property.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if(propertyConfiguration.Length > 3)
                {
                    string propertyTarget = propertyConfiguration[0];
                    string propertyParameter = propertyConfiguration[1];
                    string propertyName = propertyConfiguration[2];
                    string propertyNamespace = propertyConfiguration[3];

                    if (target == "ALL" || propertyTarget == target)
                    {
                        string parameterValue = ConfigReaderStatic.ObtenerParametro(agency, department, target, propertyParameter);

                        if (parameterValue == null)
                            parameterValue = "";

                        if (parameterValue.Length > 256)
                        {
                            pInMsg.Context.Write(propertyName, propertyNamespace, parameterValue);
                            TraceManager.PipelineComponent.TraceInfo("{0} - Propiedad escrita al contexto: {1}#{2} valor: {3}", ScopeName, propertyNamespace, propertyName, parameterValue);
                        }
                        else
                        {
                            pInMsg.Context.Promote(propertyName, propertyNamespace, parameterValue);
                            TraceManager.PipelineComponent.TraceInfo("{0} - Propiedad promovida al contexto: {1}#{2} valor: {3}", ScopeName, propertyNamespace, propertyName, parameterValue);
                        }
                    }
                }
                else
                    TraceManager.PipelineComponent.TraceInfo("{0} - Configuracion incorrecta {1}", ScopeName, property);

            }

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
        }

        private XmlDocument ObtenerMensaje(IBaseMessage pInMsg)
        {
            string ScopeName = "SetContextPropertiesFromParameter_ObtenerMensaje";
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

                TraceManager.PipelineComponent.TraceInfo("{0} - Linea {1}: {2}", ScopeName, linea + 1, Mensaje.OuterXml.Substring(inicio, cantidad));
                linea += 1;
            } while ((inicio + cantidad) < Mensaje.OuterXml.Length);

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

            return Mensaje;
        }
        #endregion Private methods
    }
}
