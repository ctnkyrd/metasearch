using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace metasearch
{
    class Program
    {
        static void Main(string[] args)
        {

            string reqXml = "<?xml version=\"1.0\" ?>\r\n<csw:GetRecords maxRecords=\"10\" outputFormat=\"application/xml\" outputSchema=\"http://www.opengis.net/cat/csw/2.0.2\" resultType=\"results\" service=\"CSW\" version=\"2.0.2\" xmlns:csw=\"http://www.opengis.net/cat/csw/2.0.2\" xmlns:ogc=\"http://www.opengis.net/ogc\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.opengis.net/cat/csw/2.0.2 http://schemas.opengis.net/csw/2.0.2/CSW-discovery.xsd\">\r\n\t<csw:Query typeNames=\"csw:Record\">\r\n\t\t<csw:ElementSetName>full</csw:ElementSetName>\r\n\t\t<csw:Constraint version=\"1.1.0\">\r\n\t\t\t<ogc:Filter>\r\n\t\t\t\t<ogc:PropertyIsLike escapeChar=\"\\\" singleChar=\"_\" wildCard=\"%\">\r\n\t\t\t\t\t<ogc:PropertyName>csw:AnyText</ogc:PropertyName>\r\n\t\t\t\t\t<ogc:Literal>TUCBS</ogc:Literal>\r\n\t\t\t\t</ogc:PropertyIsLike>\r\n\t\t\t</ogc:Filter>\r\n\t\t</csw:Constraint>\r\n\t</csw:Query>\r\n</csw:GetRecords> ";
            string cevap = postXMLData("https://metaveri.geoportal.gov.tr/csbgeoportalusermanagement/CswService/Csw", reqXml);
            Console.WriteLine(cevap);

            string postXMLData(string destinationUrl, string requestXml)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
                byte[] bytes;
                bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                request.Headers["Authorization"] = "Basic ZW50ZWdyYXN5b25hcmRhOlR1Y2JzPzA3YQ==";
                request.Method = "POST";
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
                HttpWebResponse response;
                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream responseStream = response.GetResponseStream();
                    string responseStr = new StreamReader(responseStream).ReadToEnd();
                    return responseStr;
                }
                return null;
            }
        }
    }
}
