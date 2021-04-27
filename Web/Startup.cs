namespace SuplementShop.Web
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using Stripe;
    using SuplementShop.Application.Interfaces;
    using SuplementShop.Application.Services;
    using SuplementShop.Persistence;
    using SuplementShop.Persistence.Repositories;
    using System;
    using System.Text;

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            });

            services.AddDbContext<MyDbContext>(
                options =>
                {
                    options.UseSqlite("DataSource=suplementShopDb.db");
                });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            }); ;

            StripeConfiguration.ApiKey = Configuration["Stripe:SKey"];

            services.AddScoped<ChargeService>(builder => new());

            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IProductRepo, ProductRepo>();
            services.AddScoped<ICartRepo, CartRepo>();
            services.AddScoped<ICartItemRepo, CartItemRepo>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, SuplementShop.Application.Services.ProductService>();
            services.AddScoped<ICartService, CartService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            SetUpDb(app);
        }

        private void SetUpDb(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using MyDbContext dbContext = scope.ServiceProvider.GetService<MyDbContext>();

            dbContext.Database.EnsureCreated();
        }
    }
}
