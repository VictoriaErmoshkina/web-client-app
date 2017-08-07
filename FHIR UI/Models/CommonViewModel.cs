using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHIR_UI.Models
{
    public class CommonViewModel
    {
        public ResourceTypesModel Types { get; set; }
        public SearchResultModel SearchRes { get; set; }

       public CommonViewModel (ResourceTypesModel r, SearchResultModel s)
        {
            Types = r;
            SearchRes = s;
        }

        public CommonViewModel()
        {
    
        }
    }
}
