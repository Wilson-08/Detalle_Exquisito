using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Detalle_Exquisito.Filtros;
using Detalle_Exquisito.Models;
using Detalle_Exquisito.Models.Usuarios_Modelos;
using Detalle_Exquisito.Models.Personas_Modelos;

namespace Detalle_Exquisito.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [Autorizacion_usuario(IdOperacion: 4)]
        public ActionResult Index()
        {
            cargarRoles();
            return View(Lista_usuarios());
        }

        [Autorizacion_usuario(IdOperacion: 1)]
        public ActionResult Agregar_new_users()
        {
            cargarPersonas();
            cargarRoles();
            return View();
        }

        [HttpPost]
        public ActionResult Agregar_new_users(Tabla_User modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                    {
                        var Datos_Tabla = new Usuario();

                        Datos_Tabla.correo_Electronico = modelo.correo_Electronico;
                        Datos_Tabla.contraseña = Encryptar.Codificar(modelo.contraseña);
                        Datos_Tabla.estado = modelo.estado;
                        Datos_Tabla.rol = modelo.rol_usuario;
                        Datos_Tabla.ID_Persona = modelo.ID_Persona;

                        for (int i = 0; i < Lista_usuarios().Count; i++)
                        {
                            if (Datos_Tabla.correo_Electronico == Lista_usuarios()[i].correo_Electronico)
                            {
                                i = ListaPersonas().Count;
                                HomeController.AlertaGlobal = 1;
                                return Redirect("/Usuarios/Agregar_new_users");
                            }
                        }
                        for (int i = 0; i < Lista_usuarios().Count; i++)
                        {
                            if (Datos_Tabla.ID_Persona == Lista_usuarios()[i].ID_Persona)
                            {
                                HomeController.AlertaGlobal = 2;
                                i = ListaPersonas().Count;
                                return Redirect("/Usuarios/Agregar_new_users");
                            }
                        }
                        bd.Usuario.Add(Datos_Tabla);
                        bd.SaveChanges();
                    }
                    return Redirect("/Usuarios");
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Autorizacion_usuario(IdOperacion: 2)]
        [HttpGet]
        public ActionResult Editar_usuarios(string id)
        {
            Tabla_User modelo = new Tabla_User();
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.Usuario.Find(id);
                modelo.correo_Electronico = atributos_Tabla.correo_Electronico;
                modelo.contraseña ="";
                modelo.estado = atributos_Tabla.estado;
                modelo.rol_usuario = atributos_Tabla.rol;
                modelo.ID_Persona = atributos_Tabla.ID_Persona;
            }
            cargarRoles();
            cargarPersonas();
            return View(modelo);
        }
        [HttpPost]
        public ActionResult Editar_usuarios(Tabla_User modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                    {
                        var Datos_Tabla = bd.Usuario.Find(modelo.correo_Electronico);
                        Datos_Tabla.correo_Electronico = modelo.correo_Electronico;
                        Datos_Tabla.contraseña = Encryptar.Codificar(modelo.contraseña);
                        Datos_Tabla.estado = modelo.estado;
                        Datos_Tabla.rol = modelo.rol_usuario;
                        Datos_Tabla.ID_Persona = modelo.ID_Persona;

                        for (int i = 0; i < Lista_usuarios().Count; i++)
                        {
                            if (Datos_Tabla.ID_Persona == Lista_usuarios()[i].ID_Persona & Datos_Tabla.correo_Electronico!=Lista_usuarios()[i].correo_Electronico)
                            {
                                HomeController.AlertaGlobal = 2;
                                i = ListaPersonas().Count;
                                return Redirect("/Usuarios/Editar_usuarios?id="+modelo.correo_Electronico);
                            }
                        }
                        bd.Entry(Datos_Tabla).State = System.Data.Entity.EntityState.Modified;
                        bd.SaveChanges();
                    }
                    return Redirect("/Usuarios");
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [Autorizacion_usuario(IdOperacion: 3)]
        [HttpGet]
        public ActionResult Eliminar_users(string id)
        {
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.Usuario.Find(id);
                if (atributos_Tabla != null)
                {
                    bd.Usuario.Remove(atributos_Tabla);
                    bd.SaveChanges();
                }
                else
                {
                    return Redirect("/Usuarios");
                }
            }
            return Redirect("/Usuarios");
        }

        [Autorizacion_usuario(IdOperacion: 1)]
        public ActionResult Agregar_user_Person(int id)
        {
            Tabla_User modelo = new Tabla_User();
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                var atributos_Tabla = bd.Persona.Find(id);
                modelo.correo_Electronico = "";
                modelo.contraseña = "";
                modelo.estado = "";
                modelo.rol_usuario = 0;
                modelo.ID_Persona = atributos_Tabla.Id_Per;
                ViewBag.nombreUser = (from j in bd.Persona
                                      where j.Id_Per==id
                                      select j.nombre_Persona +" "+j.apellido_Paterno_cliente).FirstOrDefault();
            }
            cargarRoles();
            cargarPersonas();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Agregar_user_Person(Tabla_User modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
                    {
                        var Datos_Tabla = new Usuario();

                        Datos_Tabla.correo_Electronico = modelo.correo_Electronico;
                        Datos_Tabla.contraseña = Encryptar.Codificar(modelo.contraseña);
                        Datos_Tabla.estado = "Habilitado";
                        Datos_Tabla.rol = modelo.rol_usuario;
                        Datos_Tabla.ID_Persona = modelo.ID_Persona;
                        for (int i = 0; i < Lista_usuarios().Count; i++)
                        {
                            if (Datos_Tabla.correo_Electronico == Lista_usuarios()[i].correo_Electronico)
                            {
                                i = ListaPersonas().Count;
                                HomeController.AlertaGlobal = 1;
                                return Redirect("/Usuarios/Agregar_user_Person/"+modelo.ID_Persona);
                            }
                        }
                        bd.Usuario.Add(Datos_Tabla);
                        bd.SaveChanges();
                    }
                    return Redirect("/Usuarios");
                }
                return View(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Models.Roles_Modelos.Tabla_Roles> Rol_user()
        {
            List<Models.Roles_Modelos.Tabla_Roles> list_roles;
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                list_roles = (from d in bd.Rol
                                select new Models.Roles_Modelos.Tabla_Roles
                                {
                                    Id_rol = d.Id_rol,
                                    nombre_rol = d.nombre_rol
                                }).ToList();
            }
            return list_roles;
        }
        public void cargarRoles()
        {
            List<SelectListItem> itemss = Rol_user().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.nombre_rol,
                    Value = d.Id_rol.ToString(),
                    Selected = false
                };
            });

            ViewBag.Roles_items = itemss;
        }

        public static List<Tabla_Personas> ListaPersonas()
        {
            List<Tabla_Personas> list_Personas;
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                list_Personas = (from d in bd.Persona
                              select new Tabla_Personas
                              {
                                  Id_Per = d.Id_Per,
                                  nombre_Persona = d.nombre_Persona,
                                  CI_Persona=d.CI_Persona,
                                  apellido_Paterno=d.apellido_Paterno_cliente,
                                  apellido_Materno=d.apellido_Materno_cliente
                              }).ToList();
            }
            return list_Personas;
        }
        public void cargarPersonas()
        {
            List<SelectListItem> item2 = ListaPersonas().ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.CI_Persona + "(" + d.nombre_Persona + " " + d.apellido_Paterno + ")",
                    Value = d.Id_Per.ToString(),
                    Selected = false
                };
            });
            ViewBag.nomPersonas = item2;
        }
        public List<Lista_Usuarios> Lista_usuarios()
        {
            List<Lista_Usuarios> list_Usuario;
            using (BD_Detalle_ExquisitoEntities bd = new BD_Detalle_ExquisitoEntities())
            {
                list_Usuario = (from d in bd.Usuario
                                select new Lista_Usuarios
                                {
                                    correo_Electronico = d.correo_Electronico,
                                    contraseña = d.contraseña,
                                    estado = d.estado,
                                    ID_Persona = d.ID_Persona,
                                    rol = d.rol
                                }).ToList();
            }
            cargarPersonas();
            return list_Usuario;
        }
    }
}