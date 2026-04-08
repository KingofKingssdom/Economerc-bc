using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceOrigin",
                table: "ProductVariants",
                newName: "OriginPrice");

            migrationBuilder.RenameColumn(
                name: "PriceDiscount",
                table: "ProductVariants",
                newName: "CurrentPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginPrice",
                table: "ProductVariants",
                newName: "PriceOrigin");

            migrationBuilder.RenameColumn(
                name: "CurrentPrice",
                table: "ProductVariants",
                newName: "PriceDiscount");
        }
    }
}
