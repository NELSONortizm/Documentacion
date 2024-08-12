using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;

namespace Mobiletec.Biztalk.AdashiInbound.Helper
{
    [Serializable]
    public class InstanceGenerator

    {

        public XmlDocument GenerateInstance(string id, bool success, string reason)
        {
            string scopeName = "AdashiInbound.Helper GenerateInstance";
            long scope = TraceManager.CustomComponent.TraceStartScope(scopeName);
           

            XmlDocument Ackxml = new XmlDocument();
            string xml = @"<Acknowledgement><id>{0}</id><success>{1}</success><reason>{2}</reason></Acknowledgement>";

            if (success)
            {
                 xml = @"<Acknowledgement><id>{0}</id><success>{1}</success></Acknowledgement>";
            }
            xml = string.Format(xml, id, success, reason);
            Ackxml.LoadXml(xml);

            TraceManager.CustomComponent.TraceInfo("{0} - Message constructed: {1}", scopeName, xml);
            TraceManager.CustomComponent.TraceEndScope(scopeName, scope);
            return Ackxml;

        }
    }
}