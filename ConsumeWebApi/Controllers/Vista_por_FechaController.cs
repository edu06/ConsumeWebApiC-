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
    public class Vista_por_FechaController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Vista_por_Fecha
        public ActionResult Index()
        {
            return View(db.Vista_por_Fecha.ToList());
        }

        // GET: Vista_por_Fecha/Details/5
        public ActionResult Details(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Fecha vista_por_Fecha = db.Vista_por_Fecha.Find(id);
            if (vista_por_Fecha == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Fecha);
        }

        // GET: Vista_por_Fecha/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vista_por_Fecha/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Fecha_Creacion,Usuario_Tecnico,Titulo_Incidencia,Descripcion,Nombre_Cliente,Apellido_Cliente,Nombre_Receptor")] Vista_por_Fecha vista_por_Fecha)
        {
            if (ModelState.IsValid)
            {
                db.Vista_por_Fecha.Add(vista_por_Fecha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vista_por_Fecha);
        }

        // GET: Vista_por_Fecha/Edit/5
        public ActionResult Edit(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Fecha vista_por_Fecha = db.Vista_por_Fecha.Find(id);
            if (vista_por_Fecha == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Fecha);
        }

        // POST: Vista_por_Fecha/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Fecha_Creacion,Usuario_Tecnico,Titulo_Incidencia,Descripcion,Nombre_Cliente,Apellido_Cliente,Nombre_Receptor")] Vista_por_Fecha vista_por_Fecha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vista_por_Fecha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vista_por_Fecha);
        }

        // GET: Vista_por_Fecha/Delete/5
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Fecha vista_por_Fecha = db.Vista_por_Fecha.Find(id);
            if (vista_por_Fecha == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Fecha);
        }

        // POST: Vista_por_Fecha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Vista_por_Fecha vista_por_Fecha = db.Vista_por_Fecha.Find(id);
            db.Vista_por_Fecha.Remove(vista_por_Fecha);
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
