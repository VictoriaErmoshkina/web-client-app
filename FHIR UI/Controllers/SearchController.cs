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
        //переделать в конфиг
        public String url = "https://fhirtest.uhn.ca/baseDstu3";
        int amountOnPage = 20;
        [HttpGet]
        //public IActionResult Index()
        //{
        //    ResourceRepository repo = new ResourceRepository(url);
        //    SearchResultModel m = new SearchResultModel();
        //    m.ResourceResultList = repo.GetIds(m.Type);
        //    return View(m);
        //}

        [HttpPost]
        public IActionResult Index(SearchResultModel m)
        {
            if (m.currentPage == 0) { m.currentPage = 1; };
            if (ModelState.IsValid)
            {
                ResourceRepository rep = new ResourceRepository(url);
                m.ResourceResultList = rep.GetIds(m.Type);
                m._amount = m.ResourceResultList.Count();
                m.pagesAmount = (int)Math.Ceiling((double)m._amount/amountOnPage );
                m.getResultOnPage( amountOnPage, m.currentPage);

                //ResourceRepository rep = new ResourceRepository(url);
                //m.bundle = rep.GetBundles(m.Type);
                //m._amount = m.bundle.Entry.Count;
            }
            return View(m);
        }

        public IActionResult Woohoo(String type=null, int page=1)
        {
                ResourceRepository rep = new ResourceRepository(url);
            SearchResultModel m = new SearchResultModel(type);
            m.Type = type;
            m.ResourceResultList = rep.GetIds(m.Type);
                m._amount = m.ResourceResultList.Count();
                m.pagesAmount = (int)Math.Ceiling((double)m._amount / amountOnPage);
            m.currentPage = page;
                m.getResultOnPage(amountOnPage, m.currentPage);
            
            return View(m);
        }


        [HttpGet]
        public IActionResult Read (string type, string id, string version = null)
        {          
            FhirClient client = new FhirClient(url);
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel();
            m.Text = text;
            return View(m);
        }

        [HttpGet]
        public IActionResult Edit(string type, string id, string version = null)
        {
            FhirClient client = new FhirClient(url);
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel();
            m.Text = text;
            return View(m);
        }
    }
}
