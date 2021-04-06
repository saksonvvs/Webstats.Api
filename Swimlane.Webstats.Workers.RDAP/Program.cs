using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Swimlane.Webstats.Workers.RDAP
{
    class Program
    {
        static HttpListener Listener;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Swimlane worker - RDAP");

            try
            {
                Listener = new HttpListener();
                Listener.Prefixes.Add("http://localhost:8080/RDAP/");
                Listener.Start();


                while (true)
                {
                    HttpListenerContext context = Listener.GetContext();
                    if (context != null && context.Request != null)
                    {
                        string domain = context.Request.RawUrl.Replace("/RDAP/", "");
                        if (String.IsNullOrEmpty(domain))
                        {
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                        else
                        {
                            using (HttpClient httpReq = new HttpClient())
                            {
                                HttpResponseMessage message = await httpReq.GetAsync($"https://rdap.verisign.com/com/v1/domain/{domain}");

                                Console.WriteLine($"Querying: {domain}");

                                string responseMessage = await message.Content.ReadAsStringAsync();

                                context.Response.StatusCode = 200;

                                HttpListenerResponse response = context.Response;
                                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseMessage);
                                response.ContentLength64 = buffer.Length;

                                System.IO.Stream output = context.Response.OutputStream;
                                output.Write(buffer, 0, buffer.Length);

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
