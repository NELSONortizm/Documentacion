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
    using System.Collections.Generic;
    using System.Linq;

    [ComponentCategory(CategoryTypes.CATID_PipelineComponent)]
    [System.Runtime.InteropServices.Guid("aee9d26d-3098-4acc-8234-4576540883cb")]
    [ComponentCategory(CategoryTypes.CATID_Any)]
    public class AddNodesToMessage : Microsoft.BizTalk.Component.Interop.IComponent, IBaseComponent, IPersistPropertyBag, IComponentUI
    {
        private System.Guid gCallToken;

        private System.Resources.ResourceManager _resourceManager = new System.Resources.ResourceManager("MobileTec.BizTalk.Cad.PipelineComponents.AddNodesToMessage", Assembly.GetExecutingAssembly());
        
        private string _rootNodeNamespace;
        
        public string RootNodeNamespace
        {
            get
            {
                return _rootNodeNamespace;
            }
            set
            {
                _rootNodeNamespace = value;
            }
        }
        
        private string _rootNodeName;
        
        public string RootNodeName
        {
            get
            {
                return _rootNodeName;
            }
            set
            {
                _rootNodeName = value;
            }
        }
        
        private string _nodeSeparator;
        
        public string NodeSeparator
        {
            get
            {
                return _nodeSeparator;
            }
            set
            {
                _nodeSeparator = value;
            }
        }
        
        private string _nodes;
        
        public string Nodes
        {
            get
            {
                return _nodes;
            }
            set
            {
                _nodes = value;
            }
        }
        
        private string _nodesSeparator;
        
        public string NodesSeparator
        {
            get
            {
                return _nodesSeparator;
            }
            set
            {
                _nodesSeparator = value;
            }
        }
        
        private bool _promoteMessageType;
        
        public bool PromoteMessageType
        {
            get
            {
                return _promoteMessageType;
            }
            set
            {
                _promoteMessageType = value;
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
            classID = new System.Guid("aee9d26d-3098-4acc-8234-4576540883cb");
        }
        
        /// <summary>
        /// not implemented
        /// </summary>
        public void InitNew()
        {
            RootNodeNamespace = "";
            RootNodeName = "";
            Nodes = "";
            NodeSeparator = "";
            NodesSeparator = "";
            PromoteMessageType = false;
        }
        
        /// <summary>
        /// Loads configuration properties for the component
        /// </summary>
        /// <param name="propertyBag">Configuration property bag</param>
        /// <param name="errorLog">Error status</param>
        public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, int errorLog)
        {
            object val = null;
            val = this.ReadPropertyBag(propertyBag, "RootNodeNamespace");
            if ((val != null))
            {
                this._rootNodeNamespace = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "RootNodeName");
            if ((val != null))
            {
                this._rootNodeName = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "NodeSeparator");
            if ((val != null))
            {
                this._nodeSeparator = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "Nodes");
            if ((val != null))
            {
                this._nodes = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "NodesSeparator");
            if ((val != null))
            {
                this._nodesSeparator = ((string)(val));
            }
            val = this.ReadPropertyBag(propertyBag, "PromoteMessageType");
            if ((val != null))
            {
                this._promoteMessageType = ((bool)(val));
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
            this.WritePropertyBag(propertyBag, "RootNodeNamespace", this.RootNodeNamespace);
            this.WritePropertyBag(propertyBag, "RootNodeName", this.RootNodeName);
            this.WritePropertyBag(propertyBag, "NodeSeparator", this.NodeSeparator);
            this.WritePropertyBag(propertyBag, "Nodes", this.Nodes);
            this.WritePropertyBag(propertyBag, "NodesSeparator", this.NodesSeparator);
            this.WritePropertyBag(propertyBag, "PromoteMessageType", this.PromoteMessageType);
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
            gCallToken = TraceManager.PipelineComponent.TraceIn("AddNodesToMessage");

            try
            {
                XmlDocument xmlDoc = GetXMLMessage(pInMsg);
                if (xmlDoc.DocumentElement != null)
                {
                    SetRootNode(xmlDoc);

                    AddNodesToDocument(xmlDoc);

                    if (PromoteMessageType)
                    {
                        PromoteMessageTypeFunc(pInMsg, xmlDoc);
                    }

                    pInMsg.BodyPart.Data = new MemoryStream();
                    xmlDoc.Save(pInMsg.BodyPart.Data);
                    pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);

                }

                TraceManager.PipelineComponent.TraceOut(gCallToken);

                return pInMsg;
            }
            catch (Exception ex)
            {
                TraceManager.PipelineComponent.TraceError(ex, true, gCallToken);
                TraceManager.PipelineComponent.TraceOut(gCallToken);
                throw ex;
            }

        }

        #endregion

        #region Private Methods

        private void SetRootNode(XmlDocument xmlDoc)
        {
            string ScopeName = "AddNodesToMessage_SetRootNode";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            if (!string.IsNullOrWhiteSpace(RootNodeName))
            {

                XmlElement newRootNode = xmlDoc.CreateElement(RootNodeName, RootNodeNamespace);
                XmlNode NodeFirstChild = xmlDoc.FirstChild.Clone();

                if (NodeFirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    xmlDoc.RemoveChild(xmlDoc.FirstChild);
                }
                                              
                newRootNode.InnerXml = xmlDoc.OuterXml;
                xmlDoc.LoadXml(newRootNode.OuterXml);

                if (NodeFirstChild.NodeType == XmlNodeType.XmlDeclaration)
                {
                    xmlDoc.InsertBefore(NodeFirstChild, xmlDoc.FirstChild);
                }
               
                TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_SetRootNode - Root node set to: {0}", newRootNode.Name);
            }

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
        }

        private void AddNodesToDocument(XmlDocument xmlDoc)
        {
            string ScopeName = "AddNodesToMessage_AddNodesToDocument";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            if (string.IsNullOrWhiteSpace(NodesSeparator) || string.IsNullOrWhiteSpace(Nodes))
            {
                TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_AddNodesToDocument - Nodes not defined");
                TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
                return;
            }

            TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_AddNodesToDocument - NodesSeparator: {0} - Nodes definition: {1}", NodesSeparator, Nodes);

            List<string> nodesDefinition = Nodes.Split(NodesSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            
            nodesDefinition.ForEach(nodeDefinition => AddNodeToDocument(nodeDefinition, xmlDoc));

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
        }

        private void AddNodeToDocument(string nodeDefinition, XmlDocument xmlDoc)
        {
            string ScopeName = "AddNodesToMessage_AddNodeToDocument";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_AddNodeToDocument - NodeSeparator: {0} - Node definition: {1}", NodeSeparator, nodeDefinition);

            List<string> definitionValues = nodeDefinition.Split(NodeSeparator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            Dictionary<string, string> definition = definitionValues
                                                        .Select(value => value.Split("=".ToCharArray()))
                                                        .Where(array => array.Length > 1)
                                                        .ToDictionary(array => array[0].ToLower(), array => string.Join("=", array.Skip(1)));
                                                        

            string nodeName = definition.ContainsKey("name") ? definition["name"] : "";

            if (!string.IsNullOrWhiteSpace(nodeName))
            {
                string nodeNamespace = definition.ContainsKey("namespace") ? definition["namespace"] : "";

                XmlElement newElement = xmlDoc.CreateElement(nodeName, nodeNamespace);

                string contentFrom = definition.ContainsKey("contentfrom") ? definition["contentfrom"].ToLower() : "Value";

                string nodeValue = nodeValue = definition.ContainsKey("value") ? definition["value"] : "";

                if (contentFrom == "xpath" && !string.IsNullOrWhiteSpace(nodeValue))
                {
                    XmlNode node = xmlDoc.SelectSingleNode(nodeValue);
                    nodeValue = node == null ? "" : node.InnerXml;
                    newElement.InnerXml = nodeValue;
                }
                else if (contentFrom == "node" && !string.IsNullOrWhiteSpace(nodeValue))
                {
                    XmlNode node = xmlDoc.SelectSingleNode(nodeValue);
                    if (node != null)
                    {
                        XmlNode newNode = node.Clone();
                        newElement.AppendChild(newNode);
                        if (node.ParentNode != null)
                        {
                            node.ParentNode.RemoveChild(node);
                        }
                        else
                        {
                            xmlDoc.RemoveChild(node);
                        }

                    }
                }
                else
                    newElement.InnerXml = nodeValue;

                string parentNode = definition.ContainsKey("parentnodexpath") ? definition["parentnodexpath"] : "";

                if (!string.IsNullOrWhiteSpace(parentNode))
                {
                    XmlNode parent = xmlDoc.SelectSingleNode(parentNode);

                    if (parent != null)
                        parent.AppendChild(newElement);
                }
                else
                    xmlDoc.DocumentElement.AppendChild(newElement);

                TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_AddNodeToDocument - Node added: {0}", newElement.Name);

            }

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
        }

        #endregion

        #region Private functions


        private XmlDocument GetXMLMessage(IBaseMessage pInMsg)
        {
            string ScopeName = "AddNodesToMessage_GetMessage";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            XmlDocument Message = new XmlDocument();

            try
            {
                try
                {
                    Message.Load(pInMsg.BodyPart.Data);
                    pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);
                }
                catch (Exception ex)
                {
                    TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_GetMessage - Receive message is not XML. Loading as CDATA.");
                    TraceManager.PipelineComponent.TraceError(ex, true, gCallToken);
                    string data = string.Format("<PipelineError><![CDATA[{0}]]></PipelineError>", ex.Message);
                    Message.LoadXml(data);
                    pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);
                }

                int line = 0;
                int charsbyline = 5000;
                int start;

                do
                {
                    start = line * charsbyline;

                    if (Message.OuterXml.Length < (start + charsbyline))
                        charsbyline = Message.OuterXml.Length - start;

                    TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_GetMessage - Line {0}: {1}", line + 1, Message.OuterXml.Substring(start, charsbyline));
                    line += 1;
                } while ((start + charsbyline) < Message.OuterXml.Length);
            }
            catch (Exception ex)
            {
                TraceManager.PipelineComponent.TraceError(ex, true, gCallToken);
                if (Message.DocumentElement == null)
                {
                    Message.LoadXml(string.Format("<PipelineError><![CDATA[{0}]]></PipelineError>", ex.Message));
                }
                else
                {
                    XmlNode node = Message.DocumentElement.AppendChild(Message.CreateNode(XmlNodeType.CDATA.ToString(), "PipelineError", ""));
                    node.Value = ex.Message;
                }
                pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);
            }
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

            return Message;
        }

        private string PromoteMessageTypeFunc(IBaseMessage pInMsg, XmlDocument Message)
        {
            string ScopeName = "AddNodesToMessage_PromoteMessageType";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            string MessageType = Message.DocumentElement.NamespaceURI + "#" + Message.DocumentElement.LocalName;
            pInMsg.Context.Promote("MessageType", "http://schemas.microsoft.com/BizTalk/2003/system-properties", MessageType);
            TraceManager.PipelineComponent.TraceInfo("AddNodesToMessage_PromoteMessageType - MessageType promoted: {0}", MessageType);
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            return MessageType;
        }
        #endregion
    }
}
