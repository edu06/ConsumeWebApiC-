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
    public class Telefonos_ClientesController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        //URL de ubicación de API
        string Baseurl = "https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Telefonos_Clientes> EmpInfo = new List<Telefonos_Clientes>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Telefonos_Clientes");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Telefonos_Clientes>>(EmpResponse);
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
            Telefonos_Clientes telefonos_Clientes = db.Telefonos_Clientes.Find(id);
            if (telefonos_Clientes == null)
            {
                return HttpNotFound();
            }
            return View(telefonos_Clientes);
        }

        // Metodo POST  O Crear 
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Telefonos_Clientes telefonos_Clientes)
        {
            using (var est = new HttpClient())
            {
                est.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net/api/Telefono_Clientes");
                var postTask = est.PostAsJsonAsync<Telefonos_Clientes>("Telefonos_Clientes", telefonos_Clientes);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");
            return View(telefonos_Clientes);
        }

        //Metodo PUT Para Editar el Cliente. 
        public ActionResult Edit(int id)
        {

          Telefonos_Clientes telefonos_Clientes = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");

                var responseTask = client.GetAsync("api/Telefonos_Clientes/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Telefonos_Clientes>();
                    readTask.Wait();
                    telefonos_Clientes = readTask.Result;
                }
            }
            return View(telefonos_Clientes);
        }

        [HttpPost]
        public ActionResult Edit(Telefonos_Clientes telefonos_Clientes)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/Telefonos_Clientes/{telefonos_Clientes.Codigo_Cliente}", telefonos_Clientes);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(telefonos_Clientes);
        }


        // DETLETE TELEFONOS 
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Telefonos_Clientes telefonos_Clientes = db.Telefonos_Clientes.Find(id);
            if (telefonos_Clientes == null)
            {
                return HttpNotFound();
            }
            return View(telefonos_Clientes);
        }

        // POST: Telefonos_Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Telefonos_Clientes telefonos_Clientes = db.Telefonos_Clientes.Find(id);
            db.Telefonos_Clientes.Remove(telefonos_Clientes);
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
