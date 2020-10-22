using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetLimiter.Service;

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

                // Register event handler - it is called every time when there are any Blocker requests
                client.FirewallRequestChange += (s, a) =>
                {
                    foreach (var request in client.FirewallRequests)
                    {
                        // Reply to all request, by returning BLOCK
                        client.ReplyFirewallRequest(request, FwAction.Block);

                        Console.WriteLine(
                            $"Denied: {request.LocalAddress}:{request.LocalPort}/{request.RemoteAddress}:{request.RemotePort} - {request.AppId}");
                    }
                };

                Filter internetZone = client.GetInternetZone();

                // Save current blocker state
                bool fwEnabledPrevState = client.IsFwEnabled;

                // Enable blocker
                client.IsFwEnabled = true;

                try
                {
                    // Create rule asking for all outgoing connections on Internet zone
                    Rule rule = new FwRule(RuleDir.Out, FwAction.Ask);
                    client.AddRule(internetZone.Id, rule);

                    Console.WriteLine("Press any key to restore previous settings and close");
                    Console.ReadKey();

                    // Remove the rule
                    client.RemoveRule(rule);
                }
                finally
                {
                    // Restore blocker state
                    client.IsFwEnabled = fwEnabledPrevState;
                }

            }
        }
    }
}
