using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Mobiletec.Biztalk.AdashiInbound.Schemas;
using System.IO;
using System.Xml;
using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;

namespace Mobiletec.Biztalk.AdashiInbound.Helper
{
    public class SchemaValidator
    {
        
        private static object _lock = new object();
        private static string _message;
        private static AdashiInboundType _schema = new AdashiInboundType();
        public static void Validate(XmlDocument businessDocument)

        {
            string scopeName = "AdashiInbound.Helper SchemaValidator";
            long scope = TraceManager.CustomComponent.TraceStartScope(scopeName);

            lock (_lock)

            {

                try

                {
                    MemoryStream ms = new MemoryStream();
                    StreamWriter sw = new StreamWriter(ms, Encoding.Unicode);
                    sw.Write(_schema.XmlContent);
                    sw.Flush();
                    ms.Position = 0;
                    XmlSchema xmlschema = XmlSchema.Read(ms, HandleValidationError);
                    businessDocument.Schemas = new XmlSchemaSet();
                    businessDocument.Schemas.Add(xmlschema);
                    businessDocument.Validate(new System.Xml.Schema.ValidationEventHandler(HandleValidationError));

                }

                catch

                {

                    throw;

                }

                if (!string.IsNullOrEmpty(_message))

                {

                    string tmpMessage = _message;

                    _message = null;

                    TraceManager.CustomComponent.TraceInfo("{0} - Message No valid : {1}", scopeName, tmpMessage);

                    throw new Exception(tmpMessage);

                }

            }

            TraceManager.CustomComponent.TraceInfo("{0} - Message validated : {1}", scopeName, businessDocument.OuterXml);
            TraceManager.CustomComponent.TraceEndScope(scopeName, scope);
        }        

        private static void HandleValidationError(object sender, ValidationEventArgs ve)

        {
            _message = ve.Exception.Message;

            
        }


    }//clas
}
