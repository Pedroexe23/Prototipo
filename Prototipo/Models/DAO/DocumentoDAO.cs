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

        public void EliminarDocumento()
        {
           
            for (int i = 0; i >= Documentos.Count(); i++)
            {
              
                    Documento d = new Documento();
                    d.Id_Documento = Documentos[i].Id_Documento;
                    d.Archivo = Documentos[i].Archivo;
                    d.Fecha= Documentos[i].Fecha;
                    d.Tamaño = Documentos[i].Tamaño;
                    d.Tipo = Documentos[i].Tipo;
                    d.Registro = Documentos[i].Registro;
                    Documentos.Remove(d);
                    Documentos.Clear();
                


            }
   
        }

        public void BorrartodolosDocumentos()
        {
            Documentos.Clear();

        }

        


    }
}