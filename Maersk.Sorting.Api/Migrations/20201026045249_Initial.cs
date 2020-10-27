using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Maersk.Sorting.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SortJob",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SortID = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Duration = table.Column<TimeSpan>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    Output = table.Column<string>(nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortJob", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SortJob");
        }
    }
}
