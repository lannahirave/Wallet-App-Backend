using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WAB.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CardBalance = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    DailyPoints = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    LastDailyPoints = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Pending = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AuthorizedUserId = table.Column<int>(type: "integer", nullable: true),
                    Icon = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_AuthorizedUserId",
                        column: x => x.AuthorizedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CardBalance", "DailyPoints", "LastDailyPoints" },
                values: new object[,]
                {
                    { -1000, 1046.618133191682000m, 31.809586821367600m, new DateTime(2023, 3, 10, 17, 53, 57, 930, DateTimeKind.Local).AddTicks(5826) },
                    { -999, 233.370281884623000m, 43.038574131343100m, new DateTime(2023, 7, 22, 21, 45, 25, 868, DateTimeKind.Local).AddTicks(1452) },
                    { -998, 359.486634612021000m, 91.905015824269200m, new DateTime(2023, 1, 22, 12, 43, 31, 552, DateTimeKind.Local).AddTicks(9777) },
                    { -997, 787.40435211663000m, 21.838570560409600m, new DateTime(2023, 6, 27, 1, 18, 55, 100, DateTimeKind.Local).AddTicks(123) },
                    { -996, 873.490348008382500m, 33.400219128934300m, new DateTime(2022, 11, 3, 5, 58, 20, 429, DateTimeKind.Local).AddTicks(1230) },
                    { -995, 740.784236899366500m, 41.427117191454700m, new DateTime(2023, 7, 9, 15, 54, 27, 231, DateTimeKind.Local).AddTicks(6943) },
                    { -994, 851.427571974181500m, 88.377479179915100m, new DateTime(2023, 1, 8, 3, 37, 55, 625, DateTimeKind.Local).AddTicks(7848) },
                    { -993, 1450.808413905981000m, 71.89139106908800m, new DateTime(2023, 7, 13, 18, 54, 11, 932, DateTimeKind.Local).AddTicks(4393) },
                    { -992, 476.086152514363500m, 22.053992190128400m, new DateTime(2022, 9, 11, 16, 28, 51, 102, DateTimeKind.Local).AddTicks(3091) },
                    { -991, 1460.132466294733500m, 39.371039840198400m, new DateTime(2023, 1, 26, 19, 20, 54, 85, DateTimeKind.Local).AddTicks(2247) }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "AuthorizedUserId", "Date", "Description", "Icon", "Name", "Pending", "Type", "UserId" },
                values: new object[,]
                {
                    { -2000, 409.58m, null, new DateTime(2023, 4, 30, 6, 52, 58, 862, DateTimeKind.Utc).AddTicks(7944), "Iure dolor odio voluptas non.", "https://picsum.photos/640/480/?image=738", "Handcrafted Plastic Fish", false, 1, -991 },
                    { -1999, 364.25m, null, new DateTime(2023, 3, 2, 2, 58, 47, 150, DateTimeKind.Utc).AddTicks(9561), "Molestiae ea praesentium expedita nemo.", "https://picsum.photos/640/480/?image=663", "Fantastic Soft Sausages", false, 0, -998 },
                    { -1998, 150.53m, -998, new DateTime(2022, 11, 21, 7, 49, 50, 847, DateTimeKind.Utc).AddTicks(7208), "Perferendis optio vero veritatis sequi.", "https://picsum.photos/640/480/?image=279", "Gorgeous Soft Table", true, 0, -992 },
                    { -1997, 384.56m, null, new DateTime(2023, 3, 9, 18, 0, 3, 755, DateTimeKind.Utc).AddTicks(1846), "Facere hic sed eius voluptatem.", "https://picsum.photos/640/480/?image=1008", "Unbranded Fresh Sausages", false, 1, -997 },
                    { -1996, 248.62m, -991, new DateTime(2023, 3, 9, 3, 29, 17, 759, DateTimeKind.Utc).AddTicks(1983), "Sint esse eaque laboriosam voluptatem.", "https://picsum.photos/640/480/?image=641", "Handcrafted Soft Gloves", false, 1, -998 },
                    { -1995, 230.57m, -994, new DateTime(2023, 6, 25, 22, 45, 29, 655, DateTimeKind.Utc).AddTicks(475), "Iste ab aut aut est.", "https://picsum.photos/640/480/?image=91", "Gorgeous Metal Tuna", false, 1, -997 },
                    { -1994, 225.35m, null, new DateTime(2022, 12, 13, 16, 4, 22, 329, DateTimeKind.Utc).AddTicks(7867), "Ipsa iste magni maxime inventore.", "https://picsum.photos/640/480/?image=86", "Refined Plastic Car", false, 0, -997 },
                    { -1993, 345.71m, null, new DateTime(2022, 12, 13, 7, 4, 51, 411, DateTimeKind.Utc).AddTicks(3077), "Dolores non aut aut omnis.", "https://picsum.photos/640/480/?image=840", "Small Frozen Sausages", true, 0, -997 },
                    { -1992, 50.20m, -996, new DateTime(2023, 6, 28, 15, 19, 24, 609, DateTimeKind.Utc).AddTicks(6019), "Error itaque deleniti praesentium sed.", "https://picsum.photos/640/480/?image=910", "Ergonomic Frozen Tuna", true, 0, -999 },
                    { -1991, 240.07m, null, new DateTime(2023, 4, 28, 15, 11, 36, 757, DateTimeKind.Utc).AddTicks(7779), "Sunt est architecto aut eum.", "https://picsum.photos/640/480/?image=397", "Small Plastic Shirt", false, 0, -994 },
                    { -1990, 177.79m, null, new DateTime(2023, 4, 6, 23, 45, 44, 729, DateTimeKind.Utc).AddTicks(6161), "Repellat laudantium animi beatae voluptatibus.", "https://picsum.photos/640/480/?image=944", "Generic Metal Soap", false, 0, -996 },
                    { -1989, 119.86m, -1000, new DateTime(2022, 9, 17, 7, 18, 44, 457, DateTimeKind.Utc).AddTicks(1713), "Ut nemo tenetur delectus cumque.", "https://picsum.photos/640/480/?image=1007", "Unbranded Concrete Computer", false, 1, -999 },
                    { -1988, 290.30m, null, new DateTime(2023, 7, 24, 2, 15, 13, 80, DateTimeKind.Utc).AddTicks(9638), "Sint qui ad laboriosam explicabo.", "https://picsum.photos/640/480/?image=807", "Refined Concrete Gloves", false, 0, -1000 },
                    { -1987, 498.81m, null, new DateTime(2022, 7, 31, 9, 10, 4, 55, DateTimeKind.Utc).AddTicks(79), "Non nihil odit qui dolorem.", "https://picsum.photos/640/480/?image=344", "Intelligent Metal Sausages", false, 1, -991 },
                    { -1986, 184.46m, -997, new DateTime(2022, 9, 12, 2, 25, 33, 833, DateTimeKind.Utc).AddTicks(1467), "Quia incidunt aliquid libero cupiditate.", "https://picsum.photos/640/480/?image=819", "Sleek Fresh Shoes", false, 0, -997 },
                    { -1985, 494.14m, null, new DateTime(2022, 11, 26, 21, 0, 38, 555, DateTimeKind.Utc).AddTicks(26), "Velit aspernatur soluta quasi distinctio.", "https://picsum.photos/640/480/?image=762", "Handcrafted Rubber Fish", true, 1, -999 },
                    { -1984, 397.52m, null, new DateTime(2022, 10, 13, 19, 0, 6, 856, DateTimeKind.Utc).AddTicks(6254), "Non aliquam sunt facilis architecto.", "https://picsum.photos/640/480/?image=551", "Sleek Metal Tuna", false, 0, -997 },
                    { -1983, 452.33m, null, new DateTime(2023, 2, 9, 19, 32, 21, 521, DateTimeKind.Utc).AddTicks(9068), "Atque qui aut saepe aliquam.", "https://picsum.photos/640/480/?image=349", "Tasty Steel Chips", false, 1, -992 },
                    { -1982, 284.05m, null, new DateTime(2023, 6, 12, 5, 56, 4, 945, DateTimeKind.Utc).AddTicks(9269), "Iure perspiciatis est iure ab.", "https://picsum.photos/640/480/?image=831", "Ergonomic Concrete Towels", false, 0, -999 },
                    { -1981, 436.30m, null, new DateTime(2022, 8, 27, 16, 37, 2, 754, DateTimeKind.Utc).AddTicks(7861), "Porro architecto sed alias molestiae.", "https://picsum.photos/640/480/?image=764", "Practical Wooden Chair", false, 0, -999 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AuthorizedUserId",
                table: "Transactions",
                column: "AuthorizedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
