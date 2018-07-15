﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NanoFabric.Docimax.Heroes.Api.Infrastructure
{
    public class ClientConfiguration
    {
        public int DelayInitialConnectSeconds { get; set; } = 5;
        public ClientRetryConfig ConnectionRetry { get; set; } = new ClientRetryConfig();
    }

    public class ClientRetryConfig
    {
        public int TotalRetries { get; set; } = 25;
        public int MinDelaySeconds { get; set; } = 3;
        public int MaxDelaySeconds { get; set; } = 10;
    }
}