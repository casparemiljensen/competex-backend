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
        Console.WriteLine(pageSize);
        Console.WriteLine(pageNumber);
        Console.WriteLine((int)pageSize * ((int)pageNumber - 1));
        return (int)pageSize * ((int)pageNumber - 1);
    }

    public static int GetTotalPages(int? pageSize, int? pageNumber, int numberOfLines)
    {
        return (int)Math.Ceiling((double)(numberOfLines / (pageSize ?? Defaults.PageSize)) + 1);
    }
}