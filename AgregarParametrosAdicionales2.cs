 private void AgregarParametrosAdicionales(XmlDocument mensaje)
        {
            XmlNode additionalParametersNode;
            XmlNode additionalParametersParent;
            string[] additionalParameters;
            string ScopeName = "EventMessageDuplicator_AgregarParametrosAdicionales";

            long ScopeStart = TraceManager.PipelineComponent.TraceStartScope(ScopeName);

            if (!string.IsNullOrWhiteSpace(XPathAdditionalParameters) && !string.IsNullOrWhiteSpace(XPathNodeToInsertAdditionalParameters))
            {
                additionalParametersNode = mensaje.SelectSingleNode(XPathAdditionalParameters);
                if (additionalParametersNode != null)
                {
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Valor nodo parametros adicionales: {0}", additionalParametersNode.InnerText);

                    additionalParameters = additionalParametersNode.InnerText.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (additionalParameters.Length > 0)
                    {
                        additionalParametersParent = mensaje.SelectSingleNode(XPathNodeToInsertAdditionalParameters);
                        if (additionalParametersParent != null)
                        {
                            foreach (string KeyValue in additionalParameters)
                            {
                                string[] KeyValueArray = KeyValue.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                if (KeyValueArray.Length > 0)
                                {
                                    XmlNode parameterNode = additionalParametersParent.AppendChild(mensaje.CreateNode(XmlNodeType.Element, KeyValueArray[0], additionalParametersParent.NamespaceURI));
                                    if (KeyValueArray.Length > 1)
                                        parameterNode.InnerText = KeyValueArray[1];

                                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - Agregado nodo: {0} - Valor: {1}", parameterNode.Name, parameterNode.InnerText);
                                }
                            }
                        }
                        else
                            TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - No existe el nodo padre para parametros adicionales.");

                    }
                    else
                        TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - El nodo parametros adicionales no tiene datos.");

                }
                else
                    TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - No se encontró nodo de parametros adicionales.");

            }
            else
                TraceManager.PipelineComponent.TraceInfo("EventMessageDuplicator - No se configuraron rutas Xpath para parametros adicionales.");

            TraceManager.PipelineComponent.TraceEndScope(ScopeName, ScopeStart);

        }