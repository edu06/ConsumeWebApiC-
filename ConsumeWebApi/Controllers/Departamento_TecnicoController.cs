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
    public class Departamento_TecnicoController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

            //URL de ubicación de API
        string Baseurl = "https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Departamento_Tecnico> EmpInfo = new List<Departamento_Tecnico>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Departamento_Tecnico");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Departamento_Tecnico>>(EmpResponse);
                }

                //Muestra la lista de todos los usuarios
                return View(EmpInfo); 
            }
        }

        // GET: Departamento_Tecnico/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento_Tecnico departamento_Tecnico = db.Departamento_Tecnico.Find(id);
            if (departamento_Tecnico == null)
            {
                return HttpNotFound();
            }
            return View(departamento_Tecnico);
        }

        // POST Departamento_Tecnico/Create
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Departamento_Tecnico departamento_Tecnico)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net/api/Departamento_Tecnico");
                var postTask = client.PostAsJsonAsync<Departamento_Tecnico>("Departamento_Tecnico", departamento_Tecnico);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");
            return View(departamento_Tecnico);
        }

        //Metodo PUT Para Editar Departamento Tecnico 
        public ActionResult Edit(int id)
        {

            Departamento_Tecnico departamento_Tecnico = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");

                var responseTask = client.GetAsync("api/Departamento_Tecnico/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Departamento_Tecnico>();
                    readTask.Wait();
                    departamento_Tecnico = readTask.Result;
                }
            }
            return View(departamento_Tecnico);
        }

        [HttpPost]
        public ActionResult Edit(Departamento_Tecnico departamento_Tecnico)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Departamento_Tecnico/{departamento_Tecnico.Codigo_Departamento_Tecnico}", departamento_Tecnico);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(departamento_Tecnico);
        }


        // GET: Departamento_Tecnico/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento_Tecnico departamento_Tecnico = db.Departamento_Tecnico.Find(id);
            if (departamento_Tecnico == null)
            {
                return HttpNotFound();
            }
            return View(departamento_Tecnico);
        }

        // POST: Departamento_Tecnico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departamento_Tecnico departamento_Tecnico = db.Departamento_Tecnico.Find(id);
            db.Departamento_Tecnico.Remove(departamento_Tecnico);
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
