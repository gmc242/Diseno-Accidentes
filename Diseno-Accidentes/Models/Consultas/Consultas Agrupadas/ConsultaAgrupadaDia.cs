using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaDia : ConsultaAgrupada
    {
        public ConsultaAgrupadaDia() : base("dia.Nombre") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS dia";
        }

        public override string ObtenerMiddle()
        {
            return base.ObtenerMiddle() + " INNER JOIN Dia dia ON a.Dia = dia.ID";
        }
    }
}