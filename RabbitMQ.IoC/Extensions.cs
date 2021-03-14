﻿using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Bus.Bus;
using RabbitMQ.Bus.Bus.Interfaces;
using RabbitMQ.Bus.Events;

namespace RabbitMQ.IoC
{
    public static class Extensions
    {
        public static void AddRabbitMq(this IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });
        }

        public static void Subscribe<T, TH>(this IApplicationBuilder app) where T : Event where TH : IEventHandler<T>
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<T, TH>();
        }
    }
}
