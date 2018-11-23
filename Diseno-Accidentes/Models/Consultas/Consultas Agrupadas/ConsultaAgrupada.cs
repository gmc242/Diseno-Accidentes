using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models.Consultas.Consultas_Agrupadas
{
    // Abstract Decorator 
    public abstract class ConsultaAgrupada : IConsultable<int>
    {
        protected Consulta consulta;
        protected SqlConnection conn = new SqlConnection("Data Source = DESKTOP-U0DSRU9\\DESARROLLOTEC; Initial Catalog = Accidentes; Integrated Security= True");

        public ConsultaAgrupada(String campo)
        {
            this.consulta = new Consulta(campo);
        }

        public virtual string ObtenerHeader()
        {
            return consulta.ObtenerHeader();
        }

        public virtual string ObtenerMiddle()
        {
            return consulta.ObtenerMiddle();
        }

        public virtual string ObtenerFooter() { return consulta.ObtenerFooter(); }

        public virtual string ObtenerFiltros() { return consulta.ObtenerFiltros(); }

        public String ObtenerConsulta()
        {
            return ObtenerHeader() + " " + ObtenerMiddle() + " " + ObtenerFiltros() + " " + ObtenerFooter();
        }

        public List<KeyValuePair<string, int>> AplicarConsulta()
        {
            List<KeyValuePair<String, int>> res = new List<KeyValuePair<String, int>>();

            conn.Open();
            String sql = ObtenerConsulta();


            SqlCommand command = new SqlCommand(sql, conn);
            command.Prepare();

            using (var reader = command.ExecuteReader())
            {
                // Primero inserta el valor agregado al header, siempre va a estar en la posición 1 del array de columnas
                KeyValuePair<String, int> header = new KeyValuePair<string, int>(reader.GetSchemaTable().Columns[1].ColumnName, -1);

                // Agrega el header
                res.Add(header);

                // Agrega cada valor
                while (reader.Read())
                {
                    int total = (int)reader[0];
                    String valor = reader[1].ToString();
                    KeyValuePair<string, int> fila = new KeyValuePair<string, int>(valor, total);
                    res.Add(fila);
                }
            }

            conn.Close();
            return res;

        }

    }
}