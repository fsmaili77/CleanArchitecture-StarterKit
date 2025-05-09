using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using FluentValidation;
using MyApp.Application.Common.Behaviors;

namespace MyApp.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Register FluentValidation validators 
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>)); // Ensure validation occurs before the request is handled

            return services;
        }
    }
}