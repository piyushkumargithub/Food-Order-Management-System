using Microsoft.EntityFrameworkCore.Migrations;

namespace CalculateCartMicroservice.Migrations
{
    public partial class initailmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserOrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOrderDetails", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "FoodItemDetails",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UserOrderDetailsOrderId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemDetails", x => x.FoodId);
                    table.ForeignKey(
                        name: "FK_FoodItemDetails_UserOrderDetails_UserOrderDetailsOrderId",
                        column: x => x.UserOrderDetailsOrderId,
                        principalTable: "UserOrderDetails",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemDetails_UserOrderDetailsOrderId",
                table: "FoodItemDetails",
                column: "UserOrderDetailsOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItemDetails");

            migrationBuilder.DropTable(
                name: "UserOrderDetails");
        }
    }
}
