using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookMessageConverter
{
    public class EmailModel
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string ReceivedOn { get; set; }
    }
}
