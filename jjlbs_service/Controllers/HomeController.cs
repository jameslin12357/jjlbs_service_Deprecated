using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using jjlbs_service.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using jjlbs_service.oracle;

namespace jjlbs_service.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //Oraclehp ohp = new Oraclehp();
            //DataSet data = ohp.Query($"select * from lbs_village");
            //DataSet data2 = ohp.Query($"select * from lbs_building");
            //DataRowCollection rows = data.Tables[0].Rows;
            //DataRowCollection rows2 = data2.Tables[0].Rows;
            //ViewData["rows"] = rows;
            //ViewData["rows2"] = rows2;

            return View();
        }

        public IActionResult Test()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
