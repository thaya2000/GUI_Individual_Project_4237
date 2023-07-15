using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Desktop_Application.Migrations
{
    /// <inheritdoc />
    public partial class AddImageColumnToStudents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Gpa",
                table: "Students",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Students",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Students");

            migrationBuilder.AlterColumn<double>(
                name: "Gpa",
                table: "Students",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }
    }
}
