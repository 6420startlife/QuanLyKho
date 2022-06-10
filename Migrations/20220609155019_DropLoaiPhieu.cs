using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    public partial class DropLoaiPhieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loaiPhieu",
                table: "PhieuNhap");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "loaiPhieu",
                table: "PhieuNhap",
                type: "nvarchar(1)",
                maxLength: 1,
                nullable: true);
        }
    }
}
