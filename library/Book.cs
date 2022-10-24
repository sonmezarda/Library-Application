using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public class Book
    {
        public string author { get; set; }
        public string altAuthor { get; set; }
        public int edition { get; set; }
        public string location { get; set; }
        public string note { get; set; }
        public int page { get; set; }
        public string publisher { get; set; }
        public string subject { get; set; }
        public string title { get; set; }
        public int year { get; set; }
        public int id { get; set; }
        public bool readed { get; set; }
    }
}
