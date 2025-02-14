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
        public int Telefono { get; set; }

        public Persona(string dNI, int edad, string nombre, string apellido, int telefono)
        {
            DNI = dNI;
            Edad = edad;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
        }

        public Persona(Persona p)
        {
            DNI = p.DNI;
            Edad = p.Edad;
            Nombre = p.Nombre;
            Apellido = p.Apellido;
            Telefono = p.Telefono;
        }

        public override string ToString()
        {
            return $"DNI:{DNI}, Nombre: {Nombre} {Apellido}, edad {Edad}, telefono {Telefono}";
        }

        public static Persona DarAltaPersona(List<Persona> listaDni)
        {
            bool salir = true;
            int edad, telefono;
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
            edad = LeerUnNumeroCorrecto(100, 0, "No existe nadie con esa edad, prueba de nuevo");

            Console.Write("Dime su nombre\n> ");
            nombre = Console.ReadLine();

            Console.Write("Y el apellido\n> ");
            apellido = Console.ReadLine();

            Console.WriteLine("Escribe el telefono");
            telefono = LeerUnNumeroCorrecto(699999999, 600000000, "No existe numero de telefono en españa así");

            return p = new Persona(dni, edad, nombre, apellido, telefono);
        }

        static int LeerUnNumeroCorrecto(int maximo, int minimo = 0, string texto = "Numero no valido")
        {
            int numeroCorrecto;
            while (true)
            {
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out numeroCorrecto) && numeroCorrecto >= minimo && numeroCorrecto <= maximo)
                    return numeroCorrecto;
                else
                    Console.WriteLine(texto);
            }
        }
    }
}
