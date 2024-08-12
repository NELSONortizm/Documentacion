private void PromoverPropiedades(IBaseMessage pInMsg, XmlDocument mensaje)
        {
            string ScopeName = "SetContextPropertiesFromParameter_ObtenerMensaje";
            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            string target = mensaje.SelectSingleNode(this.TargetXpath).InnerText;
            string agency = mensaje.SelectSingleNode(this.AgencyXpath).InnerText;
            string department = mensaje.SelectSingleNode(this.DepartmentXpath).InnerText;

            //Obtener las propiedades a promover
			//FIRERMS;HttpHeaders;HttpHeaders;http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties
			//|
			//FIRERMS;HttpMethodAndUrl;HttpMethodAndUrl;http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties	
            string[] properties = this.PromotedProperties.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			
            foreach (string property in properties)
            {
                string[] propertyConfiguration = property.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
				//FIRERMS			
				//HttpMethodAndUrl
				//HttpMethodAndUrl			
				//http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties 
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
                            pInMsg.Context.Write(propertyName, propertyNamespace, parameterValue);//Distinguished field, only write the property in the context without promoting it
                            TraceManager.PipelineComponent.TraceInfo("{0} - Propiedad escrita al contexto: {1}#{2} valor: {3}", ScopeName, propertyNamespace, propertyName, parameterValue);
                        }
                        else
                        {
                            pInMsg.Context.Promote(propertyName, propertyNamespace, parameterValue);//promnoted fields, write the property value into the message context but also flags the property as promoted
                            TraceManager.PipelineComponent.TraceInfo("{0} - Propiedad promovida al contexto: {1}#{2} valor: {3}", ScopeName, propertyNamespace, propertyName, parameterValue);
                        }
                    }
                }
                else
                    TraceManager.PipelineComponent.TraceInfo("{0} - Configuracion incorrecta {1}", ScopeName, property);

            }

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);
        }