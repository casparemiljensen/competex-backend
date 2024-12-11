using competex_backend.BLL.Interfaces;
using competex_backend.DAL.Interfaces;

namespace competex_backend.Common.Helpers
{
    public static class SearchHelper
    {
        public static async Task<List<T>> GetAllSearch<T, SType>(SType repository, Dictionary<string, object> filter) where SType : IGenericRepository<T> where T : class
        {
            var batchSize = 10;
            var localPageNumber = 1;
            var totalPageNumber = 0;
            List<T> output = [];
            var competitionResult = await repository.SearchAllAsync(batchSize, localPageNumber, filter);
            localPageNumber++;
            if (!competitionResult.IsSuccess)
            {
                throw new Exception("404");
            }

            output.AddRange(competitionResult.Value.Item2);

            totalPageNumber = PaginationHelper.GetTotalPages(batchSize, localPageNumber, competitionResult.Value.Item1);
            List<Task<ResultT<Tuple<int, IEnumerable<T>>>>> tasks = [];
            while (localPageNumber <= totalPageNumber)
            {
                tasks.Add(repository.SearchAllAsync(batchSize, localPageNumber, filter));
                localPageNumber++;
            }

            await Task.WhenAll(tasks);

            foreach (var task in tasks)
            {
                foreach (var a in task.Result.Value.Item2)
                {
                    output.Add(a);
                }
            }
            return output;
        }
    }
}
