using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitForComputer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                Filter filter = client.GetInternetZone();

                Rule rule = new LimitRule(RuleDir.In, 512 * 1024);
                client.AddRule(filter.Id, rule);

                Console.WriteLine("Press any key to delete the filter");
                Console.ReadKey();

                client.RemoveRule(rule);
            }
        }
    }
}
