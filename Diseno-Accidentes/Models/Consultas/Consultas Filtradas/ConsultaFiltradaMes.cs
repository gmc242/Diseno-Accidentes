using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaMes : ConsultaFiltrada<Fecha.Mes>
    {
        public ConsultaFiltradaMes(IConsultable<int> consulta, Fecha.Mes mes) : base(consulta, mes) { }

        public override String ObtenerHeader() { return consulta.ObtenerHeader(); }

        public override string ObtenerMiddle()
        {
            if (YaEnConsulta())
                return consulta.ObtenerMiddle();
            else
                return consulta.ObtenerMiddle() + " INNER JOIN Mes mes ON a.Mes = mes.ID";

        }

        public override string ObtenerFiltros()
        {
            String mes = valorAIgualar.ToString();

            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("mes.Nombre");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (mes.Nombre = '" + mes + "' OR " +
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
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " mes.Nombre = '" + mes + "'";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("mes.Nombre");
        }
    }
}