using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaEdadQuinquenal : ConsultaAgrupada
    {
        public ConsultaAgrupadaEdadQuinquenal() : base("((p.Edad / 5) + 1)") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS edad_quinquenal";
        }

        public override string ObtenerMiddle()
        {
            return base.ObtenerMiddle() + " INNER JOIN Persona p ON a.Persona = p.ID";
        }
    }
}