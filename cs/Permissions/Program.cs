using NetLimiter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLApiSamples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                try
                {
                    var table = client.GetAccessTable().ToList();

                    table.Add(new NetLimiter.Service.Api.AccessTableRow()
                    {
                        Name = "pokus",
                        AllowedRights = NetLimiter.Service.Security.Rights.Monitor,
                        DeniedRights = NetLimiter.Service.Security.Rights.Control
                    });

                    client.SetAccessTable(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
