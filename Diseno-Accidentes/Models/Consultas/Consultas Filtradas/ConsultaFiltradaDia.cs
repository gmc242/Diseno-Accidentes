using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaDia : ConsultaFiltrada<Fecha.Dia>
    {
        public ConsultaFiltradaDia(IConsultable<int> consulta, Fecha.Dia dia) : base(consulta, dia) { }

        public override String ObtenerHeader() { return consulta.ObtenerHeader(); }

        public override string ObtenerMiddle()
        {
            if (YaEnConsulta())
                return consulta.ObtenerMiddle();
            else
                return consulta.ObtenerMiddle() + " INNER JOIN Dia dia ON a.Dia = dia.ID";

        }

        public override string ObtenerFiltros()
        {
            String dia = valorAIgualar.ToString();

            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("dia.Nombre");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (dia.Nombre = '" + dia + "' OR " +
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
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " dia.Nombre = '" + dia + "'";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("dia.Nombre");
        }
    }
}