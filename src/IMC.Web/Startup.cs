using IMC.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net.Http;

namespace IMC.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            // Each eventual TaxCalculator would need to be configured separately.
            services.AddHttpClient(nameof(IMC.TaxJarTaxCalculator.TaxJarTaxCalculator), client => {
                client.BaseAddress = new Uri(Configuration["TaxJarUrl"]);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Configuration["TaxJarApiKey"]);
            });

            // This will inject the TaxJarTaxCalculator to any controller.
            // IS NOT ADDRESING THE POSSIBILITY OF HAVING MULTIPLE TAX CALCULATORS!
            // NEED TaxCalculatorFactory
            services.AddTransient<ITaxCalculator>(sp => { 
                var factory = sp.GetRequiredService<IHttpClientFactory>();
                var client = factory.CreateClient(nameof(IMC.TaxJarTaxCalculator.TaxJarTaxCalculator));
                return new IMC.TaxJarTaxCalculator.TaxJarTaxCalculator(client);
            });

            services.AddControllers();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
