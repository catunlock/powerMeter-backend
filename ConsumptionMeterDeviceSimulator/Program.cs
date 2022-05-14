using WebSocketSharp.Server;
using System.Net;
using Simulator;

IPAddress iPAddress = IPAddress.Loopback;
int port = 80;

var wss = new WebSocketServer(iPAddress, port);
wss.AddWebSocketService<MeterBehaviour>("/ws");
wss.Start();

Console.WriteLine($"Websocket listening at ws://{iPAddress}:{port}");
Console.ReadKey(true);
wss.Stop();
Console.WriteLine("Websocket service stopped... press any key to continue...");
Console.ReadKey(true);