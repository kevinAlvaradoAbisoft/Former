using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWithPostman.DTO
{
    public class Note
    {
        public DatumNote[] data { get; set; }
    }
    public class DatumNote
    {
        //"Note_Title": "Contacted",
        //"Note_Content": "Need to do further tracking",
        //"Parent_Id": "412963000001376069",
        //"se_module": "Leads"
        public string Note_Title { get; set; }
        public string Note_Content { get; set; }
        public string Parent_Id { get; set; }
        public string se_module { get; set; }
    }
}
