using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototipo.Models.DAO
{
    public class RegistroDAO
    {
        static List<Registro> registros = new List<Registro>();
        public void guardarRegistro(Registro r)
        {
            registros.Add(r);
        }
        public List<Registro> GetRegistros()
        {
            return registros;

        }
        public void BorrarRegistro()
        {
            for (int i = 0; i < registros.Count(); i++)
            {
                
                    Registro r = new Registro();
                    r.Personas = registros[i].Personas;
                    r.Documento = registros[i].Documento;
                    r.id_registro = registros[i].id_registro;
                    r.Fk_Id_Documento = registros[i].Fk_Id_Documento;
                    r.Fk_RUT = registros[i].Fk_RUT;
                    registros.Remove(r);
                    registros.Clear();
                
                

            }
        }
    }
}