using Microsoft.EntityFrameworkCore.Migrations;

namespace TCC.Infra.Migrations
{
    public partial class index : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(
            //    sql: "CREATE FULLTEXT CatalogosPodcasts Transcricao AS DEFAULT;",
            //    suppressTransaction: true);
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG FULLTEXT_index;");

            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT INDEX ON CatalogosPodcasts(Transcricao) KEY INDEX FULLTEXT_index;");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
