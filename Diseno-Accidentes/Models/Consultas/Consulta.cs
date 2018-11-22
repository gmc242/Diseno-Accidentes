using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Diseno_Accidentes.Models
{
    public class Consulta : IConsultable<int>
    {
        private List<String> campos;
        private SqlConnection conn = new SqlConnection("Data Source = DESKTOP-U0DSRU9\\DESARROLLOTEC; Initial Catalog = Accidentes; Integrated Security= True");

        public Consulta()
        {
            this.campos = new List<string>();
            campos.Add("count(a.ID) as Total");
        }

        public Consulta(String campo) : this ()
        {
            campos.Add(campo);
        }

        public Consulta(List<String> campos) : this ()
        {
            this.campos.AddRange(campos);
        }

        public String ObtenerConsulta() {
            return ObtenerHeader() + " " + ObtenerMiddle() + " " + ObtenerFooter();
        }

        virtual public String ObtenerHeader()
        {
            return "SELECT " + String.Join(",", campos);
        }

        virtual public String ObtenerMiddle()
        {
            return "FROM Accidente a";
        }

        virtual public String ObtenerFooter()
        {
            return (campos.Count > 1) ? " GROUP BY " + String.Join(",", campos.GetRange(1, campos.Count - 1)) : "";
        }

        virtual public String ObtenerFiltros() { return "";  }


        public List<KeyValuePair<String,int>> AplicarConsulta()
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

        public List<String> ObtenerCampos()
        {
            return campos;
        }

        public void AnadirCampo(String campo)
        {
            this.campos.Add(campo);
        }

    }
}