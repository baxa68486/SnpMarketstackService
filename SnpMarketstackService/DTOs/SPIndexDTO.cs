using System.Collections.Generic;

namespace SnpMarketstackService.DTOs
{
    public class SPIndexDTO
    {
        public SPIndexPaginationDTO Pagination { get; init; }
        public List<SPIndexValueDTO> Data { get; init; }
    }
}
