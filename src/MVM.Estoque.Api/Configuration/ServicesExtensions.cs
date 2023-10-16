using System;
using MVM.Estoque.Api.Data.Repositories;
using MVM.Estoque.Api.Interfaces;
using MVM.Estoque.Api.Interfaces.Repositories;
using MVM.Estoque.Api.Interfaces.Services;
using MVM.Estoque.Api.Notifications;
using MVM.Estoque.Api.Services;

namespace MVM.Estoque.Api.Configuration;

public static class ServicesExtensions
{
    public static IServiceCollection AddExtensions(this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IFornecedorRepository, FornecedorRepository>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<INotifyHandler, NotififyHandler>();

        return services;
    }
}

