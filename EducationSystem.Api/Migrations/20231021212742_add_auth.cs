using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_auth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelationItemsInCurriculums",
                table: "RelationItemsInCurriculums");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelationItemsInCurriculums",
                table: "RelationItemsInCurriculums",
                columns: new[] { "ItemId", "CurriculumId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RelationItemsInCurriculums",
                table: "RelationItemsInCurriculums");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RelationItemsInCurriculums",
                table: "RelationItemsInCurriculums",
                columns: new[] { "CurriculumId", "ItemId" });
        }
    }
}
