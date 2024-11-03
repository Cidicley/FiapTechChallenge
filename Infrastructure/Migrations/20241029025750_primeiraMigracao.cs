using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class primeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegiaoContato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DDD = table.Column<int>(type: "INT", nullable: false),
                    Regiao = table.Column<string>(type: "VARCHAR(15)", nullable: false),
                    Estado = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegiaoContato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INT", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    RegiaoContatoId = table.Column<int>(type: "INT", nullable: false),
                    Telefone = table.Column<int>(type: "INT", nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contato_RegiaoContato_RegiaoContatoId",
                        column: x => x.RegiaoContatoId,
                        principalTable: "RegiaoContato",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contato_RegiaoContatoId",
                table: "Contato",
                column: "RegiaoContatoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contato");

            migrationBuilder.DropTable(
                name: "RegiaoContato");
        }
    }
}
