using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dbupdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odemeler");

            migrationBuilder.DropTable(
                name: "Raporlar");

            migrationBuilder.DropColumn(
                name: "BaslangicTarihi",
                table: "Randevular");

            migrationBuilder.DropColumn(
                name: "Adi",
                table: "Hastalar");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "Hastalar");

            migrationBuilder.DropColumn(
                name: "RandevuDurumu",
                table: "Hastalar");

            migrationBuilder.DropColumn(
                name: "AraSaati",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "CalismaSaatiBaslangic",
                table: "Doktorlar");

            migrationBuilder.DropColumn(
                name: "Adi",
                table: "Adminler");

            migrationBuilder.RenameColumn(
                name: "BitisiTarihi",
                table: "Randevular",
                newName: "Tarih");

            migrationBuilder.RenameColumn(
                name: "Adi",
                table: "Hastaneler",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "Tc",
                table: "Hastalar",
                newName: "TC");

            migrationBuilder.RenameColumn(
                name: "Soyadi",
                table: "Hastalar",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "Hastalik",
                table: "Hastalar",
                newName: "Mail");

            migrationBuilder.RenameColumn(
                name: "DogumYeri",
                table: "Hastalar",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "Tc",
                table: "Doktorlar",
                newName: "TC");

            migrationBuilder.RenameColumn(
                name: "Soyadi",
                table: "Doktorlar",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "CalismaSaatiBitis",
                table: "Doktorlar",
                newName: "DogumTarihi");

            migrationBuilder.RenameColumn(
                name: "Adi",
                table: "Doktorlar",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "Adi",
                table: "Bolumler",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "Sifre",
                table: "Adminler",
                newName: "Ad");

            migrationBuilder.AlterColumn<string>(
                name: "TC",
                table: "Hastalar",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Cinsiyet",
                table: "Hastalar",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TC",
                table: "Doktorlar",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "Cinsiyet",
                table: "Doktorlar",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Favoriler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoktorId = table.Column<int>(nullable: false),
                    DoktorAdı = table.Column<int>(nullable: false),
                    HastaId = table.Column<int>(nullable: false),
                    OlusturulmaTarihi = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favoriler", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoriler");

            migrationBuilder.RenameColumn(
                name: "Tarih",
                table: "Randevular",
                newName: "BitisiTarihi");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Hastaneler",
                newName: "Adi");

            migrationBuilder.RenameColumn(
                name: "TC",
                table: "Hastalar",
                newName: "Tc");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Hastalar",
                newName: "Soyadi");

            migrationBuilder.RenameColumn(
                name: "Mail",
                table: "Hastalar",
                newName: "Hastalik");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Hastalar",
                newName: "DogumYeri");

            migrationBuilder.RenameColumn(
                name: "TC",
                table: "Doktorlar",
                newName: "Tc");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Doktorlar",
                newName: "Soyadi");

            migrationBuilder.RenameColumn(
                name: "DogumTarihi",
                table: "Doktorlar",
                newName: "CalismaSaatiBitis");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Doktorlar",
                newName: "Adi");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Bolumler",
                newName: "Adi");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Adminler",
                newName: "Sifre");

            migrationBuilder.AddColumn<DateTime>(
                name: "BaslangicTarihi",
                table: "Randevular",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Tc",
                table: "Hastalar",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cinsiyet",
                table: "Hastalar",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Adi",
                table: "Hastalar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "Hastalar",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RandevuDurumu",
                table: "Hastalar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Tc",
                table: "Doktorlar",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cinsiyet",
                table: "Doktorlar",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "AraSaati",
                table: "Doktorlar",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CalismaSaatiBaslangic",
                table: "Doktorlar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adi",
                table: "Adminler",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Odemeler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DoktorId = table.Column<int>(nullable: false),
                    Tarih = table.Column<DateTime>(nullable: true),
                    Tutar = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odemeler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Raporlar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    Tarih = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raporlar", x => x.Id);
                });
        }
    }
}
