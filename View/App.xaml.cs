﻿using Model.Data;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IRepository repository = new DataRepository();
            HospitalModel hospitalModel = new HospitalModel(repository);
            var w = new MainWindow
            {
                DataContext = new HospitalViewModel(hospitalModel)
            };

            w.Show();
        }
    }
}
