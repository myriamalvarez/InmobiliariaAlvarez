using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaAlvarez.Models
{
    public class RepositorioPropietario : RepositorioBase
    {
        public RepositorioPropietario(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Propietario> Obtener()
        {
            var res = new List<Propietario>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave" +
                    $" FROM Propietario ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Propietario p = new Propietario
                        {
                            IdPropietario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Clave = reader.GetString(6)
                        };
                        res.Add(p);
                    }
                    connection.Close();
                }
            }
            return res;
        }
        public int Alta(Propietario p)
        {
            int res = -1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Propietario (Nombre, Apellido, Dni, Telefono, Email, Clave) " +
                $"VALUES ('{p.Nombre}', '{p.Apellido}', '{p.Dni}', '{p.Telefono}', '{p.Email}', '{p.Clave}')";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.IdPropietario = Convert.ToInt32(id);
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
    }
}
