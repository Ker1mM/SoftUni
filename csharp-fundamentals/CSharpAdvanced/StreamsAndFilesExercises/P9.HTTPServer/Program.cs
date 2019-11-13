using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace P9.HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Any, 3080);

            tcpListener.Start();

            while (true)
            {
                using (NetworkStream stream = tcpListener.AcceptTcpClient().GetStream())
                {
                    byte[] request = new byte[4096];
                    int readBytes = stream.Read(request, 0, request.Length);

                    string requestStr = Encoding.UTF8.GetString(request, 0, readBytes);
                    Console.Write(requestStr);

                    string pattern = @"(?<=GET )(\/[a-z]*)(?= HTTP)";

                    Match match = Regex.Match(requestStr, pattern);

                    Console.WriteLine(match.ToString());
                    string htmlPage;
                    if (match.ToString() == "/info")
                    {
                        htmlPage = "../../../../info.html";
                    }
                    else if (match.ToString() == "/")
                    {
                        htmlPage = "../../../../index.html";
                    }
                    else
                    {
                        htmlPage = "../../../../error.html";
                    }
                    var reader = new StreamReader(htmlPage);
                    string text;
                    using (reader)
                    {
                        text = reader.ReadToEnd();
                    }
                    string page = string.Format(text, DateTime.Now, Environment.ProcessorCount);
                    string html = string.Format("HTTP/1.1 200 OK\nContent-Type:text\n\n" + "{0}", page);

                    byte[] htmlBytes = Encoding.UTF8.GetBytes(html);

                    stream.Write(htmlBytes, 0, htmlBytes.Length);
                }
            }
        }
    }
}
