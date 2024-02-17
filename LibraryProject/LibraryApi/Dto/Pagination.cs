namespace LibraryApi.Dto
{
    public class Pagination<T>
    {
     
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageSize, int pageIndex, int count, IReadOnlyList<T> data)
        {
            PageSize = pageSize;
            Count = count;
            PageIndex = pageIndex;
            Data = data;
        }



    }
}
