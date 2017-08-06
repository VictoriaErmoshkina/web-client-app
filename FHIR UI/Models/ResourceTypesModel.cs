using Hl7.Fhir.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FHIR_UI.Models
{
    public class ResourceTypesModel
    {
        ImplementationGuide ig = new ImplementationGuide();
        public Array TypesAsArray { get; set; }

       public ResourceTypesModel()
        {
            TypesAsArray = Enum.GetValues(typeof(ResourceType));
        }
    }
}
