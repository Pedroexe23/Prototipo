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
            List<Personas> limpiar = GetPersonas();
            for (int i = 0; i >= limpiar.Count(); i++)
            {
                if (limpiar.Count!=0)
                {
                    Personas p = new Personas();
                    p.Nombre = limpiar[i].Nombre;
                    p.Apellido = limpiar[i].Apellido;
                    p.Rut = limpiar[i].Rut;
                    Persona.Remove(p);
                }
                

            }
           
        }

    }
}