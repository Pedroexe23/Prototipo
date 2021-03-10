using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototipo.Models.DAO
{
    public class PersonaDAO
    {
        private static List<Personas> Persona = new List<Personas>();
        public void GuaradarPersona(Personas p)
        {
            Persona.Add(p);
        }
        public List<Personas> GetPersonas()
        {
            return Persona;
        }

        public void BorrarPersona( )
        {
            
            for (int i = 0; i < Persona.Count() ; i++)
            {
                Personas p = new Personas();
                p.Rut = Persona[i].Rut;
                p.Nombre = Persona[i].Nombre;
                p.Apellido = Persona[i].Apellido;
                p.Registro = Persona[i].Registro;
                
                Persona.Remove(p);
                Persona.Clear();

            }
        }
      
    }
}