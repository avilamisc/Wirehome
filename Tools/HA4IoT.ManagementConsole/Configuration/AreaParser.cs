﻿using System;
using System.Collections.Generic;
using HA4IoT.ManagementConsole.Configuration.ViewModels;
using HA4IoT.ManagementConsole.Configuration.ViewModels.Settings;
using HA4IoT.ManagementConsole.Json;
using Newtonsoft.Json.Linq;

namespace HA4IoT.ManagementConsole.Configuration
{
    public class AreaParser
    {
        private readonly JProperty _source;
        private JObject _appSettings;

        public AreaParser(JProperty source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            _source = source;
        }

        public AreaItemVM Parse()
        {
            var settings = (JObject)_source.Value["Settings"];
            _appSettings = settings.GetNamedObject("AppSettings", null);

            var areaItem = new AreaItemVM(_source.Name);

            string caption = _source.Name;
            int sortValue = 0;

            if (_appSettings != null)
            {
                caption = _appSettings.GetNamedString("Caption", _source.Name);
                sortValue = (int)_appSettings.GetNamedNumber("SortValue", 0);
            }

            areaItem.SortValue = sortValue;

            areaItem.Caption = new StringSettingVM("Caption", "Caption", caption);
            areaItem.Settings.Add(areaItem.Caption);
            
            areaItem.Actuators.AddRange(ParseActuators());
            
            return areaItem;
        }

        private List<ActuatorItemVM> ParseActuators()
        {
            var actuatorProperties = ((JObject) _source.Value["Actuators"]).Properties();

            var actuators = new List<ActuatorItemVM>();
            foreach (var actuatorProperty in actuatorProperties)
            {
                actuators.Add(new ActuatorParser(actuatorProperty).Parse());
            }

            actuators.Sort((x, y) => x.SortValue.CompareTo(y.SortValue));

            return actuators;
        }
    }
}
