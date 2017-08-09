using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FHIR_UI.Models
{
    public class SearchResultModel
    {

        #region Properties
        public String TypeOfResource_ { get; set; }
        public int TotalAmountOfItems_ { get; set; }
        public int CurrentPageNumb_ { get; internal set; }
        public int CurrentAmountOfItems_ { get; internal set; }
        public int PagesAmount_ { get; set; }
      //  public String[] ResultOnPage_;
       // public List<String> TotalResult_ { get; set; }

        public Bundle Bundle_ { get; set; }
        public IEnumerable<string> ResultForPage_ { get; set; }
      public ResourceRepository repository_ { get;  set; }
        public PageDirection PagingIndx_ { get; set; }
        public readonly Array Paging_ = Enum.GetValues(typeof(PageDirection));

        public List <Bundle> list;
        #endregion

        public SearchResultModel() {
            list = new List<Bundle>();
        }
        public SearchResultModel(ResourceRepository repo, string type=null)
        {
            list = new List<Bundle>();
            repository_ = repo;
            TypeOfResource_ = type;
        }

        /// <summary>
        /// Sets result for certain page
        /// </summary>
        /// <param name="amountOnPage">max amount of items on each page</param>
        /// <param name="CurrentPage">number of page</param>
        public void SetResultOnPage(int amountOnPage, bool firstBrowse = false)
        {
            // this.CurrentPageNumb_ = CurrentPage;
            //if (amountOnPage > TotalAmountOfItems_ - amountOnPage * (CurrentPageNumb_-1) 
            //    & TotalAmountOfItems_ - amountOnPage * (CurrentPageNumb_ - 1) !=0)
            //    CurrentAmountOfItems_ = TotalAmountOfItems_ - amountOnPage * (CurrentPageNumb_ - 1);
            //else CurrentAmountOfItems_ = amountOnPage;

            //   ResultOnPage_ = new String[CurrentAmountOfItems_];
            //  var firstIndex = amountOnPage * (CurrentPageNumb_ - 1);
            //TotalResult_.CopyTo(firstIndex, ResultOnPage_, 0, CurrentAmountOfItems_);

            if (!firstBrowse)
            {
                Bundle_ = repository_.redirect(Bundle_, PagingIndx_);
            }
            ResultForPage_ = Bundle_.GetResources().Select(r => r.Id);
            
        }


       
    }


}
