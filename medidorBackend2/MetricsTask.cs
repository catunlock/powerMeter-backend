namespace medidorBackend2
{
    public class MetricsTask
    {
        public MetricsTask()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("Test");
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
