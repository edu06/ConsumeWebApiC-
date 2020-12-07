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
    public class Vista_por_DeptoTecnicoController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Vista_por_DeptoTecnico
        public ActionResult Index()
        {
            return View(db.Vista_por_DeptoTecnico.ToList());
        }

        // GET: Vista_por_DeptoTecnico/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_DeptoTecnico vista_por_DeptoTecnico = db.Vista_por_DeptoTecnico.Find(id);
            if (vista_por_DeptoTecnico == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_DeptoTecnico);
        }

        // GET: Vista_por_DeptoTecnico/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vista_por_DeptoTecnico/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Departamento_Tecnico,Usuario_Tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion,Nombre_Cliente,Apellido_Cliente,Nombre_Receptor")] Vista_por_DeptoTecnico vista_por_DeptoTecnico)
        {
            if (ModelState.IsValid)
            {
                db.Vista_por_DeptoTecnico.Add(vista_por_DeptoTecnico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vista_por_DeptoTecnico);
        }

        // GET: Vista_por_DeptoTecnico/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_DeptoTecnico vista_por_DeptoTecnico = db.Vista_por_DeptoTecnico.Find(id);
            if (vista_por_DeptoTecnico == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_DeptoTecnico);
        }

        // POST: Vista_por_DeptoTecnico/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Departamento_Tecnico,Usuario_Tecnico,Codigo_Incidencia,Titulo_Incidencia,Descripcion,Nombre_Cliente,Apellido_Cliente,Nombre_Receptor")] Vista_por_DeptoTecnico vista_por_DeptoTecnico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vista_por_DeptoTecnico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vista_por_DeptoTecnico);
        }

        // GET: Vista_por_DeptoTecnico/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vista_por_DeptoTecnico vista_por_DeptoTecnico = db.Vista_por_DeptoTecnico.Find(id);
            if (vista_por_DeptoTecnico == null)
            {
                return HttpNotFound();
            }
            return View(vista_por_DeptoTecnico);
        }

        // POST: Vista_por_DeptoTecnico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Vista_por_DeptoTecnico vista_por_DeptoTecnico = db.Vista_por_DeptoTecnico.Find(id);
            db.Vista_por_DeptoTecnico.Remove(vista_por_DeptoTecnico);
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
