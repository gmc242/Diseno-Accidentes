using Diseno_Accidentes.Controlador;
using Diseno_Accidentes.Models;
using Diseno_Accidentes.Models.Consultas;
using Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diseno_Accidentes.Controllers
{
    public class ConsultaObserverController : Controller
    {
        // GET: ConsultaObserver
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult RealizarConsulta(String[] indicadores)
        {
            
            List<List<KeyValuePair<String, int>>> res_consultas = new List<List<KeyValuePair<String, int>>>();

            foreach (String indicador in indicadores)
            {

                IConsultable<int> consulta = FactoryConsulta.ObtenerConsultaAgrupada(indicador);
                res_consultas.Add(consulta.AplicarConsulta());

            }

            return Json(res_consultas);
        }
    }
}