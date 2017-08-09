using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHIR_UI.Models;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;

namespace FHIR_UI.Controllers
{
    public class HomeController : Controller
    {
        public String url = "https://fhirtest.uhn.ca/baseDstu3";

        public IActionResult Index()
        {
            ResourceTypesModel res = new ResourceTypesModel();
            return View(res);
        }

       


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        
    }
}
