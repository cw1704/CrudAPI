using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GpProject206.Services;
using GpProject206.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

namespace GpProject206
{
    public class ResponseHeaderAttribute : ActionFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public ResponseHeaderAttribute(string name, string value) =>
            (_name, _value) = (name, value);

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _value);

            base.OnResultExecuting(context);
        }
    }

    public class Startup
    {
        public static readonly string AllowSpecificOrigins = "_allowedSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Project206_Gp2", Version = "v1"}); });
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);            
            
            services.AddScoped<ProductService>();
            services.AddScoped<PromotionService>();
            services.AddScoped<MemberService>();
            services.AddScoped<OrderService>();
            services.AddScoped<CategoryService>();

            /*services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins, policy => policy.AllowAnyOrigin());
                options.AddPolicy(name: "AllowAnyHeader", policy => policy.AllowAnyHeader());
                options.AddPolicy(name: "AllowAnyMethod", policy => policy.AllowAnyMethod());
            });*/

            /*services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder => {
                    //builder.WithOrigins("http://localhost");
                    //builder.AllowAnyOrigin();
                    //builder.AllowAnyOrigin().SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
                    //builder.WithOrigins("http://localhost:3000").SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod();
                    builder.WithOrigins("http://localhost:3000");
                });
            });*/
            services.AddCors(options =>
            {
                var allowOrigins = Configuration.GetValue<string>("AllowOrigins");
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(allowOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                      .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*if (env.IsDevelopment())
            {
            }*/
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CrudAPI v1"));

            //app.UseHttpsRedirection();

            app.UseRouting();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            /*app.Use((contexto, proximo) =>
                        {
                            contexto.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                            return proximo.Invoke();
                        });*/
            app.Use(async (context, next) =>
            {

                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000");
                    return Task.FromResult(0);
                });

                await next();
            });

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            //app.UseCors(AllowSpecificOrigins);
            //app.UseCors("AllowAnyHeader");
            //app.UseCors("AllowAnyMethod");
            //app.UseCors("AllowAll");
            //app.UseCors(x => x.AllowAnyOrigin().SetIsOriginAllowedToAllowWildcardSubdomains().AllowAnyHeader().AllowAnyMethod());
            //app.UseCors();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            
        }
    }
}