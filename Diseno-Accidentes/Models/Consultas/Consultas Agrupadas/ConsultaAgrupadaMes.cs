using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaMes : ConsultaAgrupada
    {
        public ConsultaAgrupadaMes() : base("mes.Nombre") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS mes";
        }

        public override string ObtenerMiddle()
        {
            return base.ObtenerMiddle() + " INNER JOIN Mes mes ON a.Mes = mes.ID";
        }
    }
}