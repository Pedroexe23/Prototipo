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
            List<Registro> limpiar = GetRegistros();
            for (int i = 0; i == limpiar.Count(); i++)
            {
                if (limpiar.Count!=0)
                {
                    Registro R = new Registro();
                    R.id_registro = limpiar[i].id_registro;
                    R.Fk_Id_Documento= limpiar[i].Fk_Id_Documento;
                    R.Fk_RUT = limpiar[i].Fk_RUT;
                    registros.Remove(R);

                }

            }
        }
    }
}