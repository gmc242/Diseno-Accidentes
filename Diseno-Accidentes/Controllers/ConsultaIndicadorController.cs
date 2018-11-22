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
            return View();
        }

        // POST
        [HttpPost]
        public JsonResult RealizarConsulta(Object indicador)
        {
            string indicador_str = ((String[])indicador)[0];
            switch (indicador_str)
            {
                case "sexo":
                    {
                        ConsultaIndicadorHelper.SetConsulta(new ConsultaAgrupadaSexo());
                        break;
                    }
                case "lesion":
                    {
                        ConsultaIndicadorHelper.SetConsulta(new ConsultaAgrupadaLesion());
                        break;
                    }
                case "rol":
                    {
                        ConsultaIndicadorHelper.SetConsulta(new ConsultaAgrupadaRol());
                        break;
                    }
                case "provincia":
                    {
                        ConsultaIndicadorHelper.SetConsulta(new ConsultaAgrupadaProvincia());
                        break;
                    }
                default:
                    return Json("");
            }
            return Json(ConsultaIndicadorHelper.GetConsulta().AplicarConsulta());
        }
    }
}