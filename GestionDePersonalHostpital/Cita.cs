using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    internal class Cita
    {
        public Paciente Paciente { get; set; }
        public Medico Medico { get; set; }
        public DateTime? Fecha { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public List<string> Notas { get; set; } = new List<string>();

        public Cita(Paciente paciente, Medico medico, DateTime fecha)
        {
            Paciente = paciente;
            Medico = medico;
            Fecha = fecha;
        }

        public Cita(Cita cita)
        {
            Paciente = cita.Paciente;
            Medico = cita.Medico;
            Fecha = cita.Fecha;
            Diagnostico = cita.Diagnostico;
            Tratamiento = cita.Tratamiento;
            Notas = cita.Notas;
        }

        public void ModificarFecha(DateTime fecha)
        {
            Fecha = fecha;
        }

        public void RegistrarDiagnosticoYTratamiento(string diagnostico, string tratamiento)
        {
            Diagnostico = diagnostico;
            Tratamiento = tratamiento;
            // Aquí podemos registrar el diagnóstico y tratamiento en el historial médico del paciente.
            Paciente.RegistrarHistorial(new Cita(this)); 
        }

        public void AgregarNota(string nota)
        {
            Notas.Add(nota);
        }

    }
}
