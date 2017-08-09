using Hl7.Fhir.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHIR_UI.Models
{
    /// <summary>
    /// Ну я пока хз как вывести список типов ( ResourceTypesModel) в LayOut_ (ну или что-то типо того)
    /// Поэтому пока что костыль
    /// Но его надо убирать, потому что сейчас каждый раз при подгрузке поиска ResourceTypesModel создается заново
    /// </summary>
    //TODO: нужно передать ResourceTypesModel в LayOut_
    public class CommonViewModel
    {
        public readonly Array Paging_= Enum.GetValues(typeof(PageDirection));
        public ResourceTypesModel TypesModel_ { get; set; }
        public SearchResultModel SearchModel_ { get; set; }

       public CommonViewModel (ResourceTypesModel rtm, SearchResultModel srm)
        {
            Paging_ = Enum.GetValues(typeof(PageDirection));
            TypesModel_ = rtm;
            SearchModel_ = srm;
        }

        public CommonViewModel()
        {
            Paging_ = Enum.GetValues(typeof(PageDirection));
        }
    }
}
