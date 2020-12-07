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
    public class Vista_por_TecnicoController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Vista_por_Tecnico
        public ActionResult Index()
        {
            return View(db.Vista_por_Tecnico.ToList());
        }

        // GET: Vista_por_Tecnico/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Tecnico vista_por_Tecnico = db.Vista_por_Tecnico.Find(id);
            if (vista_por_Tecnico == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Tecnico);
        }

        // GET: Vista_por_Tecnico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vista_por_Tecnico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre_Tecnico,Apellido_Tecnico,Usuario_Tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion,Nombre_Receptor")] Vista_por_Tecnico vista_por_Tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Vista_por_Tecnico.Add(vista_por_Tecnico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vista_por_Tecnico);
        }

        // GET: Vista_por_Tecnico/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Tecnico vista_por_Tecnico = db.Vista_por_Tecnico.Find(id);
            if (vista_por_Tecnico == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Tecnico);
        }

        // POST: Vista_por_Tecnico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nombre_Tecnico,Apellido_Tecnico,Usuario_Tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion,Nombre_Receptor")] Vista_por_Tecnico vista_por_Tecnico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vista_por_Tecnico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vista_por_Tecnico);
        }

        // GET: Vista_por_Tecnico/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Tecnico vista_por_Tecnico = db.Vista_por_Tecnico.Find(id);
            if (vista_por_Tecnico == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Tecnico);
        }

        // POST: Vista_por_Tecnico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vista_por_Tecnico vista_por_Tecnico = db.Vista_por_Tecnico.Find(id);
            db.Vista_por_Tecnico.Remove(vista_por_Tecnico);
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
