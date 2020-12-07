using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ConsumeWebApi.Models;
using Newtonsoft.Json;

namespace ConsumeWebApi.Controllers
{
    public class IncidenciasController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        string Baseurl = " https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Incidencias> EmpInfo = new List<Incidencias>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Incidencias");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Incidencias>>(EmpResponse);
                }


                var incidencias = db.Incidencias.Include(i => i.Cliente).Include(i => i.Estados).Include(i => i.Receptor_Solicitudes).Include(i => i.Tecnico_Soporte);
                return View(incidencias.ToList());



            }
        }

        // GET: Incidencias/Detalles
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencias incidencias = db.Incidencias.Find(id);
            if (incidencias == null)
            {
                return HttpNotFound();
            }
            return View(incidencias);
        }

      
        public ActionResult Create()
        {
            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre");
            ViewBag.Estado = new SelectList(db.Estados, "Codigo_Estado", "Descripcion_Estado");
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre");
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre");
            return View();
        }
        // Metodo POST Incidencias 
        [HttpPost]
        public ActionResult create(Incidencias incidencias)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net/api/Incidencias");
                var postTask = client.PostAsJsonAsync<Incidencias>("Receptor_Solicitudes", incidencias);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");


            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre", incidencias.Codigo_Solicitante);
            ViewBag.Estado = new SelectList(db.Estados, "Codigo_Estado", "Descripcion_Estado", incidencias.Estado);
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre", incidencias.Codigo_Receptor);
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", incidencias.Codigo_Tecnico_Asignado);
            return View(incidencias);
        }

        // Metodo PUT Incidencias 
        public ActionResult Edit(int id)
        {
         
            Incidencias incidencias = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");
               
                var responseTask = client.GetAsync("api/Incidencias/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Incidencias>();
                    readTask.Wait();
                    incidencias = readTask.Result;
                }
            }
            ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre", incidencias.Codigo_Solicitante);
            ViewBag.Estado = new SelectList(db.Estados, "Codigo_Estado", "Descripcion_Estado", incidencias.Estado);
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre", incidencias.Codigo_Receptor);
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", incidencias.Codigo_Tecnico_Asignado);
            return View(incidencias);
        }


        [HttpPost]
        public ActionResult Edit(Incidencias incidencias)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Incidencias/{incidencias.Codigo_Incidencia}", incidencias);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
          
        
        ViewBag.Codigo_Solicitante = new SelectList(db.Cliente, "Codigo_Cliente", "Primer_Nombre", incidencias.Codigo_Solicitante);
            ViewBag.Estado = new SelectList(db.Estados, "Codigo_Estado", "Descripcion_Estado", incidencias.Estado);
            ViewBag.Codigo_Receptor = new SelectList(db.Receptor_Solicitudes, "Codigo_Receptor", "Primer_Nombre", incidencias.Codigo_Receptor);
            ViewBag.Codigo_Tecnico_Asignado = new SelectList(db.Tecnico_Soporte, "Codigo_Tecnico", "Primer_Nombre", incidencias.Codigo_Tecnico_Asignado);
            return View(incidencias);
        }

        // GET: Incidencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incidencias incidencias = db.Incidencias.Find(id);
            if (incidencias == null)
            {
                return HttpNotFound();
            }
            return View(incidencias);
        }

        // POST: Incidencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incidencias incidencias = db.Incidencias.Find(id);
            db.Incidencias.Remove(incidencias);
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
