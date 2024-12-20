﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    public static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Cliente
            services.AddScoped<IRequestHandler<ClientePostCommand, ModelResult>, ClientePostHandler>();
            services.AddScoped<IRequestHandler<ClientePutCommand, ModelResult>, ClientePutHandler>();
            services.AddScoped<IRequestHandler<ClienteDeleteCommand, ModelResult>, ClienteDeleteHandler>();
            services.AddScoped<IRequestHandler<ClienteFindByIdCommand, ModelResult>, ClienteFindByIdHandler>();
            services.AddScoped<IRequestHandler<ClienteGetItemsCommand, PagingQueryResult<Cliente>>, ClienteGetItemsHandler>();

            //Dispositivo
            services.AddScoped<IRequestHandler<DispositivoPostCommand, ModelResult>, DispositivoPostHandler>();
            services.AddScoped<IRequestHandler<DispositivoPutCommand, ModelResult>, DispositivoPutHandler>();
            services.AddScoped<IRequestHandler<DispositivoDeleteCommand, ModelResult>, DispositivoDeleteHandler>();
            services.AddScoped<IRequestHandler<DispositivoFindByIdCommand, ModelResult>, DispositivoFindByIdHandler>();
            services.AddScoped<IRequestHandler<DispositivoGetItemsCommand, PagingQueryResult<Dispositivo>>, DispositivoGetItemsHandler>();

            //Produto
            services.AddScoped<IRequestHandler<ProdutoPostCommand, ModelResult>, ProdutoPostHandler>();
            services.AddScoped<IRequestHandler<ProdutoPutCommand, ModelResult>, ProdutoPutHandler>();
            services.AddScoped<IRequestHandler<ProdutoDeleteCommand, ModelResult>, ProdutoDeleteHandler>();
            services.AddScoped<IRequestHandler<ProdutoFindByIdCommand, ModelResult>, ProdutoFindByIdHandler>();
            services.AddScoped<IRequestHandler<ProdutoGetItemsCommand, PagingQueryResult<Produto>>, ProdutoGetItemsHandler>();
            services.AddScoped<IRequestHandler<ProdutoGetCategoriasCommand, PagingQueryResult<KeyValuePair<short, string>>>, ProdutoGetCategoriasHandler>();
        }
    }
}
