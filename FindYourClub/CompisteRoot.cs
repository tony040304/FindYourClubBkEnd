﻿using Service.IServices;
using Service.Services;

namespace FindYourClub
{
    public static class CompisteRoot
    {
        public static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAuthService, AuthServices>();
            builder.Services.AddScoped<IAdminService, AdminSerevice>();
            builder.Services.AddScoped<IJugadorServices, JugadorServices>();
            builder.Services.AddScoped<IEquipoService, EquipoService>();
            builder.Services.AddScoped<IFactoryMethEquipo, FactoryEquipoServices>();
            builder.Services.AddScoped<IFactoryMethJugadores, FactoryJugadoresService>();
            builder.Services.AddScoped<IFactory, MiFactory>();
        }
    }
}
