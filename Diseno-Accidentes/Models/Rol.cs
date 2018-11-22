using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Rol
    {
        public enum TipoRol { Peaton, Motociclista, Conductor, Pasajero_Moto, Pasajero_Carro, Dueno_de_Propiedad,
                              Pasajero_Bus_o_Microbus, Pasajero_Bicicleta, Ciclista, Otro}

        public int Codigo { get; set; }
        public TipoRol Tipo { get; set; }

        public Rol(int codigo, TipoRol tipo)
        {
            this.Codigo = codigo;
            this.Tipo = tipo;
        }
    }
}