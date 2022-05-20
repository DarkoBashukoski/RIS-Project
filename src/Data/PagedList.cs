namespace src.Data {
    public class PagedList<T> : List<T> {
        public int pageIndex {get; private set;}
        public int totalPages {get; private set;}

        public PagedList(List<T> items, int count, int pageIndex, int pageSize) {
            this.pageIndex = pageIndex;
            this.totalPages = (int) Math.Ceiling(count / (double)pageSize);

            this.AddRange(items);
        }

        public bool hasPreviousPage {get {return pageIndex > 1;}}

        public bool hasNextPage {get {return pageIndex < totalPages;}}

        public static PagedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize) {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageIndex, pageSize);
        }
    }
}