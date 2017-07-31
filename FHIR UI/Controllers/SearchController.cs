using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHIR_UI.Models;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIR_UI.Controllers
{
    public class SearchController : Controller
    {
        // GET: /<controller>/
       
        public IActionResult Index()
        {
            ResourceRepository repo = new ResourceRepository();
            SearchResultModel m = new SearchResultModel();
            m.ResourceResultList = repo.GetAll();
            return View(m);
        }

       [HttpPost]
        public IActionResult Index(SearchResultModel m)
        {
            if (ModelState.IsValid)
            {
                ResourceRepository rep = new ResourceRepository();
                rep.Type = m.Type;
                m.ResourceResultList = rep.GetAll();
            }
            return View(m);
        }

        /// <summary>
        /// ищет ресурс по заданному id, выводит JSON 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>

        public IActionResult Read (string type, string id, string version = null)
        {
          
            SearchResultModel resource = new SearchResultModel();
            // ViewBag.JSONText = resource.GetResource(type, id, version);
            FhirClient client = new FhirClient(new Uri("https://fhirtest.uhn.ca/baseDstu3"));
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel();
            m.Text = text;
            return View(m);
        }

        public IActionResult Edit(string type, string id, string version = null)
        {

            SearchResultModel resource = new SearchResultModel();
            // ViewBag.JSONText = resource.GetResource(type, id, version);
            FhirClient client = new FhirClient(new Uri("https://fhirtest.uhn.ca/baseDstu3"));
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel();
            m.Text = text;
            return View(m);
        }
    }
}
