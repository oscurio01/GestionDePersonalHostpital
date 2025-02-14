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
        public Especialidades Especialidad {  get; set; }
        public List<Paciente> Pacientes { get; set; } = new List<Paciente>();

        public Medico(string dNI, int edad, string nombre, string apellido, int telefono, Especialidades especialidad) : base(dNI, edad, nombre, apellido, telefono)
        {
            Especialidad = especialidad;
        }

        public Medico(Persona persona, Especialidades especialidad) : base(persona)
        {
            Especialidad = especialidad;
        }

        public override string ToString()
        {
            return base.ToString() + $", especialidad {Especialidad}."; 
        }

        public void AñadirPaciente(Paciente paciente)
        {
            Pacientes.Add(paciente);
        }
        public void AñadirPacientes(List<Paciente> pacientes)
        {
            Pacientes.AddRange(pacientes);
        }

        public void QuitarPaciente(Paciente paciente)
        {
            Pacientes.Remove(paciente);
        }
        
    }
}
