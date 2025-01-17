﻿// <auto-generated />
using System;
using AppBar3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppBar3.Migrations
{
    [DbContext(typeof(AppBarContext))]
    [Migration("20241118020925_m2")]
    partial class m2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppBar3.Models.CategoriaProducto", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreCategoria")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Nombre_Categoria");

                    b.HasKey("Id")
                        .HasName("PK__Categori__3214EC274E8ABD87");

                    b.ToTable("CategoriaProducto", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.DetallePedido", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<byte?>("Cantidad")
                        .HasColumnType("tinyint");

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<byte?>("IdPedido")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Pedido");

                    b.Property<byte?>("IdProducto")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Producto");

                    b.Property<byte?>("IdSede")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Sede");

                    b.Property<decimal?>("PrecioUnitario")
                        .HasColumnType("money")
                        .HasColumnName("Precio_Unitario");

                    b.Property<decimal?>("Subtotal")
                        .HasColumnType("money");

                    b.Property<decimal?>("Total")
                        .HasColumnType("money");

                    b.HasKey("Id")
                        .HasName("PK__Detalle___3214EC27B0A4D675");

                    b.HasIndex("IdPedido");

                    b.HasIndex("IdProducto");

                    b.HasIndex("IdSede");

                    b.ToTable("DetallePedido", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.Documento", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("TipoDocumento")
                        .HasMaxLength(15)
                        .HasColumnType("nchar(15)")
                        .HasColumnName("Tipo_Documento")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Document__3214EC278CC75822");

                    b.ToTable("Documento", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.EnvaseProducto", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreEnvase")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("Nombre_Envase")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Envase_P__3214EC27F53F6F1E");

                    b.ToTable("Envase_Producto", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.Inventario", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<double?>("Cantidad")
                        .HasColumnType("float");

                    b.Property<DateTime?>("FechaMovimiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("Fecha_Movimiento")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<byte?>("IdProducto")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Producto");

                    b.Property<string>("TipoMovimiento")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("Tipo_Movimiento")
                        .IsFixedLength();

                    b.Property<byte?>("UsuarioId")
                        .HasColumnType("tinyint")
                        .HasColumnName("Usuario_ID");

                    b.HasKey("Id")
                        .HasName("PK__Inventar__3214EC275C25E7ED");

                    b.HasIndex("IdProducto");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Inventario", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.MarcaProducto", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreMarca")
                        .HasMaxLength(60)
                        .HasColumnType("nchar(60)")
                        .HasColumnName("Nombre_Marca")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Marca_Pr__3214EC273BD4B702");

                    b.ToTable("MarcaProducto", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.Mesa", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<byte?>("IdSede")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Sede");

                    b.Property<string>("NombreMesa")
                        .HasMaxLength(10)
                        .HasColumnType("nchar(10)")
                        .HasColumnName("Nombre_Mesa")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Mesas__3214EC2792E3BFCB");

                    b.HasIndex("IdSede");

                    b.ToTable("Mesas");
                });

            modelBuilder.Entity("AppBar3.Models.Pedido", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("EstadoPedido")
                        .HasMaxLength(50)
                        .HasColumnType("nchar(50)")
                        .HasColumnName("Estado_Pedido")
                        .IsFixedLength();

                    b.Property<DateOnly?>("FechaPedido")
                        .HasColumnType("date")
                        .HasColumnName("Fecha_Pedido");

                    b.Property<byte?>("IdMesa")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Mesa");

                    b.Property<byte?>("IdMetodoPago")
                        .HasColumnType("tinyint")
                        .HasColumnName("Id_Metodo_pago");

                    b.Property<decimal?>("Total")
                        .HasColumnType("money");

                    b.HasKey("Id")
                        .HasName("PK__Pedido__3214EC2738E9C418");

                    b.HasIndex("IdMesa");

                    b.ToTable("Pedido", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.Producto", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Descripcion")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<byte?>("IdCategoria")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Categoria");

                    b.Property<byte?>("IdEnvase")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Envase");

                    b.Property<byte?>("IdMarca")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Marca");

                    b.Property<string>("Nombre")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.Property<decimal?>("Precio")
                        .HasColumnType("money");

                    b.Property<decimal?>("PrecioVenta")
                        .HasColumnType("money");

                    b.HasKey("Id")
                        .HasName("PK__Producto__3214EC274A8E79BC");

                    b.HasIndex("IdCategoria");

                    b.HasIndex("IdEnvase");

                    b.HasIndex("IdMarca");

                    b.ToTable("Producto", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.Role", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("NombreRol")
                        .HasMaxLength(15)
                        .HasColumnType("nchar(15)")
                        .HasColumnName("Nombre_Rol")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Roles__3214EC270362DD95");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("AppBar3.Models.Sede", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Correo")
                        .HasMaxLength(60)
                        .HasColumnType("nchar(60)")
                        .IsFixedLength();

                    b.Property<string>("Direccion")
                        .HasMaxLength(60)
                        .HasColumnType("nchar(60)")
                        .IsFixedLength();

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("NombreSede")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .HasColumnName("Nombre_Sede")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Sede__3214EC2737B0E55C");

                    b.ToTable("Sede", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.Usuario", b =>
                {
                    b.Property<byte>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("Id"));

                    b.Property<string>("Apellido")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("Correo")
                        .HasMaxLength(60)
                        .HasColumnType("nchar(60)")
                        .IsFixedLength();

                    b.Property<string>("Documento")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<byte?>("IdDocumento")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Documento");

                    b.Property<byte?>("IdRol")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Rol");

                    b.Property<byte?>("IdSede")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Sede");

                    b.Property<string>("Nombre")
                        .HasMaxLength(20)
                        .HasColumnType("nchar(20)")
                        .IsFixedLength();

                    b.Property<string>("Password")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PK__Usuario__3214EC274FC19990");

                    b.HasIndex("IdDocumento");

                    b.HasIndex("IdRol");

                    b.HasIndex("IdSede");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.VistaProductosVendido", b =>
                {
                    b.Property<int?>("CantidadVendida")
                        .HasColumnType("int")
                        .HasColumnName("Cantidad_Vendida");

                    b.Property<decimal?>("CostoVenta")
                        .HasColumnType("money")
                        .HasColumnName("Costo_Venta");

                    b.Property<decimal?>("Ganancia")
                        .HasColumnType("money");

                    b.Property<byte>("IdProducto")
                        .HasColumnType("tinyint")
                        .HasColumnName("ID_Producto");

                    b.Property<string>("NombreProducto")
                        .HasMaxLength(100)
                        .HasColumnType("nchar(100)")
                        .HasColumnName("Nombre_Producto")
                        .IsFixedLength();

                    b.Property<decimal?>("PrecioVenta")
                        .HasColumnType("money")
                        .HasColumnName("Precio_Venta");

                    b.Property<string>("Sede")
                        .HasMaxLength(30)
                        .HasColumnType("nchar(30)")
                        .IsFixedLength();

                    b.ToTable((string)null);

                    b.ToView("Vista_Productos_Vendidos", (string)null);
                });

            modelBuilder.Entity("AppBar3.Models.DetallePedido", b =>
                {
                    b.HasOne("AppBar3.Models.Pedido", "IdPedidoNavigation")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("IdPedido")
                        .HasConstraintName("FK__Detalle_P__ID_Pe__628FA481");

                    b.HasOne("AppBar3.Models.Producto", "IdProductoNavigation")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__Detalle_P__ID_Pr__6383C8BA");

                    b.HasOne("AppBar3.Models.Sede", "IdSedeNavigation")
                        .WithMany("DetallePedidos")
                        .HasForeignKey("IdSede")
                        .HasConstraintName("FK__Detalle_P__ID_Se__6477ECF3");

                    b.Navigation("IdPedidoNavigation");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("IdSedeNavigation");
                });

            modelBuilder.Entity("AppBar3.Models.Inventario", b =>
                {
                    b.HasOne("AppBar3.Models.Producto", "IdProductoNavigation")
                        .WithMany("Inventarios")
                        .HasForeignKey("IdProducto")
                        .HasConstraintName("FK__Inventari__ID_Pr__571DF1D5");

                    b.HasOne("AppBar3.Models.Usuario", "Usuario")
                        .WithMany("Inventarios")
                        .HasForeignKey("UsuarioId")
                        .HasConstraintName("FK__Inventari__Usuar__5AEE82B9");

                    b.Navigation("IdProductoNavigation");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("AppBar3.Models.Mesa", b =>
                {
                    b.HasOne("AppBar3.Models.Sede", "IdSedeNavigation")
                        .WithMany("Mesas")
                        .HasForeignKey("IdSede")
                        .HasConstraintName("FK__Mesas__ID_Sede__3F466844");

                    b.Navigation("IdSedeNavigation");
                });

            modelBuilder.Entity("AppBar3.Models.Pedido", b =>
                {
                    b.HasOne("AppBar3.Models.Mesa", "IdMesaNavigation")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdMesa")
                        .HasConstraintName("FK__Pedido__ID_Mesa__5DCAEF64");

                    b.Navigation("IdMesaNavigation");
                });

            modelBuilder.Entity("AppBar3.Models.Producto", b =>
                {
                    b.HasOne("AppBar3.Models.CategoriaProducto", "IdCategoriaNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdCategoria")
                        .HasConstraintName("FK__Producto__ID_Cat__52593CB8");

                    b.HasOne("AppBar3.Models.EnvaseProducto", "IdEnvaseNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdEnvase")
                        .HasConstraintName("FK__Producto__ID_Env__534D60F1");

                    b.HasOne("AppBar3.Models.MarcaProducto", "IdMarcaNavigation")
                        .WithMany("Productos")
                        .HasForeignKey("IdMarca")
                        .HasConstraintName("FK__Producto__ID_Mar__5441852A");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdEnvaseNavigation");

                    b.Navigation("IdMarcaNavigation");
                });

            modelBuilder.Entity("AppBar3.Models.Usuario", b =>
                {
                    b.HasOne("AppBar3.Models.Documento", "IdDocumentoNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdDocumento")
                        .HasConstraintName("FK__Usuario__ID_Docu__4CA06362");

                    b.HasOne("AppBar3.Models.Role", "IdRolNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdRol")
                        .HasConstraintName("FK__Usuario__ID_Rol__4E88ABD4");

                    b.HasOne("AppBar3.Models.Sede", "IdSedeNavigation")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdSede")
                        .HasConstraintName("FK__Usuario__ID_Sede__4D94879B");

                    b.Navigation("IdDocumentoNavigation");

                    b.Navigation("IdRolNavigation");

                    b.Navigation("IdSedeNavigation");
                });

            modelBuilder.Entity("AppBar3.Models.CategoriaProducto", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("AppBar3.Models.Documento", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("AppBar3.Models.EnvaseProducto", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("AppBar3.Models.MarcaProducto", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("AppBar3.Models.Mesa", b =>
                {
                    b.Navigation("Pedidos");
                });

            modelBuilder.Entity("AppBar3.Models.Pedido", b =>
                {
                    b.Navigation("DetallePedidos");
                });

            modelBuilder.Entity("AppBar3.Models.Producto", b =>
                {
                    b.Navigation("DetallePedidos");

                    b.Navigation("Inventarios");
                });

            modelBuilder.Entity("AppBar3.Models.Role", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("AppBar3.Models.Sede", b =>
                {
                    b.Navigation("DetallePedidos");

                    b.Navigation("Mesas");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("AppBar3.Models.Usuario", b =>
                {
                    b.Navigation("Inventarios");
                });
#pragma warning restore 612, 618
        }
    }
}
