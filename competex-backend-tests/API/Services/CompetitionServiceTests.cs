using competex_backend.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using competex_backend.Models;
using competex_backend.API.DTOs;

namespace competex_backend_tests.API.Services
{
    public class CompetitionServiceTests
    {
        private readonly Mock<ICompetitionService> _mockCompetitionService;
        private readonly Mock<ICompetitionTypeService> _mockCompetitionTypeService;

        public CompetitionServiceTests()
        {
            _mockCompetitionTypeService = new Mock<ICompetitionTypeService>();
            _mockCompetitionService = new Mock<ICompetitionService>();
        }

        [Fact]
        public void GetItem_ValidId_ReturnsExpectedItem()
        {
            _mockCompetitionTypeService.Setup(service => service.CreateAsync(new CompetitionTypeDTO() { }));
            /*
            // Act
            var result = _mockCompetitionTypeService.();
            // Arrange
            var expectedItem = new Competition {  };
            _mockService.Setup(service => service.GetItem(1)).Returns(expectedItem);

            // Act
            var result = _controller.GetItem(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedItem.Name, result.Name);*/
        }
    }
}
