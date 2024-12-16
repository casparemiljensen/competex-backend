using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using competex_backend.BLL.Services;
using competex_backend.Common.ErrorHandling;
using competex_backend.Common.Helpers;
using competex_backend.DAL.Interfaces;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

public class PaginationHelperTests
{
    public class Defaults
    {
        public const int PageSize = 10;
    }

    private readonly Mock<IMapper> _mapperMock;
    private readonly MemberService _memberService;
    private readonly Mock<IMemberRepository> _roundRepositoryMock;

    public PaginationHelperTests()
    {
        _roundRepositoryMock = new Mock<IMemberRepository>();
        _mapperMock = new Mock<IMapper>();
        _memberService = new MemberService(_roundRepositoryMock.Object, _mapperMock.Object);
    }

    public interface IGenericRepository<T>
    {
        Task<(bool IsSuccess, (int, IEnumerable<T>) Value)> SearchAllAsync(int pageSize, int pageNr, Dictionary<string, object>? searchParams);
    }

    [Fact]
    public void GetPage_WithNullSkip_ReturnsDefaultPage()
    {
        int result = PaginationHelper.GetPage(null, 10);
        Assert.Equal(1, result);
    }

    [Fact]
    public void GetPage_WithNegativeSkip_ReturnsDefaultPage()
    {
        int result = PaginationHelper.GetPage(-1, 10);
        Assert.Equal(1, result);
    }

    [Fact]
    public void GetPage_WithNullTake_ReturnsCalculatedPage()
    {
        int result = PaginationHelper.GetPage(20, null);
        Assert.Equal(2, result);
    }

    [Fact]
    public void GetPage_WithValidSkipAndTake_ReturnsCalculatedPage()
    {
        int result = PaginationHelper.GetPage(20, 5);
        Assert.Equal(4, result);
    }

    [Fact]
    public void GetSkip_WithNullPageSize_ReturnsZero()
    {
        int result = PaginationHelper.GetSkip(null, 1);
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetSkip_WithNullPageNumber_ReturnsZero()
    {
        int result = PaginationHelper.GetSkip(10, null);
        Assert.Equal(0, result);
    }

    [Fact]
    public void GetSkip_WithValidInputs_ReturnsCalculatedSkip()
    {
        int result = PaginationHelper.GetSkip(10, 3);
        Assert.Equal(20, result);
    }

    [Fact]
    public void GetTotalPages_WithNullPageSize_ReturnsCorrectTotalPages()
    {
        int result = PaginationHelper.GetTotalPages(null, 1, 50);
        Assert.Equal(6, result); // (50 / 10) + 1 = 6
    }

    [Fact]
    public void GetTotalPages_WithValidInputs_ReturnsCorrectTotalPages()
    {
        int result = PaginationHelper.GetTotalPages(5, 1, 50);
        Assert.Equal(11, result); // (50 / 5) + 1 = 11
    }
}
