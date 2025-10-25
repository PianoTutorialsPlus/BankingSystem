using Autofac;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using System.Reflection;
using Module = Autofac.Module;

namespace BankingSystem.Application.DI;

public class ApplicationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAutoMapper(Assembly.GetExecutingAssembly());

        var configuration = MediatRConfigurationBuilder
        .Create("", Assembly.GetExecutingAssembly())
        .WithAllOpenGenericHandlerTypesRegistered()
        .Build();

        builder.RegisterMediatR(configuration);
    }
}
