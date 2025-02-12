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

        public Paciente(int dNI, int edad, string nombre, string apellido, string sintoma) : base(dNI, edad, nombre, apellido)
        {
            Sintoma = sintoma;
        }
    }
}
