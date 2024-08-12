using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Win32;
using System.Web;
using Microsoft.BizTalk.CAT.BestPractices.Framework.Instrumentation;

namespace Mobiletec.Routing.Helpers
{
    public class UploadReportsHelper
    {
        public bool AutoApproveReport(string subject, string jurisdiction)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(subject) == false)
            {
                string[] reportSubjects = subject.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (reportSubjects.Length > 1)
                {
                    string autoApproveReportCategoriesList = ConfigurationManager.AppSettings.Get(jurisdiction + "_AutoApproveReportCategoriesList");
                    if (autoApproveReportCategoriesList != null)
                    {
                        List<string> categoriesList = autoApproveReportCategoriesList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                        string reportCategory = reportSubjects[0].Trim();
                        result = categoriesList.Contains(reportCategory);
                    }
                }
            }

            return result;
        }

        public static bool AutoApproveReportStatic(string subject, string jurisdiction)
        {
            bool result = false;
            string scopeName = "AutoApproveReportStatic";
            long scope = TraceManager.CustomComponent.TraceStartScope(scopeName);
            TraceManager.CustomComponent.TraceInfo("{0} - Subject: {1} - Jurisdiction: {2}", scopeName, subject, jurisdiction);

            if (string.IsNullOrWhiteSpace(subject) == false)
            {
                string[] reportSubjects = subject.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (reportSubjects.Length > 1)
                {
                    ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
                    fileMap.ExeConfigFilename = GetConfigFileName();
                    Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                    KeyValueConfigurationElement keyce = null;

                    string autoApproveReportCategoriesList = null;
                    if (string.IsNullOrWhiteSpace(jurisdiction) == false)
                        keyce = configuration.AppSettings.Settings[jurisdiction + "_AutoApproveReportCategoriesList"];
                    if (keyce != null)
                    {
                        autoApproveReportCategoriesList = keyce.Value;
                    }

                    if (autoApproveReportCategoriesList != null)
                    {
                        List<string> categoriesList = autoApproveReportCategoriesList.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                        string reportCategory = reportSubjects[0].Trim();
                        result = categoriesList.Contains(reportCategory);
                    }
                }
            }

            TraceManager.CustomComponent.TraceInfo("{0} - result: {1}", scopeName, result);
            TraceManager.CustomComponent.TraceEndScope(scopeName, scope);
            return result;
        }


       
        public static string GetNodeValueStatic (string xmltext, string xPathNode)
        {

            string result = "";
            string scopeName = "GetNodeValueStatic";
            long scope = TraceManager.CustomComponent.TraceStartScope(scopeName);
            TraceManager.CustomComponent.TraceInfo("{0} - xmltext: {1} - xPathNode: {2}", scopeName, xmltext, xPathNode);

            if (string.IsNullOrWhiteSpace(xmltext) == false && string.IsNullOrWhiteSpace(xPathNode) == false)
            {
                string xml = HttpUtility.HtmlDecode(xmltext);
                xml = HttpUtility.HtmlDecode(xml);
                XmlDocument xmldsBody = new XmlDocument();
                xmldsBody.LoadXml(xml);
                XmlNode xmlNodeToUpdate = xmldsBody.SelectSingleNode(xPathNode);

                if (xmlNodeToUpdate != null)
                {
                    result = xmlNodeToUpdate.InnerText;                   
                    
                }
            }
            TraceManager.CustomComponent.TraceInfo("{0} - result: {1}", scopeName, result);
            TraceManager.CustomComponent.TraceEndScope(scopeName, scope);
            return result;
        }

        public string UpdateDSBODY(string dsBody, string xPathNodeToUpdate, string newValue)
        {
            string result = dsBody;
            if (string.IsNullOrWhiteSpace(dsBody) == false && string.IsNullOrWhiteSpace(xPathNodeToUpdate) == false)
            {
                XmlDocument xmldsBody = new XmlDocument();
                xmldsBody.LoadXml(dsBody);
                XmlNode xmlNodeToUpdate = xmldsBody.SelectSingleNode(xPathNodeToUpdate);

                if (xmlNodeToUpdate != null)
                {
                    xmlNodeToUpdate.InnerText = newValue;
                    result = xmldsBody.InnerXml;
                }
            }
            return result;
        }

        public static string GetConfigFileName()
        {
           
            string path = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\BizTalk Server\3.0", "InstallPath", @"C:\Program Files (x86)\Microsoft BizTalk Server 2016\").ToString();
            string configName = path + "BTSNTSvc.exe.config";

            return configName;
        }
    }
}
