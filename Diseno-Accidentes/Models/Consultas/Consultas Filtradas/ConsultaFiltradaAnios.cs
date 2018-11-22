using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas
{
    public class ConsultaFiltradaAnios : ConsultaFiltrada<KeyValuePair<int, int>>
    {
        public ConsultaFiltradaAnios(IConsultable<int> consulta, KeyValuePair<int, int> anios) : base(consulta, anios) { }

        public override String ObtenerHeader() { return consulta.ObtenerHeader(); }

        public override string ObtenerMiddle() {
            if(YaEnConsulta())
                return consulta.ObtenerMiddle();
            else
                return consulta.ObtenerMiddle() + " INNER JOIN Anio an ON a.Anio = an.ID";
            
        }

        public override string ObtenerFiltros()
        {
            if (YaEnConsulta())
            {
                return consulta.ObtenerFiltros();
            }
            else
            {
                int anioInicial = valorAIgualar.Key;
                int anioFinal = valorAIgualar.Value;

                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " an.Numero BETWEEN " + anioInicial.ToString() +
                    " AND " + anioFinal.ToString();
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("Anio =");
        }
    }
}