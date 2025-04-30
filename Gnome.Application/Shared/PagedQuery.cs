using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Shared
{
    public class PagedQuery<T> : CommandOrQueryBase<T>
    {
        private int _page;
        private int _pageSize;

        public int Page
        {
            get
            {
                return _page < 1 ? 1 : _page;
            }
            set
            {
                _page = value;
            }
        }

        public int PageSize
        {
            get
            {
                return _pageSize < 10 ? 10 : _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
