using ScanEventDemo.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanEventDemo.Models.Datastore
{
	public class ScanEventData
	{
        public int EventId { get; set; }
        public int ParcelId { get; set; }
        public String Type { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public string StatusCode { get; set; }
        public string RunId { get; set; }
    }
}
	