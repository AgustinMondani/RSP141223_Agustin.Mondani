using Entidades.Delegados;
using Entidades.Enumerados;
using Entidades.Interfaces;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Entidades.Modelos
{


    public class CentroDeEmergencia
    {

        private string nombre;
        private Emergencia emergenciaEnCurso;
        private List<Emergencia> emergenciasAtendidas;
        CancellationTokenSource cancellation;

        private Task tarea;

        public event DelegadoEmergenciaEnCurso OnEmergenciaEnCurso;
        public event DelegadoEstadoEmergenciaEnCurso OnEstadoEmergenciaEnCurso;
        public event DelegadoEmergenciaMensaje OnServidorInvalido;

        public CentroDeEmergencia(string nombre)
        {
            this.nombre = nombre;
            this.emergenciasAtendidas = new List<Emergencia>();
        }

        public string Nombre { get => this.nombre; }
        public List<Emergencia> EmergenciasAtendidas { get => this.emergenciasAtendidas; }

        public void HabilitarIngreso()
        {
            //tarea = Task.Run(() =>
            //{
            //    while (cancellation.IsCancellationRequested.Equals(false))
            //    {
            //        Random = new Random().Next(0, 6);
            //        EEmergencia tipoEmergencia = EEmergencia

            //        this.emergenciaEnCurso = tipoEmergencia.;
            //    }
            //}, cancellation.Token);

        }

        private void DarSeguimientoAEmergencia()
        {
            tarea = Task.Run(()  => 
            {
                while (cancellation.IsCancellationRequested.Equals(false) 
                        || this.emergenciaEnCurso.SegundosTranscurridos < Emergencia.TiempoLimiteEnSegundos 
                        || this.emergenciaEnCurso.EstaAtendida)
                {
                    Thread.Sleep(1000);
                    this.emergenciaEnCurso.ActualizarEstadoEmergencia();
                    this.NotificarEstadoDeEmergenciaEnCurso();
                }
            }, cancellation.Token) ;
        }


        public void EnviarServidorPublico<T>(T servidorPublico) where T : IServidorPublico
        {
            tarea = Task.Run(()  => 
            {
                while (cancellation.IsCancellationRequested.Equals(false))
                {
                    try 
                    {
                        Thread.Sleep(3000);
                        servidorPublico.Atender(this.emergenciaEnCurso);
                        this.emergenciasAtendidas.Add(this.emergenciaEnCurso);
                     }catch (Exception ex)
                    {
                        OnServidorInvalido.Invoke(ex.Message);
                    }
                    
                    
                }
            }, cancellation.Token) ;

        }

        public void DeshabilitarIngreso()
        {
            tarea = Task.Run(() =>
            {
               
            }, cancellation.Token);
        }

        private void NotificarEstadoDeEmergenciaEnCurso()
        {
            if (OnEmergenciaEnCurso != null)
            {
                OnEstadoEmergenciaEnCurso.Invoke(this.emergenciaEnCurso.EstadoEmergencia);
            } 
        }
    }
}
