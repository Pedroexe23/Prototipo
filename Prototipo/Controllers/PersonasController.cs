using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            PersonaDAO personaDAO = new PersonaDAO();
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
                    Personas Persona = new Personas();
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
            PersonaDAO personaDAO = new PersonaDAO();
            List<Personas> personas =personaDAO.GetPersonas();
            
            return View();
        }

        [HttpPost]
        public ActionResult Documento( string insertar)
        {
            return View();
        }

        public ActionResult guardado()
        {
            return View();
        }



    }
}
