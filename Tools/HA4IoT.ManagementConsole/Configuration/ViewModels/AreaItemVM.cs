﻿using System.Collections.ObjectModel;
using HA4IoT.ManagementConsole.Configuration.ViewModels.Settings;
using HA4IoT.ManagementConsole.Core;
using Newtonsoft.Json.Linq;

namespace HA4IoT.ManagementConsole.Configuration.ViewModels
{
    public class AreaItemVM : ConfigurationItemVM
    {
        public AreaItemVM(string id) : base(id)
        {
            Settings = new ObservableCollection<SettingItemVM>();
            Actuators = new SelectableObservableCollection<ActuatorItemVM>();
            Automations = new SelectableObservableCollection<AutomationItemVM>();
        }

        public int SortValue { get; set; }

        public StringSettingVM Caption { get; set; }

        public ObservableCollection<SettingItemVM> Settings { get; private set; }

        public SelectableObservableCollection<ActuatorItemVM> Actuators { get; private set; }

        public SelectableObservableCollection<AutomationItemVM> Automations { get; private set; }

        public JObject SerializeSettings()
        {
            var configuration = new JObject();
            var appSettings = new JObject();
            configuration["AppSettings"] = appSettings;

            appSettings["SortValue"] = new JValue(SortValue);
            foreach (var setting in Settings)
            {
                if (setting.IsAppSetting)
                {
                    appSettings[setting.Key] = setting.SerializeValue();
                }
                else
                {
                    configuration[setting.Key] = setting.SerializeValue();
                }
            }

            return configuration;
        }
    }
}