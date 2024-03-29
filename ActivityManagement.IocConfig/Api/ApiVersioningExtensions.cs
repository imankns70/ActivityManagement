﻿using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ActivityManagement.IocConfig.Api
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection ApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                // add true or false to request header
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(),
                    new HeaderApiVersionReader("api-version"));
            });

            return services;
        }
    }
}
