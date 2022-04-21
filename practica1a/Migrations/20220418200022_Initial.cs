using Microsoft.EntityFrameworkCore.Migrations;

namespace practica1a.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoCategoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialCategoriaEliminadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdCategoriaEliminada = table.Column<int>(type: "int", nullable: false),
                    NombreCategoriaEliminada = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialCategoriaEliminadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialDetalleCompraEliminadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdDetalleCompraEliminada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialDetalleCompraEliminadas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialProductosEliminados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProductoEliminado = table.Column<int>(type: "int", nullable: false),
                    NombreProductoEliminado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialProductosEliminados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialProveedorEliminados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdProveedorEliminado = table.Column<int>(type: "int", nullable: false),
                    NombreProveedorEliminado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialProveedorEliminados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistorialUsuariosEliminados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdUsuarioEliminado = table.Column<int>(type: "int", nullable: false),
                    NombreUsuarioEliminado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialUsuariosEliminados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rif = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProveedorCategorias",
                columns: table => new
                {
                    ProveedorId = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProveedorCategorias", x => new { x.CategoriaId, x.ProveedorId });
                    table.ForeignKey(
                        name: "FK_ProveedorCategorias_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProveedorCategorias_Proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosSessions_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioSessionId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_UsuariosSessions_UsuarioSessionId",
                        column: x => x.UsuarioSessionId,
                        principalTable: "UsuariosSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Archivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Verificada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCompras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleComprasProveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaDeCompra = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProveedoresId = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleComprasProveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleComprasProveedor_Proveedores_ProveedoresId",
                        column: x => x.ProveedoresId,
                        principalTable: "Proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleComprasProveedor_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApellidoCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CedulaCliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fecha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVentas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleVentas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Direcciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direcciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direcciones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolsUsuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolsUsuarios", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_RolsUsuarios_Rols_RolId",
                        column: x => x.RolId,
                        principalTable: "Rols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolsUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetalleCompraId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_DetalleCompras_DetalleCompraId",
                        column: x => x.DetalleCompraId,
                        principalTable: "DetalleCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compras_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComprasProveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Producto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DetalleComprasProveedorId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprasProveedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprasProveedor_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprasProveedor_DetalleComprasProveedor_DetalleComprasProveedorId",
                        column: x => x.DetalleComprasProveedorId,
                        principalTable: "DetalleComprasProveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DetalleVentaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_DetalleVentas_DetalleVentaId",
                        column: x => x.DetalleVentaId,
                        principalTable: "DetalleVentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ventas_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Status", "TipoCategoria" },
                values: new object[] { 1, null, "harinas" });

            migrationBuilder.InsertData(
                table: "Rols",
                columns: new[] { "Id", "Status", "Tipo" },
                values: new object[,]
                {
                    { 1, null, "Admin" },
                    { 2, null, "Cliente" },
                    { 3, null, "Empleado" }
                });

            migrationBuilder.InsertData(
                table: "UsuariosSessions",
                columns: new[] { "Id", "RolId", "UsuarioId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Direccion", "Foto", "Nombre", "Password", "Status", "Username", "UsuarioSessionId" },
                values: new object[] { 1, "Granados", "La concordia", "data:image/jpeg;base64,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", "Jesus", "$2a$11$atoNmsDhyF2RIqlir32g/O/PamCxrKc..rwxIMFDrQpGfHWzQRyWm", null, "jesus", 1 });

            migrationBuilder.InsertData(
                table: "RolsUsuarios",
                columns: new[] { "RolId", "UsuarioId" },
                values: new object[] { 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_DetalleCompraId",
                table: "Compras",
                column: "DetalleCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_ProductoId",
                table: "Compras",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasProveedor_CategoriaId",
                table: "ComprasProveedor",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprasProveedor_DetalleComprasProveedorId",
                table: "ComprasProveedor",
                column: "DetalleComprasProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompras_UsuarioId",
                table: "DetalleCompras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleComprasProveedor_ProveedoresId",
                table: "DetalleComprasProveedor",
                column: "ProveedoresId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleComprasProveedor_UsuarioId",
                table: "DetalleComprasProveedor",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVentas_UsuarioId",
                table: "DetalleVentas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Direcciones_UsuarioId",
                table: "Direcciones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProveedorCategorias_ProveedorId",
                table: "ProveedorCategorias",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_Rif",
                table: "Proveedores",
                column: "Rif",
                unique: true,
                filter: "[Rif] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RolsUsuarios_RolId",
                table: "RolsUsuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Username",
                table: "Usuarios",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_UsuarioSessionId",
                table: "Usuarios",
                column: "UsuarioSessionId",
                unique: true,
                filter: "[UsuarioSessionId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosSessions_RolId",
                table: "UsuariosSessions",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_DetalleVentaId",
                table: "Ventas",
                column: "DetalleVentaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_ProductoId",
                table: "Ventas",
                column: "ProductoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "ComprasProveedor");

            migrationBuilder.DropTable(
                name: "Direcciones");

            migrationBuilder.DropTable(
                name: "HistorialCategoriaEliminadas");

            migrationBuilder.DropTable(
                name: "HistorialDetalleCompraEliminadas");

            migrationBuilder.DropTable(
                name: "HistorialProductosEliminados");

            migrationBuilder.DropTable(
                name: "HistorialProveedorEliminados");

            migrationBuilder.DropTable(
                name: "HistorialUsuariosEliminados");

            migrationBuilder.DropTable(
                name: "ProveedorCategorias");

            migrationBuilder.DropTable(
                name: "RolsUsuarios");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "DetalleCompras");

            migrationBuilder.DropTable(
                name: "DetalleComprasProveedor");

            migrationBuilder.DropTable(
                name: "DetalleVentas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "UsuariosSessions");

            migrationBuilder.DropTable(
                name: "Rols");
        }
    }
}
