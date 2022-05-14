using WebSocketSharp;
using WebSocketSharp.Server;

namespace Simulator
{
    public class MeterBehaviour : WebSocketBehavior
    {
        private Random random = new Random();

        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
        }

        protected override void OnOpen()
        {
            base.OnOpen();
            Thread t = new Thread(() =>
            {
                while (true)
                {
                    int x = random.Next(300,6000);
                    this.Send(x.ToString());
                    Thread.Sleep(1000);
                }
                
            });
            t.Start();
        }
    }

}
