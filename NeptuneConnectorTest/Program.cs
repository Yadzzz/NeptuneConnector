using NeptuneConnector;

namespace NeptuneConnectorTest
{
    internal class Program
    {
        static Connector connector;

        static void Main(string[] args)
        {
            ConnectorConfiguration config = new ConnectorConfiguration
            {
                Ip = "127.0.0.1",
                Port = 30000,
                AutoConnect = true,
                KeepConnectionAlive = true
            };

            connector = new Connector(
                config,
                "2a60314f-5daf-4d9d-8b03-5d1973a36277", 
                "393be0e4-5aa5-4b5e-b915-3242085ea087", 
                "5ef32396-0ace-4133-bd84-32f44bb703ce",
                "882b904e-7899-4255-b58e-731378f3605a",
                "14a55f88-c9d5-4ffa-a080-5a300ddf5848");
            connector.Connect();

            while(true)
            {
                Console.ReadKey();
                connector.LogApplication(LogType.Log, "LogText Test");
            }
        }
    }
}