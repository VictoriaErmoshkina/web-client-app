using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHIR_UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIR_UI.Controllers
{
    public class SearchController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll(string type)
        {
            Fresource resource = new Fresource();
            List<String> result = resource.GetAll(type);
            ViewBag.Amount = result.First();
            result.RemoveAt(0);
            ViewBag.ResultList = result;   
            return View();
        }

        public IActionResult GetJSON (string type, string id, string version = null)
        {
            
            Fresource resource = new Fresource();
            ViewBag.JSONText = resource.GetResource(type, id, version);
            return View();
        }
    }
}
