using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class Initaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Category_Course",
                columns: table => new
                {
                    Category_coures_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category_description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Category_Course", x => x.Category_coures_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Class_Role",
                columns: table => new
                {
                    Class_Role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Class_Role", x => x.Class_Role_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Role",
                columns: table => new
                {
                    Role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Role", x => x.Role_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Coures",
                columns: table => new
                {
                    Coures_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coures_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category_coures_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Coures", x => x.Coures_id);
                    table.ForeignKey(
                        name: "FK_tb_Coures_tb_Category_Course_Category_coures_id",
                        column: x => x.Category_coures_id,
                        principalTable: "tb_Category_Course",
                        principalColumn: "Category_coures_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_User",
                columns: table => new
                {
                    User_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fullname = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sex_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCCD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_User", x => x.User_id);
                    table.ForeignKey(
                        name: "FK_tb_User_tb_Role_Role_id",
                        column: x => x.Role_id,
                        principalTable: "tb_Role",
                        principalColumn: "Role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Exercise",
                columns: table => new
                {
                    Exercise_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exercise_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creat_time = table.Column<DateTime>(type: "datetime2", maxLength: 250, nullable: false),
                    File_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link_submit_assignments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Exercise", x => x.Exercise_id);
                    table.ForeignKey(
                        name: "FK_tb_Exercise_tb_Coures_Course_id",
                        column: x => x.Course_id,
                        principalTable: "tb_Coures",
                        principalColumn: "Coures_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Student_Course",
                columns: table => new
                {
                    Student_id = table.Column<int>(type: "int", nullable: false),
                    Major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Student_Course", x => x.Student_id);
                    table.ForeignKey(
                        name: "FK_tb_Student_Course_tb_User_Student_id",
                        column: x => x.Student_id,
                        principalTable: "tb_User",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Teacher_Course",
                columns: table => new
                {
                    Teacher_Coures_id = table.Column<int>(type: "int", nullable: false),
                    Teaching_major = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Teacher_Course", x => x.Teacher_Coures_id);
                    table.ForeignKey(
                        name: "FK_tb_Teacher_Course_tb_Coures_Course_id",
                        column: x => x.Course_id,
                        principalTable: "tb_Coures",
                        principalColumn: "Coures_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_Teacher_Course_tb_User_Teacher_Coures_id",
                        column: x => x.Teacher_Coures_id,
                        principalTable: "tb_User",
                        principalColumn: "User_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Student_Class",
                columns: table => new
                {
                    Student_Class_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Role_id = table.Column<int>(type: "int", nullable: false),
                    Student_Course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Student_Class", x => x.Student_Class_id);
                    table.ForeignKey(
                        name: "FK_tb_Student_Class_tb_Class_Role_Class_Role_id",
                        column: x => x.Class_Role_id,
                        principalTable: "tb_Class_Role",
                        principalColumn: "Class_Role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_Student_Class_tb_Student_Course_Student_Course_id",
                        column: x => x.Student_Course_id,
                        principalTable: "tb_Student_Course",
                        principalColumn: "Student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Teacher_Class",
                columns: table => new
                {
                    Study_Class_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Class_Role_id = table.Column<int>(type: "int", nullable: false),
                    Teacher_Course_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Teacher_Class", x => x.Study_Class_id);
                    table.ForeignKey(
                        name: "FK_tb_Teacher_Class_tb_Class_Role_Class_Role_id",
                        column: x => x.Class_Role_id,
                        principalTable: "tb_Class_Role",
                        principalColumn: "Class_Role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_Teacher_Class_tb_Teacher_Course_Teacher_Course_id",
                        column: x => x.Teacher_Course_id,
                        principalTable: "tb_Teacher_Course",
                        principalColumn: "Teacher_Coures_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Coures_Category_coures_id",
                table: "tb_Coures",
                column: "Category_coures_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Exercise_Course_id",
                table: "tb_Exercise",
                column: "Course_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Student_Class_Class_Role_id",
                table: "tb_Student_Class",
                column: "Class_Role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Student_Class_Student_Course_id",
                table: "tb_Student_Class",
                column: "Student_Course_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Teacher_Class_Class_Role_id",
                table: "tb_Teacher_Class",
                column: "Class_Role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Teacher_Class_Teacher_Course_id",
                table: "tb_Teacher_Class",
                column: "Teacher_Course_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Teacher_Course_Course_id",
                table: "tb_Teacher_Course",
                column: "Course_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_User_Role_id",
                table: "tb_User",
                column: "Role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Exercise");

            migrationBuilder.DropTable(
                name: "tb_Student_Class");

            migrationBuilder.DropTable(
                name: "tb_Teacher_Class");

            migrationBuilder.DropTable(
                name: "tb_Student_Course");

            migrationBuilder.DropTable(
                name: "tb_Class_Role");

            migrationBuilder.DropTable(
                name: "tb_Teacher_Course");

            migrationBuilder.DropTable(
                name: "tb_Coures");

            migrationBuilder.DropTable(
                name: "tb_User");

            migrationBuilder.DropTable(
                name: "tb_Category_Course");

            migrationBuilder.DropTable(
                name: "tb_Role");
        }
    }
}
