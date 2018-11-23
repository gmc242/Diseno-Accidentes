using Diseno_Accidentes.Controlador;
using Diseno_Accidentes.Models;
using Diseno_Accidentes.Models.Consultas;
using Diseno_Accidentes.Models.Consultas.Consultas_Filtradas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Diseno_Accidentes.Controllers
{
    public class ConsultaDinamicaController : Controller
    {
        // GET: ConsultaLibre
        public ActionResult Index()
        {

            ViewData["filtros"] = ConsultaDinamicaHelper.GetFiltros();
            ViewData["filtrosUsados"] = ConsultaDinamicaHelper.GetFiltrosActuales();

            return View("Index");
        }

        // POST: ObtenerDatosFiltro
        [HttpPost]
        public JsonResult ObtenerDatosFiltro(Object seleccion)
        {
            String filtro = ((String[])seleccion)[0];

            List<String> valores = FuncionalidadesExtra.ObtenerValores(filtro);

            return Json(valores);

        }

        [HttpPost]
        public ActionResult AnadirFiltro(FormCollection form)
        {
            String nombreFiltro = form["filtro"];
            String valorFiltro = form["valorFiltro"];
            String valorFiltroReal = valorFiltro.Replace(" ", "_");

            String filtro = nombreFiltro + " = " + valorFiltro;

            // Maneja cuando el filtro ya existe
            if(!ConsultaDinamicaHelper.GetFiltrosActuales().Contains(filtro))
            {
                IConsultable<int> consulta = FuncionalidadesExtra.ObtenerConsultaFiltrada(
                    nombreFiltro, ConsultaDinamicaHelper.GetConsulta(), valorFiltroReal);

                ConsultaDinamicaHelper.SetConsulta(consulta);

                ConsultaDinamicaHelper.AddFiltro(filtro);
                
            }

            ViewData["filtros"] = ConsultaDinamicaHelper.GetFiltros();
            ViewData["filtrosUsados"] = ConsultaDinamicaHelper.GetFiltrosActuales();

            return View("Index");
        }

        [HttpPost]
        public JsonResult RealizarConsulta(FormCollection form)
        {
            var anioInicial = form["anioInicial"];
            var anioFinal = form["anioFinal"];

            // Parsea los años obtenidos
            KeyValuePair<int, int> anios = new KeyValuePair<int, int>(int.Parse(anioInicial), int.Parse(anioFinal));

            // Obtiene un objeto consultable
            IConsultable<int> consulta = new ConsultaFiltradaAnios(ConsultaDinamicaHelper.GetConsulta(), anios);

            // Devuelve los resultados de aplicar la consulta
            return Json(consulta.AplicarConsulta());
        }

        public ActionResult ResetFiltros()
        {
            ConsultaDinamicaHelper.Reset();
            return Index();
        }
    }
}