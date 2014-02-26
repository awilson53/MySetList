using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySetList.Models
{
    public class SetListController : Controller
    {
        private SetListContext db = new SetListContext();
        private ChordChartContext ccdb = new ChordChartContext();

        //
        // GET: /SetList/

        public ActionResult Index()
        {
            return View(db.SetLists.ToList());
        }

        //
        // GET: /SetList/Details/5

        public ActionResult Details(int id = 0)
        {
            SetList setlist = db.SetLists.Find(id);
            if (setlist == null)
            {
                return HttpNotFound();
            }
            return View(setlist);
        }

        //
        // GET: /SetList/Create

        public ActionResult Create()
        {
            ViewData["CCList"] = GetAvailableChordCharts();

            return View();
        }

        //
        // POST: /SetList/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SetList setlist)
        {
            if (ModelState.IsValid)
            {
                db.SetLists.Add(setlist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(setlist);
        }

        //
        // GET: /SetList/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SetList setlist = db.SetLists.Find(id);
            if (setlist == null)
            {
                return HttpNotFound();
            }
            return View(setlist);
        }

        //
        // POST: /SetList/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SetList setlist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(setlist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(setlist);
        }

        //
        // GET: /SetList/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SetList setlist = db.SetLists.Find(id);
            if (setlist == null)
            {
                return HttpNotFound();
            }
            return View(setlist);
        }

        //
        // POST: /SetList/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SetList setlist = db.SetLists.Find(id);
            db.SetLists.Remove(setlist);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private MultiSelectList GetAvailableChordCharts()
        {
            return new MultiSelectList(ccdb.ChordCharts.ToList(), "ID", "SongTitle");
        }
    }
}