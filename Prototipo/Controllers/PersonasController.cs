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
        private Documentacion db = new Documentacion();
        private PersonaDAO personaDAO = new PersonaDAO();
        private Personas Persona = new Personas();
        private Documento documento = new Documento();
        private DocumentoDAO documentoDAO = new DocumentoDAO();
        private Registro registro = new Registro();
        private RegistroDAO registroDAO = new RegistroDAO();
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
                cont += cont + 1;
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
            if (String.IsNullOrEmpty(Nombre) || String.IsNullOrEmpty(Apellido) || String.IsNullOrEmpty(Rut))
            {
                ViewBag.Message = "Debes ingresar el nombre  o el apellido o el rut";
                return View();
            }
            else
            {
                foreach (var item in db.Personas)
                {
                    Personas p = new Personas();
                    p.Nombre = item.Nombre;
                    p.Rut = item.Rut;
                    p.Apellido = item.Apellido;
                    if (p.Nombre.Equals(Nombre) && p.Rut.Equals(Rut) && p.Apellido.Equals(Apellido))
                    {
                        on = 1;

                        Persona.Nombre = Nombre;
                        Persona.Rut = Rut;
                        Persona.Apellido = Apellido;
                        personaDAO.GuaradarPersona(Persona);
                    }
                    else if (on == 1)
                    {
                            break; 
                    }
                    else 
                    {
                        count+= 1;  
                    }


                }

                if (count != db.Personas.Count())
                {
                    return Redirect("Localizar");
                }
                else
                {
                    ViewBag.Message = "No Existe esa persona ingrese el nombre , apellido y el Rut correpondiente";
                    return View();
                }

            }






        }
        [HttpGet]
        public ActionResult DocumentoRegistrados()
        {
            documentoDAO.EliminarDocumento();
            
          
            List<Personas> personas = personaDAO.GetPersonas();
            foreach (var item in personas)
            {
                Personas person = new Personas();
                person.Rut = item.Rut;
                foreach (var items in db.Registro)
                {
                    Registro R = new Registro();
                    R.Fk_Id_Documento = items.Fk_Id_Documento;
                    R.Fk_RUT = items.Fk_RUT;
                    if (person.Rut.Equals(R.Fk_RUT))
                    {
                        registroDAO.guardarRegistro(R);
                    }

                }
            }
            List<Registro> registros = registroDAO.GetRegistros();

            if (registros.Count!=0)
            {
                foreach (var item in registros)
                {
                    Registro registro = new Registro();
                    registro.Fk_RUT = item.Fk_RUT;
                    registro.Fk_Id_Documento = item.Fk_Id_Documento;
                    foreach (var items in db.Documento)
                    {
                        Documento documento = new Documento();
                        documento.Id_Documento = items.Id_Documento;
                        if (documento.Id_Documento == registro.Fk_Id_Documento)
                        {
                            documento.Archivo = items.Archivo;
                            documento.Tamaño = items.Tamaño;
                            documento.Tipo = items.Tipo;
                            documento.Fecha = items.Fecha;
                            documentoDAO.GuaradarDocumento(documento);
                        }
                    }
                }
                registroDAO.BorrarRegistro();
            }
           
            return View();
        }

        [HttpPost]
        public ActionResult DocumentoRegistrados(HttpPostedFileBase insertar)
        {
            
            

            if (insertar == null || insertar.ContentLength == 0)
            {
                ViewBag.Message = "Archivo Vacio, Por favor ingrese un archivo";
                return View();
            }
            else
            {
                List<Registro> Guardar_registros = new List<Registro>();
                List<Documento> Guardar_documentos = new List<Documento>();
                int Do_Re = 0;
                int onRE = 0;
                int onDo = 0;
                int idregistro = 0;
                List<Documento> Archivos = documentoDAO.GetDocumento();
                Archivos.Clear();
                documentoDAO.BorrartodolosDocumentos();

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
                Archivos = documentoDAO.GetDocumento();
                int count = 0;
                int id = 1;
                int iddoc = 0;
                int on = 1;
                foreach (var item in Archivos)
                {
                    Documento Doc = new Documento();
                    Doc.Archivo = item.Archivo;
                    Doc.Tamaño = item.Tamaño;
                    Doc.Tipo = item.Tipo;
                    Doc.Fecha = DateTime.Parse(Fechas);
                    foreach (var items in db.Documento)
                    {
                        on = 0;
                        Documento doc = new Documento();
                        doc.Id_Documento = items.Id_Documento;
                        doc.Archivo = items.Archivo;
                        doc.Tamaño = items.Tamaño;
                        doc.Tipo = items.Tipo;
                        doc.Fecha = items.Fecha;
                        if (doc.Archivo.Equals(Doc.Archivo) && doc.Tamaño == Doc.Tamaño && doc.Tipo.Equals(Doc.Tipo))
                        {
                            idregistro = doc.Id_Documento;
                            on = 1;
                            conexion.Close();
                            conexion.Open();
                            String Cadena = " update Documento set Fecha=" + "'" + Doc.Fecha + "'where Id_documento=" + doc.Id_Documento + "";
                            SqlCommand command = new SqlCommand(Cadena, conexion);
                            int cant;
                            cant = command.ExecuteNonQuery();
                            conexion.Close();
                            break;
                        }

                        else if (on == 1)
                        {
                            if (db.Documento.Count() != 0)
                            {
                                iddoc = doc.Id_Documento;
                            }
                            break;
                        }
                        else
                        {
                            if (db.Documento.Count() != 0)
                            {
                                iddoc = doc.Id_Documento;
                            }
                            count = count + 1;

                        }

                        if (count >= db.Documento.Count())
                        {
                            Doc.Id_Documento = id + iddoc;
                            idregistro = Doc.Id_Documento;
                            Guardar_documentos.Add(Doc);
                            Do_Re = Do_Re + 1;
                            onDo = onDo + 1;
                        }

                    }

                    
                }
                List<Personas> Person = personaDAO.GetPersonas();
                count = 0;
                on = 0;
                foreach (var item in Person)
                {
                    Personas personas = new Personas();
                    personas.Rut = item.Rut;
                    foreach (var items in Archivos)
                    {
                        Documento dos = new Documento();
                        dos.Id_Documento = idregistro;
                        Registro re = new Registro();
                        re.Fk_Id_Documento = idregistro;
                        re.Fk_RUT = personas.Rut;
                        foreach (var ite in db.Registro)
                        {
                            Registro registro = new Registro();
                            registro.Fk_RUT = ite.Fk_RUT;
                            registro.Fk_Id_Documento = ite.Fk_Id_Documento;
                            if (registro.Fk_RUT.Equals(re.Fk_RUT) && registro.Fk_Id_Documento == re.Fk_Id_Documento)
                            {
                                on = on + 1;
                                ViewBag.Message = "ya esta Registrado este documento para este usuario";
                                return Redirect("DocumentoRegistrados");
                            }
                            else if (on == 1)
                            {
                                break;
                            }
                            else
                            {
                                count = count + 1;
                            }
                        }
                        if (count == db.Registro.Count())
                        {
                            Guardar_registros.Add(re);
                            Do_Re = Do_Re + 1;
                            onRE = onRE + 1;
                        }
                    }
                }

                if (Do_Re==1)
                {
                    if (onDo==1)
                    {
                        db.Documento.AddRange(Guardar_documentos);
                    }
                    else if (onRE==1)
                    {
                        db.Registro.AddRange(Guardar_registros);
                    }
                   
                   
                }
                else if (Do_Re==2)
                {
                    db.Documento.AddRange(Guardar_documentos);
                    db.Registro.AddRange(Guardar_registros);
                }
                db.SaveChanges();
                documentoDAO.BorrartodolosDocumentos();
            }

            return View("DocumentoRegistrados");
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
