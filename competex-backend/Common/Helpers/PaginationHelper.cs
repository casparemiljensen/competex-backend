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
            return (int)skip / 10; //TODO : 10 should be the default value for take
        }

        return (int)skip / (int)take;
    }

    public static int GetSkip(int? pageSize, int? pageNumber)
    {
        if (pageSize == null || pageSize < 0)
        {
            return 0;
        }

        if (pageNumber == null || pageNumber <= 0)
        {
            return 0;
        }

        return (int)pageSize * ((int)pageNumber - 1);
    }
}