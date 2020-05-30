using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fleck.Samples.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FleckLog.Level = LogLevel.Debug;
            var webSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:30000");
            List<String> users = new List<String>();
            string[] data;
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    webSockets.Add(socket);               
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    webSockets.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    // get message type
                    string type = message.Substring(0, 1);
                    string temp;
                    data = message.Replace("\0", ",").Split(',');
             
                    switch (type)
                    {
                        case "1": 
                             users.Add(data[1]);
                             temp = "1" + "\0" + data[1] + "\0" + data[2] + "\0" + "Joins the net";
                             webSockets.ToList().ForEach(s => s.Send(temp));
                             Console.WriteLine(temp);
                             break;
                        
                        case "2":
                             temp = message + "\0" + "Leaves the net";
                             webSockets.ToList().ForEach(s => s.Send(temp));
                             Console.WriteLine(temp);
                            break;

                        case "/":                             
                            int index = 0;
                            int index1 = 0;
                            for (int i = 0; i < users.Count; i++)
                            {
                                if (users[i].CompareTo(data[1]) == 0)
                                {
                                    index = i;
                                    Console.WriteLine("Exists" + index);
                                }
                                else if (users[i].CompareTo(data[2]) == 0)
                                {
                                    index1 = i;
                                }
                            }
                            
                            temp = "/" + "\0" + data[1] + "\0" + data[2] + "\0" + data[3] + "\0" + data[4] + "\0" + "PM from <" + data[2] +">";
                            webSockets.ElementAt(index).Send(temp);
                            webSockets.ElementAt(index1).Send(temp);
                            Console.WriteLine(temp);
                            break;

                        default:
                            temp = data[0] + "\0" + data[1] + "\0" + data[2];

                            webSockets.ToList().ForEach(s => s.Send(temp));
                            Console.WriteLine(temp);
                            break;
                    }
                };
            });
            
            Console.ReadLine();
        }        
    }
}
