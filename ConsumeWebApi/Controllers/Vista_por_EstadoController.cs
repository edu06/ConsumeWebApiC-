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
    public class Vista_por_EstadoController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Vista_por_Estado
        public ActionResult Index()
        {
            return View(db.Vista_por_Estado.ToList());
        }

        // GET: Vista_por_Estado/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Estado vista_por_Estado = db.Vista_por_Estado.Find(id);
            if (vista_por_Estado == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Estado);
        }

        // GET: Vista_por_Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vista_por_Estado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Estado,Usuario_Tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion,Nombre_Cliente,Apellido_Cliente,Nombre_Receptor")] Vista_por_Estado vista_por_Estado)
        {
            if (ModelState.IsValid)
            {
                db.Vista_por_Estado.Add(vista_por_Estado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vista_por_Estado);
        }

        // GET: Vista_por_Estado/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Estado vista_por_Estado = db.Vista_por_Estado.Find(id);
            if (vista_por_Estado == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Estado);
        }

        // POST: Vista_por_Estado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Estado,Usuario_Tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion,Nombre_Cliente,Apellido_Cliente,Nombre_Receptor")] Vista_por_Estado vista_por_Estado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vista_por_Estado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vista_por_Estado);
        }

        // GET: Vista_por_Estado/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_Estado vista_por_Estado = db.Vista_por_Estado.Find(id);
            if (vista_por_Estado == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_Estado);
        }

        // POST: Vista_por_Estado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vista_por_Estado vista_por_Estado = db.Vista_por_Estado.Find(id);
            db.Vista_por_Estado.Remove(vista_por_Estado);
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
