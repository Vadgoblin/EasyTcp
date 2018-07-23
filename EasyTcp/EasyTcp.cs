namespace EasyTcp
{
    using System.Text;
    using System.Net;
    using System.Net.Sockets;

    public class EasyTcpListener
    {
        TcpListener listen;
        public EasyTcpListener(int port)
        {
            listen = new TcpListener(IPAddress.Any, port);
            listen.Start();
        }
        public EasyTcpListener(string ip, int port)
        {
            listen = new TcpListener(IPAddress.Parse(ip), port);
            listen.Start();
        }
        public EasyTcpListener(IPAddress ip, int port)
        {
            listen = new TcpListener(ip, port);
            listen.Start();
        }
        public EasyTcpClient GetConnecter()
        {
            return new EasyTcpClient(listen.AcceptTcpClient());
        }
        public void Start()
        {
            listen.Start();
        }
        public void Stop()
        {
            listen.Stop();
        }

    }
    public class EasyTcpClient
    {
        TcpClient client;
        NetworkStream stream;
        public EasyTcpClient(string ip, int port)
        {
            client = new TcpClient(ip, port);
            stream = client.GetStream();
        }
        public EasyTcpClient(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
        }
        public void Write(string messange)
        {
            byte[] messangee = Encoding.UTF8.GetBytes(messange);
            stream.Write(messangee, 0, messangee.Length);
        }
        public void Write(byte buffer)
        {
            stream.WriteByte(buffer);
        }
        public void Write(byte[] buffer )
        {
            stream.Write(buffer, 0, buffer.Length);
        }
        public void Write(byte[] buffer,int offset ,int size)
        {
            stream.Write(buffer, offset, size);
        }
        public string Read()
        {
            byte[] buffer = new byte[client.ReceiveBufferSize];
            int data = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, data);
        }
        public string Read(int size)
        {
            byte[] buffer = new byte[size];
            int data = stream.Read(buffer, 0,size);
            return Encoding.UTF8.GetString(buffer, 0, data);
        }
        public int Read(byte[] buffer)
        {
            return stream.Read(buffer, 0, buffer.Length);
        }
        public int Read(byte[] buffer,int offset,int size)
        {
            return stream.Read(buffer, offset, size);
        }
        public void Close()
        {
            client.Close();
        }
        //extra cuccos
        public bool Connected
        {
            get
            {
                return client.Connected;
            }
        }
        public EndPoint RemoteEndPoint
        {
            get
            {
                return client.Client.RemoteEndPoint;
            }
        }
        public EndPoint LocalEndPoint
        {
            get
            {
                return client.Client.LocalEndPoint;
            }
        }
        //Equals and get has code
        public override bool Equals(object obj)
        {
            if (!(obj is EasyTcpClient)) return false;
            EasyTcpClient target = obj as EasyTcpClient;
            if (target.stream.Equals(this.stream))
            {
                if (target.client.Equals(this.client))
                {
                    return true;
                }
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}