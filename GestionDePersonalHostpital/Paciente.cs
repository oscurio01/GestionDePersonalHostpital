using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    internal class Paciente : Persona
    {
        public string Sintoma { get; set; }
        public Medico medico;

        public Paciente(string dNI, int edad, string nombre, string apellido, string sintoma, Medico medico = null) : base(dNI, edad, nombre, apellido)
        {
            Sintoma = sintoma;
            this.medico = medico;
        }

        public Paciente(Persona persona, string sintoma, Medico medico) : base(persona)
        {
            Sintoma = sintoma;
            this.medico = medico;
        }

        public void AñadirMedico(Medico medico)
        {
            this.medico = medico;
        }

        public void QuitarMedico()
        {
            medico = null;
        }

        public override string ToString()
        {
            return base.ToString() + (medico != null ? $", Sintomas {Sintoma} y tiene como medico: {medico.Nombre}." : $", Sintomas {Sintoma}.");
        }
    }
}
