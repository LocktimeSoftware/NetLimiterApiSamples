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
        static void Main(string[] args)
        {
            using (NLClient client = new NLClient())
            {
                // Connect to local NetLimiter service
                client.Connect();

                try
                {
                    var table = client.GetAccessTable().ToList();

                    var users = table.FirstOrDefault(x => string.Equals(x.Name, "users", 
                        StringComparison.InvariantCultureIgnoreCase) ||
                        string.Equals(x.Name, "builtin\\users", StringComparison.InvariantCultureIgnoreCase));

                    if (users == null)
                    {
                        table.Add(users = new NetLimiter.Service.Api.AccessTableRow()
                        {
                            Name = "Users",
                        });
                    }

                    users.AllowedRights = NetLimiter.Service.Security.Rights.Monitor;
                    users.DeniedRights = 0;

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
