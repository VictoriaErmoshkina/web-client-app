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
        public IActionResult Index(String type = null, int page = 1)
        {
            ResourceRepository repo = new ResourceRepository(url);
            SearchResultModel resultModel = new SearchResultModel();
            resultModel.ResourceResultList = repo.GetIds(resultModel.Type);
            ResourceTypesModel resTypes = new ResourceTypesModel();
            CommonViewModel m = new CommonViewModel(resTypes, resultModel); 
            return View(m);
        }

        [HttpPost]
        public IActionResult Index(SearchResultModel resultModel)
        {
            if (resultModel.currentPage == 0) { resultModel.currentPage = 1; };
            if (ModelState.IsValid)
            {
                ResourceRepository rep = new ResourceRepository(url);
                resultModel.ResourceResultList = rep.GetIds(resultModel.Type);
                resultModel._amount = resultModel.ResourceResultList.Count();
                resultModel.pagesAmount = (int)Math.Ceiling((double)resultModel._amount/amountOnPage );
                resultModel.getResultOnPage( amountOnPage, resultModel.currentPage);
            }

            ResourceTypesModel resTypes = new ResourceTypesModel();
            CommonViewModel m = new CommonViewModel(resTypes, resultModel);

            return View(m);
        }

        [HttpGet]
        public IActionResult Woohoo(String type=null, int page=1)
        {
            ResourceRepository rep = new ResourceRepository(url);
            SearchResultModel resultModel = new SearchResultModel(type);
            resultModel.Type = type;
            resultModel.ResourceResultList = rep.GetIds(resultModel.Type);
            resultModel._amount = resultModel.ResourceResultList.Count();
            resultModel.pagesAmount = (int)Math.Ceiling((double)resultModel._amount / amountOnPage);
            resultModel.currentPage = page;
            resultModel.getResultOnPage(amountOnPage, resultModel.currentPage);

            ResourceTypesModel resTypes = new ResourceTypesModel();
            CommonViewModel m = new CommonViewModel(resTypes, resultModel);

            
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
