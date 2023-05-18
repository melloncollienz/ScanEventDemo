using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanEventDemo.Models.Api
{
	internal class ParcelEvent
	{
        public int EventId { get; set; }
        public int ParcelId { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDateTimeUtc { get; set; }
        public string StatusCode { get; set; }
        public Device Device { get; set; }
        public User User { get; set; }

    }
}
