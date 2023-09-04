using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Pariks;
using System.Windows.Controls;
using System.Windows;

using Pariks.View;
using System.Diagnostics;
using System.Collections.Generic;

namespace TestPariks
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mainWindow = new MainWindow();
            var btn = (Button)mainWindow.FindName("btnExit");

            btn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

        }


    }
}