﻿using System;

namespace RabbitMQ.Bus.Events
{
    public abstract class Event
    {
        public DateTime TimeStamp { get; protected set; }
        protected Event()
        {
            TimeStamp = DateTime.Now;
        }
    }
}