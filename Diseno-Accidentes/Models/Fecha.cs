using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Fecha
    {
        // Enums necesarios para la definicion de una fecha en el contexto dado
        public enum Dia { Lunes, Martes, Miercoles, Jueves, Viernes, Sabado, Domingo };
        public enum Mes { Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Setiembre, Octubre, Noviembre, Diciembre };

        // Properties auto-generados
        public int Anio { get; set; }
        public Dia DiaFecha { get; set; }
        public Mes MesFecha { get; set; }

        public Fecha(int anio, Dia dia, Mes mes)
        {
            this.Anio = anio;
            this.DiaFecha = dia;
            this.MesFecha = mes;
        }
    }
}