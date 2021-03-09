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

        public void EliminarDocumento(Documento d)
        {
            Documentos.Remove(d);
        }

        public void BorrartodolosDocumentos()
        {
            Documentos.Clear();

        }


    }
}