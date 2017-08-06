using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FHIR_UI.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FHIR_UI.Controllers
{
    public class ResrcController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ResourceTypesModel rtm = new ResourceTypesModel();
            return View(rtm);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
