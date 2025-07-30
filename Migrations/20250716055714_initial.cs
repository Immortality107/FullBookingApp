using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookingApp.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentDetails = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServiceDescribtion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServicePriceLocal = table.Column<double>(type: "float", nullable: false),
                    ServicePriceInternational = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Bookings_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Services_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Services",
                        principalColumn: "ServiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientID", "Age", "ClientName", "Country", "Email", "Gender", "Notes", "Phone" },
                values: new object[,]
                {
                    { new Guid("485621b3-2a44-4d22-a870-ee9cc3f22326"), 36, "Maha", "USA", "Test@gmail.com", "female", "", "+12029882614" },
                    { new Guid("5ee9b5ae-1d0a-41ea-8943-eb52a4c9f20a"), 28, "Kholoud", "Egypt", "kholouddesouky@gmail.com", "female", "", "0100000000" },
                    { new Guid("dd651db5-c90d-4bda-bab9-a8e8e83b7df2"), 40, "Rania", "Germany", "Rania@gmail.com", "female", "", "+4932211077146" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "Location", "PaymentDetails", "PaymentName" },
                values: new object[,]
                {
                    { new Guid("2ebdc7a1-1398-48f1-940d-c4022acce082"), "Egypt", "100044321747", "CIB Bank Transfer" },
                    { new Guid("8762980a-28f1-47b4-84cd-47d65ae7ca71"), "Egypt", "Asmaa.mostafa1987@instapay", "Instapay" },
                    { new Guid("945f0aa6-2210-46c5-bcb5-67b77d221f29"), "International", "+1 (214) 912-7068", "Zelle (US)" },
                    { new Guid("bd622138-dc61-456e-9187-386bd55c7656"), "International", "a_abdelgawad@hotmail.com", "Paypal" },
                    { new Guid("dfc9ec27-9b64-47b8-ab41-799dc2f61f03"), "International", "IBAN:SA16 8000 0858 6080 1286 3374 - Accountnumber:  077030010006082863374 - Name: Hassan Mohamed Shawki ElHayawan", "بنك الراجحي السعودي" },
                    { new Guid("fc64db83-e94b-4e07-a438-1b71cc4e2fb8"), "Egypt", "01006970792", "Vodafone Cash" },
                    { new Guid("fffe67f8-db58-4f1d-907e-a3753ce3c939"), "International", "IBAN: AE85 0260 0010 1579 8179 101 - Accountnumber: 1015798179101 - Name: Mayar Ahmed", "Emirates NBD" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "ServiceDescribtion", "ServiceName", "ServicePriceInternational", "ServicePriceLocal" },
                values: new object[,]
                {
                    { new Guid("0e0dd7bd-4941-4d50-956e-d3553580fadc"), "لمشاركة كل تحديات الحياه بشكل عام ، وانطلاقه امنه واكثر صحية", "دايرة سنجل مامي", 60.0, 600.0 },
                    { new Guid("10f2cd9c-95c3-4d80-afc6-b3b36836f588"), " لتشافي علاقتك مع المال والوصول للثراء والحريه الماليه بالتناغم مع الانوثه", "دايرة الوفرة المالية", 350.0, 3500.0 },
                    { new Guid("3fe0d485-b0e4-4697-8cbe-651ef310b751"), " لتعلم قبول الرفض وازاي تقولي لاءه وتصنعي حدود امنه بكل لطف . دايره تعايش وتدريب", "دايرة الرفض", 300.0, 3000.0 },
                    { new Guid("64ed73c8-94c4-44b8-b8ef-af46be9f2d73"), "لاسكتشاف شغفك وبصمتك الفريده وتحويلها لمشروع وادارته والربح منه بالتناغم مع الانوثه", "دايرة سحر التمكين", 350.0, 3500.0 },
                    { new Guid("756d3b5a-a2a2-426c-b9b6-7f8552c64bb4"), "لممارسة الامتنان", "دايرة الشكر", 60.0, 600.0 },
                    { new Guid("8ec38d0b-1e87-43a2-92fc-f8511a9de30d"), "لتواصل اعمق و مريح مع جسدك  .. هي فرصة لعمل علاقه حقيقيه مع جسمك كأنثى واستكشافه ، وقبوله زي ماهو بدون تعديلات او بدون شروط", "دايرة مساحة جسد", 500.0, 5000.0 },
                    { new Guid("a0561a1e-f2ee-4256-bf77-7b4a83d20d35"), "هنتقابل مرة في الشهر نحكي ونتشارك ونسمع بعض بقلوب رحيمة بدون احكام ولا نصايح ندعم , نطبطب , نحضن , نقبل بعض بكل اللي فينا باللي عاجبنا و اللي مش عاجبنا", "دايرة الأنس", 60.0, 600.0 },
                    { new Guid("e412d77e-77d3-4c06-8f7b-4df9490e04d6"), "", "(استشارات فردية (اونلاين/اوفلاين", 170.0, 1700.0 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingId", "Amount", "ClientID", "PaymentID", "ServiceID", "Status" },
                values: new object[,]
                {
                    { new Guid("1a3e69e4-7ec9-48ff-8713-3a0c5d48702c"), 200.0, new Guid("485621b3-2a44-4d22-a870-ee9cc3f22326"), new Guid("2ebdc7a1-1398-48f1-940d-c4022acce082"), new Guid("756d3b5a-a2a2-426c-b9b6-7f8552c64bb4"), "Waiting" },
                    { new Guid("627bb73e-8232-466f-b348-799b7bcd9b01"), 50.0, new Guid("485621b3-2a44-4d22-a870-ee9cc3f22326"), new Guid("2ebdc7a1-1398-48f1-940d-c4022acce082"), new Guid("756d3b5a-a2a2-426c-b9b6-7f8552c64bb4"), "Cancelled" },
                    { new Guid("d2e84756-7743-4265-a40f-bac354fe0f31"), 2000.0, new Guid("485621b3-2a44-4d22-a870-ee9cc3f22326"), new Guid("2ebdc7a1-1398-48f1-940d-c4022acce082"), new Guid("756d3b5a-a2a2-426c-b9b6-7f8552c64bb4"), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "BookingID", "ReviewMessage" },
                values: new object[,]
                {
                    { new Guid("627bb73e-8232-466f-b348-799b7bcd9b01"), new Guid("d2e84756-7743-4265-a40f-bac354fe0f31"), "Very friendly staff and clean environment." },
                    { new Guid("d84c39b2-0f8e-4e6d-8918-31c264ce3fd2"), new Guid("1a3e69e4-7ec9-48ff-8713-3a0c5d48702c"), "Satisfactory experience overall." },
                    { new Guid("e3cfe13f-03bb-4b12-85d8-f4e4a7cd4aa1"), new Guid("d2e84756-7743-4265-a40f-bac354fe0f31"), "Excellent Payment, highly recommended!" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientID",
                table: "Bookings",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PaymentID",
                table: "Bookings",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ServiceID",
                table: "Bookings",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookingID",
                table: "Reviews",
                column: "BookingID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
