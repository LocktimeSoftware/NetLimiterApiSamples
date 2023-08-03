using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLApiSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                // Create filter named MyMSEdgeFilter
                Filter filter = new Filter("MyMSEdgeFilter");

                // Add filter function to match the application path
                filter.Functions.Add(new FFAppIdEqual(new AppId(@"c:\program files (x86)\microsoft\edge dev\application\msedge.exe")));

                client.AddFilter(filter);

                // Create limit rule. The limit is set to 512 KB/s
                client.AddRule(filter.Id, new LimitRule(RuleDir.In, 512 * 1024));

                Console.WriteLine("Press any key to delete the filter");
                Console.ReadKey();

                // Remove the filter. The rule will be removed as well.
                client.RemoveFilter(filter);
            }
        }
    }
}
