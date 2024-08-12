using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Mobiletec.Biztalk.Cad.Helpers.DTO;

namespace Mobiletec.Biztalk.Cad.Helpers
{
    public class DataExportHelper
    {
        public string CallCensusApiNoTask(string apiurl, string paramfield)
        {
            string rta = CallCensusApi(apiurl, paramfield).GetAwaiter().GetResult();
            return rta;
        }
        public async Task<string> CallCensusApi(string apiurl, string paramfield)
        {
            string rta = "";
            Rootobject root = new Rootobject();
            var client = new HttpClient();
            try
            {
                using (HttpResponseMessage response = client.GetAsync(apiurl).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        Stream userResult = await response.Content.ReadAsStreamAsync();
                        if (userResult == null) return rta = "";
                        using (StreamReader objReader = new StreamReader(userResult))
                        {
                            string responseBody = objReader.ReadToEnd();
                            //Console.WriteLine(responseBody);
                            root = JsonConvert.DeserializeObject<Rootobject>(responseBody);
                            if (root.result.addressMatches.Length > 0)
                            {
                                if (paramfield == "basename")
                                {
                                    if (root.result.addressMatches[0].geographies.Counties[0].BASENAME != null)
                                    {
                                        rta = root.result.addressMatches[0].geographies.Counties[0].BASENAME;
                                    }
                                }
                                if (paramfield == "tract")
                                {
                                    if (root.result.addressMatches[0].geographies.CensusBlocks[0].TRACT != null)
                                    {
                                        rta = root.result.addressMatches[0].geographies.CensusBlocks[0].TRACT;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode.ToString());
                        return rta = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return rta = "";
            }
            return rta;

        }//GetBasename

        
    }//
}
