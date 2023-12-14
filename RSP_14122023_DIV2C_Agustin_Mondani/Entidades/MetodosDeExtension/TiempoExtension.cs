using Entidades.Enumerados;

namespace Entidades.MetodosDeExtension
{
    public static class TiempoExtension
    {
        public static double SegundosTrasncurridos(this DateTime inicio)
        {
            double valorRetorno = 0;

            valorRetorno = (int)(DateTime.Now - inicio).TotalSeconds;

            return valorRetorno;
        }
    }
}
