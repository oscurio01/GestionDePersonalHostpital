using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    enum Especialidades { 
        MedicoGeneral,
        MedicoDeCabecera, 
        Cirujano, 
        Dermatologo, 
        Endocrinologo, 
        Hematologo, 
        Neumologo, 
        Neurologo, 
        Urologo, 
        Ginecologo, 
        Psiquiatra, 
        Oftanmologo,
        Otorrinolaringolo}
    internal class Medico : Persona
    {
        Especialidades Especialidad {  get; set; }
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();
        public Medico(int dNI, int edad, string nombre, string apellido, Especialidades especialidad) : base(dNI, edad, nombre, apellido)
        {
            Especialidad = especialidad;
        }

        public override string ToString()
        {
            return $"Medico: {Nombre} {Apellido}, edad {Edad}, especialidad {Especialidad}."; 
        }

        public void AñadirPacientes(Paciente paciente)
        {
            Pacientes.Add(paciente);
        }

        public void QuitarPaciente(Paciente paciente)
        {
            Pacientes.Remove(paciente);
        }
        
    }
}
