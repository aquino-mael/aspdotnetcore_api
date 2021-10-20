using System;
using System.Collections.Generic;
using Api.CrossCuting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      ConfigureService.ConfigureDependenciesService(services);
      ConfigureRepository.ConfigureDependenciesRepository(services);

      SigningConfigurations signingConfiguration = new SigningConfigurations();
      services.AddSingleton(signingConfiguration);

      TokenConfigurations tokenConfigurations = new TokenConfigurations();
      new ConfigureFromConfigurationOptions<TokenConfigurations>(
        Configuration.GetSection("TokenConfigurations"))
          .Configure(tokenConfigurations);
      services.AddSingleton(tokenConfigurations);

      services.AddAuthentication(authOptions =>
      {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(bearerOptions =>
      {
        TokenValidationParameters paramsValidation = bearerOptions.TokenValidationParameters;

        paramsValidation.IssuerSigningKey = signingConfiguration.Key;
        paramsValidation.ValidAudience = tokenConfigurations.Audience;
        paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
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

      services.AddControllers();
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
          swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API com AspNetCore 3.1");
          swaggerUIOptions.RoutePrefix = string.Empty;
        }
      );

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
