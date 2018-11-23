using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Filtradas
{
    public class ConsultaFiltradaEdadQuinquenal : ConsultaFiltrada<int>
    {
        public ConsultaFiltradaEdadQuinquenal(IConsultable<int> consultaP, int edad) : base(consultaP, edad) { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerConsulta();
        }

        public override string ObtenerMiddle()
        {
            if (consulta.ObtenerMiddle().Contains("Persona p"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                return consulta.ObtenerMiddle() + " INNER JOIN Persona p ON a.Persona = p.ID";
            }
        }

        public override string ObtenerFiltros()
        {
            String edad_str = valorAIgualar.ToString();

            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("((p.Edad / 5) + 1)");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (((p.Edad / 5) + 1) = " + edad_str + " OR " +
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
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " ((p.Edad/5)+1) = " + edad_str;
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("((p.Edad/5)+1)");
        }
    }
}