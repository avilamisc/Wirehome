﻿using System;
using HA4IoT.Hardware;

namespace HA4IoT.Actuators.Contracts
{
    public interface IHumiditySensor
    {
        event EventHandler<SingleValueSensorValueChangedEventArgs> ValueChanged;

        float Value { get; }
    }
}
