﻿using System;
using Newtonsoft.Json.Linq;
using Wirehome.Extensions.Contracts;
using Wirehome.Contracts.Core;

namespace Wirehome.Extensions.Messaging
{
    //TODO
    public class DebugMessage //: IBinaryMessage
    {
        public string Message { get; set; }

        public MessageType Type()
        {
            return MessageType.Debug;
        }

        public bool CanDeserialize(byte messageType, byte messageSize)
        {
            if (messageType == (byte)Type())
            {
                return true;
            }

            return false;
        }

        public bool CanSerialize(string messageType)
        {
            return false;
        }

        public object Deserialize(IBinaryReader reader, byte? messageSize)
        {
            var message = reader.ReadString(messageSize.GetValueOrDefault());

            return new DebugMessage
            {
                Message = message
            };
        }

        public byte[] Serialize(JObject message)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Debug message: {Message}";
        }

        public string MessageAddress(JObject message)
        {
            throw new NotImplementedException();
        }
    }
}