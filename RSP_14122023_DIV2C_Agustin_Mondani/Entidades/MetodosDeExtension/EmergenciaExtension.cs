using Entidades.Enumerados;
using System.Drawing;

namespace Entidades.MetodosDeExtension
{
    public static class EmergenciaExtension
    {
        public static bool ValidarEmergencia(this List<EEmergencia> lista, EEmergencia emergencia)
        {
            bool valorRetorno = lista.Any(emergenciaElem => emergenciaElem == emergencia);
            return valorRetorno;
        }
    }
}
