using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDePersonalHostpital
{
    enum CargoAdministrativo { recepcionista, coordinador, secretario}
    internal class PersonalAdministrativo : Persona
    {
        public CargoAdministrativo CargoAdministrativo { get; set; }
        public string Departamento { get; set; }
        string HorarioDeTrabajo { get; set; }
        public PersonalAdministrativo(int dNI, int edad, string nombre, string apellido, CargoAdministrativo cargoAdministrativo, string departamento, string horarioDeTrabajo) : base(dNI, edad, nombre, apellido)
        {
            CargoAdministrativo = cargoAdministrativo;
            Departamento = departamento;
            HorarioDeTrabajo = horarioDeTrabajo;
        }
    }
}
