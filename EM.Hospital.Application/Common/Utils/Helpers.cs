using EM.Hospital.Domain.Common;

namespace EM.Hospital.Application.Common.Utils
{
    public static class Helpers
    {
        public static Result<int> CalculateTotalPages(int totalRows, int pageSize) 
        {
            if (pageSize <= 0)            
                return Result.Failure<int>("Page size must be greater than zero.");
            return Result.Success((int)Math.Ceiling((double)totalRows / pageSize));
        }
    }
}
