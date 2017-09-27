using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Threading.Tasks;


namespace Common
{
    public static class RestInteraction
    {
        private static int SERVICE_PORT = 8170;
        private static string SERVICE_URL = "http://localhost:{0}/";
        private static string ADD_ACTION = "Api/Add/AddLog{0}";

        public static string AddLog(ErrorLoggerModel.Errors errorlog)
        {
            string result = string.Empty;

            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(@"http://localhost:51963/"+ "api/Add/AddLog"), //RequestUri = new Uri(@"http://localhost/LoggerService/" + "api/Add/AddLog"),
                Method = HttpMethod.Post,
            };

            try
            {
                request.Content = new ObjectContent<ErrorLoggerModel.Errors>(errorlog, new JsonMediaTypeFormatter());
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    Task<string> task = response.Content.ReadAsStringAsync();  // returns immediately
                    string temp = task.Result;  // blocks until task completes

                    result = "And the result is: " + temp.ToString();
                }
                else
                {
                    result = string.Format("ERROR: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    HttpError error = response.Content.ReadAsAsync<HttpError>().Result;
                    string a = "pls";
                }
            }
            catch (Exception e) 
            {
                String a = e.Message;
            }
            return result;
        }
    }
}
