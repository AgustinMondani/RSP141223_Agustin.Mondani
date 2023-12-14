using Entidades.Enumerados;
using Entidades.Excepciones;
using Entidades.Interfaces;
using Entidades.MetodosDeExtension;

namespace Entidades.Modelos
{
    public class Paramedico : IServidorPublico
    {
        private static List<EEmergencia> emergenciasAtendibles;

        static Paramedico()
        {
            Paramedico.emergenciasAtendibles = new List<EEmergencia>() { EEmergencia.Accidentes_De_Trafico, EEmergencia.Desastres_Naturales, EEmergencia.Emergencias_Medicas };
        }

        public string Imagen => $"./assets/{this.GetType().Name}.gif";

        public void Atender(Emergencia emergencia)
        {
            bool sePuedeAtender = EmergenciaExtension.ValidarEmergencia(Paramedico.emergenciasAtendibles, emergencia.Tipo);
            if (sePuedeAtender)
            {
                emergencia.EstaAtendida = true;
            }
            else
            {
                throw new ServidorPublicoInvalidoException("El servidor público no puede atender este tipo de emergencias.");
            }
        }
    }
}
