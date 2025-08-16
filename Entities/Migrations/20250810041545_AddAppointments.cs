using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("3fe0d485-b0e4-4697-8cbe-651ef310b751"),
                column: "ServiceDescribtion",
                value: " لتعلم قبول الرفض وازاي تقولي لاءه وتصنعي حدود امنه بكل لطف دايره تعايش وتدريب .");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_BookingID",
                table: "Appointments",
                column: "BookingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ServiceId",
                keyValue: new Guid("3fe0d485-b0e4-4697-8cbe-651ef310b751"),
                column: "ServiceDescribtion",
                value: " لتعلم قبول الرفض وازاي تقولي لاءه وتصنعي حدود امنه بكل لطف . دايره تعايش وتدريب");
        }
    }
}
