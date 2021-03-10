using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototipo.Models.DAO
{
    public class PersonaDAO
    {
        static List<Personas> Persona = new List<Personas>();
        public void GuaradarPersona( Personas p)
        {
            Persona.Add(p);
        }
        public List<Personas> GetPersonas()
        {
            return Persona;
        }

        public void BorrarPersona()
        {
             Persona = GetPersonas();
            for (int i = 0; i >= Persona.Count(); i++)
            {
                if (Persona.Count!=0)
                {
                    Personas p = new Personas();
                    p.Nombre = Persona[i].Nombre;
                    p.Apellido = Persona[i].Apellido;
                    p.Rut = Persona[i].Rut;
                    Persona.Remove(p);
                }
                

            }
           
        }

    }
}