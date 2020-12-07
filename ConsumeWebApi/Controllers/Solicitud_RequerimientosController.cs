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
    public class Solicitud_RequerimientosController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // GET: Solicitud_Requerimientos
        public ActionResult Index()
        {
            var solicitud_Requerimientos = db.Solicitud_Requerimientos.Include(s => s.Cliente).Include(s => s.Receptor_Solicitudes).Include(s => s.Tecnico_Soporte);
            return View(solicitud_Requerimientos.ToList());
        }

        // GET: Solicitud_Requerimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud_Requerimientos solicitud_Requerimientos = db.Solicitud_Requerimientos.Find(id);
            if (solicitud_Requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(solicitud_Requerimientos);
        }

        // GET: Solicitud_Requerimientos/Create
        public ActionResult Create()
        {
            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre");
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre");
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre");
            return View();
        }

        // POST: Solicitud_Requerimientos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Codigo_Requerimientos,Codigo_Receptor,Codigo_Solicitante,Titulo_Requerimiento,Descripcion,Adjuntos,Codigo_Tecnico_Asignado,Fecha_Creacion,Estado")] Solicitud_Requerimientos solicitud_Requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.Solicitud_Requerimientos.Add(solicitud_Requerimientos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre", solicitud_Requerimientos.Codigo_Solicitante);
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre", solicitud_Requerimientos.Codigo_Receptor);
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", solicitud_Requerimientos.Codigo_Tecnico_Asignado);
            return View(solicitud_Requerimientos);
        }

        // GET: Solicitud_Requerimientos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud_Requerimientos solicitud_Requerimientos = db.Solicitud_Requerimientos.Find(id);
            if (solicitud_Requerimientos == null)
            {
                return HttpNotFound();
            }
            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre", solicitud_Requerimientos.Codigo_Solicitante);
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre", solicitud_Requerimientos.Codigo_Receptor);
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", solicitud_Requerimientos.Codigo_Tecnico_Asignado);
            return View(solicitud_Requerimientos);
        }

        // POST: Solicitud_Requerimientos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Codigo_Requerimientos,Codigo_Receptor,Codigo_Solicitante,Titulo_Requerimiento,Descripcion,Adjuntos,Codigo_Tecnico_Asignado,Fecha_Creacion,Estado")] Solicitud_Requerimientos solicitud_Requerimientos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud_Requerimientos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre", solicitud_Requerimientos.Codigo_Solicitante);
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre", solicitud_Requerimientos.Codigo_Receptor);
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", solicitud_Requerimientos.Codigo_Tecnico_Asignado);
            return View(solicitud_Requerimientos);
        }

        // GET: Solicitud_Requerimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud_Requerimientos solicitud_Requerimientos = db.Solicitud_Requerimientos.Find(id);
            if (solicitud_Requerimientos == null)
            {
                return HttpNotFound();
            }
            return View(solicitud_Requerimientos);
        }

        // POST: Solicitud_Requerimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud_Requerimientos solicitud_Requerimientos = db.Solicitud_Requerimientos.Find(id);
            db.Solicitud_Requerimientos.Remove(solicitud_Requerimientos);
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
