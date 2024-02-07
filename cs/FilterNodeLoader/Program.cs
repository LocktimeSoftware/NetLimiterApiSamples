using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterNodeLoader
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                var nodeLoader = client.CreateNodeLoader();

                nodeLoader.Filters.SelectAll();
                
                nodeLoader.Load();

                foreach (var filterNode in nodeLoader.Filters.Nodes)
                {
                    if (client.Filters.FirstOrDefault(x => x.InternalId == filterNode.FilterId) is Filter filter)
                    {
                        Console.WriteLine($"Filter loaded: name={filter.Name}, received(B)={filterNode.Transferred.In}, sent(B)={filterNode.Transferred.Out}");
                    }
                }

                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
