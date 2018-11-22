using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diseno_Accidentes.Models
{
    public interface IConsultable<T>
    {
       
        List<KeyValuePair<String, T>> AplicarConsulta(); 

        // Este método permite obtener el string para aplicar la consulta de manera simple y para distintos manejos
        // así como pasos de debugging
        String ObtenerConsulta();

        // Estos métodos darán forma a las distintas consultas
        // La idea es con el uso de decorator, cada filtro caiga con sus parámetros sobre cada parte necesaria
        String ObtenerHeader();
        String ObtenerMiddle();
        String ObtenerFooter();
        String ObtenerFiltros();

        
    }
}
