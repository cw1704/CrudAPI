namespace GpProject206
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);            
            hostBuilder.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("http://*:5000");                   
                    webBuilder.UseUrls("http://*:5000","http://localhost:5000", "http://192.168.1.187:5000");                   
                    //webBuilder.UseUrls("http://192.168.1.187:5000");                                    
                    //webBuilder.UseUrls("http://localhost:5000");              
                });
    }
}