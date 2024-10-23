﻿using System.Windows;
using Autofac;
using CryptoMarket.Infrastructure.Settings;
using CryptoMarket.ViewModels;
using CryptoMarket.Views.MainWindow;

namespace CryptoMarket.Bootstrapper;

public class Bootstrapper : IDisposable
{
    private readonly IContainer _container;

    public Bootstrapper()
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder
            .RegisterModule<Infrastructure.RegistrationModule>()
            .RegisterModule<Views.RegistrationModule>()
            .RegisterModule<RegistrationModule>();

        _container = containerBuilder.Build();
    }

    public void Dispose()
    {
        _container.Dispose();
    }

    public Window Run()
    {
        InitializeDependencies();

        var mainWindow = _container.Resolve<IMainWindow>();

        if (mainWindow is not Window window) 
            throw new NotImplementedException();

        window.Show();

        return window;
    }

    private void InitializeDependencies()
    {
        _container.Resolve<IMainWindowMementoWrapperInitializer>().Initialize();
    }
}