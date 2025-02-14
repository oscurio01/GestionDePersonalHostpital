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
        public string HorarioDeTrabajo { get; set; }
        public PersonalAdministrativo(Persona persona, CargoAdministrativo cargoAdministrativo, string departamento, string horarioDeTrabajo) : base(persona)
        {
            CargoAdministrativo = cargoAdministrativo;
            Departamento = departamento;
            HorarioDeTrabajo = horarioDeTrabajo;
        }

        public override string ToString()
        {
            return base.ToString() + $", Cargo administrarivo:{CargoAdministrativo}, departamento: {Departamento}, Horario de trabajo {HorarioDeTrabajo}";
        }
    }
}
