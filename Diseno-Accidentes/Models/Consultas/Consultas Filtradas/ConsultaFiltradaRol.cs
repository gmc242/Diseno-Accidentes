using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas
{
    public class ConsultaFiltradaRol : ConsultaFiltrada<Rol.TipoRol>
    {
        public ConsultaFiltradaRol(IConsultable<int> consultaP, Rol.TipoRol rol) : base(consultaP, rol) { }

        public override string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public override string ObtenerMiddle()
        {
            if(consulta.ObtenerMiddle().Contains("Rol r"))
            {
                return consulta.ObtenerMiddle();
            }
            else
            {
                String temp = (consulta.ObtenerMiddle().Contains("Persona p")) ? "" : " INNER JOIN Persona p ON a.Persona = p.ID";
                return consulta.ObtenerMiddle() + temp + " INNER JOIN Rol r ON p.Rol = r.ID";
            }
        }

        public override string ObtenerFooter()
        {
            return consulta.ObtenerFooter();
        }

        public override string ObtenerFiltros()
        {
            String rol_str = valorAIgualar.ToString().Replace("_", " ");



            // Si los filtros ya contienen algún filtro del mismo tipo, debe usar un or en una posición inteligente
            if (YaEnConsulta())
            {
                int indexSF = consulta.ObtenerFiltros().IndexOf("r.NombreRol");
                int indexFF = consulta.ObtenerFiltros().Substring(indexSF).IndexOf("AND");
                indexFF = (indexFF == -1) ? consulta.ObtenerFiltros().Length : indexSF + indexFF;

                string filtros = consulta.ObtenerFiltros().Substring(0, indexSF) + " (r.NombreRol = '" + rol_str + "' OR " +
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
                return consulta.ObtenerFiltros() + " " + ObtenerEnlace() + " r.NombreRol = '" + rol_str + "'";
            }
        }


        protected override Boolean YaEnConsulta()
        {
            return consulta.ObtenerFiltros().Contains("r.NombreRol");
        }

    }
}