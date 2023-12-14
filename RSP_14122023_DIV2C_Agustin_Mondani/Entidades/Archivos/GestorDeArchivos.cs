using System.Text.Json;

namespace Entidades.Archivos
{
    public class GestorDeArchivos
    {

        private static string basePath;

        /// <summary>
        /// Es el path a la carpeta 20231207_Agustin.Mondani.2C en el escritorio
        /// </summary>
        static GestorDeArchivos()
        {
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            basePath = Path.Combine(basePath, "RSP_14122023_DIV2C_Agustin_Mondani");

            ValidaExitenciaDeDirectorio();
        }

        private static void ValidaExitenciaDeDirectorio()
        {
            try
            {
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                FileInfo fileInfo = new FileInfo(basePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el directorio", ex);
            }
        }

        /// <summary>
        /// Guarda la data en un archivo
        /// </summary>
        /// <param name="informacion">Informacion a guardar</param>
        /// <param name="path">Nombre del archivo</param>
        /// <param name="append">Anexar</param>
        private static void Guardar(string informacion, string path)
        {
            try
            {
                string pathCompleto = Path.Combine(GestorDeArchivos.basePath, path);
                using (StreamWriter streamWriter = new StreamWriter(pathCompleto))
                {
                    streamWriter.WriteLine(informacion);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error ruta", ex);
            }
        }

        public static bool Serializar<T>(T elemento, string nombreDelArchivo) where T : class
        {
            try
            {
                string json = JsonSerializer.Serialize(elemento);
                Guardar(json, nombreDelArchivo);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al serializar", ex);
            }
        }
    }
}
