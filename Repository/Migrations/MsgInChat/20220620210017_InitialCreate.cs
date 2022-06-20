using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations.MsgInChat
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "MsgInChat",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MsgInChat_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "ChatId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MsgInChat_ChatId",
                table: "MsgInChat",
                column: "ChatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsgInChat");
        }
    }
}
