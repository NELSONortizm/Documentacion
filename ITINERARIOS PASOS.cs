Itinerary.Itinerary = Microsoft.Practices.ESB.Itinerary.ItineraryOMFactory.Create(msg_in);
ItineraryStep.ItineraryStep = Itinerary.Itinerary.GetItineraryStep(msg_in);

ResolverCollection = ItineraryStep.ItineraryStep.ResolverCollection;
ResolverCollection.MoveNext();
ResolverDictionaryString = ResolverCollection.Current;

ResolverDictionary = Microsoft.Practices.ESB.Resolver.ResolverMgr.Resolve(msg_in, ResolverDictionaryString);
Transform_Type = ResolverDictionary.Item("Resolver.TransformType");


Scope_Send_SMS


Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation.TraceManager.WorkflowComponent.TraceInfo("{0} - {1}",sOrchestrationName, System.String.Format("Transform Type: {0}", Transform_Type));


sScopeName="Scope_Debatch SMS";
GetPipelineOutput=Microsoft.XLANGs.Pipeline.XLANGPipelineManager.ExecuteReceivePipeline(typeof(Microsoft.BizTalk.DefaultPipelines.XMLReceive),SMS_Message_Envelope);

Itinerary_copy.Itinerary = Microsoft.Practices.ESB.Itinerary.ItineraryOMFactory.Create(msg_in);
ItineraryStep.ItineraryStep = Itinerary_copy.Itinerary.GetItineraryStep(msg_in);


SMS_Message.MessagePart_SMSMessage = null;
GetPipelineOutput.GetCurrent(SMS_Message);
Itinerary_copy.Itinerary.Advance(SMS_Message, ItineraryStep.ItineraryStep);//avance un paso del itinerario
Itinerary_copy.Itinerary.Write(SMS_Message);
Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation.TraceManager.WorkflowComponent.TraceInfo(" se avanza el itinerario, Orquestacion :{0}", sOrchestrationName);
