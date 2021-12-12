using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using IServiceScope serviceScope = host.Services.CreateScope();
            ApplicationDbContext appDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (false)
            {
                //foreach (var item in appDbContext.Groups)
                //{
                //    appDbContext.Groups.Remove(item);
                //}
                //await appDbContext.SaveChangesAsync();
                appDbContext.Groups.Add(new Data.Entities.Group
                {
                    Name = "DevOps Learning Group",
                    PictureURI = "https://th.bing.com/th/id/R.4bb4ef273cf791b36d347f171d41ddce?rik=6QClw6AaWcUzmQ&pid=ImgRaw&r=0",
                    Purpose = "Come and learn about DevOps with us!",
                    Messages = new List<Data.Entities.GroupMessage>
                    {
                        new Data.Entities.GroupMessage
                        {
                            TextMessage = "Welcome to the DevOps LearningGroup"
                        }
                    }
                });
                appDbContext.Groups.Add(new Data.Entities.Group
                {
                    Name = "CSS Tricks Group",
                    PictureURI = "https://3wa.fr/wp-content/uploads/2020/04/logo-css.png",
                    Purpose = "Share your CSS Tricks!",
                    Messages = new List<Data.Entities.GroupMessage>
                    {
                        new Data.Entities.GroupMessage
                        {
                            TextMessage = "Welcome to the CSS Tricks Group"
                        }
                    }
                });
                appDbContext.Groups.Add(new Data.Entities.Group
                {
                    Name = "C# Group",
                    PictureURI = "https://escuela.it/uploads/c-sharp-92.png",
                    Purpose = "Let's learn about C#!",
                    Messages = new List<Data.Entities.GroupMessage>
                    {
                        new Data.Entities.GroupMessage
                        {
                            TextMessage = "Welcome to the C# Group"
                        }
                    }
                });
                await appDbContext.SaveChangesAsync();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext) =>
            {
                hostingContext.AddAzureKeyVault(new Uri("https://coderclankeyvault.vault.azure.net/"),
                        new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = "3def3d2b-3bc3-4ce4-984f-1a29fe437722" }));
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
