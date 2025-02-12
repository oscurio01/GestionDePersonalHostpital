using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    internal class Persona
    {
        public int DNI { get; set; }
        public int Edad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Persona(int dNI, int edad, string nombre, string apellido)
        {
            DNI = dNI;
            Edad = edad;
            Nombre = nombre;
            Apellido = apellido;
        }
    }
}
