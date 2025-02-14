using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    internal class Program
    {
        static List<Persona> PersonasEnElHospital = new List<Persona>()
        {
            new Medico("5552233B", 35, "Paco", "Gafotas", Especialidades.MedicoGeneral)
        };
        static bool Salir = true;
        static void Main(string[] args)
        {
            int eleccion = 0;

            while (Salir)
            {
                Console.Clear();
                Console.WriteLine(@"Que quieres hacer
==========================================
1. Dar de alta un medico
2. Dar de alta un paciente
3. Dar de alta un personal administativo
4. Listar los medicos
5. Listar los paciente de un medico 
6. Dar de baja un paciente
7. Dar de baja un medico
8. Ver la lista de personas en el hospital
0. Para salir
==========================================
");
                eleccion = LeerUnNumeroCorrecto(8, 0);
                Console.Clear();

                switch (eleccion)
                {
                    case 1:
                        DarAltaMedico();
                        break;
                    case 2: 
                        DarAltaPaciente();
                        break;
                    case 3:
                        DarAltaPersonal();
                        break;
                    case 4:
                        ListarMedicos();
                        break;
                    case 5:
                        ListarPacientesDeMedico();
                        break;
                    case 6:
                        DarDeBajaPaciente();
                        break;
                    case 7:
                        DarDeBajaMedico();
                        break;
                    case 8:
                        foreach(var personas in PersonasEnElHospital)
                        {
                            Console.WriteLine(personas.ToString());
                        }
                        break;
                    case 0:
                        Salir = false;
                        break;
                }
                Console.WriteLine("Pulsa cualquier boton para continuar...");
                Console.ReadLine();

            }
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

        

        static void DarAltaMedico()
        {
            Persona p;

            Especialidades especialidad;

            //Consigue los datos de una persona y los aplica al medico
            p = Persona.DarAltaPersona(PersonasEnElHospital);

            Console.WriteLine("Dime en que se especializa");
            foreach(var esp in Enum.GetValues(typeof(Especialidades)))
            {
                Console.WriteLine($"{(int)esp + 1}. {esp}");
            }
            especialidad = (Especialidades)LeerUnNumeroCorrecto(Enum.GetValues(typeof(Especialidades)).Length-1, 1) - 1;


            PersonasEnElHospital.Add(new Medico(p, especialidad));
        }

        static void DarAltaPaciente()
        {
            if (!PersonasEnElHospital.OfType<Medico>().Any())
            {
                Console.WriteLine("Ahora mismo no hay un medico operable, por favor de de alta a un medico");
                return;
            }

            string sintomas;
            Persona p;
            List<Medico> listaMedicos = new List<Medico>();

            //Consigue los datos de una persona y los aplica al paciente
            p = Persona.DarAltaPersona(PersonasEnElHospital);

            Console.Write("Dime que sintomas tiene el paciente\n");
            sintomas = Console.ReadLine();

            Console.WriteLine("Asignale un medico, escribe su DNI correctamente");
            ListarMedicos();

            Console.Write("A quien eliges: \n> ");

            Medico medico = LeerDNIExacto<Medico>();

            if (medico != null)
            {
                Paciente paciente = new Paciente(p, sintomas, medico);
                medico.AñadirPacientes(paciente);
                PersonasEnElHospital.Add(paciente);
            }
            else
                Console.WriteLine("No hay medicos");
        }

        static void DarAltaPersonal()
        {
            string departamento;
            string horarioDeTrabajo;
            CargoAdministrativo cargo;
            Persona p;
            p = Persona.DarAltaPersona(PersonasEnElHospital);
            // Departamento HorarioDeTrabajo
            Console.WriteLine("Dime que cargo administrativo tiene:\n> ");

            foreach (var esp in Enum.GetValues(typeof(CargoAdministrativo)))
            {
                Console.WriteLine($"{(int)esp + 1}. {esp}");
            }
            cargo = (CargoAdministrativo)LeerUnNumeroCorrecto(Enum.GetValues(typeof(CargoAdministrativo)).Length - 1, 1) - 1;

            Console.WriteLine("En que departamento esta:\n> ");
            departamento = Console.ReadLine();

            Console.WriteLine("Dime que horario tiene:\n> ");
            horarioDeTrabajo = Console.ReadLine();

            PersonasEnElHospital.Add(new PersonalAdministrativo(p, cargo, departamento, horarioDeTrabajo));
            Console.WriteLine($"Se ha añadido al hospital:\n {PersonasEnElHospital.Last()}");

        }

        static void ListarMedicos()
        {
            if (!PersonasEnElHospital.OfType<Medico>().Any())
            {
                Console.WriteLine("Ahora mismo no hay un medico operable, por favor de de alta a un medico");
                return;
            }

            foreach (var persona in PersonasEnElHospital)
            {
            if(persona is Medico)
                Console.WriteLine(persona.ToString());
            }

        }

        static void ListarPacientesDeMedico()
        {
            if (!PersonasEnElHospital.OfType<Medico>().Any())
            {
                Console.WriteLine("Ahora mismo no hay un medico operable, por favor de de alta a un medico");
                return;
            }

                Console.WriteLine("Dime el medico que quieres revisar: ");
            ListarMedicos();

            Console.Write("Escribe su DNI: \n> ");

            Medico medico = LeerDNIExacto<Medico>();

            if (medico != null)
            {
                ListaDePacientes(medico);
            }
            else
                Console.WriteLine("El DNI no existe o lo has escrito mal");

        }

        static void ListaDePacientes(Medico medico)
        {
            if (medico.Pacientes.Count == 0)
                Console.WriteLine("Este medico no tiene ningun paciente a su cargo");

            foreach (var persona in medico.Pacientes)
            {
                Console.WriteLine(persona.ToString());
            }
        }

        static void DarDeBajaPaciente()
        {
            if (!PersonasEnElHospital.OfType<Paciente>().Any())
            {
                Console.WriteLine("No existen mas pacientes");
                return;
            }

            Console.WriteLine("Dime que paciente quieres dar de baja, existen: ");

            foreach (Persona persona in PersonasEnElHospital)
            {
                if(persona is Paciente p)
                    Console.WriteLine(p.ToString());
            }

            Console.Write("Escribe su DNI\n> ");

            Paciente paciente = LeerDNIExacto<Paciente>();

            var medico = PersonasEnElHospital.OfType<Medico>()
                .FirstOrDefault(m => m.Pacientes.Any(p=> p ==  paciente));

            if (medico != null)
                medico.QuitarPaciente(paciente);
            else
                Console.WriteLine("No se encontraron médicos con pacientes con el mismo DNI.");

            Console.WriteLine("Se ha eliminado a {0}", paciente.ToString());
            PersonasEnElHospital.Remove(paciente);
        }

        static void DarDeBajaMedico()
        {
            if (!PersonasEnElHospital.OfType<Medico>().Any())
            {
                Console.WriteLine("No existen mas medicos da de alta nuevos");
                return;
            }

                Console.WriteLine("Dime que medico quieres dar de baja, existen: ");

            foreach (Persona persona in PersonasEnElHospital)
            {
                if (persona is Medico med)
                    Console.WriteLine(med.ToString());
            }

            Console.Write("Escribe su DNI\n> ");
            Medico medico = LeerDNIExacto<Medico>();

            // Rehasigna a los pacientes a otro medico
            if (medico.Pacientes.Count > 0 && PersonasEnElHospital.OfType<Medico>().Count() != 1)
            {
                Console.WriteLine("Reasigna los pacientes a otros medicos: ");
                foreach(var paciente in medico.Pacientes)
                {
                    Console.WriteLine(paciente.ToString());
                }
                Console.WriteLine("\nA que medico se los asignas: ");
                foreach(var medic in PersonasEnElHospital.OfType<Medico>())
                {
                    if(medic.DNI != medico.DNI)
                        Console.WriteLine(medic.ToString());
                }

                Medico NuevoMedico = LeerDNIExacto<Medico>(medico.DNI);
                // El nuevo medico adquiere los pacientes del anterior
                foreach(var pacientes in medico.Pacientes)
                {
                    pacientes.AñadirMedico(NuevoMedico);
                }

                NuevoMedico.AñadirPacientes(medico.Pacientes);
            }
            // Si no hay mas medico los pacientes se quedan sin medicos y mueren
            if(PersonasEnElHospital.OfType<Medico>().Count() != 1)
            {
                foreach (var pacientes in medico.Pacientes)
                {
                    pacientes.QuitarMedico();
                }
            }

            Console.WriteLine("Se ha eliminado a {0}", medico.ToString());
            PersonasEnElHospital.Remove(medico);

        }

        static P LeerDNIExacto<P>(string excepcionDNI = "") where P : Persona
        {
            bool salir = true;
            while (salir)
            {
                string dni = Console.ReadLine();
                salir = true;

                if (dni == excepcionDNI)
                {
                    Console.WriteLine("Esa persona no se puede elegir, usa otro dni");
                    continue;
                }

                var persona = PersonasEnElHospital.OfType<P>()
                    .FirstOrDefault(m => string.Compare(m.DNI, dni, StringComparison.OrdinalIgnoreCase) == 0);

                if (persona != null)
                    return persona;
                else
                    Console.WriteLine("No se encontraron médicos con el mismo DNI.");

                if(PersonasEnElHospital.OfType<P>().Count() <= 0)
                    salir = false;
            }

            return null;
        }
    }
}
