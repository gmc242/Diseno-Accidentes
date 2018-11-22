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
            switch (filtro)
            {
                case "Sexo":
                    {
                        var value = Json(Enum.GetValues(typeof(Models.Persona.Sexo))
                        .Cast<Models.Persona.Sexo>()
                        .Select(e => e.ToString())
                        .ToList());
                        return value;
                    }
                case "Lesion":
                    {
                        var value = Json(Enum.GetValues(typeof(Models.Lesion.TipoLesion))
                        .Cast<Models.Lesion.TipoLesion>()
                        .Select(e => e.ToString().Replace("_", " "))
                        .ToList());
                        return value;
                    }
                case "Rol":
                    {
                        var value = Json(Enum.GetValues(typeof(Models.Rol.TipoRol))
                        .Cast<Models.Rol.TipoRol>()
                        .Select(e => e.ToString().Replace("_", " "))
                        .ToList());
                        return value;
                    }
                case "Provincia":
                    {
                        List<String> provincias = new List<string>()
                            { "San Jose", "Cartago", "Alajuela", "Heredia", "Puntarenas", "Guanacaste", "Limon" };

                        var value = Json(provincias);

                        return value;
                    }
                default:
                    return Json("");
            }
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
                switch (nombreFiltro)
                {
                    case "Sexo":
                        {
                            ConsultaDinamicaHelper.SetConsulta(new ConsultaFiltradaSexo(ConsultaDinamicaHelper.GetConsulta(),
                                (Persona.Sexo)Enum.Parse(typeof(Persona.Sexo), valorFiltroReal)));
                            break;
                        }
                    case "Lesion":
                        {
                            ConsultaDinamicaHelper.SetConsulta(new ConsultaFiltradaLesion(ConsultaDinamicaHelper.GetConsulta(),
                                (Lesion.TipoLesion)Enum.Parse(typeof(Lesion.TipoLesion), valorFiltroReal)));
                            break;
                        }
                    case "Rol":
                        {
                            ConsultaDinamicaHelper.SetConsulta(new ConsultaFiltradaRol(ConsultaDinamicaHelper.GetConsulta(),
                                (Rol.TipoRol)Enum.Parse(typeof(Rol.TipoRol), valorFiltroReal)));
                            break;
                        }
                    case "Provincia":
                        {
                            ConsultaDinamicaHelper.SetConsulta(new ConsultaFiltradaProvincia(ConsultaDinamicaHelper.GetConsulta(),
                                valorFiltroReal));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

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