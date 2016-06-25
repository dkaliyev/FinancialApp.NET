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
        public static string Serialize(object obj)
        {
            if (obj == null)
                return string.Empty;
            var serializer = new JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new NHibernateContractResolver()
            };
            StringWriter stringWriter = new StringWriter();
            JsonWriter jsonWriter = new JsonTextWriter(stringWriter);
            serializer.Serialize(jsonWriter, obj);
            string serializedObject = stringWriter.ToString();
            return serializedObject;
        }
    }
}
