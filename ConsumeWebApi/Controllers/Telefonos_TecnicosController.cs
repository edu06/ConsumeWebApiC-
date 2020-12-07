using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsumeWebApi.Models;

namespace ConsumeWebApi.Controllers
{
    public class Telefonos_TecnicosController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Telefonos_Tecnicos
        public ActionResult Index()
        {
            var telefonos_Tecnicos = db.Telefonos_Tecnicos.Include(t => t.Tecnico_Soporte);
            return View(telefonos_Tecnicos.ToList());
        }

        // GET: Telefonos_Tecnicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos_Tecnicos telefonos_Tecnicos = db.Telefonos_Tecnicos.Find(id);
            if (telefonos_Tecnicos == null)
            {
                return HttpNotFound();
            }
            return View(telefonos_Tecnicos);
        }

        // GET: Telefonos_Tecnicos/Create
        public ActionResult Create()
        {
            ViewBag.Codigo_Tecnico = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre");
            return View();
        }

        // POST: Telefonos_Tecnicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo_Tecnico,Numero_Telefono")] Telefonos_Tecnicos telefonos_Tecnicos)
        {
            if (ModelState.IsValid)
            {
                db.Telefonos_Tecnicos.Add(telefonos_Tecnicos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Codigo_Tecnico = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", telefonos_Tecnicos.Codigo_Tecnico);
            return View(telefonos_Tecnicos);
        }

        // GET: Telefonos_Tecnicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos_Tecnicos telefonos_Tecnicos = db.Telefonos_Tecnicos.Find(id);
            if (telefonos_Tecnicos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Codigo_Tecnico = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", telefonos_Tecnicos.Codigo_Tecnico);
            return View(telefonos_Tecnicos);
        }

        // POST: Telefonos_Tecnicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo_Tecnico,Numero_Telefono")] Telefonos_Tecnicos telefonos_Tecnicos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telefonos_Tecnicos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Codigo_Tecnico = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", telefonos_Tecnicos.Codigo_Tecnico);
            return View(telefonos_Tecnicos);
        }

        // GET: Telefonos_Tecnicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos_Tecnicos telefonos_Tecnicos = db.Telefonos_Tecnicos.Find(id);
            if (telefonos_Tecnicos == null)
            {
                return HttpNotFound();
            }
            return View(telefonos_Tecnicos);
        }

        // POST: Telefonos_Tecnicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefonos_Tecnicos telefonos_Tecnicos = db.Telefonos_Tecnicos.Find(id);
            db.Telefonos_Tecnicos.Remove(telefonos_Tecnicos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
