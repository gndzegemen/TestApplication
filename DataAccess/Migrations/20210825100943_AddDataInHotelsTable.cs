using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddDataInHotelsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Hotels values('Asteria Hotel','https://book.asteriahotels.com')");
            migrationBuilder.Sql("insert into Hotels values('Selectum Hotel','https://book.selectumhotels.com')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
