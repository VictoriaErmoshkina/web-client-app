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
        public String typeOfResource_ { get; set; }
        public int totalAmountOfItems_ { get; set; }
        public int currentPageNumb_ { get; internal set; }
        public int currentAmountOfItems_ { get; internal set; }
        public int pagesAmount_ { get; set; }
        public String[] resultOnPage_;
        public List<String> totalResult_ { get; set; }
        #endregion

        public SearchResultModel() { }
        public SearchResultModel(string type)
        {
            typeOfResource_ = type;
        }

       /// <summary>
       /// Sets result for certain page
       /// </summary>
       /// <param name="amountOnPage">max amount of items on each page</param>
       /// <param name="CurrentPage">number of page</param>
        public void SetResultOnPage( int amountOnPage, int CurrentPage)
        {
            this.currentPageNumb_ = CurrentPage;
            if (amountOnPage > totalAmountOfItems_ - amountOnPage * (currentPageNumb_-1) 
                & totalAmountOfItems_ - amountOnPage * (currentPageNumb_ - 1) !=0)
                currentAmountOfItems_ = totalAmountOfItems_ - amountOnPage * (currentPageNumb_ - 1);
            else currentAmountOfItems_ = amountOnPage;

            resultOnPage_ = new String[currentAmountOfItems_];
            var firstIndex = amountOnPage * (currentPageNumb_ - 1);
            totalResult_.CopyTo(firstIndex, resultOnPage_, 0, currentAmountOfItems_);
        }


       
    }


}
