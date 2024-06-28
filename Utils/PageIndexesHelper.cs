namespace SocialEmpires.Utils
{
    public static class PageIndexesHelper
    {
        public static IEnumerable<int> CalculatePageIndexes(int pageIndex, int pageTotal, int indexTotal)
        {
            int halfVisible = indexTotal / 2;
            int startPage = pageIndex - halfVisible;
            int endPage = pageIndex + halfVisible;

            if (startPage < 1)
            {
                startPage = 1;
                endPage = indexTotal < pageTotal ? indexTotal : pageTotal;
            }
            else if (endPage > pageTotal)
            {
                endPage = pageTotal;
                startPage = pageTotal - indexTotal + 1;
                if (startPage < 1)
                {
                    startPage = 1;
                }
            }

            return Enumerable.Range(startPage, endPage - startPage + 1);
        }
    }
}
