using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaLesion : ConsultaFiltrada<Lesion.TipoLesion>
    {
        public ConsultaFiltradaLesion(IConsultable<int> consultaP, Lesion.TipoLesion lesion) : base(consultaP, lesion) { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public override string ObtenerMiddle()
        {
            if (consulta.ObtenerMiddle().Contains("Lesion l"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                return consulta.ObtenerMiddle() + " INNER JOIN Lesion l ON a.Lesion = l.ID";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        public override string ObtenerFiltros()
        {
            String lesion_str = valorAIgualar.ToString().Replace("_", " ");

            // Si los filtros ya contienen algún filtro del mismo tipo, debe usar un or en una posición inteligente
            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("l.Descripcion");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (l.Descripcion= '" + lesion_str + "' OR " +
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
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " l.Descripcion = '" + lesion_str + "'";
            }
        }


        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("l.Descripcion");
        }
    }
}