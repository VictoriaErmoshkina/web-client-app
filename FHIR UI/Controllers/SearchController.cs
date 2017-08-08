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
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIR_UI.Controllers
{
    public class SearchController : Controller
    {
        //переделать в конфиг
        public String url = "https://fhirtest.uhn.ca/baseDstu3";
        int amountOnPage = 20;


        [HttpGet]
        public IActionResult Index(String type = null, int page = 1, string[] q = null)
        {
           q = new string[1];
           q[0] = "birthdate=2017-08-08";
            ResourceRepository repo = new ResourceRepository(url);
            SearchResultModel resultModel = new SearchResultModel();
            resultModel.typeOfResource_ = type;
            resultModel.totalResult_ = repo.FilteredGetIds(resultModel.typeOfResource_, page, q);
                        
            resultModel.totalAmountOfItems_ = resultModel.totalResult_.Count();
            resultModel.pagesAmount_ = (int)Math.Ceiling((double)resultModel.totalAmountOfItems_ / amountOnPage);
            resultModel.currentPageNumb_ = page;
            if (resultModel.totalAmountOfItems_ >0)
                resultModel.SetResultOnPage(amountOnPage, resultModel.currentPageNumb_);

            ResourceTypesModel resTypes = new ResourceTypesModel();
            CommonViewModel m = new CommonViewModel(resTypes, resultModel); 
            return View(m);
        }

        [HttpPost]
        public IActionResult Index(CommonViewModel m)
        {
            if (m.searchModel_.currentPageNumb_ == 0) { m.searchModel_.currentPageNumb_ = 1; };
            if (ModelState.IsValid)
            {
                ResourceRepository rep = new ResourceRepository(url);
                m.searchModel_.totalResult_ = rep.FilteredGetIds(m.searchModel_.typeOfResource_);
                m.searchModel_.totalAmountOfItems_ = m.searchModel_.totalResult_.Count();
                m.searchModel_.pagesAmount_ = (int)Math.Ceiling((double)m.searchModel_.totalAmountOfItems_/amountOnPage );
                m.searchModel_.SetResultOnPage( amountOnPage, m.searchModel_.currentPageNumb_);
            }

           

            return View(m);
        }

        [HttpGet]
        public IActionResult Read (string type, string id, string version = null)
        {          
            FhirClient client = new FhirClient(url);
           
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel();
            m.jsonText_ = text;
            return View(m);
        }

        [HttpGet]
        public IActionResult Edit(string type, string id, string version = null)
        {
            FhirClient client = new FhirClient(url);
            String text = FhirSerializer.SerializeResourceToJson(client.Get(ResourceIdentity.Build(type, id, version)));
            JsonTextModel m = new JsonTextModel();
            m.jsonText_ = text;
            return View(m);
        }

        public IActionResult Update(String json)
        {
            json = @"{ 'resourceType': 'Patient',
                      'id': '203452',
                       'meta': {
                         'versionId': '1',
                         'lastUpdated': '2017-08-08T07:46:49.406-04:00'
                       },
                       'text': {
                         'status': 'generated',
                         'div': '<div xmlns=\'http://www.w3.org/1999/xhtml\'><div class=\'hapiHeaderText\'>Automation <b>PATIENT </b> Number</div><table class=\'hapiPropertyTable\'><tbody><tr><td>Date of birth</td><td><span>08 August 2017</span></td></tr></tbody></table></div>'
                       },
                       'name': [
                         {
                           'family': 'Patient',
                           'given': [
                             'Automation'
                           ],
                           'suffix': [
                             'Number'
                           ]
                         }
                       ],
                       'birthDate': '2017-08-08',
                       'gender': 'male',
                    } ";
            var parser = new FhirJsonParser();
            String s = "ok";
            try
            {
                var res = parser.Parse<Resource>(json);
                s += "parsed ^_^";

                FhirClient client = new FhirClient(url);
                var resEntry = client.Update(res);
                s += " updated ^_^";
                var resId = resEntry.Id;
                var resVersion = resEntry.Meta.VersionId;
                s += "id: " + resId+" version: "+resVersion;
            }
            catch (Exception e)
            {
                s += " ЖОПАЖОПАЖОПА error of parcing" + e.Message;
            }
            return View();

        }

        /// <summary>
        /// создает ресурс
        /// </summary>
        /// <param name="json">представление ресурса</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(String json)
        {
            json = @"{
                        'resourceType': 'Patient',
                        'name': 
                         [
                            {
                            'family': 'Patient',
                            'given': [
                                    'Automation'
                                     ],
                            'suffix': [
                                     'Number'
                                      ]
                            }
                          ],
                        'birthDate': '2017-08-08',
                        }

                ]
                }";
            var parser = new FhirJsonParser();
            String s = "ok;";
            try
            {
               var res = parser.Parse<Resource>(json);
                s += "parsed ^_^";

                FhirClient client = new FhirClient(url);
               var resEntry= client.Create(res);
                s += " created ^_^";
                var resId = resEntry.Id;
                s += "id: "+resId;
            }
            catch (Exception e)
            {
                 s += " ЖОПАЖОПАЖОПА error of parcing"+ e.Message;
            }

            return View();
        }
    }
}
