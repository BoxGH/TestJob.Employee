using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace TestJob.Employee.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workman",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Position = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    Tasso = table.Column<decimal>(nullable: false),
                    Tax = table.Column<decimal>(nullable: false),
                    TaxRate = table.Column<int>(nullable: false),
                    Wages = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workman", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Workman");
        }
    }
}
