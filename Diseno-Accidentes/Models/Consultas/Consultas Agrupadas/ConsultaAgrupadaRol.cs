using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaRol : ConsultaAgrupada
    {
        public ConsultaAgrupadaRol() : base("r.NombreRol") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS rol";
        }

        public override string ObtenerMiddle()
        {
            return "FROM Accidente a " +
                "INNER JOIN Persona p " +
                "ON a.Persona = p.ID " +
                "INNER JOIN Rol r " +
                "ON p.Rol = r.ID ";
        }
    }
}