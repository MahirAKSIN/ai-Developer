using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAi.Project3_RapidApi.ViewModels
{
    public class ApiSeriesResponse
    {
        public string page { get; set; }
        public List<ApiSeriesModels> result { get; set; }
    }

    public class ApiSeriesModels
    {
        public int rank { get; set; }
        public string Series_Title { get; set; }
        public string Released_Year { get; set; }
        public string IMDB_Rating { get; set; }
    }
}

