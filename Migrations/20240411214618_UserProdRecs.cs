using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntexII.Migrations
{
    /// <inheritdoc />
    public partial class UserProdRecs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    transaction_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    day_of_week = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    time = table.Column<int>(type: "int", nullable: true),
                    entry_mode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: true),
                    type_of_transaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country_of_transaction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shipping_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type_of_card = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fraud = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.transaction_Id);
                });*/

            migrationBuilder.CreateTable(
                name: "ProductRecommendations",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recommendation_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRecommendations", x => x.ProductId);
                });
/*
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    num_Parts = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    img_Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    primary_Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    secondary_Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });
*/
            migrationBuilder.CreateTable(
                name: "UserRecommendations",
                columns: table => new
                {
                    user_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    selected_product_ID = table.Column<int>(type: "int", nullable: false),
                    recommendation_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recommendation_5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecommendations", x => x.user_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
                //name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductRecommendations");

            //migrationBuilder.DropTable(
                //name: "Products");

            migrationBuilder.DropTable(
                name: "UserRecommendations");
        }
    }
}
