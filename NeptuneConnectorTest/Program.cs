using NeptuneConnector;

namespace NeptuneConnectorTest
{
    internal class Program
    {
        static Connector connector;

        static void Main(string[] args)
        {
            connector = new Connector("127.0.0.1", 30000, "2a60314f-5daf-4d9d-8b03-5d1973a36277", "393be0e4-5aa5-4b5e-b915-3242085ea087", "123", "123", 1);
            connector.Connect();

            while(true)
            {
                Console.ReadKey();
                connector.LogApplication(LogType.Log, "LogText Test");
            }
        }
    }
}