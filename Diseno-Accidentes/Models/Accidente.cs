using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Diseno_Accidentes.Models
{
    public class Accidente : ISerializableJSON
    {
        public Accidente(Persona persona, Distrito distrito, Fecha fecha)
        {
            this.Persona = persona;
            this.Distrito = distrito;
            this.Fecha = fecha;
        }

        public Fecha Fecha { get; }
        public Persona Persona { get; }
        public Distrito Distrito { get;  }
        public Canton Canton { get { return Distrito.Canton;  } }
        public Provincia Provincia { get { return Distrito.Canton.Provincia;  } }

        public String ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}