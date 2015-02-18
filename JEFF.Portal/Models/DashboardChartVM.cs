using System.Collections.Generic;
using System.Linq;

namespace JEFF.Portal.Models
{
    /// <summary>
    /// DashboardChartVM
    /// </summary>
    public class DashboardChartVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardChartVM"/> class.
        /// </summary>
        public DashboardChartVM()
        {
            Series = new List<DashboardChartSeriesVM>();
        }

        /// <summary>
        /// Gets or sets the series.
        /// </summary>
        /// <value>
        /// The series.
        /// </value>
        public IList<DashboardChartSeriesVM> Series { get; set; }
    }

    /// <summary>
    /// DashboardChartSeriesVM
    /// </summary>
    public class DashboardChartSeriesVM
    {
        string _total = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardChartSeriesVM"/> class.
        /// </summary>
        public DashboardChartSeriesVM()
        {
            Items = new List<DateTimeSeriesItemVM>();
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public string Total
        {
            get
            {
                return string.IsNullOrEmpty(_total) ? (from i in Items
                                                       select i.Value).Sum().ToString() : _total;
            }
            set
            {
                _total = value;
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public IList<DateTimeSeriesItemVM> Items { get; set; }
    }
}