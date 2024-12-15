using AutoMapper;
using Common.ResultPattern;
using competex_backend;
using competex_backend.API.DTOs;
using competex_backend.BLL.Interfaces;
using competex_backend.Models;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace competex_backend_tests.API.Common
{
    public class AutomapperTests
    {
        private readonly IMapper _mapper;
        public AutomapperTests() {

            var mockService = new Mock<IGenericService<LocationDTO>>();
            mockService
                .Setup(service => service.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) =>
                {
                    if (id == Guid.Empty)
                        return ResultT<LocationDTO>.Failure(Error.NotFound("Empty Guid", ""));
                    return ResultT<LocationDTO>.Success(new LocationDTO { Id = id, Name = "Test Location" });
                });

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Guid, LocationDTO>()
                   .ConvertUsing(new GuidToObjectConverter<LocationDTO>(mockService.Object)); // Inject the mock into the converter
            });

            _mapper = mapperConfig.CreateMapper();

            //mapperConfig.AssertConfigurationIsValid();
        }

        [Fact]
        public void GuidToObjectConverter_ConvertsGuidToLocationDTO()
        {
            var testGuid = Guid.NewGuid();

            // Act: Map a Guid to LocationDTO
            var location = _mapper.Map<LocationDTO>(testGuid);

            // Assert: Verify the mapping result
            Assert.NotNull(location);
            Assert.Equal(testGuid, location.Id);
            Assert.Equal("Test Location", location.Name);
        }

        [Fact]
        public void EmptyList_Mapping_DoesNotThrow()
        {
            // Arrange
            var emptyList = new List<Round>();

            // Act
            var result = _mapper.Map<List<RoundDTO>>(emptyList);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
