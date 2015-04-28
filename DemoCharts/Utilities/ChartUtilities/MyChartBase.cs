﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;

namespace DemoCharts.Utilities.ChartUtilities
{
    public class MyChartBase
    {
        protected List<Series> ChartSeriesData { get; set; }
        protected string ChartTitle { get; set; }

        // This is the method to get the chart image
        public MemoryStream GetChartImage(int width, int height)
        {
            var chart = InitiateChart(width, height);
            chart.RenderType = RenderType.BinaryStreaming;

            var ms = new MemoryStream();
            chart.SaveImage(ms, ChartImageFormat.Png);

            return ms;
        }

        // This is the method to get the chart image map
        public string GetChartImageMap(int width, int height, string mapName)
        {
            var chart = InitiateChart(width, height);
            chart.RenderType = RenderType.ImageMap;
            chart.SaveImage(Stream.Null);

            return chart.GetHtmlImageMap(mapName);
        }

        // Override this method to add title to the chart
        protected virtual void AddChartTitle()
        {
            ChartTitle = null;
        }

        // Override this method to add data to the chart
        protected virtual void AddChartSeries()
        {
            ChartSeriesData = new List<Series>();
        }

        // Initiate the chart to be rendered
        public Chart InitiateChart(int width, int height)
        {
            var chart = new Chart();
            chart.Width = width;
            chart.Height = height;
            chart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
            chart.BorderSkin.PageColor = System.Drawing.Color.Transparent;
            chart.BackColor = System.Drawing.Color.FromArgb(211, 223, 240);
            chart.BorderlineDashStyle = ChartDashStyle.Solid;
            chart.BackSecondaryColor = System.Drawing.Color.White;
            chart.BackGradientStyle = GradientStyle.TopBottom;
            chart.BorderlineWidth = 1;
            chart.Palette = ChartColorPalette.BrightPastel;
            chart.BorderlineColor = System.Drawing.Color.FromArgb(26, 59, 105);
            chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            chart.AntiAliasing = AntiAliasingStyles.All;
            chart.TextAntiAliasingQuality = TextAntiAliasingQuality.Normal;




            AddChartTitle();
            if (ChartTitle != null)
            {
                chart.Titles.Add(CreateTitle());
            }
            chart.Legends.Add(CreateLegend());

            AddChartSeries();
            foreach (var series in ChartSeriesData)
            {
                chart.Series.Add(series);
            }
            chart.ChartAreas.Add(CreateChartArea());
            return chart;
        }

        // Create chart title
        private Title CreateTitle()
        {
            return new Title()
            {
                Text = ChartTitle,
                ShadowColor = System.Drawing.Color.FromArgb(32, 0, 0, 0),
                Font = new System.Drawing.Font("Trebuchet MS", 10, FontStyle.Bold),
                ShadowOffset = 3,
                ForeColor = System.Drawing.Color.FromArgb(26, 59, 105),
                MapAreaAttributes = "Target=\"_Blank\""
            };
        }

        // configure chart Legend
        private Legend CreateLegend()
        {
            return new Legend()
            {
                Docking = Docking.Bottom,
                Alignment = StringAlignment.Center,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font(new System.Drawing.FontFamily("Trebuchet MS"), 8),
                LegendStyle = LegendStyle.Row
            };
        }

        // Configure the chart area - the chart frame x/y axes
        private ChartArea CreateChartArea()
        {
            var area = new ChartArea()
            {
                Name = ChartTitle,
                BackColor = System.Drawing.Color.Transparent,
            };

            area.AxisX.IsLabelAutoFit = true;
            area.AxisX.LabelStyle.Font =
                new System.Drawing.Font("Verdana,Arial,Helvetica,sans-serif",
                                        8F, FontStyle.Regular);
            area.AxisX.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            area.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            area.AxisX.Interval = 1;


            area.AxisY.LabelStyle.Font =
                new System.Drawing.Font("Verdana,Arial,Helvetica,sans-serif",
                                        8F, FontStyle.Regular);
            area.AxisY.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);
            area.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(64, 64, 64, 64);

            return area;
        }
    }
}