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
    public class Receptor_SolicitudesController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        string Baseurl = " https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Receptor_Solicitudes> EmpInfo = new List<Receptor_Solicitudes>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Receptor_Solicitudes");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Receptor_Solicitudes>>(EmpResponse);
                }


                var receptor_Solicitudes = db.Receptor_Solicitudes.Include(r => r.Perfiles);
                return View(receptor_Solicitudes.ToList());



            }
        }

        // GET: Receptor_Solicitudes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptor_Solicitudes receptor_Solicitudes = db.Receptor_Solicitudes.Find(id);
            if (receptor_Solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(receptor_Solicitudes);
        }
        // GET: Receptor_Solicitudes1/Create
        public ActionResult Create()
        {
            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil");
            return View();
        }


        // Metodo POST Receptor Solicitudes. 
        [HttpPost]
        public ActionResult create(Receptor_Solicitudes receptor_Solicitudes)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net/api/Receptor_Solicitudes");
                var postTask = client.PostAsJsonAsync<Receptor_Solicitudes>("Receptor_Solicitudes",receptor_Solicitudes);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");

            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil", receptor_Solicitudes.Codigo_Perfil);
            return View(receptor_Solicitudes);

        }

        // Metodo PUT Receptor de SOlicitudes 
        public ActionResult Edit(int id)
        {
            //MODELS
            Receptor_Solicitudes receptor_Solicitudes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP GET
                var responseTask = client.GetAsync("api/Receptor_Solicitudes/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Receptor_Solicitudes>();
                    readTask.Wait();
                    receptor_Solicitudes = readTask.Result;
                }
            }
            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil", receptor_Solicitudes.Codigo_Perfil);
            return View(receptor_Solicitudes);

            
        }

        [HttpPost]
        public ActionResult Edit(Receptor_Solicitudes receptor_Solicitudes)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Receptor_Solicitudes/{receptor_Solicitudes.Codigo_Receptor}",receptor_Solicitudes);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                return View(receptor_Solicitudes);
            }
 
        }

               
        // GET: Receptor_Solicitudes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Receptor_Solicitudes receptor_Solicitudes = db.Receptor_Solicitudes.Find(id);
            if (receptor_Solicitudes == null)
            {
                return HttpNotFound();
            }
            return View(receptor_Solicitudes);
        }

        // POST: Receptor_Solicitudes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Receptor_Solicitudes receptor_Solicitudes = db.Receptor_Solicitudes.Find(id);
            db.Receptor_Solicitudes.Remove(receptor_Solicitudes);
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
