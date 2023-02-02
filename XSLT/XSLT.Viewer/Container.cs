﻿using Ninject;
using System;
using System.Windows;
using XSLT.Implementations;
using XSLT.Interfaces;

namespace XSLT.Viewer
{
    public static class Container
    {
        private static IKernel container;

        static Container()
        {
            container = new StandardKernel();
            container.Bind<IFileProvider>().To<FileProvider>();
            container.Bind<IXSLTTransformer>().To<XSLTTransformer>();
        }

        public static T Get<T>()
        {
            try
            {
                return container.Get<T>();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return container.Get<T>();
            }
        }
    }
}
