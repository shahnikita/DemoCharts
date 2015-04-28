using DemoCharts.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.DataVisualization.Charting;

namespace DemoCharts.Controllers
{
    public class ChartController : Controller
    {
        //
        // GET: /Chart/
        public static string ImageMap = "";
        [HttpGet]
        public ActionResult Index()
        {
            var chartData = BrowserShareRepository.GetBrowserShares();
            return View(chartData);
        }

        [HttpGet]
        public FileResult GetChart()
        {
            var chartData = BrowserShareRepository.GetBrowserShares();
            return File(chartData.ChartImageStream().GetBuffer()
                , @"image/png", "BrowserShareChart.png");
        }

        #region interactive charts



        public static Chart CreateDummyChart()
        {
            var chart = new Chart() { Width = 600, Height = 400 };
            chart.Palette = ChartColorPalette.Excel;
            chart.Legends.Add(new Legend("legend1") { Docking = Docking.Bottom });

            var title = new Title("Test chart", Docking.Top, new Font("Arial", 15, FontStyle.Bold), Color.Brown);
            chart.Titles.Add(title);
            chart.ChartAreas.Add("Area 1");

            chart.Series.Add("Series 1");
            chart.Series.Add("Series 2");

            chart.BackColor = Color.Azure;
            var random = new Random();

            //add random data: series 1
            foreach (int value in new List<int>() { random.Next(100), random.Next(100), random.Next(100), random.Next(100) })
            {
                chart.Series["Series 1"].Points.AddY(value);

                //attach JavaScript events - it can also be ajax call
                chart.Series["Series 1"].Points.Last().MapAreaAttributes = "onclick=\"alert('value: #VAL, series: #SER');\"";
            }

            //add random data: series 2
            foreach (int value in new List<int>() { random.Next(100), random.Next(100), random.Next(100), random.Next(100) })
            {
                chart.Series["Series 2"].Points.AddY(value);

                //attach JavaScript events - it can also be ajax call
                chart.Series["Series 2"].Points.Last().MapAreaAttributes = "onclick=\"alert('value: #VAL, series: #SER');\"";
            }

            return chart;
        }

        public static string GetChartImageHtml(Chart chart)
        {
            using (var stream = new MemoryStream())
            {
                var img = "<img src='data:image/png;base64,{0}' alt='' usemap='#" + ImageMap + "'>";

                chart.SaveImage(stream, ChartImageFormat.Png);

                var encoded = Convert.ToBase64String(stream.ToArray());

                return string.Format(img, encoded);
            }
        }

        public ActionResult DisplayChart()
        {

            var chartData = BrowserShareRepository.GetBrowserShares();

            var chart = chartData.MyChart();

            //  var chart = CreateDummyChart();

            var result = new StringBuilder();
            result.Append(GetChartImageHtml(chart));
            result.Append(chart.GetHtmlImageMap(ImageMap));

            return Content(result.ToString());
        }

        #endregion


    }
}
