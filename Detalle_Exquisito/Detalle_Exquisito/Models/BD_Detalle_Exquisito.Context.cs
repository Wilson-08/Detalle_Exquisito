//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Detalle_Exquisito.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class BD_Detalle_ExquisitoEntities : DbContext
    {
        public BD_Detalle_ExquisitoEntities()
            : base("name=BD_Detalle_ExquisitoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Persona> Persona { get; set; }
        public virtual DbSet<Cargo_Empleo> Cargo_Empleo { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Decoracion_Extra> Decoracion_Extra { get; set; }
        public virtual DbSet<Descuento> Descuento { get; set; }
        public virtual DbSet<Descuento_Cliente> Descuento_Cliente { get; set; }
        public virtual DbSet<Detalle_producto> Detalle_producto { get; set; }
        public virtual DbSet<Detalle_Productos_Extras> Detalle_Productos_Extras { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<Extra_Productos> Extra_Productos { get; set; }
        public virtual DbSet<Festividad> Festividad { get; set; }
        public virtual DbSet<modulo> modulo { get; set; }
        public virtual DbSet<Operaciones> Operaciones { get; set; }
        public virtual DbSet<Operaciones_Menu> Operaciones_Menu { get; set; }
        public virtual DbSet<Pago> Pago { get; set; }
        public virtual DbSet<producto> producto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tipo_Producto> Tipo_Producto { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Detalle_Decoracion_Extras> Detalle_Decoracion_Extras { get; set; }
        public virtual DbSet<detalle_Pedido> detalle_Pedido { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
    
        public virtual ObjectResult<Ultima_Persona_Result> Ultima_Persona()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Ultima_Persona_Result>("Ultima_Persona");
        }
    
        public virtual ObjectResult<buscar_Result> buscar(string valor)
        {
            var valorParameter = valor != null ?
                new ObjectParameter("valor", valor) :
                new ObjectParameter("valor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<buscar_Result>("buscar", valorParameter);
        }
    
        public virtual ObjectResult<buscar1_Result> buscar1(string valor)
        {
            var valorParameter = valor != null ?
                new ObjectParameter("valor", valor) :
                new ObjectParameter("valor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<buscar1_Result>("buscar1", valorParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> Ultima_Persona1()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("Ultima_Persona1");
        }
    
        public virtual ObjectResult<buscar_P_Result> buscar_P(string valor)
        {
            var valorParameter = valor != null ?
                new ObjectParameter("valor", valor) :
                new ObjectParameter("valor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<buscar_P_Result>("buscar_P", valorParameter);
        }
    }
}
