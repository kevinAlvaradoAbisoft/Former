using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class Zresponse
    {
        public ZohoResponse[] data { get; set; }
    }

    public class ZohoResponse
    {
        public string code { get; set; }
        public object duplicate_field { get; set; }
        public string action { get; set; }
        public Details details { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }

    public class Details
    {
        public DateTime Modified_Time { get; set; }
        public Modified_By Modified_By { get; set; }
        public DateTime Created_Time { get; set; }
        public string id { get; set; }
        public Created_By Created_By { get; set; }
        public string approval_state { get; set; }
    }

    public class Modified_By
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Created_By
    {
        public string name { get; set; }
        public string id { get; set; }
    }

}
