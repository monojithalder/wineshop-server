using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Data.OleDb;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Data;

namespace testing_server
{
    //public class oledbc : Class1
    //{
        
        
        
    //}
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8888);
            MemoryStream ms;
            BinaryFormatter formatter;
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            DataTable dt = new DataTable();
            Console.WriteLine(" >> Server Started");
            
            //oledbc oc =new oledbc();
            
           
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;
            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[10025];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    string path = Environment.CurrentDirectory + "\\test.mdb";
                    OleDbConnection Cn = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + path + "; Jet OLEDB:Database password = medicine");
                    Cn.Open();
                    //OleDbCommand cmd = new OleDbCommand(dataFromClient, Cn);
                    OleDbCommand cmd = new OleDbCommand(dataFromClient, Cn);
                    OleDbDataReader rd = cmd.ExecuteReader();
   
                    dt.Load(rd);

                    Console.WriteLine(" >> Data from client - " + dataFromClient);
                    string serverResponse = "Last Message from client" + dataFromClient;

                    Class1 c1 = new Class1();
                    ms = new MemoryStream();
                    formatter = new BinaryFormatter();

                    formatter.Serialize(ms, dt);

                    Byte[] sendBytes = ms.ToArray();
            
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error");
                }

            }
            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();

        }
    }
}
