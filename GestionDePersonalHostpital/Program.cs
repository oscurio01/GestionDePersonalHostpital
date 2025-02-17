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
            new Medico("5552233B", 35, "Paco", "Gafotas", 699999999, Especialidades.MedicoGeneral)
        };
        static bool Salir = true;
        static void Main(string[] args)
        {
            int eleccion;

            while (Salir)
            {
                Console.Clear();
                Console.WriteLine(@"Que quieres hacer
==========================================
1. Dar de alta un medico
2. Modificar medico
3. Dar de alta un paciente
4. Modificar paciente
5. Dar de alta un personal administativo
6. Modificar personal administrativo
7. Listar los medicos
8. Listar los paciente de un medico 
9. Dar de baja un paciente
10. Dar de baja personal administrativo
11. Dar de baja un medico
12. Ver la lista de personas en el hospital
13. Gestion de citas
0. Para salir
==========================================
");
                eleccion = LeerUnNumeroCorrecto(13, 0);
                Console.Clear();

                switch (eleccion)
                {
                    case 1:
                        DarAltaMedico();
                        break;
                    case 2:
                        ModificarDatosMedico();
                        break;
                    case 3:
                        DarAltaPaciente(); 
                        break;
                    case 4:
                        ModificarDatosPaciente();
                        break;
                    case 5:
                        DarAltaPersonal(); 
                        break;
                    case 6:
                        ModificarDatosPersonalAdministrativo();
                        break;
                    case 7:
                        ListarMedicos(); 
                        break;
                    case 8:
                        ListarPacientesDeMedico(); 
                        break;
                    case 9:
                        DarDeBajaPaciente();
                        break;
                    case 10:
                        DarDeBajaAdministrativo();
                        break;
                    case 11:
                        DarDeBajaMedico();
                        break;
                    case 12:
                        foreach (var personas in PersonasEnElHospital)
                        {
                            Console.WriteLine(personas.ToString());
                        }
                        break;
                    case 13:
                        GestionarCitas();
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

        static void MostrarEspecialidadesMedico()
        {
            foreach (var esp in Enum.GetValues(typeof(Especialidades)))
            {
                Console.WriteLine($"{(int)esp + 1}. {esp}");
            }
        }

        static void DarAltaMedico()
        {
            Persona p;

            Especialidades especialidad;

            //Consigue los datos de una persona y los aplica al medico
            p = Persona.DarAltaPersona(PersonasEnElHospital);

            Console.WriteLine("Dime en que se especializa");
            MostrarEspecialidadesMedico();
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
                medico.AñadirPaciente(paciente);
                PersonasEnElHospital.Add(paciente);
            }
            else
                Console.WriteLine("No hay medicos");
        }

        static void MostrarCargosAdministrativos()
        {
            foreach (var esp in Enum.GetValues(typeof(CargoAdministrativo)))
            {
                Console.WriteLine($"{(int)esp + 1}. {esp}");
            }
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

            MostrarCargosAdministrativos();
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

        static void ListarPacientes()
        {
            foreach (var p in PersonasEnElHospital.OfType<Paciente>())
            {
                Console.WriteLine(p.ToString());
            }
        }

        static void ListarPersonalAdministrativo()
        {
            foreach(var personal in PersonasEnElHospital.OfType<PersonalAdministrativo>())
            {
                Console.WriteLine(personal.ToString());
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

        static void DarDeBajaAdministrativo()
        {
            if (!PersonasEnElHospital.OfType<PersonalAdministrativo>().Any())
            {
                Console.WriteLine("No existe mas personal administrativo");
                return;
            }

            Console.WriteLine("Dime que personal quieres dar de baja, existen: ");

            foreach (Persona persona in PersonasEnElHospital)
            {
                if (persona is PersonalAdministrativo p)
                    Console.WriteLine(p.ToString());
            }

            Console.Write("Escribe su DNI\n> ");

            PersonalAdministrativo personal = LeerDNIExacto<PersonalAdministrativo>();

            Console.WriteLine("Se ha eliminado a {0}", personal.ToString());
            PersonasEnElHospital.Remove(personal);
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
            if(PersonasEnElHospital.OfType<Medico>().Count() == 1)
            {
                foreach (var pacientes in medico.Pacientes)
                {
                    pacientes.QuitarMedico();
                    PersonasEnElHospital.Remove(pacientes);
                }
            }

            Console.WriteLine("Se ha eliminado a {0}", medico.ToString());
            PersonasEnElHospital.Remove(medico);

        }

        static void MenuModificarPersona()
        {
            Console.Write(@"Dime que quieres Cambiar
0. Salir   
1. Dni
2. Nombre
3. Apellido
4. Edad
5. Telefono"
           );
        }

        static void SwitchModificarPersona(int eleccion, ref bool salir, Persona persona)
        {
            switch (eleccion)
            {
                case 0:
                    salir = false;
                    break;
                case 1:
                    ModificarDNI(persona);
                    break;
                case 2:
                    Console.WriteLine("Dime el nuevo nombre");
                    persona.Nombre = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Dime el nuevo apellido");
                    persona.Apellido = Console.ReadLine();
                    break;
                case 4:
                    Console.WriteLine("Dime la edad");
                    persona.Edad = LeerUnNumeroCorrecto(100, 0);
                    break;
                case 5:
                    Console.WriteLine("Dime el nuevo telefono");
                    persona.Telefono = LeerUnNumeroCorrecto(699999999, 600000000, "No existe numero de telefono en españa así");
                    break;
            }
        }

        static void ModificarDNI(Persona persona)
        {
            Console.WriteLine("Dime el nuevo DNI");
            string dni = Console.ReadLine();
                
            Persona personaConEsteDNI = PersonasEnElHospital.OfType<Persona>()
                .FirstOrDefault(m => string.Compare(m.DNI, dni, StringComparison
                .OrdinalIgnoreCase) == 0);

            // revisa y comprueba que no haya nadie con ese dni
            if (personaConEsteDNI == null )
                persona.DNI = dni;
            else
                Console.WriteLine("Ese DNI ya esta escogido elige otro");
        }

        static void ModificarDatosPaciente()
        {
            bool salir = true;
            int eleccion;
            Paciente paciente;

            if (!PersonasEnElHospital.OfType<Paciente>().Any())
            {
                Console.WriteLine("No existen pacientes da de alta nuevos");
                return;
            }

            Console.WriteLine("============== Modificar Paciente ==============");
            ListarPacientes();

            Console.WriteLine("Escribe el DNI del paciente que quieres modificar");
            paciente = LeerDNIExacto<Paciente>();

            while (salir)
            {
                Console.Clear();
                Console.WriteLine(paciente.ToString());

                MenuModificarPersona();
                Console.WriteLine(@"
6. Sintomas
7. Medico
");
                eleccion = LeerUnNumeroCorrecto(7,0);
                switch (eleccion)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        SwitchModificarPersona(eleccion, ref salir, paciente);
                        break;
                    case 6:
                        Console.WriteLine("Dime los sintomas");
                        paciente.Sintoma = Console.ReadLine();
                        break;
                    case 7:
                        ListarMedicos();
                        Console.WriteLine("Dime el DNI del nuevo medico");
                        Medico antiguoMedico = paciente.medico;
                        Medico nuevoMedico = LeerDNIExacto<Medico>(paciente.medico.DNI);

                        antiguoMedico.QuitarPaciente(paciente);
                        paciente.AñadirMedico(nuevoMedico); 
                        break;
                }
            }
        }

        static void ModificarDatosMedico()
        {
            bool salir = true;
            int eleccion;
            Medico medico;

            if (!PersonasEnElHospital.OfType<Medico>().Any())
            {
                Console.WriteLine("No existen medicos da de alta nuevos");
                return;
            }

            Console.WriteLine("============== Modificar Medico ==============");
            ListarMedicos();

            Console.WriteLine("Escribe el DNI del medico que quieres modificar");
            medico = LeerDNIExacto<Medico>();

            while (salir)
            {
                Console.Clear();

                Console.WriteLine(medico.ToString());

                MenuModificarPersona();
                Console.WriteLine(@"
6. Especialidad
");
                eleccion = LeerUnNumeroCorrecto(7, 0);

                switch (eleccion)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        SwitchModificarPersona(eleccion, ref salir, medico);
                        break;
                    case 6:
                        Console.WriteLine("Elige la especialidad");
                        MostrarEspecialidadesMedico();

                        medico.Especialidad = (Especialidades)LeerUnNumeroCorrecto(Enum.GetValues(typeof(Especialidades)).Length - 1, 1) - 1;
                        break;
                }
            }
        }

        static void ModificarDatosPersonalAdministrativo()
        {
            bool salir = true;

            if (!PersonasEnElHospital.OfType<PersonalAdministrativo>().Any())
            {
                Console.WriteLine("No existen personales administrativos da de alta nuevos");
                return;
            }

            Console.WriteLine("============== Modificar Personal Administrativo ==============");
            ListarPersonalAdministrativo();

            Console.WriteLine("Escribe el DNI del personal que quieres modificar");
            PersonalAdministrativo personal = LeerDNIExacto<PersonalAdministrativo>();

            while (salir)
            {
                Console.Clear();
                Console.WriteLine(personal.ToString());

                MenuModificarPersona();
                Console.WriteLine(@"
6. CargoAdministrativo
7. Departamento
8. Horario de trabajo
");
                int eleccion = LeerUnNumeroCorrecto(8, 0);

                switch (eleccion)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        SwitchModificarPersona(eleccion, ref salir, personal);
                        break;
                    case 6:
                        MostrarCargosAdministrativos();
                        Console.WriteLine("Elige el cargo administrativo\n> ");
                        personal.CargoAdministrativo = (CargoAdministrativo)LeerUnNumeroCorrecto(Enum.GetValues(typeof(CargoAdministrativo)).Length - 1, 1) - 1;
                        break;
                    case 7:
                        Console.WriteLine("Escribe el departamento\n> ");
                        personal.Departamento = Console.ReadLine();
                        break;
                    case 8:
                        Console.WriteLine("Escribe el horario de trabajo\n> ");
                        personal.HorarioDeTrabajo = Console.ReadLine();
                        break;
                }
            }
        }

        static void GestionarCitas()
        {
            bool salir = true;
            int eleccion = 0;
            Paciente paciente;

            if (!PersonasEnElHospital.OfType<Paciente>().Any())
            {
                Console.WriteLine("No hay ningun paciente");
                return;
            }
            Console.WriteLine("Escribe el dni del paciente que quieres gestionar una cita");

            foreach (var persona in PersonasEnElHospital.OfType<Paciente>())
            {
                Console.WriteLine(persona.ToString());
            }

            paciente = LeerDNIExacto<Paciente>();


            while (salir)
            {
                Console.WriteLine(@"
Que quieres hacer?
1. Añadir cita
2. Modificar cita
3. Borrar cita
4. Ver cita
0. Salir");

                eleccion = LeerUnNumeroCorrecto(4, 0);

                switch (eleccion)
                {
                    case 1:
                        AñadirCita(paciente);
                        break;
                    case 2:
                        ModificarCita(paciente);
                        break;
                    case 3:
                        BorrarCita(paciente);
                        break;
                    case 4:
                        LeerCita(paciente);
                        break;
                    case 0:
                        salir = false;
                        break;
                }
            }
        }

        static void AñadirCita(Paciente paciente)
        {
            int eleccion = 0;
            Cita cita;
            DateTime fechaCorregida;

            fechaCorregida = ComprobarPosicionDeFecha();

            cita = new Cita(paciente, paciente.medico, fechaCorregida);

            Console.WriteLine("Escribe el diagnostico y el tratamiento");

            Console.Write("Prescribe el diagnostico\n> ");
            string diagnostico = Console.ReadLine();

            Console.Write("Prescribe el tratamiento\n> ");
            string tratamiento = Console.ReadLine();
            cita.RegistrarDiagnosticoYTratamiento(diagnostico, tratamiento);
            

            Console.WriteLine(@"Quieres añadir una nota?
1. Si
2. No");

            eleccion = LeerUnNumeroCorrecto(2, 1);
             
            if(eleccion == 1)
            {
                string nota = Console.ReadLine();
                cita.Notas.Add(nota);
            }

            paciente.RegistrarHistorial(cita);
        }

        static DateTime ComprobarPosicionDeFecha()
        {
            bool salir = true;
            DateTime fechaCorregida;

            Console.WriteLine("Escribe la fecha de esta manera AAAA/MM/dd hh:mm");

            while (salir)
            {
                string fecha = Console.ReadLine();
                if (DateTime.TryParseExact(fecha, "yyyy/MM/dd HH:mm", null, System.Globalization.DateTimeStyles.None, out fechaCorregida))
                {
                    salir = false;
                    return fechaCorregida;
                }
                else
                    Console.Write("No has escrito bien la fecha intentalo de nuevo:\n> ");
            }

            return new DateTime();
        }

        static void ModificarCita(Paciente paciente)
        {
            int eleccion = 0;
            DateTime fecha = new DateTime();
            DateTime nuevaFecha = new DateTime();
            if(paciente.HistorialMedico.Count() == 0)
            {
                Console.WriteLine("No hay citas para modificar");
                return;
            }
            LeerCita(paciente);

            Console.WriteLine("Escribe la fecha de la cita para cambiar");
            fecha = ComprobarPosicionDeFecha();

            Console.WriteLine(@"Que quieres modificar?
1. Fecha
2. Medico
3. agregar nota");

            eleccion = LeerUnNumeroCorrecto(3,1);

            switch (eleccion)
            {
                case 1:
                    nuevaFecha = ComprobarPosicionDeFecha();
                    Cita cita = paciente.HistorialMedico.FirstOrDefault(c => c.Fecha == fecha);
                    cita.ModificarFecha(nuevaFecha);
                    break;
                case 2:
                    ListarMedicos();
                    Console.WriteLine("Dime el dni del medico");
                    Medico medico = LeerDNIExacto<Medico>();

                    paciente.HistorialMedico.FirstOrDefault(c => c.Fecha == fecha).ModificarMedico(medico);
                    paciente.AñadirMedico(medico);
                    break;
                case 3:
                    string nota = Console.ReadLine();
                    paciente.HistorialMedico.FirstOrDefault(c => c.Fecha == fecha).AgregarNota(nota);
                    break;
            }

        }

        static void BorrarCita(Paciente paciente)
        {
            bool salir = true;
            Cita cita;
            DateTime fechaCorregida;

            if (paciente.HistorialMedico.Count() == 0)
            {
                Console.WriteLine("No hay citas para borrar");
                return;
            }

            Console.WriteLine("Cual de las citas quieres borrar? Escribe la fecha");
            LeerCita(paciente);

            while (salir)
            {
                fechaCorregida = ComprobarPosicionDeFecha();

                cita = paciente.HistorialMedico.FirstOrDefault(c => c.Fecha == fechaCorregida);

                if (cita != null)
                {
                    paciente.CancelarCita(fechaCorregida);
                    salir = false;
                }
                else
                    Console.WriteLine("No existe esa fecha prueba de nuevo");
            }
        }

        static void LeerCita(Paciente paciente)
        {
            int indice = 0;
            foreach(Cita cita in paciente.HistorialMedico)
            {
                Console.WriteLine(cita.ToString());
                if (cita.Notas.Count() != 0)
                    Console.Write(cita.Notas[indice]);
                indice++;
            }
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
                    Console.WriteLine("No se encontraron personas con el mismo DNI.");

                if(PersonasEnElHospital.OfType<P>().Count() <= 0)
                    salir = false;
            }

            return null;
        }
    }
}
