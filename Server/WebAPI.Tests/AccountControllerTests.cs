using System;
using Moq;
using WebAPI.Models;
using WebAPI.Controllers;
using Xunit;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using Microsoft.Extensions.Options;

namespace WebAPI.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void LoginTokenTest()
        {
            // Arrange
            UserDto userDto = new UserDto { FirstName = "Postmand", LastName = "Per", Email = "postmand@per.com", Password = "KatEmil123" };

            var connection = new SqliteConnection("Data source = :memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();

                var appSettings = new AppSettings();
                appSettings.SecretKey = "1234567890abcdef";
                var appSettingOptions = new OptionsWrapper<AppSettings>(appSettings);
            
                AccountController uut = new AccountController(context, appSettingOptions);
                
                // Act
                var result = uut.Register(userDto);
                result.Wait();

                var jwt = uut.Login(userDto);
                jwt.Wait();

                // Assert
                Assert.Contains("eyJhbGc", jwt.Result.Value.JWT); // Token value gotten from test in Postman

            }      

        }
    }
}
