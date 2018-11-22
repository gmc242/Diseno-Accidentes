using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    public class ConsultaAgrupadaSexo : Consulta
    {

        public ConsultaAgrupadaSexo() : base("s.Nombre") { }

        public override string ObtenerHeader()
        {
            return base.ObtenerHeader() + " AS sexo";
        }

        public override string ObtenerMiddle()
        {
            return "FROM Accidente a " +
                "INNER JOIN Persona p ON a.Persona = p.ID " +
                "INNER JOIN Sexo s ON p.Sexo = s.ID";
        }

    }
}