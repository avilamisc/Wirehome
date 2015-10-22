﻿using HA4IoT.Hardware.GenericIOBoard;
using HA4IoT.Hardware.PortExpanderDrivers;
using HA4IoT.Notifications;

namespace HA4IoT.Hardware.CCTools
{
    public class HSPE8 : IOBoardController, IBinaryOutputController
    {
        public HSPE8(string id, int address, II2cBusAccessor bus, INotificationHandler notificationHandler)
            : base(id, new PCF8574Driver(address, bus), notificationHandler)
        {
            FetchState();
        }

        public IBinaryOutput GetOutput(int number)
        {
            return GetPort(number);
        }
    }
}
