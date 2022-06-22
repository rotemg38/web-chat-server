using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations.Data
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<string>(type: "TEXT", nullable: true),
                    Sent = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    RateNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Feedback = table.Column<string>(type: "TEXT", nullable: true),
                    WhenCreated = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Image = table.Column<string>(type: "TEXT", nullable: true),
                    last = table.Column<string>(type: "TEXT", nullable: true),
                    lastdate = table.Column<string>(type: "TEXT", nullable: true),
                    Server = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    user1Id = table.Column<string>(type: "TEXT", nullable: true),
                    user2Id = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chat_User_user1Id",
                        column: x => x.user1Id,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chat_User_user2Id",
                        column: x => x.user2Id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MsgUsers",
                columns: table => new
                {
                    ChatId = table.Column<int>(type: "INTEGER", nullable: true),
                    MsgId = table.Column<int>(type: "INTEGER", nullable: true),
                    FromId = table.Column<string>(type: "TEXT", nullable: true),
                    ToId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_MsgUsers_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "ChatId");
                    table.ForeignKey(
                        name: "FK_MsgUsers_Message_MsgId",
                        column: x => x.MsgId,
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
                name: "IX_Chat_user1Id",
                table: "Chat",
                column: "user1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_user2Id",
                table: "Chat",
                column: "user2Id");

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_ChatId",
                table: "MsgUsers",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_FromId",
                table: "MsgUsers",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_MsgId",
                table: "MsgUsers",
                column: "MsgId");

            migrationBuilder.CreateIndex(
                name: "IX_MsgUsers_ToId",
                table: "MsgUsers",
                column: "ToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MsgUsers");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
