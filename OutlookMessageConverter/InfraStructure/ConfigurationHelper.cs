using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutlookMessageConverter.Properties;

namespace OutlookMessageConverter.InfraStructure
{
    public class ConfigurationHelper
    {
        public static bool AlwaysOnTop
        {
            get
            {
                return Settings.Default.AlwaysOnTop;
            }
            set
            {
                Settings.Default.AlwaysOnTop = value;
                Settings.Default.Save();
            }
        }

        public static bool DrawSeparatorLine
        {
            get
            {
                return Settings.Default.DrawSeparatorLine;
            }
            set
            {
                Settings.Default.DrawSeparatorLine = value;
                Settings.Default.Save();
            }
        }

        public static bool ConfirmDelete
        {
            get
            {
                return Settings.Default.ConfirmDelete;
            }
            set
            {
                Settings.Default.ConfirmDelete = value;
                Settings.Default.Save();
            }
        }
    }
}
