 //Promover al contexto
 
 pInMsg.Context.Write(propertyName, propertyNamespace, parameterValue);//string strname, string namespace, object obj
 
 //ejemplo2
 string rlocationname = (string)pInMsg.Context.Read("ReceiveLocationName", "http://schemas.microsoft.com/BizTalk/2003/system-properties");
pInMsg.Context.Promote("""", "https://BizTalk.Pipeline.Components.RcvLocationPromotion.PropertySchema", rlocationname);


string rlocationname = (string)pInMsg.Context.Read("ReceiveLocationName", "http://schemas.microsoft.com/BizTalk/2003/system-properties");
pInMsg.Context.Promote("ReceiveLocationName", "<target-namespace-schema-created-above>", rlocationname)


BizTalk Isolated Host Users
BizTalk Isolated Host Users