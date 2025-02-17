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

        public override string ToString()
        {
            string texto = $"Cita {Fecha}, El paciente {Paciente.Nombre} con medico {Medico.Nombre}";
            if(Diagnostico != string.Empty)
            {
                texto += $" Diagnostico {Diagnostico} y tratamiento {Tratamiento}";
            }
            return texto;
        }

        public void ModificarFecha(DateTime fecha)
        {
            Fecha = fecha;
        }

        public void ModificarMedico(Medico medico)
        {
            if (Diagnostico == string.Empty)
                Medico = medico;
            else
                Console.WriteLine("Lo siento pero ya hay un medico que ha escrito un informe, no se puede cambiar");
        }

        public void RegistrarDiagnosticoYTratamiento(string diagnostico, string tratamiento)
        {
            Diagnostico = diagnostico;
            Tratamiento = tratamiento;
            
        }

        public void AgregarNota(string nota)
        {
            Notas.Add(nota);
        }

    }
}
