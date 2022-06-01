using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detalle_Exquisito.Models;
using Detalle_Exquisito.Models.Personas_Modelos;
using Detalle_Exquisito.Models.Cargos_Models;
using Detalle_Exquisito.Controllers;
using Detalle_Exquisito.Filtros;

namespace Detalle_Exquisito.Controllers
{
    public class PersonasController : Controller
    {
        // GET: Personas
        [Autorizacion_usuario(IdOperacion: 1)]
        public ActionResult Personas_Lista(string consulta="")
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            List<Tabla_Personas> list_personas;
            if (consulta.Length>0)
            {
                list_personas = (from c in bd.buscar_P(consulta)
                                 orderby c.nombre_Persona ascending
                                 select new Tabla_Personas
                                 {
                                     Id_Per = c.Id_Per,
                                     CI_Persona = c.CI_Persona,
                                     nombre_Persona = c.nombre_Persona,
                                     apellido_Paterno = c.apellido_Paterno_cliente,
                                     apellido_Materno = c.apellido_Materno_cliente,
                                     nro_telefono = c.nro_telefono,
                                     tipo = c.tipo
                                 }).ToList();
            }
            else
            {
                list_personas = (from c in bd.Persona
                                 orderby c.nombre_Persona ascending
                                 select new Tabla_Personas
                                 {
                                     Id_Per = c.Id_Per,
                                     CI_Persona = c.CI_Persona,
                                     nombre_Persona = c.nombre_Persona,
                                     apellido_Paterno = c.apellido_Paterno_cliente,
                                     apellido_Materno = c.apellido_Materno_cliente,
                                     nro_telefono = c.nro_telefono,
                                     tipo = c.tipo
                                 }).ToList();
            }

            List<bool> registrado = new List<bool>();

            foreach (var usuarios in list_personas)
            {
                var oUser = (from d in bd.Usuario
                             where d.ID_Persona == usuarios.Id_Per
                             select d.ID_Persona).FirstOrDefault();
                if (oUser!=0)
                {
                    registrado.Add(true);
                }
                else
                {
                    registrado.Add(false);
                }
            }
            ViewBag.registrado = registrado;
            return View(list_personas);
        }

        [Autorizacion_usuario(IdOperacion: 2)]
        public ActionResult AgregarPersonas()
        {
            lista_Cargos();
            return View();
        }

        [HttpPost]
        public ActionResult AgregarPersonas(PersonasModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                    {
                        var Datos_Tabla = new Persona();
                        var Atr_Empleado = new Empleado();
                        //Datos_Tabla.Id_Per = modelo.Id_Per;
                        Datos_Tabla.CI_Persona = modelo.CI_Persona;
                        Datos_Tabla.nombre_Persona = modelo.nombre_Persona;
                        Datos_Tabla.apellido_Paterno_cliente = modelo.apellido_Paterno;
                        Datos_Tabla.apellido_Materno_cliente = modelo.apellido_Materno;
                        Datos_Tabla.nro_telefono = modelo.nro_telefono;
                        Datos_Tabla.tipo = modelo.tipo; 
                        for (int i = 0; i < lista_personas().Count; i++)
                        {
                            if (modelo.CI_Persona == lista_personas()[i].CI_Persona)
                            {
                                HomeController.AlertaGlobal = 3;
                                //return Redirect("/Personas/AgregarPersonas");
                                return View();
                            }
                        }
                        bd.Persona.Add(Datos_Tabla);
                        bd.SaveChanges();
                        if (modelo.id_cargoEmpleado != 0)
                        {
                            Atr_Empleado.Id_Per = obtenerID();
                            Atr_Empleado.ID_EmCar = modelo.id_cargoEmpleado;
                            bd.Empleado.Add(Atr_Empleado);
                            bd.SaveChanges();
                        }
                    }
                    return Redirect("/Personas/Personas_Lista");
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Autorizacion_usuario(IdOperacion: 3)]
        public ActionResult Editar_Person(int id)
        {
            PersonasModel modelo = new PersonasModel();

            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.Persona.Find(id);

                modelo.Id_Per = atributos_Tabla.Id_Per;
                modelo.CI_Persona = atributos_Tabla.CI_Persona;
                modelo.nombre_Persona = atributos_Tabla.nombre_Persona;
                modelo.apellido_Paterno = atributos_Tabla.apellido_Paterno_cliente;
                modelo.apellido_Materno = atributos_Tabla.apellido_Materno_cliente;
                modelo.nro_telefono = atributos_Tabla.nro_telefono;
                modelo.tipo = atributos_Tabla.tipo;
                if (bd.Empleado.Find(id)!=null)
                {
                    var atributos_empleado = bd.Empleado.Find(id);
                    modelo.id_cargoEmpleado = Convert.ToInt32(atributos_empleado.ID_EmCar);
                }
            }
            lista_Cargos();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Editar_Person(PersonasModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                    {
                        var Datos_Tabla = bd.Persona.Find(modelo.Id_Per);
                        var Datos_T_Empleado = bd.Empleado.Find(modelo.Id_Per);
                        Datos_Tabla.Id_Per = modelo.Id_Per;
                        Datos_Tabla.CI_Persona = modelo.CI_Persona;
                        Datos_Tabla.nombre_Persona = modelo.nombre_Persona;
                        Datos_Tabla.apellido_Paterno_cliente = modelo.apellido_Paterno;
                        Datos_Tabla.apellido_Materno_cliente = modelo.apellido_Materno;
                        Datos_Tabla.nro_telefono = modelo.nro_telefono;
                        Datos_Tabla.tipo = modelo.tipo;

                        for (int i = 0; i < lista_personas().Count; i++)
                        {
                            if (modelo.CI_Persona == lista_personas()[i].CI_Persona)
                            {
                                HomeController.AlertaGlobal = 3;
                                return View(modelo);
                            }
                        }

                        bd.Entry(Datos_Tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();

                        if (modelo.id_cargoEmpleado > 0)
                        {
                            Datos_T_Empleado.Id_Per = modelo.Id_Per;
                            Datos_T_Empleado.ID_EmCar = modelo.id_cargoEmpleado;
                            bd.Entry(Datos_T_Empleado).State = System.Data.Entity.EntityState.Modified;
                            bd.SaveChanges();
                        }
                    }
                    return Redirect("/Personas/Personas_Lista");
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpGet]
        [Autorizacion_usuario(IdOperacion: 4)]
        public ActionResult Eliminar_Persona(int id)
        {
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.Persona.Find(id);
                if (atributos_Tabla != null)
                {
                    bd.Persona.Remove(atributos_Tabla);
                    bd.SaveChanges();
                }
                else
                {
                    return Redirect("/Personas/Personas_Lista");
                }
            }
            return Redirect("/Personas/Personas_Lista");
        }

        public static List<Tabla_Personas> lista_personas()
        {
            List<Tabla_Personas> list_personas;
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                list_personas = (from c in bd.Persona
                                 select new Tabla_Personas
                                 {
                                     Id_Per = c.Id_Per,
                                     CI_Persona = c.CI_Persona,
                                     nombre_Persona = c.nombre_Persona,
                                     apellido_Paterno = c.apellido_Paterno_cliente,
                                     apellido_Materno = c.apellido_Materno_cliente,
                                     nro_telefono = c.nro_telefono,
                                     tipo = c.tipo
                                 }).ToList();
            }
            return list_personas;
        }
        private int obtenerID()
        {
            BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities();
            var ope = from op in bd.Persona
                      orderby op.Id_Per descending
                      select op.Id_Per;
            int ID;
            try
            {
                ID = ope.First();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ID;
        }
        private void lista_Cargos()
        {
            List<tbl_Cargo> list_cargos;
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                list_cargos = (from c in bd.Cargo_Empleo
                                 select new tbl_Cargo
                                 {
                                     ID_EmCar=c.ID_EmCar,
                                     Nombre_car=c.Nombre_car
                                 }).ToList();
            }
            List<SelectListItem> itemCargos = list_cargos.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre_car,
                    Value = d.ID_EmCar.ToString(),
                    Selected = false
                };
            });
            ViewBag.Cargos = itemCargos;
        }
    }
}