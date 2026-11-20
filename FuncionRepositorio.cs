using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarteleraCine
{
    public class FuncionRepositorio
    {
        private const string CadenaConexion =
            "Server=(localdb)\\MSSQLLocalDB;Database=CarteleraDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public void Insertar(Funcion x)
        {
            const string sql = "INSERT INTO Funciones (Pelicula, Sala, Precio) VALUES (@pelicula, @sala, @precio)";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@pelicula", x.Pelicula);
                cmd.Parameters.AddWithValue("@sala", x.Sala);
                cmd.Parameters.AddWithValue("@precio", x.Precio);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Funcion> ListarTodos()
        {
            List<Funcion> lista = new List<Funcion>();
            const string sql = "SELECT Id, Pelicula, Sala, Precio FROM Funciones ORDER BY Id";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        lista.Add(new Funcion
                        {
                            Id = r.GetInt32(0),
                            Pelicula = r.GetString(1),
                            Sala = r.GetString(2),
                            Precio = r.GetDecimal(3)
                        });
                    }
                }
            }
            return lista;
        }

        public Funcion? BuscarPorId(int id)
        {
            const string sql = "SELECT Id, Pelicula, Sala, Precio FROM Funciones WHERE Id = @id";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    if (r.Read())
                    {
                        return new Funcion
                        {
                            Id = r.GetInt32(0),
                            Pelicula = r.GetString(1),
                            Sala = r.GetString(2),
                            Precio = r.GetDecimal(3)
                        };
                    }
                }
            }
            return null;
        }

        public bool Actualizar(Funcion x)
        {
            const string sql = "UPDATE Funciones SET Pelicula = @pelicula, Sala = @sala, Precio = @precio WHERE Id = @id";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@pelicula", x.Pelicula);
                cmd.Parameters.AddWithValue("@sala", x.Sala);
                cmd.Parameters.AddWithValue("@precio", x.Precio);
                cmd.Parameters.AddWithValue("@id", x.Id);
                con.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Eliminar(int id)
        {
            const string sql = "DELETE FROM Funciones WHERE Id = @id";
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                return cmd.ExecuteNonQuery() == 1;
            }
        }
    }
}