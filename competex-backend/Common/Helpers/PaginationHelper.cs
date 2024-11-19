using competex_backend.Common.ErrorHandling;
using competex_backend.DAL.Interfaces;

namespace competex_backend.Common.Helpers;

public static class PaginationHelper
{
    public static int GetPage(int? skip, int? take)
    {
        if (skip == null || skip < 0)
        {
            return 1;
        }

        if (take == null || take <= 0)
        {
            return (int)skip / Defaults.PageSize;
        }

        return (int)skip / (int)take;
    }

    public static int GetSkip(int? pageSize, int? pageNumber)
    {
        pageSize = pageSize ?? Defaults.PageSize;

        if (pageNumber == null || pageNumber <= 0)
        {
            return 0;
        }
        return (int)pageSize * ((int)pageNumber - 1);
    }

    public static int GetTotalPages(int? pageSize, int? pageNumber, int numberOfLines)
    {
        return (int)Math.Ceiling((double)(numberOfLines / (pageSize ?? Defaults.PageSize)) + 1);
    }

    public async static Task<IEnumerable<T>> GetAll<T, TRepo>(TRepo repo, Dictionary<string, object>? searchParams) where TRepo : IGenericRepository<T> where T : class
    {
        List<T> output = [];
        int pageNr = 0;
        while (true)
        {
            var result = await repo.SearchAllAsync(2, pageNr, searchParams);

            if (!result.IsSuccess)
            {
                throw new ApiException(500, "Failed to access DataBase");
            }

            foreach (var item in result.Value.Item2)
            {
                output.Add(item);
            }

            if (result.Value.Item1 == pageNr)
            {
                break;
            }
            pageNr++;
        }
        return output.ToList();
    }
}