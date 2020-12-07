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
    public class EstadosController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        //URL de ubicación de API
        string Baseurl = "https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Estados> EmpInfo = new List<Estados>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Estados");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Estados>>(EmpResponse);
                }

                //Muestra la lista de todos los usuarios
                return View(EmpInfo);
            }
        }
        // GET Estados por ID Detalle 
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return View(estados);
        }

        // Metodo POST  O Crear 
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Estados estados)
        {
            using (var est = new HttpClient())
            {
                est.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net/api/Estados");
                var postTask = est.PostAsJsonAsync<Estados>("Estados", estados);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");
            return View(estados);
        }

        //Metodo PUT Para Editar el Cliente. 
        public ActionResult Edit(int id)
        {

            Estados estados = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");

                var responseTask = client.GetAsync("api/Estados/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Estados>();
                    readTask.Wait();
                    estados = readTask.Result;
                }
            }
            return View(estados);
        }

        [HttpPost]
        public ActionResult Edit(Estados estados)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Estados/{estados.Codigo_Estado}", estados);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(estados);
        }


        // GET: Estados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estados estados = db.Estados.Find(id);
            if (estados == null)
            {
                return HttpNotFound();
            }
            return View(estados);
        }

        // POST: Estados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estados estados = db.Estados.Find(id);
            db.Estados.Remove(estados);
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
