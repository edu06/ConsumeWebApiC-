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
    public class ClientesController : Controller
    {
        private DBProyectoSkyNetEntities db = new DBProyectoSkyNetEntities();

        // Ver Clientes GET 
        string Baseurl = " https://aplicacionwebapirest100.azurewebsites.net";

        public async Task<ActionResult> Index()
        {
            List<Cliente> EmpInfo = new List<Cliente>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Llama a todos usuarios usando Http Client
                HttpResponseMessage Res = await client.GetAsync("api/Clientes");
                if (Res.IsSuccessStatusCode)
                {
                    //Si Res = true entra y asigna los datos
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializar el api almacena los datos
                    EmpInfo = JsonConvert.DeserializeObject<List<Cliente>>(EmpResponse);
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
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // Metodo POST  O Crear 
        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(" https://aplicacionwebapirest100.azurewebsites.net/api/Cliente");
                var postTask = client.PostAsJsonAsync<Cliente>("Clientes", cliente);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador");
            return View(cliente);
        }
        //Metodo PUT Para Editar el Cliente. 
        public ActionResult Edit(int id)
        {

            Cliente cliente = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");

                var responseTask = client.GetAsync("api/Clientes/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Cliente>();
                    readTask.Wait();
                    cliente = readTask.Result;
                }
            }
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://aplicacionwebapirest100.azurewebsites.net");
                //HTTP POST
                var putTask = client.PutAsJsonAsync($"api/CLientes/{cliente.Codigo_Cliente}", cliente);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(cliente);
        }


    }
}
