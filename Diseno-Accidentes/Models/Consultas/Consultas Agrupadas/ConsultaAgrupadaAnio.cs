using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaAnio : Consulta
    {

        public ConsultaAgrupadaAnio() : base("an.Numero") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS anio";
        }

        public override string ObtenerMiddle()
        {
            return "FROM Accidente a " +
                "INNER JOIN Anio an " +
                "ON a.Anio = an.ID ";
        }

    }
}