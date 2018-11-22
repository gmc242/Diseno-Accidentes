using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Lesion
    {
        public enum TipoLesion { Herido_leve, Ileso, Herido_grave, Muerto }

        public int Codigo { get; set; }
        public TipoLesion Tipo { get; set; }

        public Lesion(int codigo, TipoLesion tipo)
        {
            this.Codigo = codigo;
            this.Tipo = tipo;
        }
    }
}