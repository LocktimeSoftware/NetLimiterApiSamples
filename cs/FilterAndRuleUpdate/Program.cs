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

                // Create filter for apps 
                Filter filter = new Filter("MyFilter");
                filter.Functions.Add(new FFPathEqual("*\\chrome.exe"));

                // Add the filter to the NetLimiter service
                filter = client.AddFilter(filter);

                // Create ALLOW rule
                Rule rule = new FwRule(RuleDir.Both, FwAction.Allow);

                // Add the rule to the NetLimiter service
                rule = client.AddRule(filter.Id, rule);

                Console.WriteLine("Press any key to update the filter and rule");
                Console.ReadKey();

                // Update filter name
                filter.Name = "MyChromeFilter";
                client.UpdateFilter(filter);

                // Disable the rule
                rule.IsEnabled = false;
                client.UpdateRule(rule);

                Console.WriteLine("Press any key to exit");
                Console.ReadKey();

                client.RemoveFilter(filter);
            }
        }
    }
}
