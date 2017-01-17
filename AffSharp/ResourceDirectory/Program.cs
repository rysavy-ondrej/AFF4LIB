//  
// Copyright (c) BRNO UNIVERSITY OF TECHNOLOGY. All rights reserved.  
// Licensed under the MIT License. See LICENSE file in the solution root for full license information.  
//
using System;
using System.ServiceModel.Web;

namespace ResourceDirectory
{
    class Program
    {
        static void PrintAPIInfo(WebServiceHost wshost)
        {
            Console.WriteLine("Service Info:");
            Console.WriteLine($"  Service Name: {wshost.Description.Name}");
        }

        static void Main(string[] args)
        {
            ResourceDirectory resourceDirectory = new ResourceDirectory();
            WebServiceHost _serviceHost = new WebServiceHost(resourceDirectory, new Uri("http://localhost:8465/AfxResourceDirectory"));
            _serviceHost.Open();
            Console.WriteLine("ResourceDirectory is running...");            
            PrintAPIInfo(_serviceHost);
            Console.WriteLine("Press any key to stop the service.");
            Console.ReadKey();
            Console.Write("Closing ...");
            _serviceHost.Close();
            Console.WriteLine("Done.");
        }
    }

}
