using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHIR_UI.Models;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;
using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
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
            //ResourceRepository repo = new ResourceRepository(url);
            SearchResultModel resultModel = new SearchResultModel(new ResourceRepository(url));
            resultModel.TypeOfResource_ = type;
            // resultModel.TotalResult_ = repo.FilteredGetIds(resultModel.TypeOfResource_, page);
            resultModel.Bundle_ = resultModel.repository_.getBunde(type);
            
            resultModel.TotalAmountOfItems_ = resultModel.Bundle_.Entry.Count();
          //  resultModel.PagesAmount_ = (int)Math.Ceiling((double)resultModel.TotalAmountOfItems_ / amountOnPage);
            resultModel.CurrentPageNumb_ = page;
            if (resultModel.TotalAmountOfItems_ > 0)
                
                resultModel.SetResultOnPage(amountOnPage, true);

            //  ResourceTypesModel resTypes = new ResourceTypesModel();
            //  CommonViewModel m = new CommonViewModel(resTypes, resultModel); 

            ViewBag.Bundle_ = resultModel.Bundle_;
            resultModel.list.Add(resultModel.Bundle_);
            return View(resultModel);
        }


        /// <summary>
        /// я не могу проверить листание страниц т.к хз как сохранить Bundle
        /// но без представления просто все работало
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Index(SearchResultModel m)
        {
            //if (m.SearchModel_.CurrentPageNumb_ == 0) { m.SearchModel_.CurrentPageNumb_ = 1; };
            if (ModelState.IsValid)
            {
                
                ResourceRepository rep = new ResourceRepository(url);
                m.Bundle_ = rep.getBunde(m.TypeOfResource_);
               // m.TotalAmountOfItems_ = m.Bundle_.Entry.Count();
             //   m.PagesAmount_ = (int)Math.Ceiling((double)m.TotalAmountOfItems_ /amountOnPage );

                m.SetResultOnPage( amountOnPage);
            }

            //  ResourceTypesModel resTypes = new ResourceTypesModel();
            // CommonViewModel common = new CommonViewModel(resTypes, resultModel);

            ViewBag.Bundle_ = m.Bundle_;

            return View(m);
        }

        /// <summary>
        /// чтение работает правильно
        /// </summary>
        /// <param name="type">тип ресурса</param>
        /// <param name="id">ид ресурса</param>
        /// <param name="version">версия ресурса </param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Read (string type, string id, string version = null)
        {          
            FhirClient client = new FhirClient(url);           
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel(type,id, version, text);          
            m.Status_ = JsonTextModel.READING;
            return View(m);
        }


        /// <summary>
        /// редактор ресурса работает, все ок. 
        /// Единственное надо бы как-то сделать вывод текста не одной строкой, а разбить на абзацы
        /// </summary>
        /// <param name="type">тип ресурса</param>
        /// <param name="id">ид ресурса</param>
        /// <param name="version">версия ресурса</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Edit(string type, string id, string version = null)
        {
            FhirClient client = new FhirClient(url);
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel(type, id, version, text);
            m.Status_ = JsonTextModel.UPDATING;
            return View(m);
        }

        /// <summary>
        /// редактирует ресурс 
        /// </summary>
        /// <param name="json">представление ресурса</param>
        /// <returns></returns
        [HttpPost]
        public IActionResult Edit(JsonTextModel json)
        {
            if (string.IsNullOrWhiteSpace(json.JsonText_))
                return View(json);
            var parser = new FhirJsonParser();
            try
            {
                var res = parser.Parse<Resource>(json.JsonText_);
                FhirClient client = new FhirClient(url);
                var resEntry = client.Update(res);
                json.Status_ = JsonTextModel.UPDATED;
            }
           
            catch (Exception e)
            {
                json.Status_ = JsonTextModel.FAILED;
                return View(json);
            }
            return View(json);

        }

        private IActionResult View(JsonTextModel json, Exception e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// создает ресурс (проверяла без view -- все ок )
        /// </summary>
        /// <param name="json">представление ресурса</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(JsonTextModel json)
        {
            var parser = new FhirJsonParser();
            try
            {
               var res = parser.Parse<Resource>(json.JsonText_);
               FhirClient client = new FhirClient(url);
               var resEntry= client.Create(res);
               json.Status_ = JsonTextModel.CREATED;
            }
            catch (Exception e)
            {
                json.Status_ = JsonTextModel.FAILED;
                return View(json);
            }
            return View(json);
        }
    }
}
