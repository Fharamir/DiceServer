using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace DiceServer
{
    class Program
    {

        static IPAddress MyIP = GetLocalIPAddress();
        static int Port = 44455;

        static void Main(string[] args)
        {
            Console.WriteLine("{0} - Welcome to DiceServer!", Get_Time());
            Console.WriteLine("{0} - ", Get_Time());

            Console.WriteLine("{0} - Starting listening on {1}:{2}", Get_Time(), MyIP.ToString(), Port.ToString());
            DataExchange D = new DataExchange(MyIP, Port);
            Console.WriteLine("{0} - Listening...", Get_Time());
            while (true)
            {
                string[] s = D.ReceiveString().Split('|');
                
                switch (Convert.ToInt32(s[1]))
                {
                    //flip a coin
                    case 0:
                        s[1] = "flipped a coin";
                        break;
                    //Roll 1d3
                    case 1:
                        s[1] = "rolled 1d3";
                        break;
                    //Roll 1d4
                    case 2:
                        s[1] = "rolled 1d4";
                        break;
                    //Roll 1d6
                    case 3:
                        s[1] = "rolled 1d6";
                        break;
                    //Roll 1d8
                    case 4:
                        s[1] = "rolled 1d8";
                        break;
                    //Roll 1d10
                    case 5:
                        s[1] = "rolled 1d10";
                        break;
                    //Roll 1d12
                    case 6:
                        s[1] = "rolled 1d12";
                        break;
                    //Roll 1d20
                    case 7:
                        s[1] = "rolled 1d20";
                        break;
                    //Roll 1d30
                    case 8:
                        s[1] = "rolled 1d30";
                        break;
                    //Roll 1d100
                    case 9:
                        s[1] = "rolled 1d100";
                        break;
                    //Roll all characteristics
                    case 10:
                        s[1] = "rolled new characteristics";
                        break;
                    //Roll 2d4
                    case 11:
                        s[1] = "rolled 2d4";
                        break;
                    //Roll 3d4
                    case 12:
                        s[1] = "rolled 3d4";
                        break;
                    //Roll 2d6
                    case 13:
                        s[1] = "rolled 2d6";
                        break;
                    //Roll 3d6
                    case 14:
                        s[1] = "rolled 3d6";
                        break;
                    //Roll 4d6
                    case 15:
                        s[1] = "rolled 4d6";
                        break;
                    //Roll 5d6
                    case 16:
                        s[1] = "rolled 5d6";
                        break;
                    //Roll 6d6
                    case 17:
                        s[1] = "rolled 6d6";
                        break;
                }

                Console.WriteLine("{0} - ***************************", Get_Time());
                Console.WriteLine("{0} - {1} {2}", Get_Time(), s[0], s[1]);
                Console.WriteLine("{0} - with result: {1}", Get_Time(), s[2]);
            }

        }

        public static IPAddress GetLocalIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private static string Get_Time()
        {
            string time = "";

            int a = DateTime.Now.Hour;
            if (a < 10) time += "0" + a;
            else time += a;
            time += ":";
            a = DateTime.Now.Minute;
            if (a < 10) time += "0" + a;
            else time += a;
            time += ":";
            a = DateTime.Now.Second;
            if (a < 10) time += "0" + a;
            else time += a;

            return time;
        }
    }
}
