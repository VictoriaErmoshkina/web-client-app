using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHIR_UI.Models;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIR_UI.Controllers
{
    public class FhirResourceController : Controller
    {
        public String url = "https://fhirtest.uhn.ca/baseDstu3";


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(JsonTextModel json = null)
        {
            if (json.JsonText_ != null)
            {
                var parser = new FhirJsonParser();
                try
                {
                    var res = parser.Parse<Resource>(json.JsonText_);
                    FhirClient client = new FhirClient(url);
                    var resEntry = client.Create(res);
                    json.Status_ = JsonTextModel.CREATED;
                }
                catch (Exception e)
                {
                    json.Status_ = JsonTextModel.FAILED;
                    return View(json);
                }
            }
            else
            {
                json = new JsonTextModel();
                json.Status_ = JsonTextModel.CREATING;
            }
            return View(json);
        }
    }
}
