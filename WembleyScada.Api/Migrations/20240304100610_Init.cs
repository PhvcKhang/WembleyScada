using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WembleyScada.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    ReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReferenceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.ReferenceId);
                    table.ForeignKey(
                        name: "FK_References_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineType = table.Column<int>(type: "int", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.LineId);
                    table.ForeignKey(
                        name: "FK_Lines_References_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "References",
                        principalColumn: "ReferenceId");
                });

            migrationBuilder.CreateTable(
                name: "Lot",
                columns: table => new
                {
                    LotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LotSize = table.Column<int>(type: "int", nullable: false),
                    LotStatus = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lot", x => x.LotId);
                    table.ForeignKey(
                        name: "FK_Lot_References_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "References",
                        principalColumn: "ReferenceId");
                });

            migrationBuilder.CreateTable(
                name: "LineProduct",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsableLinesLineId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineProduct", x => new { x.ProductId, x.UsableLinesLineId });
                    table.ForeignKey(
                        name: "FK_LineProduct_Lines_UsableLinesLineId",
                        column: x => x.UsableLinesLineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineProduct_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LineId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                    table.ForeignKey(
                        name: "FK_Stations_Lines_LineId",
                        column: x => x.LineId,
                        principalTable: "Lines",
                        principalColumn: "LineId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorInformations",
                columns: table => new
                {
                    ErrorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ErrorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorInformations", x => x.ErrorId);
                    table.ForeignKey(
                        name: "FK_ErrorInformations_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineStatus",
                columns: table => new
                {
                    MachineStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineStatus", x => x.MachineStatusId);
                    table.ForeignKey(
                        name: "FK_MachineStatus_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStation",
                columns: table => new
                {
                    ProductId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StationsStationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStation", x => new { x.ProductId, x.StationsStationId });
                    table.ForeignKey(
                        name: "FK_ProductStation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStation_Stations_StationsStationId",
                        column: x => x.StationsStationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShiftReports",
                columns: table => new
                {
                    ShiftReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCount = table.Column<int>(type: "int", nullable: false),
                    DefectCount = table.Column<int>(type: "int", nullable: false),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ElapsedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StationId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftReports", x => x.ShiftReportId);
                    table.ForeignKey(
                        name: "FK_ShiftReports_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShiftReports_Stations_StationId1",
                        column: x => x.StationId1,
                        principalTable: "Stations",
                        principalColumn: "StationId");
                });

            migrationBuilder.CreateTable(
                name: "StationReferences",
                columns: table => new
                {
                    ReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationReferences", x => new { x.ReferenceId, x.StationId });
                    table.ForeignKey(
                        name: "FK_StationReferences_References_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "References",
                        principalColumn: "ReferenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StationReferences_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkRecord",
                columns: table => new
                {
                    WorkRecordId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkStatus = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkRecord", x => x.WorkRecordId);
                    table.ForeignKey(
                        name: "FK_WorkRecord_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkRecord_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErrorStatus",
                columns: table => new
                {
                    ErrorStatusId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    ShiftNumber = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErrorId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorStatus", x => x.ErrorStatusId);
                    table.ForeignKey(
                        name: "FK_ErrorStatus_ErrorInformations_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "ErrorInformations",
                        principalColumn: "ErrorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shot",
                columns: table => new
                {
                    ShiftReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExecutionTime = table.Column<double>(type: "float", nullable: false),
                    CycleTime = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    A = table.Column<double>(type: "float", nullable: false),
                    P = table.Column<double>(type: "float", nullable: false),
                    Q = table.Column<double>(type: "float", nullable: false),
                    OEE = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shot", x => new { x.ShiftReportId, x.Id });
                    table.ForeignKey(
                        name: "FK_Shot_ShiftReports_ShiftReportId",
                        column: x => x.ShiftReportId,
                        principalTable: "ShiftReports",
                        principalColumn: "ShiftReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MFC",
                columns: table => new
                {
                    MFCId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MFCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinValue = table.Column<double>(type: "float", nullable: false),
                    MaxValue = table.Column<double>(type: "float", nullable: false),
                    StationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReferenceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MFC", x => x.MFCId);
                    table.ForeignKey(
                        name: "FK_MFC_StationReferences_ReferenceId_StationId",
                        columns: x => new { x.ReferenceId, x.StationId },
                        principalTable: "StationReferences",
                        principalColumns: new[] { "ReferenceId", "StationId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorInformations_StationId",
                table: "ErrorInformations",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorStatus_ErrorId",
                table: "ErrorStatus",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_LineProduct_UsableLinesLineId",
                table: "LineProduct",
                column: "UsableLinesLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_ReferenceId",
                table: "Lines",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Lot_LotCode",
                table: "Lot",
                column: "LotCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lot_ReferenceId",
                table: "Lot",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineStatus_StationId",
                table: "MachineStatus",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_MFC_ReferenceId_StationId",
                table: "MFC",
                columns: new[] { "ReferenceId", "StationId" });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStation_StationsStationId",
                table: "ProductStation",
                column: "StationsStationId");

            migrationBuilder.CreateIndex(
                name: "IX_References_ProductId",
                table: "References",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_References_ReferenceName",
                table: "References",
                column: "ReferenceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftReports_StationId",
                table: "ShiftReports",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftReports_StationId1",
                table: "ShiftReports",
                column: "StationId1");

            migrationBuilder.CreateIndex(
                name: "IX_StationReferences_StationId",
                table: "StationReferences",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_LineId",
                table: "Stations",
                column: "LineId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecord_EmployeeId",
                table: "WorkRecord",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkRecord_StationId",
                table: "WorkRecord",
                column: "StationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorStatus");

            migrationBuilder.DropTable(
                name: "LineProduct");

            migrationBuilder.DropTable(
                name: "Lot");

            migrationBuilder.DropTable(
                name: "MachineStatus");

            migrationBuilder.DropTable(
                name: "MFC");

            migrationBuilder.DropTable(
                name: "ProductStation");

            migrationBuilder.DropTable(
                name: "Shot");

            migrationBuilder.DropTable(
                name: "WorkRecord");

            migrationBuilder.DropTable(
                name: "ErrorInformations");

            migrationBuilder.DropTable(
                name: "StationReferences");

            migrationBuilder.DropTable(
                name: "ShiftReports");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
