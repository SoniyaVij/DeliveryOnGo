﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using CloudantDotNet.Services;
using System.Threading.Tasks;

public class Startup
{
    public IConfiguration Configuration { get; set; }

    public Startup(IHostingEnvironment env)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("config.json", optional: true);
        Configuration = configBuilder.Build();

        string vcapServices = System.Environment.GetEnvironmentVariable("VCAP_SERVICES");
        if (vcapServices != null)
        {
            dynamic json = JsonConvert.DeserializeObject(vcapServices);
            foreach (dynamic obj in json.Children()) {
                if (((string)obj.Name).ToLowerInvariant().Contains("cloudant")) {
                    dynamic credentials = (((JProperty)obj).Value[0] as dynamic).credentials;
                    if (credentials != null) {
                        string host = credentials.host;
                        string username = credentials.username;
                        string password = credentials.password;
                        Configuration["cloudantNoSQLDB:0:credentials:username"] = username;
                        Configuration["cloudantNoSQLDB:0:credentials:password"] = password;
                        Configuration["cloudantNoSQLDB:0:credentials:host"] = host;
                        break;
                    }
                }
            }
        }
        //"username": "329c769e-9655-456d-9711-cd8100fe49b3-bluemix",
        //"password": "d597c9e894b22e8a678548fbcdcdbee5a008e53a06e67f555963277ca905834e",
        //"host": "329c769e-9655-456d-9711-cd8100fe49b3-bluemix.cloudant.com",
        //"port": 443,
        string hostLocal = "329c769e-9655-456d-9711-cd8100fe49b3-bluemix.cloudant.com";
        string usernameLocal = "329c769e-9655-456d-9711-cd8100fe49b3-bluemix";
        string passwordLocal = "d597c9e894b22e8a678548fbcdcdbee5a008e53a06e67f555963277ca905834e";
        Configuration["cloudantNoSQLDB:0:credentials:username"] = usernameLocal;
        Configuration["cloudantNoSQLDB:0:credentials:password"] = passwordLocal;
        Configuration["cloudantNoSQLDB:0:credentials:host"] = hostLocal;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();

        // works with VCAP_SERVICES JSON value added to config.json when running locally,
        // and works with actual VCAP_SERVICES env var based on configuration set above when running in CF
        var creds = new CloudantDotNet.Models.Creds()
        {
            username = Configuration["cloudantNoSQLDB:0:credentials:username"],
            password = Configuration["cloudantNoSQLDB:0:credentials:password"],
            host = Configuration["cloudantNoSQLDB:0:credentials:host"]
        };
        services.AddSingleton(typeof(CloudantDotNet.Models.Creds), creds);
        services.AddTransient<ICloudantService, CloudantService>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        // Populate test data in the database
        var cloudantService = ((ICloudantService)app.ApplicationServices.GetService(typeof(ICloudantService)));
        cloudantService.PopulateTestData().Wait();

        loggerFactory.AddConsole();
        app.UseDeveloperExceptionPage();
        app.UseStaticFiles();
        app.UseMvcWithDefaultRoute();
        app.UseMvc(routes =>
        {
            routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            routes.MapRoute(
                        name: "NewUserRegistration",
                        template: "{controller=Home}/{action=NewUserRegistration}/{id?}");

        });
    }

    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build();

        var host = new WebHostBuilder()
                    .UseKestrel()
                    .UseConfiguration(config)
                    .UseIISIntegration()
                    .UseStartup<Startup>()
                    .Build();

        host.Run();
    }

    //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    //{
    //    loggerFactory.AddConsole(Configuration.GetSection("Logging"));
    //    loggerFactory.AddDebug();

    //    if (env.IsDevelopment())
    //    {
    //        app.UseDeveloperExceptionPage();
    //    }
    //    else
    //    {
    //        app.UseExceptionHandler("/Home/Error");
    //    }

    //    app.UseStaticFiles();

    //    app.UseMvc(routes =>
    //    {
    //        routes.MapRoute(
    //            name: "default",
    //            template: "{controller=Home}/{action=Index}/{id?}");
    //    });
    //}
}
