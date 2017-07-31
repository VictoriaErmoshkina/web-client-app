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
        Uri endpoint = new Uri("https://fhirtest.uhn.ca/baseDstu3");


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
