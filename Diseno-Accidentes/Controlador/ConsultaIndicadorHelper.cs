using Diseno_Accidentes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Controlador
{
    public class ConsultaIndicadorHelper
    {
        private static Consulta consulta = new Consulta();

        public static Consulta GetConsulta() { return consulta;  }
        public static void SetConsulta(Consulta consulta) { ConsultaIndicadorHelper.consulta = consulta;  }

    }
}