using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Distrito
    {
        public Distrito(int id, String nombre, Canton canton)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.Canton = canton;
            this.Canton.Distritos.Add(this);
        }

        // Properties auto-generados
        public int Id { get; set; }
        public String Nombre { get; set; }
        public Canton Canton { get; set; }
        // Info de localización ?
    }
}