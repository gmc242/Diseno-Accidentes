using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaDistrito : ConsultaFiltrada<String>
    {
        public ConsultaFiltradaDistrito(IConsultable<int> consultaP, String distrito) : base(consultaP, distrito) { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public override string ObtenerMiddle()
        {
            if (consulta.ObtenerMiddle().Contains("Distrito d"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                return consulta.ObtenerMiddle() + " INNER JOIN Distrito d ON a.Distrito = d.ID";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        public override string ObtenerFiltros()
        {
            String distrito_str = valorAIgualar.ToString().Replace("_", " ");

            // Si los filtros ya contienen algún filtro del mismo tipo, debe usar un or en una posición inteligente
            if (YaEnConsulta())
            {

                int indexSF = consulta.ObtenerFiltros().IndexOf("d.Nombre");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (d.Nombre = '" + distrito_str + "' OR " +
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
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " d.Nombre = '" + distrito_str + "'";
            }
        }


        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("d.Nombre");
        }
    }
}