using System;
using System.Collections.Generic;
using System.Text;

namespace ChatChallenge.Core.Models
{
    public class NServiceBusAppSettings
    {
        public string SenderName { get; set; }
        public string PublisherName { get; set; }
        public string ConnectionString { get; set; }
    }
}
