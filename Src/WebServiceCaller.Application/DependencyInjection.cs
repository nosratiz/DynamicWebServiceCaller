using System.Linq;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Application.Common.Service;

namespace WebServiceCaller.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IMediator, Mediator>();

            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IWebService, WebService>();

            #region Api Behavior

            services.Configure<ApiBehaviorOptions>(options =>
            {
                //options.SuppressModelStateInvalidFilter = true;
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = new
                    {
                        message =
                            actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage.ToString())
                                .FirstOrDefault()
                    };
                    return new BadRequestObjectResult(errors);
                };
            });

            #endregion Api Behavior

            return services;
        }
    }
}