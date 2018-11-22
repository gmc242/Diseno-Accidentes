using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas
{
    public class ConsultaFiltradaSexo : ConsultaFiltrada<Models.Persona.Sexo>
    {
        public ConsultaFiltradaSexo(IConsultable<int> consultaP, Persona.Sexo sexo) : base(consultaP, sexo) {}

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public override string ObtenerMiddle()
        {
            if (consulta.ObtenerMiddle().Contains("Sexo s"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                String temp = (consulta.ObtenerMiddle().Contains("Persona p")) ? "" : " INNER JOIN Persona p ON a.Persona = p.ID";
                return consulta.ObtenerMiddle() + temp + " INNER JOIN Sexo s ON p.Sexo = s.ID";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        public override string ObtenerFiltros()
        {
            String sexo_str = valorAIgualar.ToString().Replace("_", " ");

            // Si los filtros ya contienen algún filtro del mismo tipo, debe usar un or en una posición inteligente
            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("s.Nombre");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexFF;
                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (s.Nombre = '" + sexo_str + "' OR " +
                    consulta.ObtenerFiltros().Substring(indexSF, (indexFF - indexSF)) + ")"
                    + consulta.ObtenerFiltros().Substring(indexFF);

                return filtros;
            }
            else { return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " s.Nombre = '" + sexo_str + "'"; }
            
        }

        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("s.Nombre");
        }


    }
}