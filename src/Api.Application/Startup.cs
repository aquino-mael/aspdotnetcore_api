using System;
using System.Collections.Generic;
using Api.CrossCuting.DependencyInjection;
using Api.CrossCuting.Mappings;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
      Configuration = configuration;
      _environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment _environment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      if (_environment.IsEnvironment("Testing"))
      {
        Environment.SetEnvironmentVariable(
          "DB_CONNECTION",
          @"
          Server = localhost;
          Initial Catalog = dbapi_Integration;
          MultipleActiveResultSets = true;
          User ID = sa;
          Password = DockerSql2017!"
        );
        Environment.SetEnvironmentVariable("DATABASE", "SQLSERVER");
        Environment.SetEnvironmentVariable("MIGRATION", "APPLY");
        Environment.SetEnvironmentVariable("AUDIENCE", "ExempleAudience");
        Environment.SetEnvironmentVariable("ISSUER", "ExempleIssuer");
        Environment.SetEnvironmentVariable("TOKEN_SECONDS", "28800");
      }

      services.AddControllers();

      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependenciesRepository(services);

      var config = new AutoMapper.MapperConfiguration(config =>
      {
        config.AddProfile(new DtoToModelProfile());
        config.AddProfile(new EntityToDtoProfile());
        config.AddProfile(new ModelToEntityProfile());
      });

      IMapper mapper = config.CreateMapper();
      services.AddSingleton(mapper);

      SigningConfigurations signingConfiguration = new SigningConfigurations();
      services.AddSingleton(signingConfiguration);

      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(bearerOptions =>
      {
        TokenValidationParameters paramsValidation = bearerOptions.TokenValidationParameters;

        paramsValidation.IssuerSigningKey = signingConfiguration.Key;
        paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("AUDIENCE");
        paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("ISSUER");
        paramsValidation.ValidateIssuerSigningKey = true;
        paramsValidation.ValidateLifetime = true;
        paramsValidation.ClockSkew = TimeSpan.Zero;
      });

      services.AddAuthorization(auth =>
      {
        auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                                .RequireAuthenticatedUser().Build());
      });

      services.AddSwaggerGen(swaggerGenOptions =>
      {
        swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Curso de API com AspNetCore 3.1 - Na Prática",
          Description = "Arquitetura DDD",
          TermsOfService = new Uri("http://mfrinfo.com.br"),
          Contact = new OpenApiContact
          {
            Name = "Ismael Aquino",
            Email = "ismaellAquino@hotmail.com",
          },
        });

        swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "Gerar JWT",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
        });

        swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
          {
            new OpenApiSecurityScheme {
              Reference = new OpenApiReference {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme
              }
            }, new List<string>()
          }
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseSwagger();

      app.UseSwaggerUI(
        swaggerUIOptions =>
        {
          swaggerUIOptions.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Curso de API com AspNetCore 3.1"
          );
          swaggerUIOptions.RoutePrefix = string.Empty;
        }
      );

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      if (Environment.GetEnvironmentVariable("MIGRATION").ToLower() == "APPLY".ToLower())
      {
        using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
          using (var context = service.ServiceProvider.GetService<MyContext>())
          {
            context.Database.Migrate();
          }
        }
      }
    }
  }
}
