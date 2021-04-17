using BusinessAccess;
using BusinessAccess.Interfaces;
using BusinessAccess.Services;
using DataAccess.DataModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using ThinkBridgeAssignment.Middlewares;

namespace ThinkBridgeAssignment
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Latest).ConfigureApiBehaviorOptions(opions =>
            {
                opions.InvalidModelStateResponseFactory = (ActionContext) =>
                  {
                      var errors = ActionContext.ModelState.Values.SelectMany(x => x.Errors.Select(p => new
                      {
                          errorMessage = p.ErrorMessage
                      })).ToList();

                      var result = new ApiResponse
                      {
                          Status = 0,
                          Data = errors,
                          Message = "Validation Failed",

                      };
                      return new BadRequestObjectResult(result);
                  };
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ThinkBridgeAssignment", Version = "v1" });
            });

            
            var ConnectionString = Configuration.GetConnectionString("ThinkInvDb");

            services.AddDbContext<ModelContext>(options => options.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("ThinkBridgeAssignment")));

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ITransactionService, TransactionService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ModelContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThinkBridgeAssignment v1"));
            }

            db.Database.EnsureCreated();
            app.UseRouting();
            app.UseMiddleware(typeof(ErrorMiddleware));
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
