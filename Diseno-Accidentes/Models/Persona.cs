using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Persona
    {
        public enum Sexo { Hombre, Mujer, Otro }

        public Persona(int id, int edad, Lesion lesion, Rol rol)
        {
            this.Id = id;
            this.Edad = edad;
            this.Rol = rol;
            this.Lesion = lesion;
        }

        public Lesion Lesion { get; set; }
        public Rol Rol { get; set; }
        public int Edad { get; set; }
        public int Id { get; set; }
        public int EdadQuinquenal { get { return Edad / 5; } }
    }
}