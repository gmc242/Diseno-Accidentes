using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaEdad : ConsultaAgrupada
    {
        public ConsultaAgrupadaEdad() : base("p.Edad") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS edad";
        }

        public override string ObtenerMiddle()
        {
            return base.ObtenerMiddle() + " INNER JOIN Persona p ON a.Persona = p.ID";
        }
    }
}