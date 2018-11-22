using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Canton
    {
        public Canton(int id, String nombre, Provincia provincia)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Provincia = provincia;
            provincia.Cantones.Add(this);
        }

        // Properties auto-generados
        public int Id { get; set; }
        public String Nombre { get; set; }
        public Provincia Provincia { get; set; }
        public List<Distrito> Distritos { get; }
        // Info de localización ?
    }
}