using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledRule
{
    class Program
    {
        static string _ruleId;

        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                // Listen for rule changes
                client.RuleUpdated += OnRuleUpdated;

                // Get filter matching all traffic (whole computer or any)
                Filter filter = client.GetAnyFilter();

                LimitRule limitRule = new LimitRule(RuleDir.In, 1024 * 1024 /* 1 MB/s */);
                _ruleId = limitRule.Id;

                // Create Start condition
                limitRule.Conditions.Add(new TimeCondition()
                {
                    Action = RuleConditionAction.Start,
                    TimeConditionType = TimeConditionType.Once,
                    Time = DateTime.UtcNow.AddSeconds(10), // Will start in 10s
                });

                // Create Stop condition
                limitRule.Conditions.Add(new TimeCondition()
                {
                    Action = RuleConditionAction.Stop,
                    TimeConditionType = TimeConditionType.Once,
                    Time = DateTime.UtcNow.AddSeconds(20), // Will stop in 10s after start
                });

                // Add the rule to NetLimiter system
                client.AddRule(filter.Id, limitRule);

                Console.WriteLine("Rule was created. The rule will start in 10s and stop in next 10s.");
                Console.WriteLine("PRESS ANY KEY TO DELETE THE RULE AND EXIT.");
                Console.ReadKey();

                // remove filter
                client.RemoveRule(limitRule);
            }
        }

        private static void OnRuleUpdated(object sender, RuleUpdateEventArgs e)
        {
            //
            // This handler is called when a rule is updated ie. started / stopped. 
            // You can try to disabled/enable our rule from the GUI application or you can try to change the limit size.
            //
            if (e.Rule is LimitRule limitRule && limitRule.Id == _ruleId)
            {
                Console.WriteLine($"Our LimitRule: state={limitRule.State}, limit={limitRule.LimitSize} B/s");
            }
        }
    }
}
