using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace JsonToXmlHTTPtrigger
{
    public static class JsonToXmlHTTPtrigger
    {
        [FunctionName("JsonToXmlHTTPtrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var jsonObject = JsonConvert.DeserializeObject(requestBody);
            var xml = JsonConvert.DeserializeXNode(jsonObject.ToString(), "Root");
            return new OkObjectResult(xml.ToString());
        }
    }
}
