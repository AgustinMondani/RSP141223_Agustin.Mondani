using System.Data.SqlClient;

namespace Entidades.BaseDeDatos
{
    public static class GestorDeBaseDeDatos
    {
        private static SqlConnection connection;
        private static string stringConnection;

        static GestorDeBaseDeDatos()
        {
            GestorDeBaseDeDatos.stringConnection = "Server = .; Database = 20230622SP; Trusted_Connection = True";
        }

        public static bool RegistrarTrabajo(string nombreAlumno, int cantidadPacientes)
        {
            try
            {
                using (connection = new SqlConnection(stringConnection))
                {
                    string query = "INSERT INTO log (pacientes_atendidos, alumno) VALUES (@pacientes_atendidos, @alumno)";

                    SqlCommand cmd = new SqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@pacientes_atendidos", cantidadPacientes);
                    cmd.Parameters.AddWithValue("@alumno", nombreAlumno);

                    connection.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    return filasAfectadas > 0;

                }
            }catch (Exception ex)
            {
                throw new Exception("Error al guardar en la base de datos", ex;
            }
        }
    }
}
