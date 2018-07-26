using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace XuatThuy.Utils
{
    public class BangDTu : IDisposable
    {
        public enum eSendType
        {
            Line1 = 1,
            Line2 = 2,
            SetSpeed = 3,
            AllLine = 4,
            Error = 5
        }

        public string IPAdress { get; set; }
        public int Port { get; set; }
        public string Line1_Text { get; set; }
        public string Line2_Text { get; set; }

        TcpClient client;
        NetworkStream Stream;
        int ReadTimeOut;
        int WriteTimeOut;

        bool _connected;
        public bool Connected
        {
            get
            {
                return _connected;
            }

            set
            {
                _connected = value;
            }
        }

        public BangDTu(string IPAdr, int PortNum, int Speed = 1)
        {
            IPAdress = IPAdr;
            Port = PortNum;
            ReadTimeOut = 100;
            WriteTimeOut = 100;

            //DispatcherTimer timer;
            //timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += new EventHandler(timer_Tick);
            //timer.Start();
        }

        int index;

        private void timer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine("index ====== " + index.ToString());
            Line2_Text = index.ToString();
            Line1_Text = (index * 2).ToString();
            SetTextLine(4);


            //if (index <= reconnectTime &&         // quá 10s thì ko kết nối lại
            //    ((index % reconnectSpan) == 0) &&   // 2s mới kết nối lại
            //    //!IsConnected())                   // mat ket noi 
            //    !this.Ping())                   // mat ket noi 
            //{
            //    Debug.WriteLine("RECONNECT ====================== ");
            //    this.Connect();
            //}

            //// bắt đầu kết nối lại sau 120s
            //if (index >= reconnectInterval)
            //{
            //    index = 0;
            //}

            index++;
        }

        public bool IsConnected()
        {
            try
            {
                return !(client.Client.Poll(1, SelectMode.SelectRead) && client.Available == 0);
            }
            catch (Exception se)
            {
                Debug.WriteLine("IsConnected ===== false. se message: " + se.Message);
                return false;
            }
        }

        public bool Ping()
        {
            Debug.WriteLine("Ping đến bảng điện tử ????????????????????");
            try
            {
                string message = "ping";
                Byte[] data = System.Text.Encoding.GetEncoding(28591).GetBytes(message);
                    
                // Send the message to the connected TcpServer. 
                Stream.Write(data, 0, data.Length);
                _connected = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Ping đến bảng điện tử thất bại. Message: " + ex.Message);
                _connected = false;
                this.Dispose();
                return false;
            }

            return true;
        }

        public bool Connect()
        {
            Debug.WriteLine("========== Kết nối Bảng điện tử =========");
            this.Dispose();

            client = new TcpClient();
            bool res = client.ConnectAsync(IPAdress, Port).Wait(500);

            if (res && client.Connected)
            {
                // Get a client stream for reading and writing.
                Stream = client.GetStream();
                Stream.ReadTimeout = ReadTimeOut;
                Stream.WriteTimeout = WriteTimeOut;
            }
            else
            {
                //res = false;
                //this.Dispose();
            }

            return false;
        }

        void Send(string message, int sendType = 0)
        {
            //Debug.WriteLine("Send Message ====== " + message);
            try
            {
                client = new TcpClient();
                bool res = client.ConnectAsync(IPAdress, Port).Wait(500);

                //Debug.WriteLine("client.Connected  =========   " + client.Connected.ToString());

                if (res && client.Connected)
                {
                    Stream = client.GetStream();
                    Stream.ReadTimeout = ReadTimeOut;
                    Stream.WriteTimeout = WriteTimeOut;
                    //SetSpeed(Speed);

                    // Translate the passed message into ASCII and store it as a Byte array.
                    //Byte[] data = System.Text.Encoding.GetEncoding(28591).GetBytes(message);
                    Byte[] data = System.Text.Encoding.Default.GetBytes(message);

                    Byte[] data_tcvn3 = Encoding.Convert(Encoding.Default, Encoding.GetEncoding(28591), data);
                    // Send the message to the connected TcpServer. 
                    //Stream.Write(data, 0, data.Length);
                    Stream.Write(data_tcvn3, 0, data_tcvn3.Length);

                    data = new Byte[256];
                    // Read the first batch of the TcpServer response bytes.
                    int bytes = Stream.Read(data, 0, 10);


                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.Dispose();
                Debug.WriteLine("Lỗi khi ghi vào bảng. ex message: " + ex.Message);

                LogUtils.PrintLog("ERROR: Không ghi được vào bảng điện tử. Nội dung ghi: " + message);
                LogUtils.PrintLog("Exception message: " + ex.Message);
            }
            //Stream.Close();
        }

        public void SetTextLine(int sendType = 1)
        {
            if (sendType == (int)eSendType.Line1)
            {
                SetLine1();
            }
            else if (sendType == (int)eSendType.Line2)
            {
                SetLine2();
            }
            else if (sendType == (int)eSendType.AllLine)
            {
                SetLine1();
                System.Threading.Thread.Sleep(200);
                SetLine2();
            }
        }

        void SetLine1()
        {
            try
            {
                int t = (13 - Line1_Text.Length) < 0 ? 1 : (13 - Line1_Text.Length);
                var tabs = new string(' ', t);
                var Space = new StringBuilder();
                string Msg;

                //Set Line 2

                tabs = new string(' ', t);

                if (Line1_Text.Length <= 12)
                {
                    Msg = (char)2 + "10" + (new string(' ', t / 2)) + Line1_Text + (new string(' ', t / 2)) + (char)13; //+ (new string(' ', t / 2)) + 
                }
                else
                {
                    Msg = (char)2 + "11" + Line1_Text + Space.Append(' ', 5) + (char)13;
                }
                Send(Msg, (int)eSendType.Line1);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Line1: " + ex.Message);
            }
        }

        void SetLine2()
        {
            try
            {
                int t = (13 - Line2_Text.Length) < 0 ? 1 : (13 - Line2_Text.Length);
                var tabs = new string(' ', t);
                var Space = new StringBuilder();
                string Msg;

                //Set Line 2

                tabs = new string(' ', t);

                if (Line2_Text.Length <= 12)
                {
                    Msg = (char)2 + "20" + (new string(' ', t / 2)) + Line2_Text + (new string(' ', t / 2)) + (char)13;
                }
                else
                {
                    Msg = (char)2 + "21" + Line2_Text + Space.Append(' ', 5) + (char)13;
                }
                Send(Msg, (int)eSendType.Line2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Line2: " + ex.Message);
            }
        }

        public void SetSpeed(int speed)
        {
            if (speed > 0)
            {
                string speedStr = (char)2 + "t" + speed.ToString() + (char)13;
                Send(speedStr, (int)eSendType.SetSpeed);
            }
        }

        public void Dispose()
        {
            if (Stream != null)
            {
                Stream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
        }
    }
}
