using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntexII.Migrations.UsersDB
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    admin_Id = table.Column<int>(type: "int", nullable: false),
                    user_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
