using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using System.ComponentModel.DataAnnotations;

namespace FHIR_UI.Models
{ 
    public class SearchResultModel
    {

        #region Properties
        [Required(ErrorMessage ="Please, Enter type of resource")]
        public String Type { get; set; }

        public int Amount { get; set; }
        //[Required(ErrorMessage = "Please, set server uri")]
        //public Uri Endpoint { get; set; }

        public List<String> ResourceResultList { get; set; }
        #endregion

        public SearchResultModel()
        {
            Type = null;
            ResourceResultList = new List<String>();
        }

       

        // public String Id { get; set; }
        // public String Version { get; set; }

        Uri endpoint = new Uri("https://fhirtest.uhn.ca/baseDstu3");
        /// <summary>
        /// Ищет все ресурсы заданного типа
        /// </summary>
        /// <param name="resourceType"></param> тип искомого ресурса
        /// <returns></returns> количество найденных ресурсов, их тип и id
        public  List<String> GetAll(string resourceType)
        {
            FhirClient client = new FhirClient(endpoint);
            List<String> result = new List<String>();

            client.PreferredFormat = ResourceFormat.Json;
            try { 
            var bundle = client.Search(resourceType);
            client.ReturnFullResource = true;
            
            int amount = bundle.Entry.Count;
           
                    result.Add(amount.ToString());
                    foreach (var entry in bundle.Entry)
                    {
                        var p = entry.Resource;
                        String str = p.ResourceType.ToString() + " (id: " + p.Id + " )";
                        result.Add(str);                      
                        bundle.GetResources();
                }
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

        public void TypeList()
        {
            var client = new FhirClient(endpoint);
           

        }


    }

    /// <summary>
    ///     //TODO: Как получить список типов ресурсов? Ну а пока в ручную(((

    /// </summary>
    public enum TypeModel
    {
        Observation,
        Encounte,
        Condition,
        Patient,
        Immunization,
        MedicationRequest,
        Sequence,
        DiagnosticReport,
        CarePlan,
        MedicationDispense
    }

}
