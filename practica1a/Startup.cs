using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using practica1a.Data;
using practica1a.Services.CategoriaService;
using practica1a.Services.CompraService;
using practica1a.Services.ComprobarVentaService;
using practica1a.Services.DetalleCompraService;
using practica1a.Services.DetalleVentaService;
using practica1a.Services.LoginService;
using practica1a.Services.ProductoService;
using practica1a.Services.ProveedoresService;
using practica1a.Services.RolService;
using practica1a.Services.scheduled;
using practica1a.Services.UsuarioService;
using practica1a.Services.VentaService;
using System.Text;

namespace practica1a
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = false,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["JWT:Issuer"],
                      ValidAudience = Configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(Configuration["JWT:Key"])
                      )
                  };
              });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "practica1a", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

                c.AddSecurityRequirement(securityRequirement);


            });


            services.AddDbContext<practicaDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("practica1a"));
            });

            services.AddCors(options => options.AddPolicy("AllowWebApp",
                builder => builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddScoped<IDetalleVentaService, DetalleVentaService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<IDetalleCompraService, DetalleCompraService>();
            services.AddScoped<IComprobarVentaService, ComprobarVentaService>();
            services.AddScoped<IProveedoresService, ProveedoresService>();
            services.AddScoped<ISheduled, scheduled>();
            services.AddHttpContextAccessor();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "practica1a v1"));
            }

            app.UseCors("AllowWebApp");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
