using Diseno_Accidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Controlador
{
    public static class ConsultaIndicadorHelper
    {
        private static IConsultable<int> consulta = new Consulta();

        public static IConsultable<int> GetConsulta() { return consulta;  }
        public static void SetConsulta(IConsultable<int> consulta) { ConsultaIndicadorHelper.consulta = consulta;  }

        public static List<String> GetIndicadores()
        {
            return new List<String>() { "Sexo", "Lesion", "Rol", "Anio", "Dia",
                "Mes", "Edad", "Edad_Quinquenal", "Provincia" };
        }
    }
}