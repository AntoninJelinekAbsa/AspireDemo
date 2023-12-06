namespace AspireDemo.Frontend.Grid
{
    public class GridControls : IIdeaFilters
    {
        // Keep state of paging.
        public IPageHelper PageHelper { get; set; }

        public GridControls(IPageHelper pageHelper)
        {
            PageHelper = pageHelper;
        }

        // Avoid multiple concurrent requests.
        public bool Loading { get; set; }

        // Firstname Lastname, or Lastname, Firstname.
        public bool ShowFirstNameFirst { get; set; }

        // Column to sort by.
        public IdeaFilterColumns SortColumn { get; set; } = IdeaFilterColumns.Title;

        // True when sorting ascending, otherwise sort descending.
        public bool SortAscending { get; set; } = true;

        // Column filtered text is against.
        public IdeaFilterColumns FilterColumn { get; set; } = IdeaFilterColumns.Title;

        // Text to filter on.
        public string? FilterText { get; set; }
    }
}
