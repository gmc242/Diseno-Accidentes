using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Provincia
    {
        public Provincia(int id, String nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Cantones = new List<Canton>();
        }

        // Properties auto-generados
        public int Id { get; set; }
        public String Nombre { get; set; }
        public List<Canton> Cantones { get; }
        // Info de localización ?
        
    }
}