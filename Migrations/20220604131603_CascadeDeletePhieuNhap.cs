using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyKho.Migrations
{
    public partial class CascadeDeletePhieuNhap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuNhap_PhieuNhap",
                table: "ChiTietPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuNhap_PhieuNhap",
                table: "ChiTietPhieuNhap",
                column: "soPhieu",
                principalTable: "PhieuNhap",
                principalColumn: "soPhieu",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChiTietPhieuNhap_PhieuNhap",
                table: "ChiTietPhieuNhap");

            migrationBuilder.AddForeignKey(
                name: "FK_ChiTietPhieuNhap_PhieuNhap",
                table: "ChiTietPhieuNhap",
                column: "soPhieu",
                principalTable: "PhieuNhap",
                principalColumn: "soPhieu");
        }
    }
}
