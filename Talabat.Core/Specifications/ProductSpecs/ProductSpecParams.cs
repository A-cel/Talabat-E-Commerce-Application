namespace Talabat.Core.Specifications.ProductSpecs
{
    public class ProductSpecParams
    {
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        private const int MaxSize = 10;
        private int pageSize =5;

        public int PageSize
        {
            get { return pageSize ; }
            set { pageSize = value > MaxSize ? MaxSize: value ; }
        }



        public int PageIndex { get; set; } = 1;
        public string? Search {  get; set; } 
       
        
    }
}
