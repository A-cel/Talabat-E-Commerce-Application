namespace Talabat.Api.Helpers
{
    public class PagintationData<T> 
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public PagintationData(int pagesize , int pageindex , IReadOnlyList<T> data , int count)
        {
            Count = count;
            PageSize = pagesize;
            PageIndex = pageindex;
            Data = data;
        }
    }
}
