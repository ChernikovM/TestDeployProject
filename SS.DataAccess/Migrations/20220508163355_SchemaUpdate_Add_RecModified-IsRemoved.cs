using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SS.DataAccess.Migrations
{
    public partial class SchemaUpdate_Add_RecModifiedIsRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackLabelStickerPack");

            migrationBuilder.DropTable(
                name: "PackLabels");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecCreated",
                table: "StickerPacks",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "StickerPacks",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecModified",
                table: "StickerPacks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RecCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsRemoved = table.Column<bool>(type: "boolean", nullable: true),
                    RecModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabelStickerPack",
                columns: table => new
                {
                    LabelsId = table.Column<long>(type: "bigint", nullable: false),
                    StickerPacksId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelStickerPack", x => new { x.LabelsId, x.StickerPacksId });
                    table.ForeignKey(
                        name: "FK_LabelStickerPack_Labels_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "Labels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabelStickerPack_StickerPacks_StickerPacksId",
                        column: x => x.StickerPacksId,
                        principalTable: "StickerPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Labels_Name",
                table: "Labels",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabelStickerPack_StickerPacksId",
                table: "LabelStickerPack",
                column: "StickerPacksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabelStickerPack");

            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "StickerPacks");

            migrationBuilder.DropColumn(
                name: "RecModified",
                table: "StickerPacks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecCreated",
                table: "StickerPacks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PackLabels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    RecCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackLabels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackLabelStickerPack",
                columns: table => new
                {
                    LabelsId = table.Column<long>(type: "bigint", nullable: false),
                    StickerPacksId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackLabelStickerPack", x => new { x.LabelsId, x.StickerPacksId });
                    table.ForeignKey(
                        name: "FK_PackLabelStickerPack_PackLabels_LabelsId",
                        column: x => x.LabelsId,
                        principalTable: "PackLabels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackLabelStickerPack_StickerPacks_StickerPacksId",
                        column: x => x.StickerPacksId,
                        principalTable: "StickerPacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackLabelStickerPack_StickerPacksId",
                table: "PackLabelStickerPack",
                column: "StickerPacksId");
        }
    }
}