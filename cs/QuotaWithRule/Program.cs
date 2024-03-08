using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuotaWithRule
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                client.Connect();

                Filter filter = new Filter("WholeMachineFilter");

                client.AddFilter(filter);

                var limitRule = client.AddRule(filter.Id, new LimitRule(RuleDir.In, 512 * 1024) { IsEnabled = false });

                QuotaRule quotaRule = new QuotaRule(RuleDir.Both, 2 * 1024 * 1024);

                quotaRule.OnOverflowRules.Add(limitRule.Id);

                client.AddRule(filter.Id, quotaRule);

                Console.WriteLine("Press any key to delete the filter");
                Console.ReadKey();

                client.RemoveFilter(filter);
            }
        }
    }
}
