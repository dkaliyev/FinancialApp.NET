using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinancialThing.Utilities
{
    public class FTJsonSerializer
    {
        public static HttpResponseMessage Serialize(object obj)
        {
            if (obj == null)
                return new HttpResponseMessage();
            var serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };
            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new Newtonsoft.Json.JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, obj);
            string serializedObject = stringWriter.ToString();
            return new HttpResponseMessage()
            {
                Content = new StringContent(serializedObject, System.Text.Encoding.UTF8, "text/html")
            };
        }
    }
}
