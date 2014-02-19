using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using MySetList.Models;

namespace MySetList.Controllers
{
    public class ChordChartController : Controller
    {
        private ChordChartContext db = new ChordChartContext();
        private const string CHART_STORAGE = @"~/ChartStorage";


        //
        // GET: /ChordChart/

        public ActionResult Index()
        {
            return View(db.ChordCharts.ToList());
        }

        //
        // GET: /ChordChart/Details/5

        public ActionResult Details(int id = 0)
        {
            ChordChart chordchart = db.ChordCharts.Find(id);
            if (chordchart == null)
            {
                return HttpNotFound();
            }
            return View(chordchart);
        }

        //
        // GET: /ChordChart/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /ChordChart/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ChordChart chordchart)
        {
            if (ModelState.IsValid)
            {
                db.ChordCharts.Add(chordchart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chordchart);
        }

        public ActionResult EnterSongChart()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterSongChart(ChordChart chordChart)
        {
            if(ModelState.IsValid)
            {
                var chartGuid = Guid.NewGuid();
                chordChart.ChartID = chartGuid;
                
                var storagePath = Path.Combine(Server.MapPath(CHART_STORAGE), chartGuid.ToString() + ".pdf");
                chordChart.StoragePath = storagePath;

                //todo: need to sanitize the file name before we store it (safeguard against slashes, etc)
                chordChart.OriginalFileName = chordChart.SongTitle + ".pdf";

                db.ChordCharts.Add(chordChart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chordChart);
        }

        // GET: /ChordChart/Upload
        public ActionResult Upload()
        {
            return View();
        }

        // POST: /ChordChart/Upload
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if(ModelState.IsValid)
            {                                                
                // save the file to the server
                var chartGuid = Guid.NewGuid();
                var path = Path.Combine(Server.MapPath(CHART_STORAGE), chartGuid.ToString() + ".pdf");
                
                if(!Directory.Exists(Server.MapPath(CHART_STORAGE)))
                {
                    Directory.CreateDirectory(Server.MapPath(CHART_STORAGE));
                }

                file.SaveAs(path);

                // record chart to database
                var newChart = new ChordChart();
                newChart.ChartID = chartGuid;
                newChart.OriginalFileName = file.FileName;
                newChart.StoragePath = Server.MapPath(CHART_STORAGE) + chartGuid.ToString() + ".pdf";
                db.ChordCharts.Add(newChart);
                db.SaveChanges();                
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /ChordChart/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ChordChart chordchart = db.ChordCharts.Find(id);
            if (chordchart == null)
            {
                return HttpNotFound();
            }
            return View(chordchart);
        }

        //
        // POST: /ChordChart/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ChordChart chordchart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chordchart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chordchart);
        }

        //
        // GET: /ChordChart/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ChordChart chordchart = db.ChordCharts.Find(id);
            if (chordchart == null)
            {
                return HttpNotFound();
            }
            return View(chordchart);
        }

        //
        // POST: /ChordChart/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChordChart chordchart = db.ChordCharts.Find(id);
            db.ChordCharts.Remove(chordchart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }        
    }
}