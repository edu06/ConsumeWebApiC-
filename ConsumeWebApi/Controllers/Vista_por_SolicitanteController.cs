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
    public class Vista_por_SolicitanteController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Vista_por_Solicitante
        public ActionResult Index()
        {
            return View(db.Vista_por_Solicitante.ToList());
        }

        // GET: Vista_por_Solicitante/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Solicitante vista_por_Solicitante = db.Vista_por_Solicitante.Find(id);
            if (vista_por_Solicitante == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Solicitante);
        }

        // GET: Vista_por_Solicitante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vista_por_Solicitante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre_Solicitante,Apellido_Solicitante,Usuario_tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion_Incidencia,Nombre_Receptor")] Vista_por_Solicitante vista_por_Solicitante)
        {
            if (ModelState.IsValid)
            {
                db.Vista_por_Solicitante.Add(vista_por_Solicitante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vista_por_Solicitante);
        }

        // GET: Vista_por_Solicitante/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Solicitante vista_por_Solicitante = db.Vista_por_Solicitante.Find(id);
            if (vista_por_Solicitante == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Solicitante);
        }

        // POST: Vista_por_Solicitante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nombre_Solicitante,Apellido_Solicitante,Usuario_tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion_Incidencia,Nombre_Receptor")] Vista_por_Solicitante vista_por_Solicitante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vista_por_Solicitante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vista_por_Solicitante);
        }

        // GET: Vista_por_Solicitante/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Solicitante vista_por_Solicitante = db.Vista_por_Solicitante.Find(id);
            if (vista_por_Solicitante == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Solicitante);
        }

        // POST: Vista_por_Solicitante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vista_por_Solicitante vista_por_Solicitante = db.Vista_por_Solicitante.Find(id);
            db.Vista_por_Solicitante.Remove(vista_por_Solicitante);
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
