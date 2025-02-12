using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    internal class Program
    {
        static List<Persona> personasEnElHospital = new List<Persona>();
        static void Main(string[] args)
        {
            /*
            Crear una aplicación de consola, con un menú, que permita: 

            • Dar de alta un medico 

            • Dar de alta un paciente, para un médico concreto 

            . Dar de alta personal administrativo

            • Listar los médicos 

            • Listar los pacientes de un medico 

            • Eliminar a un paciente 

            • Ver la lista de personas del hospital 
            */


        }

        void DarAltaMedico()
        {

        }

        void DarAltaPaciente()
        {

        }

        void DarAltaPersonal()
        {

        }

        void ListarMedicos()
        {
            foreach(var persona in personasEnElHospital)
            {
                if(persona is Medico)
                    Console.WriteLine(persona.ToString());
            }
        }

        void ListarPacientesDeMedico(Medico medico)
        {
            foreach (var persona in medico.Pacientes)
            {

            }
        }
    }
}
