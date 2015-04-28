using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace DemoCharts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult GetRainfallChart()
        {
            var key = new Chart(width: 600, height: 400)
                .AddSeries(
                    chartType: "pie",
                    legend: "Rainfall",
                    xValue: new[] { "Jan", "Feb", "Mar", "Apr", "May" },
                    yValues: new[] { "20", "20", "40", "10", "10" })
                .Write();

            return null;
        }
        [HttpPost]
        public JsonResult Piechart()
        {

            List<DataMonth> dataset = new List<DataMonth>();
            dataset.Add(new DataMonth { Month = "Jan", PayRate = 20 });
            dataset.Add(new DataMonth { Month = "Jan", PayRate = 20 });
            dataset.Add(new DataMonth { Month = "Feb", PayRate = 10 });
            dataset.Add(new DataMonth { Month = "Mar", PayRate = 40 });
            dataset.Add(new DataMonth { Month = "Apr", PayRate = 10 });
            dataset.Add(new DataMonth { Month = "May", PayRate = 10 });
            dataset.Add(new DataMonth { Month = "June", PayRate = 10 });

            return Json(dataset);
        }
        public class DataMonth
        {
            public string Month { get; set; }
            public int PayRate { get; set; }
        }
    }
}
