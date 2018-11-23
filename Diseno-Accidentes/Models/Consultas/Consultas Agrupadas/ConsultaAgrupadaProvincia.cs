using Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas
{
    public class ConsultaAgrupadaProvincia : ConsultaAgrupada
    {
        public ConsultaAgrupadaProvincia() : base("pr.Nombre") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS provincia";
        }

        public override string ObtenerMiddle()
        {
            return "FROM Accidente a " +
                "INNER JOIN Distrito d " +
                "ON a.Distrito = d.ID " +
                "INNER JOIN Canton c " +
                "ON d.Canton = c.ID " +
                "INNER JOIN Provincia pr " +
                "ON c.Provincia = pr.ID";
        }
    }
}