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
                return View();
        }

        private PersonaDAO dao = new PersonaDAO();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Localizar(FormCollection formCollection)
        {
         /* se crear una lista de personas del archivo temporal Persona DAO  */   
            List<Personas> personas = new List<Personas>();
            int on = 0;
            int count = 0;
            /*en caso que se almaceno alguna persona en PersonaDAO va a ser almacenada en la lista personas  */
            if (new PersonaDAO().GetPersonas().Count!=0)
            {
                personas = new PersonaDAO().GetPersonas();
            }

            /*se toma los datos almacenado en la vista  en variables Strings llamados "Nombre", "Apellido" Y "Rut"   */
            String Nombre = formCollection["Nombretxt"];
            String Apellido = formCollection["Apellidotxt"];
            String Rut = formCollection["Ruttxt"];
            /* si el Nombre, o el Rut o el Apellido esten vacios se enviara un mensaje  a la vista */
            if (String.IsNullOrEmpty(Nombre) || String.IsNullOrEmpty(Apellido) || String.IsNullOrEmpty(Rut))
            {
                ViewBag.Message = "Debes ingresar el nombre  o el apellido o el rut";
                return View();
            }
            /* si no pasara por un foreach que se va a mostrar los datos almacenados en la base de datos persona por persona   */
            else
            {
                foreach (var item in db.Personas)
                {
                    Personas p = new Personas();
                    p.Nombre = item.Nombre;
                    p.Rut = item.Rut;
                    p.Apellido = item.Apellido;
                    /*si el nombre del String es igual al Nombre de la base de datos tambien 
                     * el Rut del String es igual al Rut de la base de datos y
                     * el Apellido del String es igual al Apellido de la base de datos entonces la  variable on sera 1
                     * tomara los datos  y se creara un segundo objecto de Persona */
                    if (p.Nombre.Equals(Nombre) && p.Rut.Equals(Rut) && p.Apellido.Equals(Apellido))
                    {
                        on = 1;
                        Personas Persona = new Personas();
                        Persona.Nombre = Nombre;
                        Persona.Rut = Rut;
                        Persona.Apellido = Apellido;
                        /* Si la lista personas esta vacia entonces el objecto Persona se almacenada,
                         * por que sera la primera vez que sera almacenado en la clase de almacenamiento temporal  PersonaDAO
                         */
                        if (personas.Count == 0)
                        {
                            new PersonaDAO().GuaradarPersona(Persona);
                        }
                        /*Si no entonces ser extraera los datos de la lista personas   */
                        else
                        {

                            for (int k = 0; k <personas.Count() ; k++)

                            {
                                Personas personas_guardada = new Personas();
                                personas_guardada.Nombre = personas[k].Nombre;
                                personas_guardada.Rut = personas[k].Rut;
                                personas_guardada.Apellido = personas[k].Apellido;

                                /*si los nombre , los apellidos y los  ruts no son iguales entonces se va a borrar lo que se  almaceno en el almacenamiento temporal de PersonaDAO
                                 * y se va a guardar  el nuevo objecto en el almacenamiento temporal esto pasara si ya se almaceno anteriormente y se ira a la vista ademas se va mostrar
                                 * con el nuevo objecto  */
                                if (!personas_guardada.Nombre.Equals(Nombre)&&!personas_guardada.Apellido.Equals(Apellido) &&!personas_guardada.Rut.Equals(Rut))
                                {
                                    dao.BorrarPersona();
                                    new PersonaDAO().GuaradarPersona(Persona);
                                    return View();
                                }
                                /*Pero si son iguales se pasara por alto y no se hara nada */
                                else
                                {
                                    break;
                                }
                               
                            }
                        }
                        
                    }
                    /* sino si se ha encontrado el nombre por primera vez y a la vez se  guardado en el almacenamiento temporal  tomara un break y pasara al siguiente Persona */
                    else if (on == 1)
                    {
                            break; 
                    }
                    /* Si no se ha encotrado a la persona se va acumular la variable count*/
                    else
                    {
                        count+= 1;  
                    }


                }
                /* si se encotrado a la persona por primera vez se va a mostrar en la vista */
                if (count != db.Personas.Count())
                {
                    return View();

                }
                /*si no se ha encotrado a la persona se enviara un mensaje a la vista */
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
        /*es caso de que use  reintentar en el navegador de hara este proceso de inicio se borrara todos los documentos guardados en el almacenamiento temporal de  DocuemtosDAO  */
            documentoDAO.BorrartodolosDocumentos();
            /*se creara una lista de personas y se extraera con lo que sea almacendado en PersonaDAO*/
            List<Personas> personas = new PersonaDAO().GetPersonas();
            /* se a  mostrar en foreach el objecto Persona que esta en la lista personas y se usara dentro del objecto la variable Rut por que es  una variable de clave primaria*/
            foreach (var item in personas)
            {
                Personas person = new Personas();
                person.Rut = item.Rut;
                /* se hara  un segundo foreach pero ahora con base de datos de el objecto Registro que se extraera los datos  de Fk_Id_Documento y Fk_Rut  y
                 * sera  almacenados en el objecto Registro con el nombre R */
                foreach (var items in db.Registro)
                {
                    Registro R = new Registro();
                    R.Fk_Id_Documento = items.Fk_Id_Documento;
                    R.Fk_RUT = items.Fk_RUT;
                    /* si la variable  de la clave primaria Rut es  igual a la clave foranea Fk_RUT entonces se  guardara en el almacenamiento de temporal de RegistroDAO 
                       sino se  pasara de largo hasta que la clave primaria Rut sea igual a la clave foranea Fk_RUT  */
                    if (person.Rut.Equals(R.Fk_RUT))
                    {
                        registroDAO.guardarRegistro(R);
                    }

                }
            }
            /* se creara una lista de Registros llamada registros que sera extraidos del almacenamieto temporal RegistroDAO  */
            List<Registro> registros = registroDAO.GetRegistros();
            /* si la lista registros esta vacia entonces ira la vista con datos de Documentos Vacios 
             * sino se creara un foreach que mostrara los datos de la lista de registro   */
            if (registros.Count!=0)
            {
                foreach (var item in registros)
                {
                    Registro registro = new Registro();
                    registro.Fk_RUT = item.Fk_RUT;
                    registro.Fk_Id_Documento = item.Fk_Id_Documento;
                    /* se va crear un segundo foreach que mostrara los id de los documentos que son una clave primara de la clase Documentos que estan en la base de datos */
                    foreach (var items in db.Documento)
                    {
                        Documento documento = new Documento();
                        documento.Id_Documento = items.Id_Documento;
                        /* si la clave primaria de los id de los documentos es igual la clave foranea Fk_Id_Documento entoces los demas datos
                         * que estaban la clase Documentos que estan en la base de datos seran guardados en el almacenamieto temporal de DocumentoDAO 
                         y se  borrara todo los datos del almacenamieto temporal RegistrosDAO ademas que  iran a la vista con los datos del DocumentoDAO  */
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
            /*si no se ha subido ningun documento se enviara un mensaje que ira a la vista */
            

            if (insertar == null || insertar.ContentLength == 0)
            {
                ViewBag.Message = "Archivo Vacio, Por favor ingrese un archivo";
                return View();
            }
            else
            {
                /* pero si se subido un documento se creara tres listas una lista para la clase registro y dos listas de la clase Documento
                 * ademas 4 variables int
                 * la varable onRE sirve para guardar en la base de datos el nuevo objecto Registro, 
                 * la variable onDo es para guardar en la base de datos el nuevo documento,
                 * la variable Do_Re es para guardar el nuevo objecto Registro o el nuevo objecto Documento y
                 * la variable Idregistro es para agregar para clave foranea de Fk_ID_Documento del nuevo objecto Registro
                 * */
                List<Registro> Guardar_registros = new List<Registro>();
                List<Documento> Guardar_documentos = new List<Documento>();
                int Do_Re = 0;
                int onRE = 0;
                int onDo = 0;
                int idregistro = 0;
                /* La lista  Archivos es una lista que esta extraida del Almacenmiento temporal  de DocumentoDAO */
                List<Documento> Archivos = documentoDAO.GetDocumento();
                /* y se limpia por Completo */
                Archivos.Clear();
                /*se elimnia todo el contenido de los documentoDAO*/
                documentoDAO.BorrartodolosDocumentos();
                /*se toma el archivo que esta en el summit*/
                String fileName = Path.GetFileName(insertar.FileName);
                /*lo direcciona a la carpeta DocumentosG con su nombre*/
                String folderpath = Path.Combine(Server.MapPath("~/DocumentosG"), fileName);
                /* y se  guarda en la carpeta DocumentosG */
                insertar.SaveAs(folderpath);

                /* la variable Fechas extrae de la fecha de hoy     */
                String Fechas = DateTime.Now.Date.ToString("yyyy/MM/dd");
                /* se hace un foreach con la informacion que esta en los documentos almacenados en la carpeta DocumentosG   */
                foreach (string strfile in Directory.GetFiles(Server.MapPath("~/DocumentosG")))
                {
                    /* se usara la clase FileInfo para extraer la informacion del documento  */
                    FileInfo fi = new FileInfo(strfile);
                    /* si el nombre del documento es igual a nombre del documento que hemos subido entonces se vas a extraer 
                     * el nombre, el tamaño, la extension y la fecha del el dia que se subio yse  guardara en el archivo temporal de documentoDAO
                     * para mas informacion de la extension dirijase al metodo GetFileTypeByExtension */
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
               /* La lista Archivos extrae los nuevos datos que se ha almacenado en el almacenamieto temporal de DocumentoDAO
                  y se crea  nuevas variables
                  la variable count que es para guardar los documentos y  los registros, 
                  la variable id es para agregar una id a los documentos la base de datos en la parte esta vacia por defectos  se  usara la  id para asignar a la tabla de documentos,
                  la variable iddoc es igual que  la id pero con una diferencia que se agregara el ultimo id del documeto mas con la id  ej: si la ultima id es 4 iddoc tomara ese id de documento 
                  y se  sumara con la variable id que por defecto es 1 y el nuevo documento con una id sera 5 ademas 
                  la variable on su funcion es saber si un documento ya esta registrado en la base de datos */
                Archivos = documentoDAO.GetDocumento();
                int count = 0;
                int id = 1;
                int iddoc = 0;
                int on ;
                /* se empieza a leer el documento que esta en la lista archivos   */
                foreach (var item in Archivos)
                {
                    on = 0;
                    Documento Doc = new Documento();
                    Doc.Archivo = item.Archivo;
                    Doc.Tamaño = item.Tamaño;
                    Doc.Tipo = item.Tipo;
                    Doc.Fecha = DateTime.Parse(Fechas);
                    /* se crea un foreach para leer los documentos que estan en la base de datos*/
                    foreach (var items in db.Documento)
                    {
                       
                        Documento doc = new Documento();
                        doc.Id_Documento = items.Id_Documento;
                        doc.Archivo = items.Archivo;
                        doc.Tamaño = items.Tamaño;
                        doc.Tipo = items.Tipo;
                        doc.Fecha = items.Fecha;
                        /*  si el nombre del documento que esta en la lista es igual al nombre del documento que esta en la base de datos
                         *  y el tamaño del documento que esta en la lista es igual al tamaño del documento que esta en la base de datos 
                         *  y el Tipo del documento que esta en la lista es igual al Tipo del documento que esta en la base de datos
                         *  entonces la id del documento sera guardado en la varible idregistro y la variable on sera 1 y seguira leyendo los documentos  */
                        if (doc.Archivo.Equals(Doc.Archivo) && doc.Tamaño == Doc.Tamaño && doc.Tipo.Equals(Doc.Tipo))
                        {
                            idregistro = doc.Id_Documento;
                            on = 1;
                            break;
                        }
                        /* sino si la varible on es igual a 1 entonces 
                         * si la cantidad de los Documentos de la base de datos no es igual a 0 entoces iddoc tomara la id del documento de la base de datos ademas 
                         * seguira leyendo */
                        else if (on == 1)
                        {
                            if (db.Documento.Count() != 0)
                            {
                                iddoc = doc.Id_Documento;
                            }
                            break;
                        }
                        /* sino entonces 
                         * si la cantidad de los Documentos de la base de datos no es igual a 0 entoces iddoc tomara la id del documento de la base de datos ademas la variable count se acumulara 
                         * seguira leyendo  */
                       
                        else
                        {
                            if (db.Documento.Count() != 0)
                            {
                                iddoc = doc.Id_Documento;
                            }
                            count += 1;

                        }

                        if (count >= db.Documento.Count())
                        {
                            Doc.Id_Documento = id + iddoc;
                            idregistro = Doc.Id_Documento;
                            Guardar_documentos.Add(Doc);
                            Do_Re +=  1;
                            onDo +=  1;
                        }

                    }

                    
                }
                List<Personas> Person = new PersonaDAO().GetPersonas();
                count = 0;
                on = 0;
                foreach (var item in Person)
                {
                    Personas personas1 = new Personas();
                    personas1.Rut = item.Rut;
                    foreach (var items in Archivos)
                    {
                        Documento dos = new Documento();
                        dos.Id_Documento = idregistro;
                        Registro re = new Registro();
                        re.Fk_Id_Documento = idregistro;
                        re.Fk_RUT = personas1.Rut;
                        foreach (var ite in db.Registro)
                        {
                            Registro registro = new Registro();
                            registro.Fk_RUT = ite.Fk_RUT;
                            registro.Fk_Id_Documento = ite.Fk_Id_Documento;
                            if (registro.Fk_RUT.Equals(re.Fk_RUT) && registro.Fk_Id_Documento == re.Fk_Id_Documento)
                            {
                                on += 1;
                                ViewBag.Message = "ya esta Registrado este documento para este usuario";
                                return Redirect("DocumentoRegistrados");
                            }
                            else if (on == 1)
                            {
                                break;
                            }
                            else
                            {
                                count += 1 ;
                            }
                        }
                        if (count == db.Registro.Count())
                        {
                            Guardar_registros.Add(re);
                            Do_Re +=  1;
                            onRE +=  1;
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
                List<Personas> personas = new PersonaDAO().GetPersonas();
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

                if (registros.Count != 0)
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
            


        }

        private DocumentoDAO DoDAO = new DocumentoDAO();
       
        public ActionResult Volver()
        {
            dao.BorrarPersona();
            DoDAO.BorrartodolosDocumentos();
            return Redirect("Localizar");
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
