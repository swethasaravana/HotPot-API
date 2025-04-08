using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotPotAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumnsAndMakeRestaurantIdRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantManagers_Restaurants_RestaurantId",
                table: "RestaurantManagers");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "RestaurantManagers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "RestaurantManagers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RestaurantManagers",
                newName: "ManagerId");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "RestaurantManagers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantManagers_Restaurants_RestaurantId",
                table: "RestaurantManagers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantManagers_Restaurants_RestaurantId",
                table: "RestaurantManagers");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "RestaurantManagers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "RestaurantManagers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "ManagerId",
                table: "RestaurantManagers",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "RestaurantManagers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantManagers_Restaurants_RestaurantId",
                table: "RestaurantManagers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId");
        }
    }
}
