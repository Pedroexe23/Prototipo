using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prototipo.Models;
using Prototipo.Models.DAO;

namespace Prototipo.Controllers
{
    public class PersonasController : Controller
    {
        private Models.Prototipo db = new Models.Prototipo();
        private PersonaDAO personaDAO = new PersonaDAO();
        private Personas Persona = new Personas();
        private Documento documento = new Documento();
        private DocumentoDAO documentoDAO = new DocumentoDAO();
        private SqlConnection conexion = new SqlConnection("data source=TECNO-PRACTI;initial catalog=Municipalidad;integrated security=True;");

        // GET: Personas
        public ActionResult Index()
        {
            return View(db.Personas.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Rut,Nombre,Apellido")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Personas.Add(personas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personas);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Rut,Nombre,Apellido")] Personas personas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personas);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personas personas = db.Personas.Find(id);
            if (personas == null)
            {
                return HttpNotFound();
            }
            return View(personas);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Personas personas = db.Personas.Find(id);
            db.Personas.Remove(personas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Localizar()
        {
            int cont = 0;
            
            List<Personas> buscados = personaDAO.GetPersonas();

            for (int i = 0; i < buscados.Count(); i++)
            {
                cont = cont + 1;



            }
            if (cont == 0)
            {
                return View();
            }
            else
            {
                return View(buscados.ToList());
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Localizar(FormCollection formCollection)
        {
            int on = 0;
            int count = 0;
            PersonaDAO personaDAO = new PersonaDAO();
            personaDAO.BorrarPersona();
            String Nombre = formCollection["Nombretxt"];
            String Apellido = formCollection["Apellidotxt"];
            String Rut = formCollection["Ruttxt"];
            foreach (var item in db.Personas)
            {
                Personas p = new Personas();
                p.Nombre = item.Nombre;
                p.Rut = item.Rut;
                p.Apellido = item.Apellido;
                if (p.Nombre.Equals(Nombre) && p.Rut.Equals(Rut))
                {
                    on = 1;
                   
                    Persona.Nombre = Nombre;
                    Persona.Rut = Rut;
                    Persona.Apellido = Apellido;
                    personaDAO.GuaradarPersona(Persona);

                }
                else
                {
                    count = count + 1;
                    if (on == 1)
                    {
                        break;
                    }
                }


            }
            if (count != db.Personas.Count())
            {
                return Redirect("Localizar");
            }
            else
            {


                for (int i = 0; i == 30; i++)
                {
                    ViewBag.Message = "No Existe esa persona";
                }
                personaDAO.BorrarPersona();
                return View();
            }



        }

        public ActionResult Documento()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Documento(HttpPostedFileBase insertar)
        {
            //personaDAO.BorrarPersona();
            //documentoDAO.BorrarDocumento();
           
            
            if (insertar== null || insertar.ContentLength==0)
            {
                return ViewBag.Message = "Archivo Vacio";
            }
            else
            {
                try
                {
                    String fileName = Path.GetFileName(insertar.FileName);

                    String folderpath = Path.Combine(Server.MapPath("~/DocumentosG"), fileName);
                    insertar.SaveAs(folderpath);

                    String result = string.Empty;
                    String Fechas = DateTime.Now.Date.ToString("yyyy/MM/dd");

                    foreach (string strfile in Directory.GetFiles(Server.MapPath("~/DocumentosG")))
                    {
                            FileInfo fi = new FileInfo(strfile);
                             if (fi.Name.Equals(fileName))
                            {
                            Documento Doc = new Documento();
                            Doc.Archivo = fi.Name;
                            Doc.Tamaño = fi.Length;
                            Doc.Tipo = GetFileTypeByExtension(fi.Extension);
                            Doc.Fecha = DateTime.Parse(Fechas);
                            documentoDAO.GuaradarDocumento(Doc);
                            }
                             
                        }
                    List<Documento> Archivos = documentoDAO.GetDocumento();
                    int count = 0;
                    int id = 1;
                    int iddoc = 0;

                    foreach (var item in Archivos)
                    {
                        Documento Doc = new Documento();
                        Doc.Archivo = item.Archivo;
                        Doc.Tamaño = item.Tamaño;
                        Doc.Tipo = item.Tipo;
                        Doc.Fecha = DateTime.Parse(Fechas);
                        foreach (var items in db.Documento)
                        {
                            Documento doc = new Documento();
                            doc.Id_Documento = items.Id_Documento;
                            doc.Archivo = items.Archivo;
                            doc.Tamaño = items.Tamaño;
                            doc.Tipo = items.Tipo;
                            doc.Fecha = items.Fecha;
                            if (doc.Archivo.Equals(Doc.Archivo) && doc.Tamaño== Doc.Tamaño && doc.Tipo.Equals(Doc.Tipo))
                            {
                                conexion.Close();
                                conexion.Open();
                                String Cadena = " update Documento set Fecha=" + "'" + Doc.Fecha + "'where Id_documento=" + doc.Id_Documento + "";
                                SqlCommand command = new SqlCommand(Cadena, conexion);
                                int cant;
                                cant = command.ExecuteNonQuery();
                                conexion.Close();
                                break;
                            }
                            
                            else
                            {
                                count = count + 1;
                                id = id + 1;
                            }
                            if (db.Documento.Count()!=0)
                            {
                                iddoc = doc.Id_Documento;
                            }


                        }
                        if (count == db.Documento.Count())
                        {
                            Doc.Id_Documento = id+iddoc;
                            db.Documento.Add(Doc);
                            db.SaveChanges();
                        }
                    }
                  

                    }
                catch (Exception )
                { 
                
                }
                }

            return View();
        }
        private string GetFileTypeByExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".docx":
                case ".doc":
                    return "Microsoft Word Document";

                case ".pptx":
                    return " Microsoft PowerPoint";

                case ".pdf":
                    return "Achivo PDF";

                case ".xlsx":
                case ".xls":
                case ".csv":
                    return "Microsoft Excel Document";

                default:
                    return "Unknown";
            }
        }
    }
}
