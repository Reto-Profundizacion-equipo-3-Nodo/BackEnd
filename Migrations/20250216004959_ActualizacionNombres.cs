using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FundAntivirus.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionNombres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "categorias",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "categorias",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "categorias",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "categorias",
                newName: "Descripcion");
        }
    }
}
