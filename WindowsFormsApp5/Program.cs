using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    internal static class Program
    {
        static ApplicationContext MainContext = new ApplicationContext();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            MainContext.MainForm = new SettingForm();
            Application.Run(MainContext);
        }
        public static void SetMainForm(Form MainForm)
        {
            MainContext.MainForm = MainForm;
        }

        public static void ShowMainForm()
        {
            MainContext.MainForm.Show();
        }
    }
}
