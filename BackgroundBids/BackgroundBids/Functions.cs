using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BackgroundBids
{
    public class Functions
    {
        public static void ProcessBid([ServiceBusTrigger("testqueue1")] string msg, TextWriter log)
        {
            log.Write($"Message: {msg}");
        }
    }
}
