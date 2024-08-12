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