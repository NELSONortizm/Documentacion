namespace Mobiletec.Biztalk.Utilities.PipelineComponents
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
    using Microsoft.BizTalk.Component.Utilities;

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("14212839-d893-4fc7-8b87-510048026274")]
    [ComponentCategory(CategoryTypes.CATID_DisassemblingParser)]
    public class FlatFileAddRawData : Microsoft.BizTalk.Component.Interop.IDisassemblerComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {

        #region FFDasmComp
        private FFDasmComp ffDasmComp;

        [BtsDescription("DescDocumentSpecName")]
        [BtsPropertyName("PropDocumentSpecName")]
        public SchemaWithNone DocumentSpecName { get; set; }
        [BtsDescription("DescHeaderSpecName")]
        [BtsPropertyName("PropHeaderSpecName")]
        public SchemaWithNone HeaderSpecName { get; set; }
        [BtsDescription("DescPreserveHeader")]
        [BtsPropertyName("PropPreserveHeader")]
        public bool PreserveHeader { get; set; }
        [BtsDescription("DescRecoverableInterchangeProcessing")]
        [BtsPropertyName("PropRecoverableInterchangeProcessing")]
        public bool RecoverableInterchangeProcessing { get; set; }
        [BtsDescription("DescTrailerSpecName")]
        [BtsPropertyName("PropTrailerSpecName")]
        public SchemaWithNone TrailerSpecName { get; set; }
        [BtsDescription("DescValidateDocumentStructure")]
        [BtsPropertyName("PropValidateDocumentStructure")]
        public bool ValidateDocumentStructure { get; set; }
        #endregion

        bool TraceInitiated = false;

        System.Guid GcallToken = System.Guid.NewGuid();
        public FlatFileAddRawData()
        {
            ffDasmComp = new FFDasmComp();
        }

        private System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Mobiletec.Biztalk.Utilities.PipelineComponents.FlatFileAddRawData", Assembly.GetExecutingAssembly());
        private string RawData;
        private string _RawDataNamespace;
        
        public string RawDataNamespace
        {
            get
            {
                return _RawDataNamespace;
            }
            set
            {
                _RawDataNamespace = value;
            }
        }
        
                
        private string _RawDataNodeName;
        
        public string RawDataNodeName
        {
            get
            {
                return _RawDataNodeName;
            }
            set
            {
                _RawDataNodeName = value;
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
            classid = new System.Guid("14212839-d893-4fc7-8b87-510048026274");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
            
            ffDasmComp.InitNew();
            RawDataNodeName = "";
            RawDataNamespace = "";
            DocumentSpecName = new SchemaWithNone("");
            HeaderSpecName = new SchemaWithNone("");
            PreserveHeader = false;
            RecoverableInterchangeProcessing = false;
            TrailerSpecName = new SchemaWithNone("");
            ValidateDocumentStructure = false;
        }

        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="errlog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, int errlog)
        {
            //base.Load(pb, errlog);
            object val = null;
            val = this.ReadPropertyBag(pb, "RawDataNamespace");
            if ((val != null))
            {
                this._RawDataNamespace = ((string)(val));
            }
            else if(this._RawDataNamespace == null)
            {
                this._RawDataNamespace = "";
            }
            val = this.ReadPropertyBag(pb, "RawDataNodeName");            
            if ((val != null))
            {
                this._RawDataNodeName = ((string)(val));
            }

            val = this.ReadPropertyBag(pb, "DocumentSpecName");
            if ((val != null))
            {
                this.DocumentSpecName = new SchemaWithNone ((string)val);
                ffDasmComp.DocumentSpecName = this.DocumentSpecName;
            }

            val = this.ReadPropertyBag(pb, "HeaderSpecName");
            if ((val != null))
            {
                this.HeaderSpecName = new SchemaWithNone((string)val);
                ffDasmComp.HeaderSpecName = this.HeaderSpecName;
            }

            val = this.ReadPropertyBag(pb, "PreserveHeader");
            if ((val != null))
            {
                this.PreserveHeader = ((bool)(val));
                ffDasmComp.PreserveHeader = this.PreserveHeader;
            }

            val = this.ReadPropertyBag(pb, "RecoverableInterchangeProcessing");
            if ((val != null))
            {
                this.RecoverableInterchangeProcessing = ((bool)(val));
                ffDasmComp.RecoverableInterchangeProcessing = this.RecoverableInterchangeProcessing;
            }

            val = this.ReadPropertyBag(pb, "TrailerSpecName");
            if ((val != null))
            {
                this.TrailerSpecName = new SchemaWithNone((string)val);
                ffDasmComp.TrailerSpecName = this.TrailerSpecName;
            }

            val = this.ReadPropertyBag(pb, "ValidateDocumentStructure");
            if ((val != null))
            {
                this.ValidateDocumentStructure = ((bool)(val));
                ffDasmComp.ValidateDocumentStructure = this.ValidateDocumentStructure;
            }
        }
        
        /// <summary>
        /// Saves the current component configuration into the property bag
        /// </summary>
        /// <param name="pb">Configuration property bag</param>
        /// <param name="fClearDirty">not used</param>
        /// <param name="fSaveAllProperties">not used</param>
        public  virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag pb, bool fClearDirty, bool fSaveAllProperties)
        {
           // base.Save(pb,fClearDirty,fSaveAllProperties);
            this.WritePropertyBag(pb, "RawDataNamespace", this.RawDataNamespace);
            this.WritePropertyBag(pb, "RawDataNodeName", this.RawDataNodeName);
            this.WritePropertyBag(pb, "DocumentSpecName", this.DocumentSpecName);
            this.WritePropertyBag(pb, "HeaderSpecName", this.HeaderSpecName);
            this.WritePropertyBag(pb, "PreserveHeader", this.PreserveHeader);
            this.WritePropertyBag(pb, "RecoverableInterchangeProcessing", this.RecoverableInterchangeProcessing);
            this.WritePropertyBag(pb, "TrailerSpecName", this.TrailerSpecName);
            this.WritePropertyBag(pb, "ValidateDocumentStructure", this.ValidateDocumentStructure);
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
        
             
        
        #region IDisassemblerComponent members
        /// <summary>
        /// called by the messaging engine until returned null, after disassemble has been called
        /// </summary>
        /// <param name="pc">the pipeline context</param>
        /// <returns>an IBaseMessage instance representing the message created</returns>
        public Microsoft.BizTalk.Message.Interop.IBaseMessage GetNext(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext)
        {
            // Check if don't need to stop collecting messages            
           if (TraceInitiated == false)
            {
                TraceInitiated = true;
                GcallToken = TraceManager.PipelineComponent.TraceIn("FlatFileAddRawData_GetNext");
            }           

            IBaseMessage disassembledMessage = null;
            disassembledMessage = ffDasmComp.GetNext(pContext);

            if (disassembledMessage != null && !string.IsNullOrWhiteSpace(RawDataNodeName))
            {
                XmlNode xmlNode;
                XmlDocument xmlMensaje = ObtenerMensaje(disassembledMessage);
                xmlNode = xmlMensaje.DocumentElement.SelectSingleNode("./*[local-name()='" + RawDataNodeName + "' and namespace-uri()='" + RawDataNamespace + "']");
                if (xmlNode == null)
                {
                    xmlNode = xmlMensaje.CreateNode(XmlNodeType.Element, RawDataNodeName, RawDataNamespace);
                    xmlMensaje.DocumentElement.AppendChild(xmlNode);
                    TraceManager.PipelineComponent.TraceInfo(string.Format("FlatFileAddRawData_GetNext - Nodo agregado: {0} :  {1}", RawDataNamespace, RawDataNodeName));
                }
                else
                {
                    TraceManager.PipelineComponent.TraceInfo(string.Format("FlatFileAddRawData_GetNext - Nodo Encontrado: {0} :  {1}", xmlNode.NamespaceURI,xmlNode.Name));
                }
               
                xmlNode.InnerText = RawData;                
                // Create a new memory stream to contain the disassembled message.  
                MemoryStream messageStream = new MemoryStream();
                xmlMensaje.Save(messageStream);//convierto el XMl a un stream
                messageStream.Flush();//Adjust this if you want read your data
                messageStream.Position = 0;
                disassembledMessage.BodyPart.Data = messageStream;//asigno el stream XML a la propiedad Data de IbasseMessage
                TraceMessage(xmlMensaje);
                TraceManager.PipelineComponent.TraceInfo(string.Format("FlatFileAddRawData_GetNext - Mensaje entregado: {0}", disassembledMessage.MessageID.ToString()));
                }
            else
            {
                TraceManager.PipelineComponent.TraceOut(GcallToken);
            }

                return disassembledMessage;
        }

        private XmlDocument ObtenerMensaje(IBaseMessage pInMsg)
        {
            string ScopeName = "FlatFileAddRawData_ObtenerMensaje";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            XmlDocument Mensaje = new XmlDocument();
            Mensaje.Load(pInMsg.BodyPart.Data);
            // pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);

            TraceMessage(Mensaje);

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

            return Mensaje;
        }

        private void TraceMessage(XmlDocument Mensaje)
        {
            int linea = 0;
            int cantidad = 5000;
            int inicio;

            do
            {
                inicio = linea * cantidad;

                if (Mensaje.OuterXml.Length < (inicio + cantidad))
                    cantidad = Mensaje.OuterXml.Length - inicio;

                TraceManager.PipelineComponent.TraceInfo("FlatFileAddRawData - Linea {0}: {1}", linea + 1, Mensaje.OuterXml.Substring(inicio, cantidad));
                linea += 1;
            } while ((inicio + cantidad) < Mensaje.OuterXml.Length);
        }

        /// <summary>
        /// called by the messaging engine when a new message arrives
        /// </summary>
        /// <param name="pc">the pipeline context</param>
        /// <param name="inmsg">the actual message</param>
        public void Disassemble(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
        {
            System.Guid callToken = TraceManager.PipelineComponent.TraceIn("FlatFileAddRawData");
            string ScopeName = "FlatFileAddRawData_Disassemble";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);          
           
            StreamReader reader = new StreamReader(pInMsg.BodyPart.GetOriginalDataStream());
            RawData = reader.ReadToEnd();
            pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);
            ffDasmComp.Disassemble(pContext, pInMsg);
            TraceManager.PipelineComponent.TraceInfo(string.Format("FlatFileAddRawData - RawData {0}", RawData));
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            TraceManager.PipelineComponent.TraceOut(callToken);
        }
        #endregion
    }
}
