using Newtonsoft.Json;
using Swimlane.Webstats.Workers.PingHost.Dto;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Swimlane.Webstats.Workers.PingHost
{
    class Program
    {
        static HttpListener Listener;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Swimlane worker - Ping");

            try
            {
                Listener = new HttpListener();
                Listener.Prefixes.Add("http://localhost:8081/Ping/");
                Listener.Start();


                while (true)
                {
                    HttpListenerContext context = Listener.GetContext();
                    if (context != null && context.Request != null)
                    {
                        string domain = context.Request.RawUrl.Replace("/Ping/", "");
                        if (String.IsNullOrEmpty(domain))
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                        else
                        {
                            List<PingReplyDto> result = PingHost(domain);


                            context.Response.StatusCode = 200;

                            HttpListenerResponse response = context.Response;
                            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                            response.ContentLength64 = buffer.Length;

                            System.IO.Stream output = context.Response.OutputStream;
                            output.Write(buffer, 0, buffer.Length);

                            context.Response.Close();
                        }
                    }

                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Worker Exception:" + ex.Message.ToString());
            }

        }


        static List<PingReplyDto> PingHost(string host)
        {
            List<PingReplyDto> result = new List<PingReplyDto>();

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            // Use the default Ttl value which is 128,
            // but change the fragmentation behavior.
            options.DontFragment = true;

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            for (var i = 0; i < 4; i++)
            {
                PingReply reply = pingSender.Send(host, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    PingReplyDto res = new PingReplyDto();
                    res.Address = reply.Address.ToString();
                    res.RoundtripTime = reply.RoundtripTime;
                    res.Ttl = reply.Options.Ttl;
                    res.DontFragment = reply.Options.DontFragment;
                    res.BufferSize = reply.Buffer.Length;

                    result.Add(res);
                }
            }

            return result;
        }


    }
}
