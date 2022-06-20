using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations.MsgUsers
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsgUsers",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromId = table.Column<string>(type: "TEXT", nullable: true),
                    ToId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MsgUsers_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MsgUsers_User_FromId",
                        column: x => x.FromId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MsgUsers_User_ToId",
                        column: x => x.ToId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_FromId",
                table: "MsgUsers",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_MessageId",
                table: "MsgUsers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_ToId",
                table: "MsgUsers",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsgUsers");
        }
    }
}
