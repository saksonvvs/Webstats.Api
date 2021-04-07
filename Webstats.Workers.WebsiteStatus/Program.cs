using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Webstats.Dto.Ping;

namespace Webstats.Workers.WebsiteStatus
{
    class Program
    {
        static HttpListener Listener;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Worker - Website Status");

            try
            {
                Listener = new HttpListener();
                Listener.Prefixes.Add("http://localhost:8082/WebsiteStatus/");
                Listener.Start();


                while (true)
                {
                    HttpListenerContext context = Listener.GetContext();
                    if (context != null && context.Request != null)
                    {
                        string domain = context.Request.RawUrl.Replace("/WebsiteStatus/", "");
                        if (String.IsNullOrEmpty(domain))
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                        else
                        {
                            try
                            {
                                using (HttpClient httpReq = new HttpClient())
                                {
                                    WebsiteStatusDto result = new WebsiteStatusDto();

                                    Stopwatch stWatch = new Stopwatch();
                                    stWatch.Start();

                                    HttpResponseMessage message = await httpReq.GetAsync($"https://{domain}");

                                    
                                    Console.WriteLine($"Checking: {domain}");
                                    
                                    string responseMessage = message.StatusCode.ToString();

                                    stWatch.Stop();

                                    result.Url = $"https://{domain}";
                                    result.Status = responseMessage;
                                    result.Latency = stWatch.ElapsedMilliseconds;

                                    context.Response.StatusCode = 200;

                                    HttpListenerResponse response = context.Response;
                                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                                    response.ContentLength64 = buffer.Length;

                                    System.IO.Stream output = context.Response.OutputStream;
                                    output.Write(buffer, 0, buffer.Length);

                                    context.Response.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error:" + ex.Message.ToString());
                                context.Response.StatusCode = 500;
                                context.Response.Close();
                            }
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


    }
}
