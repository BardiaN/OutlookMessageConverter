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
    }
}
