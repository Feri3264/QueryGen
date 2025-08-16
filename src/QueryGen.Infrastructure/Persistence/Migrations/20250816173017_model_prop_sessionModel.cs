using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueryGen.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class model_prop_sessionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LlmModel",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LlmModel",
                table: "Sessions");
        }
    }
}
