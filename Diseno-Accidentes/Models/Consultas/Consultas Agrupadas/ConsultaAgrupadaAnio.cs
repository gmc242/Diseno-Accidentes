using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaAnio : ConsultaAgrupada
    {

        public ConsultaAgrupadaAnio() : base("an.Numero") { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader() + " AS anio";
        }

        public override string ObtenerMiddle()
        {
            return consulta.ObtenerMiddle() +
                " INNER JOIN Anio an " +
                "ON a.Anio = an.ID ";
        }

    }
}