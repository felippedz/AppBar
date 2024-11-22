using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppBar3.Models;

public partial class AppBarContext : DbContext
{
    public AppBarContext()
    {
    }

    public AppBarContext(DbContextOptions<AppBarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<EnvaseProducto> EnvaseProductos { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<MarcaProducto> MarcaProductos { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sede> Sedes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VistaProductosVendido> VistaProductosVendidos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=localhost; database=AppBar; integrated security=true; TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC274E8ABD87");

            entity.ToTable("CategoriaProducto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(20)
                .HasColumnName("Nombre_Categoria");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Detalle___3214EC27B0A4D675");

            entity.ToTable("DetallePedido");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IdPedido).HasColumnName("ID_Pedido");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("money")
                .HasColumnName("Precio_Unitario");
            entity.Property(e => e.Subtotal).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedido)
                .HasConstraintName("FK__Detalle_P__ID_Pe__628FA481");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Detalle_P__ID_Pr__6383C8BA");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Detalle_P__ID_Se__6477ECF3");
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC278CC75822");

            entity.ToTable("Documento");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("Tipo_Documento");
        });

        modelBuilder.Entity<EnvaseProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Envase_P__3214EC27F53F6F1E");

            entity.ToTable("Envase_Producto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreEnvase)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Nombre_Envase");
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Inventar__3214EC275C25E7ED");

            entity.ToTable("Inventario");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.FechaMovimiento)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Fecha_Movimiento");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Tipo_Movimiento");
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_ID");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Inventari__ID_Pr__571DF1D5");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Inventari__Usuar__5AEE82B9");
        });

        modelBuilder.Entity<MarcaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marca_Pr__3214EC273BD4B702");

            entity.ToTable("MarcaProducto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreMarca)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("Nombre_Marca");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesas__3214EC2792E3BFCB");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.NombreMesa)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Nombre_Mesa");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Mesas__ID_Sede__3F466844");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pedido__3214EC2738E9C418");

            entity.ToTable("Pedido");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.EstadoPedido)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Estado_Pedido");
            entity.Property(e => e.FechaPedido).HasColumnName("Fecha_Pedido");
            entity.Property(e => e.IdMesa).HasColumnName("ID_Mesa");
            entity.Property(e => e.MetodoPago).HasColumnName("Metodopago");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdMesa)
                .HasConstraintName("FK__Pedido__ID_Mesa__5DCAEF64");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3214EC274A8E79BC");

            entity.ToTable("Producto");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.IdCategoria).HasColumnName("ID_Categoria");
            entity.Property(e => e.IdEnvase).HasColumnName("ID_Envase");
            entity.Property(e => e.IdMarca).HasColumnName("ID_Marca");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Precio).HasColumnType("money");
            entity.Property(e => e.PrecioVenta).HasColumnType("money");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__ID_Cat__52593CB8");

            entity.HasOne(d => d.IdEnvaseNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdEnvase)
                .HasConstraintName("FK__Producto__ID_Env__534D60F1");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .HasConstraintName("FK__Producto__ID_Mar__5441852A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC270362DD95");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(15)
                .IsFixedLength()
                .HasColumnName("Nombre_Rol");
        });

        modelBuilder.Entity<Sede>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sede__3214EC2737B0E55C");

            entity.ToTable("Sede");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.Direccion)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.NombreSede)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("Nombre_Sede");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3214EC274FC19990");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Apellido)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsFixedLength();
            entity.Property(e => e.Documento)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.IdDocumento).HasColumnName("ID_Documento");
            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.IdSede).HasColumnName("ID_Sede");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.IdDocumentoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDocumento)
                .HasConstraintName("FK__Usuario__ID_Docu__4CA06362");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__ID_Rol__4E88ABD4");

            entity.HasOne(d => d.IdSedeNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdSede)
                .HasConstraintName("FK__Usuario__ID_Sede__4D94879B");
        });

        modelBuilder.Entity<VistaProductosVendido>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("Vista_Productos_Vendidos");

            entity.Property(e => e.CantidadVendida).HasColumnName("Cantidad_Vendida");
            entity.Property(e => e.CostoVenta)
                .HasColumnType("money")
                .HasColumnName("Costo_Venta");
            entity.Property(e => e.Ganancia).HasColumnType("money");
            entity.Property(e => e.IdProducto).HasColumnName("ID_Producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("Nombre_Producto");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("money")
                .HasColumnName("Precio_Venta");
            entity.Property(e => e.Sede)
                .HasMaxLength(30)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
