using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaLesion : Consulta
    {
        public ConsultaAgrupadaLesion() : base("l.Descripcion") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS lesion";
        }

        public override string ObtenerMiddle()
        {
            return "FROM Accidente a " +
                "INNER JOIN Lesion l " +
                "ON a.Lesion = l.ID ";
        }
    }
}