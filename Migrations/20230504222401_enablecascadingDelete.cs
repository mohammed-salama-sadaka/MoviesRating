using Microsoft.EntityFrameworkCore.Migrations;
using MoviesRating.Models;
using System.Data;

namespace MoviesRating.Migrations
{
    public partial class enablecascadingDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.Reviews DROP CONSTRAINT FK_Reviews_Movies_MovieId;");
            migrationBuilder.Sql("ALTER TABLE dbo.Reviews DROP CONSTRAINT FK_Reviews_Users_UserId;");

            migrationBuilder.Sql("ALTER TABLE dbo.Reviews ADD CONSTRAINT FK_Reviews_Movies_MovieId FOREIGN KEY (MovieId) REFERENCES dbo.Movies (Id) ON DELETE CASCADE;");
            migrationBuilder.Sql("ALTER TABLE dbo.Reviews ADD CONSTRAINT FK_Reviews_Users_UserId FOREIGN KEY (UserId) REFERENCES dbo.Users (Id) ON DELETE CASCADE;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE dbo.Reviews ADD CONSTRAINT FK_Reviews_Movies_MovieId FOREIGN KEY (MovieId) REFERENCES dbo.Movies (Id) ON DELETE Restrict;");
            migrationBuilder.Sql("ALTER TABLE dbo.Reviews ADD CONSTRAINT FK_Reviews_Users_UserId FOREIGN KEY (UserId) REFERENCES dbo.Users (Id) ON DELETE Restrict;");
        }
    }
}
