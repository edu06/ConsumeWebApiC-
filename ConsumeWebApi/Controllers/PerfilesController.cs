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
    public class PerfilesController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // Ver Clientes GET 
        string Baseurl = " https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Perfiles> EmpInfo = new List<Perfiles>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Perfiles");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Perfiles>>(EmpResponse);
                }

                //Muestra la lista de todos los usuarios
                return View(EmpInfo);
            }
        }

        //GET: por ID o Detalle de Cliente. 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           Perfiles perf = db.Perfiles.Find(id);
            if (perf == null)
            {
                return HttpNotFound();
            }
            return View(perf);
        }

        // Metodo POST  O Crear 
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Perfiles perfiles)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net/api/Perfiles");
                var postTask = client.PostAsJsonAsync<Perfiles>("Perfiles", perfiles);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");
            return View(perfiles);
        }
        //Metodo PUT Para Editar el Cliente. 
        public ActionResult Edit(int id)
        {

            Perfiles perfiles = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");

                var responseTask = client.GetAsync("api/Perfiles/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Perfiles>();
                    readTask.Wait();
                    perfiles = readTask.Result;
                }
            }
            return View(perfiles);
        }

        [HttpPost]
        public ActionResult Edit(Perfiles perfiles)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Perfiles/{perfiles.Codigo_Perfil}", perfiles);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(perfiles);
        }



// GET: Perfiles/Delete/5
public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Perfiles perfiles = db.Perfiles.Find(id);
            if (perfiles == null)
            {
                return HttpNotFound();
            }
            return View(perfiles);
        }

        // POST: Perfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Perfiles perfiles = db.Perfiles.Find(id);
            db.Perfiles.Remove(perfiles);
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
