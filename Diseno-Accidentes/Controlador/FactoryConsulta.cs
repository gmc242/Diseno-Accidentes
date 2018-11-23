using Diseno_Accidentes.Models;
using Diseno_Accidentes.Models.Consultas;
using Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas;
using Diseno_Accidentes.Models.Consultas.Consultas_Filtradas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Controlador
{
    public static class FactoryConsulta
    {
        public static IConsultable<int> ObtenerConsultaFiltrada(String tipo, IConsultable<int> consulta, String valor)
        {
            switch (tipo.ToLower())
            {
                case "sexo":
                    return new ConsultaFiltradaSexo(consulta, 
                        (Persona.Sexo)Enum.Parse(typeof(Persona.Sexo), valor));
                case "lesion":
                    return new ConsultaFiltradaLesion(consulta, 
                        (Lesion.TipoLesion)Enum.Parse(typeof(Lesion.TipoLesion), valor));
                case "rol":
                    return new ConsultaFiltradaRol(consulta,
                        (Rol.TipoRol)Enum.Parse(typeof(Rol.TipoRol), valor));
                case "dia":
                    return new ConsultaFiltradaDia(consulta,
                        (Fecha.Dia)Enum.Parse(typeof(Fecha.Dia), valor));
                case "mes":
                    return new ConsultaFiltradaMes(consulta,
                        (Fecha.Mes)Enum.Parse(typeof(Fecha.Mes), valor));
                case "edad":
                    return new ConsultaFiltradaEdad(consulta, int.Parse(valor));
                case "edad_quinquenal":
                    return new ConsultaFiltradaEdadQuinquenal(consulta, int.Parse(valor));
                case "provincia":
                    return new ConsultaFiltradaProvincia(consulta, valor);
                case "canton":
                    return new ConsultaFiltradaCanton(consulta, valor);
                case "distrito":
                    return new ConsultaFiltradaDistrito(consulta, valor);
                default:
                    return new ConsultaAgrupadaProvincia();
            }
        }

        public static IConsultable<int> ObtenerConsultaAgrupada(String tipo)
        {
            switch (tipo.ToLower())
            {
                case "sexo":
                    return new ConsultaAgrupadaSexo();
                case "lesion":
                    return new ConsultaAgrupadaLesion();
                case "rol":
                    return new ConsultaAgrupadaRol();
                case "anio":
                    return new ConsultaAgrupadaAnio();
                case "dia":
                    return new ConsultaAgrupadaDia();
                case "mes":
                    return new ConsultaAgrupadaMes();
                case "edad":
                    return new ConsultaAgrupadaEdad();
                case "edad_quinquenal":
                    return new ConsultaAgrupadaEdadQuinquenal();
                case "provincia":
                    return new ConsultaAgrupadaProvincia();
                default:
                    return new ConsultaAgrupadaProvincia();
            }
        }

        public static List<String> ObtenerValores(String filtro)
        {
            switch (filtro)
            {
                case "Sexo":
                    {
                        return Enum.GetValues(typeof(Models.Persona.Sexo))
                        .Cast<Models.Persona.Sexo>()
                        .Select(e => e.ToString())
                        .ToList();
                    }
                case "Lesion":
                    {
                        return Enum.GetValues(typeof(Models.Lesion.TipoLesion))
                        .Cast<Models.Lesion.TipoLesion>()
                        .Select(e => e.ToString().Replace("_", " "))
                        .ToList();
                    }
                case "Rol":
                    {
                        return Enum.GetValues(typeof(Models.Rol.TipoRol))
                        .Cast<Models.Rol.TipoRol>()
                        .Select(e => e.ToString().Replace("_", " "))
                        .ToList();
                    }
                case "Provincia":
                    {
                        return new List<string>()
                            { "San Jose", "Cartago", "Alajuela", "Heredia", "Puntarenas", "Guanacaste", "Limon" };
                        
                    }
                case "Dia":
                    {
                        return Enum.GetValues(typeof(Fecha.Dia))
                        .Cast<Fecha.Dia>()
                        .Select(e => e.ToString().Replace("_", " "))
                        .ToList();
                    }
                case "Mes":
                    {
                        return Enum.GetValues(typeof(Fecha.Mes))
                        .Cast<Fecha.Mes>()
                        .Select(e => e.ToString().Replace("_", " "))
                        .ToList();
                    }
                case "Edad":
                    {
                        List<int> numeros = Enumerable.Range(1, 102).ToList();
                        return numeros.Select(i => i.ToString()).ToList();
                    }
                case "Edad Quinquenal":
                    {
                        List<int> numeros = Enumerable.Range(1, 21).ToList();
                        return numeros.Select(i => i.ToString()).ToList();
                    }
                default:
                    return new List<String>();
            }
        }
    }
}