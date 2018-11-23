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
    public class ConsultaIndicadorController : Controller
    {
        // GET: ConsultaPredeterminada
        public ActionResult Index()
        {
            ViewData["indicadores"] = ConsultaIndicadorHelper.GetIndicadores();
            return View();
        }

        // POST
        [HttpPost]
        public JsonResult RealizarConsulta(Object indicador)
        {
            string indicador_str = ((String[])indicador)[0];

            IConsultable<int> consulta = FactoryConsulta.ObtenerConsultaAgrupada(indicador_str);
            ConsultaIndicadorHelper.SetConsulta(consulta);

            return Json(ConsultaIndicadorHelper.GetConsulta().AplicarConsulta());

        }
    }
}