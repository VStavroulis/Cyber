using Microsoft.EntityFrameworkCore.Migrations;

namespace Cyber.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "IPAddress",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IP = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BlackListed = table.Column<bool>(type: "bit", nullable: false),
                    SourceType = table.Column<int>(type: "int", nullable: false),
                    IPCounter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AlertIps",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlertId = table.Column<int>(type: "int", nullable: false),
                    IPAddressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertIps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlertIps_Alerts_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alerts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertIps_IPAddress_IPAddressId",
                        column: x => x.IPAddressId,
                        principalTable: "IPAddress",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertIps_AlertId_IPAddressId",
                table: "AlertIps",
                columns: new[] { "AlertId", "IPAddressId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AlertIps_IPAddressId",
                table: "AlertIps",
                column: "IPAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_Title_Description_Severity",
                table: "Alerts",
                columns: new[] { "Title", "Description", "Severity" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IPAddress_IP",
                table: "IPAddress",
                column: "IP",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertIps");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "IPAddress");
        }
    }
}
