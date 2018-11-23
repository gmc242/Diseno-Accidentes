using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaCanton : ConsultaFiltrada<String>
    {
        public ConsultaFiltradaCanton(IConsultable<int> consultaP, String canton) : base(consultaP, canton) { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public override string ObtenerMiddle()
        {
            if (consulta.ObtenerMiddle().Contains("Canton c"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                // Si el distrito y el cantón ya existen como parte de los "INNER JOIN" se deben omitir
                String middleDistrito = (consulta.ObtenerMiddle().Contains("Distrito d")) ? ""
                    : " INNER JOIN Distrito d ON a.Distrito = d.ID";

                return consulta.ObtenerMiddle() + middleDistrito + " INNER JOIN Canton c ON d.Canton = c.ID";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        public override string ObtenerFiltros()
        {
            String canton_str = valorAIgualar.ToString().Replace("_", " ");

            // Si los filtros ya contienen algún filtro del mismo tipo, debe usar un or en una posición inteligente
            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("c.Nombre");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (c.Nombre = '" + canton_str + "' OR " +
                    consulta.ObtenerFiltros().Substring(indexSF, (indexFF - indexSF)) + ")";

                try
                {
                    filtros += " " + consulta.ObtenerFiltros().Substring(indexFF);
                    return filtros;
                }
                catch (Exception e)
                {
                    return filtros;
                }
                
            }
            else
            {
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " c.Nombre = '" + canton_str + "'";
            }
        }


        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("c.Nombre");
        }
    }
}