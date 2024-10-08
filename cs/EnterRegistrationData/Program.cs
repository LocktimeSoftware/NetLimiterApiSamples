﻿using NetLimiter.Service;
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
                // Listen for license changes
                client.LicenseChanged += OnLicenseChanged;

                // Connect to local NetLimiter service
                client.Connect();

                Console.WriteLine($"Prior registration: IsRegistered={client.License.IsRegistered}");

                try
                {
                    await client.SetRegistrationCodeAsync("FBT7X-BDGTY-JLYKL-4NDFJ-W5Q1E");

                    Console.WriteLine($"After registration: IsRegistered={client.License.IsRegistered}");

                    client.RemoveRegistrationData();

                    Console.WriteLine($"After unregistration: IsRegistered={client.License.IsRegistered}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Event handler called even if the registration data are changed from other clients like NetLimiter GUI.
        /// </summary>
        private static void OnLicenseChanged(object sender, EventArgs e)
        {
            if (sender is NLClient client)
            {
                Console.WriteLine($"OnLicenseChanged: IsRegistered={client.License.IsRegistered}, DaysLeft={client.License.DaysLeft}");
            }
        }
    }
}
