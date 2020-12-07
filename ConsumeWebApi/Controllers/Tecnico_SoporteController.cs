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
    public class Tecnico_SoporteController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        string Baseurl = " https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Tecnico_Soporte> EmpInfo = new List<Tecnico_Soporte>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Tecnico_Soporte");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Tecnico_Soporte>>(EmpResponse);
                }

                var tecnico_Soporte = db.Tecnico_Soporte.Include(t => t.Departamento_Tecnico).Include(t => t.Perfiles);
                return View(tecnico_Soporte.ToList());
                   


                } }

                             

        // GET: Tecnico_Soporte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico_Soporte tecnico_Soporte = db.Tecnico_Soporte.Find(id);
            if (tecnico_Soporte == null)
            {
                return HttpNotFound();
            }
            return View(tecnico_Soporte);
        }

        // GET: Tecnico_Soporte/Create
        public ActionResult Create()
        {
            ViewBag.Codigo_Departamento_Tecnico = new SelectList(db.Departamento_Tecnico, "Codigo_Departamento_Tecnico", "Nombre");
            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil");
            return View();
        }

        // POST: Tecnico_Soporte/Create
                // POST TECNICO SOPORTE 
        [HttpPost]
        public ActionResult create(Tecnico_Soporte tecnico_Soporte)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net/api/Tecnico_Soporte");
                var postTask = client.PostAsJsonAsync<Tecnico_Soporte>("Tecnico_Soporte", tecnico_Soporte);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");

            ViewBag.Codigo_Departamento_Tecnico = new SelectList(db.Departamento_Tecnico, "Codigo_Departamento_Tecnico", "Nombre", tecnico_Soporte.Codigo_Departamento_Tecnico);
            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil", tecnico_Soporte.Codigo_Perfil);
            return View(tecnico_Soporte);
        }

        // Metodo PUT Para Editar un Registro. 

        //Modificar registro
        public ActionResult Edit(int id)
        {
            //MODELS
         Tecnico_Soporte tecnico_Soporte = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP GET
                var responseTask = client.GetAsync("api/Tecnico_Soporte/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Tecnico_Soporte>();
                    readTask.Wait();
                    tecnico_Soporte = readTask.Result;
                }
            }
            ViewBag.Codigo_Departamento_Tecnico = new SelectList(db.Departamento_Tecnico, "Codigo_Departamento_Tecnico", "Nombre", tecnico_Soporte.Codigo_Departamento_Tecnico);
            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil", tecnico_Soporte.Codigo_Perfil);



            return View(tecnico_Soporte);
        }

        [HttpPost]
        public ActionResult Edit(Tecnico_Soporte tecnico_Soporte)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Tecnico_Soporte/{tecnico_Soporte.Codigo_Tecnico}", tecnico_Soporte);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ViewBag.Codigo_Departamento_Tecnico = new SelectList(db.Departamento_Tecnico, "Codigo_Departamento_Tecnico", "Nombre", tecnico_Soporte.Codigo_Departamento_Tecnico);
            ViewBag.Codigo_Perfil = new SelectList(db.Perfiles, "Codigo_Perfil", "Descripcion_Perfil", tecnico_Soporte.Codigo_Perfil);
            return View(tecnico_Soporte);
                     
        }

        // GET: Tecnico_Soporte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tecnico_Soporte tecnico_Soporte = db.Tecnico_Soporte.Find(id);
            if (tecnico_Soporte == null)
            {
                return HttpNotFound();
            }
            return View(tecnico_Soporte);
        }

        // POST: Tecnico_Soporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tecnico_Soporte tecnico_Soporte = db.Tecnico_Soporte.Find(id);
            db.Tecnico_Soporte.Remove(tecnico_Soporte);
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
