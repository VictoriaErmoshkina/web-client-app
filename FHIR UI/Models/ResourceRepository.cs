using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace FHIR_UI.Models
{
    public class ResourceRepository
    {
        private readonly FhirClient _client;

        public ResourceRepository(string url)       
        {
            _client = new FhirClient(new Uri(url));
            
        }


        public List<String> GetIds(string type)
        {
            List<String> result = new List<String>();
            if (string.IsNullOrWhiteSpace(type))
                return result;
            var bundle = _client.Search(type);
            while (bundle!= null)
            {
                foreach (var entry in bundle.Entry)
                {
                    result.Add(entry.Resource.Id);
                }
                bundle = _client.Continue(bundle);
            }
            return result;
        }

        public Bundle GetBundles(string type)
        {
            List<String> result = new List<String>();
            if (string.IsNullOrWhiteSpace(type))
                return null;
            return _client.Search(type);
        }

        public Bundle NextBundles( Bundle bundle)
        {
            return _client.Continue(bundle);
        }
    }

    /// <summary>
    ///     //TODO: Как получить список типов ресурсов? Ну а пока в ручную(((

    /// </summary>
    public enum TypeName
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
        MedicationDispense,
        ChargeItem,
        Coverage
    }
}
