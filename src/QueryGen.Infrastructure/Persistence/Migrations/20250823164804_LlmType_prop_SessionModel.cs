using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueryGen.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LlmType_prop_SessionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LlmType",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LlmType",
                table: "Sessions");
        }
    }
}
