using System.Collections.Generic;

namespace Prueba.Pagination
{
    public class GenericPaginator<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public string CurrentSearch { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentSortType { get; set; }
        public IEnumerable<T> Result { get; set; }

    }
}
