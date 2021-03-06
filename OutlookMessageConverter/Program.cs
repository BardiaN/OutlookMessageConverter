﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookMessageConverter.InfraStructure;

namespace OutlookMessageConverter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMain mainForm = new OutlookMessageConverter.FormMain();
            mainForm.Icon = Properties.Resources.mail_32;
            mainForm.TopMost = ConfigurationHelper.AlwaysOnTop;
            Application.Run(mainForm);
        }
    }
}
