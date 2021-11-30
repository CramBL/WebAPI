using System;
using Moq;
using WebAPI.Models;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void GenerateTokenTest()
        {
            // arrange
            Mock<User> mockUser = new Mock<User>();
            mockUser.SetupSet(u => u.FirstName = "Jens");
            mockUser.SetupSet(u => u.Email = "jens@email.com");
            mockUser.SetupSet(u => u.UserId = 12);

            
        }
    }
}
