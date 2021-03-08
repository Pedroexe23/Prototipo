using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prototipo.Models.DAO
{
    public class DocumentoDAO
    {
        static List<Documento> Documentos = new List<Documento>();
        public void GuaradarDocumento(Documento d)
        {
            Documentos.Add(d);
        }
        public List<Documento> GetDocumento()
        {
            return Documentos;
        }

        public void BorrarDocumento()
        {
            List<Documento> limpiar = GetDocumento();
            for (int i = 0; i >= limpiar.Count(); i++)
            {
                if (limpiar.Count != 0)
                {
                    Documento D = new Documento();
                    D.Id_Documento = limpiar[i].Id_Documento;
                    D.Archivo = limpiar[i].Archivo;
                    D.Fecha = limpiar[i].Fecha;
                    D.Tamaño = limpiar[i].Tamaño;
                    D.Tipo = limpiar[i].Tipo;
                    Documentos.Remove(D);
                }


            }

        }


    }
}