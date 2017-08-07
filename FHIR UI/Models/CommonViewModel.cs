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
        public ResourceTypesModel typesModel_ { get; set; }
        public SearchResultModel searchModel_ { get; set; }

       public CommonViewModel (ResourceTypesModel rtm, SearchResultModel srm)
        {
            typesModel_ = rtm;
            searchModel_ = srm;
        }

        public CommonViewModel()
        {
    
        }
    }
}
