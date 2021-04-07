using System;

namespace Webstats.Common
{
    public class ServiceTypes
    {
        public static readonly string Unknown = "Unknown";
        public static readonly string Ping = "Ping";
        public static readonly string ReverseDns = "ReverseDns";
        public static readonly string GeoIp = "GeoIp";
        public static readonly string RDAP = "RDAP";
        public static readonly string TraceRoute = "TraceRoute";
        public static readonly string DNSLookup = "DNSLookup";

        public static readonly string[] ServiceList = new string[]
        {
            Unknown,
            Ping,
            ReverseDns,
            GeoIp,
            RDAP,
            TraceRoute,
            DNSLookup
        };

    }
}
