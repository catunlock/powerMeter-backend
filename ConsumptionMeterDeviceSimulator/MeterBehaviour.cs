using WebSocketSharp;
using WebSocketSharp.Server;

namespace Simulator
{
    public class MeterBehaviour : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            base.OnMessage(e);
        }
    }

}
