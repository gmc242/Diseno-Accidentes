using Diseno_Accidentes.Models;
using Diseno_Accidentes.Models.Consultas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Controlador
{
    public static class ConsultaDinamicaHelper
    {
        private static IConsultable<int> consulta = new ConsultaAgrupadaProvincia();
        private static List<String> filtros = new List<string>() { "Sexo", "Lesion", "Rol", "Provincia" };
        private static List<String> filtrosActuales = new List<string>();

        public static void Reset()
        {
            consulta = new ConsultaAgrupadaProvincia();
            filtrosActuales = new List<string>();
        }

        public static IConsultable<int> GetConsulta() { return consulta; }
        public static void SetConsulta(IConsultable<int> consulta) { ConsultaDinamicaHelper.consulta = consulta; }

        public static List<String> GetFiltros() { return filtros; }

        public static List<String> GetFiltrosActuales() { return filtrosActuales; }
        public static void AddFiltro(String filtro) { filtrosActuales.Add(filtro); }
    }
}