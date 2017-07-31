using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
namespace FHIR_UI.Models
{
    public class Fresource
    {
        Uri endpoint = new Uri("https://fhirtest.uhn.ca/baseDstu3");
        /// <summary>
        /// Ищет все ресурсы заданного типа
        /// </summary>
        /// <param name="resourceType"></param> 
        /// <returns></returns> количество найденных ресурсов, их тип и id
        public  List<String> GetAll(string resourceType)
        {
          resourceType = "Patient";
            FhirClient client = new FhirClient(endpoint);
            List<String> result = new List<String>();

            client.PreferredFormat = ResourceFormat.Json;
            try { 
            var bundle = client.Search(resourceType);
            client.ReturnFullResource = true;
            
            int amount = bundle.Entry.Count;
            if (amount == 0)
            {
               result.Add("Server did not return results for search criteria ");

            }

            result.Add(amount.ToString());
            foreach (var entry in bundle.Entry)
            {
                var p = entry.Resource;

                try
                {
                    String str = p.ResourceType.ToString() + " (id: " + p.Id + " )";
                    result.Add(str);
                }
                catch (Exception)
                {
                    result.Add(" SCHEISSE SCHEISSE SCHEISSE");
                }
            }
            bundle.GetResources();
            }
            catch
            {
                result.Add("!!!!!!!!!!!!! SCHEISSE SCHEISSE SCHEISSE!!!!!!!!!!!!!!!!!!!");

            }

            return result;
        }

        public String GetResource (string resourceType, string id, string version=null)
        {
            var client = new FhirClient(endpoint);
            var identity = ResourceIdentity.Build(resourceType, id, version);
            var res = client.Get(identity);
            var resId = res.Id;
            var resName = res.Meta.TypeName;

            String text;
            text = FhirSerializer.SerializeResourceToJson(res);

            return text;
        }

     
    }
}
