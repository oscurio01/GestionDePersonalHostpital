using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    internal class Persona
    {
        public string DNI { get; set; }
        public int Edad { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public Persona(string dNI, int edad, string nombre, string apellido)
        {
            DNI = dNI;
            Edad = edad;
            Nombre = nombre;
            Apellido = apellido;
        }

        public Persona(Persona p)
        {
            DNI = p.DNI;
            Edad = p.Edad;
            Nombre = p.Nombre;
            Apellido = p.Apellido;
        }

        public override string ToString()
        {
            return $"DNI:{DNI}, Nombre: {Nombre} {Apellido}, edad {Edad}";
        }

        public static Persona DarAltaPersona(List<Persona> listaDni)
        {
            bool salir = true;
            int edad = 0;
            string dni = "", nombre, apellido;
            Persona p;

            Console.Write("Dime su DNI\n> ");
            while (salir)
            {
                salir = false;
                dni = Console.ReadLine();

                // Verificar que el DNI no esté repetido
                foreach (Persona persona in listaDni)
                {
                    if (persona.DNI == dni)
                    {
                        salir = true;
                        Console.WriteLine("Ese DNI ya existe usa otro");
                    }
                    else if (dni.Length > 8)
                    {
                        salir = true;
                        Console.WriteLine("No existe un DNI tan largo vuelve lo a intentar");
                    }
                }

            }

            Console.WriteLine("Dime su edad");

            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out edad) && edad >= 0 && edad <= 100)
                    break;
                else
                    Console.WriteLine("No existe nadie con esa edad, prueba de nuevo");
            }

            Console.Write("Dime su nombre\n> ");
            nombre = Console.ReadLine();

            Console.Write("Y el apellido\n> ");
            apellido = Console.ReadLine();

            return p = new Persona(dni, edad, nombre, apellido);
        }


    }
}
