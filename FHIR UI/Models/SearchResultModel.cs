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
       // [Required(ErrorMessage = "Please, Enter type of resource")]
        public String Type { get; set; }
        public int _amount { get; set; }

        public List<String> ResourceResultList { get; set; }
        #endregion

        public int currentPage { get; internal set; }

        public int currentAmount { get; internal set; }
       public int pagesAmount { get; set; }

        public  String [] result;

        public SearchResultModel(string type)
        {
            Type = type;
        }
        public SearchResultModel()
        {
          
        }

        public ResourceType rt;

        public void grt()
        {
            Array valuesAsArray = Enum.GetValues(typeof(ResourceType));

        }
        public void getResultOnPage( int amountOnPage, int CurrentPage)
        {
            this.currentPage = CurrentPage;
            if (amountOnPage > _amount - amountOnPage * (currentPage-1) 
                & _amount - amountOnPage * (currentPage - 1) !=0)
                currentAmount = _amount - amountOnPage * (currentPage - 1);
            else currentAmount = amountOnPage;

            result = new String[currentAmount];
            var firstIndex = amountOnPage * (currentPage - 1);
            ResourceResultList.CopyTo(firstIndex, result, 0, currentAmount);
           
        }
    }


}
