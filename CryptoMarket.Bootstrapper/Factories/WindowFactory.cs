﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CryptoMarket.ViewModels.MainWindow;
using CryptoMarket.ViewModels.Windows;
using CryptoMarket.Views.Factories;
using CryptoMarket.Views.MainWindow;

namespace CryptoMarket.Bootstrapper.Factories
{
    internal class WindowFactory : IWindowFactory
    {
        private readonly IComponentContext _componentContext;

        private readonly Dictionary<Type, Type> _map = new()
        {
            {typeof(IMainWindowViewModel), typeof(IMainWindow)}
        };

        public WindowFactory(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }
        public IWindow Create<TWindowViewModel>(TWindowViewModel viewModel) where TWindowViewModel : IWindowViewModel
        {
            if (!_map.TryGetValue(typeof(TWindowViewModel), out var windowType))
            {
                throw new InvalidOperationException($"There is no window registered for {typeof(TWindowViewModel)}");
            }

            var instance =  _componentContext.Resolve(windowType, TypedParameter.From(viewModel));
            return (IWindow)instance;
        }
    }
}
