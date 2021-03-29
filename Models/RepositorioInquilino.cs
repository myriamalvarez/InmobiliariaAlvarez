using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaAlvarez.Models
{
    public class RepositorioInquilino : RepositorioBase
    {
       

        public RepositorioInquilino(IConfiguration configuration) : base(configuration)
        { 
         
        }

    public IList<Inquilino> ObtenerTodos()
        {
            IList<Inquilino> lista = new List<Inquilino>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdInquilino, Dni, Apellido, Nombre, Direccion, Telefono FROM Inquilino ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Inquilino e = new Inquilino
                        {
                            IdInquilino = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Nombre = reader.GetString(3),
                            Direccion = reader.GetString(4),
                            Telefono = reader.GetString(5)
                        };
                        lista.Add(e);

                    }
                    connection.Close();
                }
            }
                    return lista;
                    

                }

                
 

        public int Alta(Inquilino e)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Inquilino(Dni, Apellido, Nombre, Direccion, Telefono) " +
                $"VALUES ('{e.Dni}', '{e.Apellido}', '{e.Nombre}', '{e.Direccion}', '{e.Telefono}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    e.IdInquilino = Convert.ToInt32(id);
                    connection.Close();


                   /* command.Parameters.AddWithValue("@dni", e.Dni);
                    command.Parameters.AddWithValue("@apellido", e.Apellido);
                    command.Parameters.AddWithValue("@nombre", e.Nombre);
                    command.Parameters.AddWithValue("@direccion", e.Direccion);
                    command.Parameters.AddWithValue("@telefono", e.Telefono);
                    connection.Open();
                    res = Convert.ToInt32(command.ExecuteScalar());
                    e.IdInquilino = res;
                    connection.Close();*/
                }
            }
            return res;
        }
        public Inquilino Obtener(int IdInquilino)
        {
            Inquilino res = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdInquilino, Dni, Apellido, Nombre, Direccion, Telefono FROM Inquilino " +
                    $"WHERE IdInquilino = {@IdInquilino} ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IdInquilino", IdInquilino);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                         res = new Inquilino
                        {
                            IdInquilino = reader.GetInt32(0),
                            Dni = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Nombre = reader.GetString(3),
                            Direccion = reader.GetString(4),
                            Telefono = reader.GetString(5)



                        };
                       
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public int Baja(int IdInquilino)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM Inquilino WHERE IdInquilino = {IdInquilino}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
        public int Modificacion(Inquilino e)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Inquilino SET Dni='{e.Dni}', Apellido='{e.Apellido}', Nombre='{e.Nombre}', Direccion='{e.Direccion}', Telefono='{e.Telefono}' " +
                    $"WHERE IdInquilino = {e.IdInquilino}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }
    }
}
