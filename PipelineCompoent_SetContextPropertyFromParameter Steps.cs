SetContextPropertyFromParameter Steps

CategoryTypes.CATID_Any

1. Se definen las variables que se configuraran en el pipeline, ejM

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

2. InitNew()

public void InitNew()
        {
            this.AgencyXpath = "";
            this.DepartmentXpath = "";
            this.TargetXpath = "";
            this.PromotedProperties = "";
        }

3. Load()

public virtual void Load(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, int errorLog)
        {
            object val = null;
            val = this.ReadPropertyBag(propertyBag, "TargetXpath");
            if ((val != null))
            {
                this._targetXpath = ((string)(val));
            }
			
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

4. Save()

 public virtual void Save(Microsoft.BizTalk.Component.Interop.IPropertyBag propertyBag, bool clearDirty, bool saveAllProperties)
        {
            this.WritePropertyBag(propertyBag, "TargetXpath", this.TargetXpath);
            this.WritePropertyBag(propertyBag, "AgencyXpath", this.AgencyXpath);
            this.WritePropertyBag(propertyBag, "DepartmentXpath", this.DepartmentXpath);
            this.WritePropertyBag(propertyBag, "PromotedProperties", this.PromotedProperties);
        }

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
		
5 Execute()
Si PromotedProperties entonce
Si AgencyXpath, DepartmentXpath, TargetXpath propiedades del pipeline tienen valor entonces
Si estan configuradas entonces se obtiene el mensaje : Mensaje = ObtenerMensaje(pInMsg);
luego se promueven las propiedades :  PromoverPropiedades(pInMsg, Mensaje);


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
5.1 ObtenerMensaje

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

5.2 PromoverPropiedades: se utiliza para promover propiedades y extraer su valor dinamicamente para luego ser enviados en un pipeline de salida
    
	Se extrae target, agency y department del documento XML
	Extrae los valores de PromotedProperties y hace split:
	FIRERMS;HttpHeaders;HttpHeaders;http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties|
	FIRERMS;HttpMethodAndUrl;HttpMethodAndUrl;http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties|
	FIRERMS;SuppressMessageBodyforHttpVerbs;SuppressMessageBodyForHttpVerbs;http://schemas.microsoft.com/BizTalk/2006/01/Adapters/WCF-properties
	
	string propertyTarget = propertyConfiguration[0];
    string propertyParameter = propertyConfiguration[1];
    string propertyName = propertyConfiguration[2];
    string propertyNamespace = propertyConfiguration[3];
	
	5.2.1 ObtenerParametro
		  parameterValue = ConfigReaderStatic.ObtenerParametro(agency, department, target, propertyParameter);
		  Escribe las propiedades de parameterValue al contexto:
		  pInMsg.Context.Write(propertyName, propertyNamespace, parameterValue);
		   pInMsg.Context.Promote(propertyName, propertyNamespace, parameterValue);
		  
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