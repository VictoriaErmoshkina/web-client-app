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
        FhirClient client = new FhirClient(new Uri("https://fhirtest.uhn.ca/baseDstu3"));

   //     [Required(ErrorMessage = "Please, Enter type of resource")]
        public String Type { get; set; }


        public List<String> GetAll()
        {
            
            List < String > ResourceList = new List<String>();
            if (Type != null)
            {
                var bundle = client.Search(Type);
                client.ReturnFullResource = true;
                foreach (var entry in bundle.Entry)
                {
                    var p = entry.Resource;
                    ResourceList.Add(p.Id);
                    bundle.GetResources();
                }
            }
            return ResourceList;
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
        MedicationDispense
    }
}
