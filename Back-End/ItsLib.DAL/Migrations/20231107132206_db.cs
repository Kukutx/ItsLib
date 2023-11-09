using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItsLib.DAL.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string db = File.ReadAllText("./ScriptDB.sql");
            migrationBuilder.Sql(db);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
