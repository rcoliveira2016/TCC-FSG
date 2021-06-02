using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC.Infra.Migrations
{
    public partial class MyMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogosPodcasts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeEpisodio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlSitePodcast = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Transcricao = table.Column<string>(type: "VARCHAR(max)", nullable: true),
                    Audio = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErroTranscricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogosPodcasts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogosPodcasts");
        }
    }
}
