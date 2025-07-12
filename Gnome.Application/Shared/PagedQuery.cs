using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public class PagedQuery<T> : CommandOrQueryBase<T>
    {
        private int _page = 1; // Default to page 1
        private int _pageSize = 25; // Default to 25 items per page

        public int Page
        {
            get
            {
                return _page < 1 ? 1 : _page;
            }
            set
            {
                _page = value < 1 ? 1 : value;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize < 1 ? 25 : _pageSize;
            }
            set
            {
                _pageSize = value < 1 ? 25 : value;
            }
        }

        public string SortBy { get; set; } = "created-date-time"; // Default sort by created date
        public string SortOrder { get; set; } = "desc"; // Default sort order
    }
}
