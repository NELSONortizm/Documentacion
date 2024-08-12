convertir MEMORY STREAM A XML, IBASEMESSAGE TO XML

public void Disassemble(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext, Microsoft.BizTalk.Message.Interop.IBaseMessage pInMsg)
        {
            System.Guid callToken = TraceManager.PipelineComponent.TraceIn("FlatFileAddRawData");
            string ScopeName = "PocFlatFileDisassembleExtender_Disassemble";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            //llevo a un streamreader el IBaseMessage
            StreamReader reader = new StreamReader(pInMsg.BodyPart.GetOriginalDataStream());
            //llevo al reader a la variable string Rawdata
            RawData = reader.ReadToEnd();
            pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);
            ffDasmComp.Disassemble(pContext, pInMsg);
            //instrumentacion
            TraceManager.PipelineComponent.TraceInfo(string.Format("PocFlatFileDisassembleExtender - RawData {0}", RawData));
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            TraceManager.PipelineComponent.TraceOut(callToken);

        }

private XmlDocument ObtenerMensaje(IBaseMessage pInMsg)
        {
            //cargo el mensaje IBaseMessage a un XML
            XmlDocument Mensaje = new XmlDocument();
            Mensaje.Load(pInMsg.BodyPart.Data);
            // pInMsg.BodyPart.Data.Seek(0, SeekOrigin.Begin);
            return Mensaje;
        }
		
		
		
 public Microsoft.BizTalk.Message.Interop.IBaseMessage GetNext(Microsoft.BizTalk.Component.Interop.IPipelineContext pContext)
        {
             
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
                }
               
                xmlNode.InnerText = RawData;                
                // Create a new memory stream to contain the disassembled message.  
                MemoryStream messageStream = new MemoryStream();
                xmlMensaje.Save(messageStream);//convierto el XMl a un stream
                messageStream.Flush();//Adjust this if you want read your data
                messageStream.Position = 0;
                disassembledMessage.BodyPart.Data = messageStream;//asigno el stream XML a la propiedad Data de IbasseMessage
                TraceManager.PipelineComponent.TraceInfo(string.Format("FlatFileAddRawData_GetNext - Mensaje entregado: {0}", disassembledMessage.MessageID.ToString()));
                }
            else
            {
                TraceManager.PipelineComponent.TraceOut(GcallToken);
            }

                return disassembledMessage;
        }
		
		