using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcMovie.Migrations
{
    /// <inheritdoc />
    public partial class addtable_buoi12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceType",
                columns: table => new
                {
                    Type_ID = table.Column<string>(type: "TEXT", nullable: false),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceTypeType_ID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.Type_ID);
                    table.ForeignKey(
                        name: "FK_DeviceType_DeviceType_DeviceTypeType_ID",
                        column: x => x.DeviceTypeType_ID,
                        principalTable: "DeviceType",
                        principalColumn: "Type_ID");
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierID = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    SupplierName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Device_ID = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceName = table.Column<string>(type: "TEXT", nullable: false),
                    Type_ID = table.Column<string>(type: "TEXT", nullable: false),
                    Remain = table.Column<int>(type: "INTEGER", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    SupplierID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Device_ID);
                    table.ForeignKey(
                        name: "FK_Device_DeviceType_Type_ID",
                        column: x => x.Type_ID,
                        principalTable: "DeviceType",
                        principalColumn: "Type_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID");
                });

            migrationBuilder.CreateTable(
                name: "IssueNotes",
                columns: table => new
                {
                    IN_ID = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SupplierID = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueNotes", x => x.IN_ID);
                    table.ForeignKey(
                        name: "FK_IssueNotes_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptID = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SupplierID = table.Column<string>(type: "TEXT", nullable: false),
                    Total = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptID);
                    table.ForeignKey(
                        name: "FK_Receipts_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueNoteDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Device_ID = table.Column<string>(type: "TEXT", nullable: false),
                    SupplierID = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    SumAll = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueNoteDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_IssueNoteDetails_Device_Device_ID",
                        column: x => x.Device_ID,
                        principalTable: "Device",
                        principalColumn: "Device_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueNoteDetails_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Device_ID = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierID = table.Column<string>(type: "TEXT", nullable: true),
                    Type_ID = table.Column<string>(type: "TEXT", nullable: true),
                    DeviceType = table.Column<string>(type: "TEXT", nullable: true),
                    ReceiptID = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Cost = table.Column<decimal>(type: "TEXT", nullable: false),
                    SumAll = table.Column<decimal>(type: "TEXT", nullable: false),
                    IssueNoteIN_ID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_DeviceType_DeviceType",
                        column: x => x.DeviceType,
                        principalTable: "DeviceType",
                        principalColumn: "Type_ID");
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_Device_Device_ID",
                        column: x => x.Device_ID,
                        principalTable: "Device",
                        principalColumn: "Device_ID");
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_IssueNotes_IssueNoteIN_ID",
                        column: x => x.IssueNoteIN_ID,
                        principalTable: "IssueNotes",
                        principalColumn: "IN_ID");
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_Receipts_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptID");
                    table.ForeignKey(
                        name: "FK_ReceiptDetails_Suppliers_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Device_SupplierID",
                table: "Device",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Type_ID",
                table: "Device",
                column: "Type_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceType_DeviceTypeType_ID",
                table: "DeviceType",
                column: "DeviceTypeType_ID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteDetails_Device_ID",
                table: "IssueNoteDetails",
                column: "Device_ID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNoteDetails_SupplierID",
                table: "IssueNoteDetails",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueNotes_SupplierID",
                table: "IssueNotes",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_Device_ID",
                table: "ReceiptDetails",
                column: "Device_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_DeviceType",
                table: "ReceiptDetails",
                column: "DeviceType");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_IssueNoteIN_ID",
                table: "ReceiptDetails",
                column: "IssueNoteIN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_ReceiptID",
                table: "ReceiptDetails",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptDetails_SupplierID",
                table: "ReceiptDetails",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SupplierID",
                table: "Receipts",
                column: "SupplierID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueNoteDetails");

            migrationBuilder.DropTable(
                name: "ReceiptDetails");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "IssueNotes");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "DeviceType");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
