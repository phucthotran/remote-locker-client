using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows;

namespace RemoteLocker
{
    public static class MainWindowExtender
    {
        /// <summary>
        /// Create and load a module to 'Container'
        /// </summary>
        /// <typeparam name="T">Type of module</typeparam>
        /// <param name="container">Parent container</param>
        /// <param name="args">Module's constructor arguments</param>
        /// <returns></returns>
        public static T LoadModule<T>(this Panel container, object[] args)
        {
            List<object> modules = container.Children.Cast<object>().Where(m => m.GetType().Equals(typeof(T))).ToList();
            bool moduleLoaded = modules.Count > 0;

            if (moduleLoaded)
                return (T)modules.ElementAt(0);

            object moduleInstance = args == null ? Activator.CreateInstance(typeof(T)) : Activator.CreateInstance(typeof(T), args);

            container.Children.Clear();
            container.Children.Add((UIElement)moduleInstance);

            return (T)moduleInstance;
        }

        /// <summary>
        /// Unload a module on 'Container'
        /// </summary>
        /// <typeparam name="T">Type of module</typeparam>
        /// <param name="container">Parent container</param>
        public static void UnloadModule<T>(this Panel container)
        {
            List<object> modules = container.Children.Cast<object>().Where(m => m.GetType().Equals(typeof(T))).ToList();
            bool moduleLoaded = modules.Count > 0;          

            if (moduleLoaded)
                container.Children.Remove((modules.ElementAt(0) as UIElement));
        }
    }
}
