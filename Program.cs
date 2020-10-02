using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace Epson_Control
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Epson Projector Control");
            Console.WriteLine("(c) Jennifer Fischer 2020");

            if (args.Length >= 2)
            {
                Console.WriteLine($"Sending {args[1]} to {args[0]}"); 
                sendControl(args[1].ToUpper(), args[0]);
            }
            else
            {
                Console.WriteLine("usage: Epson Control.exe [hostname/ip] [code]");
                Console.WriteLine("examlpe: Epson Control.exe 192.168.178.55 3B");
            }
        }

        public static void sendControl(string Control, string Hostname)
        {
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create($"http://{Hostname}/cgi-bin/sender.exe?KEY={Control}");
            wr.Headers["Accept"] = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            wr.Headers["Accept-Encoding"] = "gzip, deflate";
            wr.Headers["Accept-Language"] = "en-GB,en-US;q=0.9,en;q=0.8";
            wr.Headers["Connection"] = "keep-alive";
            wr.Headers["Host"] = Hostname;
            wr.Headers["Referer"] = $"http://{Hostname}/cgi-bin/webconf.exe?page=13";
            wr.Headers["Upgrade-Insecure-Requests"] = "1";
            wr.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36";

            HttpWebResponse response = (HttpWebResponse)wr.GetResponse();

            Console.WriteLine(response.StatusDescription);

            Stream dataStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(dataStream);
            string sResponse = streamReader.ReadToEnd();
            //Console.WriteLine(sResponse);

            streamReader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
