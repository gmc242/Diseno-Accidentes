using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaProvincia : ConsultaFiltrada<String>
    {

        public ConsultaFiltradaProvincia(IConsultable<int> consultaP, String provincia) : base(consultaP, provincia) { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public override string ObtenerMiddle()
        {
            if (consulta.ObtenerMiddle().Contains("Provincia pr"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                // Si el distrito y el cantón ya existen como parte de los "INNER JOIN" se deben omitir
                String middleDistrito = (consulta.ObtenerMiddle().Contains("Distrito d")) ? "" 
                    : " INNER JOIN Distrito d ON a.Distrito = d.ID";
                String middleCanton = (consulta.ObtenerMiddle().Contains("Canton c")) ? ""
                    : " INNER JOIN Canton c ON d.Canton = c.ID";

                return consulta.ObtenerMiddle() + middleDistrito + 
                    middleCanton + " INNER JOIN Provincia pr ON c.Provincia = pr.ID";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        public override string ObtenerFiltros()
        {
            String provincia_str = valorAIgualar.ToString().Replace("_", " ");

            // Si los filtros ya contienen algún filtro del mismo tipo, debe usar un or en una posición inteligente
            if (YaEnConsulta())
            {
                // Obtiene los indices de el filtro del mismo tipo
                int indexSF = consulta.ObtenerFiltros().IndexOf("pr.Nombre");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexFF;

                // Construye el nuevo filtro con un or
                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (pr.Nombre = '" + provincia_str + "' OR " +
                    consulta.ObtenerFiltros().Substring(indexSF, (indexFF - indexSF)) + ")"
                    + consulta.ObtenerFiltros().Substring(indexFF);

                return filtros;
            }
            else
            {
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " pr.Nombre = '" + provincia_str + "'";
            }
        }


        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("pr.Nombre");
        }
    }

}
