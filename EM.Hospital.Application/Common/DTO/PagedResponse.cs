using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.DTO
{
    public class PagedResponse<TResponse>
    {
        public ICollection<TResponse> Data { get; set; } = new List<TResponse>();
        public int TotalRowsPerPage { get; set; }
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
    }
}
