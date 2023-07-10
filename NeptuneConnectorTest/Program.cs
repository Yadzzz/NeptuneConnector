using NeptuneConnector;

namespace NeptuneConnectorTest
{
    internal class Program
    {
        static Connector connector;

        static void Main(string[] args)
        {
            connector = new Connector("127.0.0.1", 30000, "123", "123", 1);
            connector.Connect();

            while(true)
            {
                Console.ReadKey();
                connector.LogApplication("Warning", "LogText Test", DateTime.Now.ToString());
            }
        }
    }
}