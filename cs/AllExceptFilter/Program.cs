using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLimiter.Service;
using CoreLib.Net;

namespace FiltersAndRules
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                Filter filter;

                // Create a filter for whole computer, but by-pass system services - all "system" and svchost.exe processes
                filter = new Filter("AllExceptSystemServices");

                FFPathEqual filterFunction = new FFPathEqual();
                filterFunction.IsMatch = false; // catch all traffic that DOESN'T match the value
                filterFunction.Values.Add(@"system"); // system process
                filterFunction.Values.Add(@"c:\windows\system32\svchost.exe"); // path - it could be different on your machine

                filter.Functions.Add(filterFunction);

                client.AddFilter(filter);

                Console.WriteLine("Press ENTER to delete the filter and Exit.");
                Console.ReadKey();

                // Remove filter
                client.RemoveFilter(filter);
            }
        }
    }
}
