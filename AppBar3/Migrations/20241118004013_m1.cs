using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppBar3.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProducto",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Categoria = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Categori__3214EC274E8ABD87", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo_Documento = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__3214EC278CC75822", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Envase_Producto",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Envase = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Envase_P__3214EC27F53F6F1E", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MarcaProducto",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Marca = table.Column<string>(type: "nchar(60)", fixedLength: true, maxLength: 60, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Marca_Pr__3214EC273BD4B702", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Rol = table.Column<string>(type: "nchar(15)", fixedLength: true, maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC270362DD95", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sede",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Sede = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: true),
                    Direccion = table.Column<string>(type: "nchar(60)", fixedLength: true, maxLength: 60, nullable: true),
                    Correo = table.Column<string>(type: "nchar(60)", fixedLength: true, maxLength: 60, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sede__3214EC2737B0E55C", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Precio = table.Column<decimal>(type: "money", nullable: true),
                    PrecioVenta = table.Column<decimal>(type: "money", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true),
                    ID_Categoria = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Envase = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Marca = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Producto__3214EC274A8E79BC", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Producto__ID_Cat__52593CB8",
                        column: x => x.ID_Categoria,
                        principalTable: "CategoriaProducto",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Producto__ID_Env__534D60F1",
                        column: x => x.ID_Envase,
                        principalTable: "Envase_Producto",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Producto__ID_Mar__5441852A",
                        column: x => x.ID_Marca,
                        principalTable: "MarcaProducto",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Mesa = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    ID_Sede = table.Column<byte>(type: "tinyint", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Mesas__3214EC2792E3BFCB", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Mesas__ID_Sede__3F466844",
                        column: x => x.ID_Sede,
                        principalTable: "Sede",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    Apellido = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    Documento = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    Correo = table.Column<string>(type: "nchar(60)", fixedLength: true, maxLength: 60, nullable: true),
                    Password = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: true),
                    ID_Documento = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Sede = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Rol = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuario__3214EC274FC19990", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Usuario__ID_Docu__4CA06362",
                        column: x => x.ID_Documento,
                        principalTable: "Documento",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Usuario__ID_Rol__4E88ABD4",
                        column: x => x.ID_Rol,
                        principalTable: "Roles",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Usuario__ID_Sede__4D94879B",
                        column: x => x.ID_Sede,
                        principalTable: "Sede",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Pedido",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Pedido = table.Column<DateOnly>(type: "date", nullable: true),
                    Estado_Pedido = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    Total = table.Column<decimal>(type: "money", nullable: true),
                    Id_Metodo_pago = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Mesa = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Pedido__3214EC2738E9C418", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Pedido__ID_Mesa__5DCAEF64",
                        column: x => x.ID_Mesa,
                        principalTable: "Mesas",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Inventario",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Producto = table.Column<byte>(type: "tinyint", nullable: true),
                    Tipo_Movimiento = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Cantidad = table.Column<double>(type: "float", nullable: true),
                    Fecha_Movimiento = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    Usuario_ID = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Inventar__3214EC275C25E7ED", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Inventari__ID_Pr__571DF1D5",
                        column: x => x.ID_Producto,
                        principalTable: "Producto",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Inventari__Usuar__5AEE82B9",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuario",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DetallePedido",
                columns: table => new
                {
                    ID = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<bool>(type: "bit", nullable: true),
                    Cantidad = table.Column<byte>(type: "tinyint", nullable: true),
                    Precio_Unitario = table.Column<decimal>(type: "money", nullable: true),
                    Subtotal = table.Column<decimal>(type: "money", nullable: true),
                    Total = table.Column<decimal>(type: "money", nullable: true),
                    ID_Pedido = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Producto = table.Column<byte>(type: "tinyint", nullable: true),
                    ID_Sede = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Detalle___3214EC27B0A4D675", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Detalle_P__ID_Pe__628FA481",
                        column: x => x.ID_Pedido,
                        principalTable: "Pedido",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Detalle_P__ID_Pr__6383C8BA",
                        column: x => x.ID_Producto,
                        principalTable: "Producto",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK__Detalle_P__ID_Se__6477ECF3",
                        column: x => x.ID_Sede,
                        principalTable: "Sede",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_ID_Pedido",
                table: "DetallePedido",
                column: "ID_Pedido");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_ID_Producto",
                table: "DetallePedido",
                column: "ID_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedido_ID_Sede",
                table: "DetallePedido",
                column: "ID_Sede");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_ID_Producto",
                table: "Inventario",
                column: "ID_Producto");

            migrationBuilder.CreateIndex(
                name: "IX_Inventario_Usuario_ID",
                table: "Inventario",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Mesas_ID_Sede",
                table: "Mesas",
                column: "ID_Sede");

            migrationBuilder.CreateIndex(
                name: "IX_Pedido_ID_Mesa",
                table: "Pedido",
                column: "ID_Mesa");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_ID_Categoria",
                table: "Producto",
                column: "ID_Categoria");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_ID_Envase",
                table: "Producto",
                column: "ID_Envase");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_ID_Marca",
                table: "Producto",
                column: "ID_Marca");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ID_Documento",
                table: "Usuario",
                column: "ID_Documento");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ID_Rol",
                table: "Usuario",
                column: "ID_Rol");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_ID_Sede",
                table: "Usuario",
                column: "ID_Sede");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetallePedido");

            migrationBuilder.DropTable(
                name: "Inventario");

            migrationBuilder.DropTable(
                name: "Pedido");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Mesas");

            migrationBuilder.DropTable(
                name: "CategoriaProducto");

            migrationBuilder.DropTable(
                name: "Envase_Producto");

            migrationBuilder.DropTable(
                name: "MarcaProducto");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Sede");
        }
    }
}
