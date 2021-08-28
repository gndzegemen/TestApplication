using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDataInTestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Tests values('end to end')");
            migrationBuilder.Sql("insert into Tests values('price')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
