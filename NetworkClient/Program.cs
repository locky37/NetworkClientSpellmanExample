using System.Net.Sockets;
using System.Net;
namespace NetworkClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m_socClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            IPAddress ipAdd = IPAddress.Parse("192.168.1.4");
            IPEndPoint remoteEP = new IPEndPoint(ipAdd, int.Parse("50000"));

            try
            {
                byte[] bCmd = SendCMD(0x22);
                if (m_socClient.Connected)
                {
                    // < STX >< CMD ><,> ARG ><,>< CSUM >< ETX >
                    int x = m_socClient.Send(bCmd, bCmd.Length, SocketFlags.Partial);
                }
            }
            catch (Exception oex)
            {
                Console.WriteLine(oex.ToString());
            }
            finally { 
                m_socClient.Close(); 
                Console.WriteLine("Connection Close");
            }

            Console.WriteLine("Connection OK");
        }

        private static byte[] SendCMD(byte cmd, byte arg = 0x00)
        {
            byte[] bCmd = { 0x02, 0x00, 0x2c, 0x00, 0x2c, 0x03 };

            // CMD
            bCmd[1] = cmd;

            //ARG
            bCmd[3] = arg;

            return bCmd;
        }
    }
}