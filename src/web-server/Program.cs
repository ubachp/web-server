using System;
using Topshelf;

namespace P.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var rc = HostFactory.Run(x =>
            {
                x.Service<PWebServer>(s =>                                   
                {
                    s.ConstructUsing(name => new PWebServer());                
                    s.WhenStarted( (server, host) => server.Start());                         
                    s.WhenStopped( (server, host) => server.Stop());                          
                });
                x.RunAsLocalSystem();                                       

                x.SetDescription("Paulo Web Server Implementation");                   
                x.SetDisplayName("P-Web-Server");                                  
                x.SetServiceName("PWebServer");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  
            Environment.ExitCode = exitCode;
        }
    }
}
