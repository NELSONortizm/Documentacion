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
		
		
		private string PromoverDestino(IBaseMessage pInMsg, XmlDocument Mensaje)
        {
            string ScopeName = "EventMessageDuplicator_PromoverMessageType";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);
			
			string targetvalue = Mensaje.SelectSingleNode(this.TargetXpath).InnerText;
            string targetcontext = (string)pInMsg.Context.Read("Target", "http://schemas.microsoft.com/BizTalk/2003/system-properties");

            if (string.IsNullOrWhiteSpace(targetcontext))
            {
				TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - SI EL TARGET NO ESTA EN EL CONTEXTO: {0}", targetcontext);
                targetcontext = Mensaje.DocumentElement.NamespaceURI + "#" + Mensaje.DocumentElement.LocalName;
                pInMsg.Context.Promote("Target", "http://schemas.microsoft.com/BizTalk/2003/system-properties", targetvalue);
				//pInMsg.Context.Promote("Target", "http://schemas.microsoft.com/BizTalk/2003/system-properties", targetcontext);
				
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - MessageType promovido: {0}", targetvalue);
            }
			else
			{
				 TraceManager.PipelineComponent.TraceInfo("PromoverDestino - el target ya está en el contexto: {0}", targetcontext);
			}
            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
            return MessageType;
        }