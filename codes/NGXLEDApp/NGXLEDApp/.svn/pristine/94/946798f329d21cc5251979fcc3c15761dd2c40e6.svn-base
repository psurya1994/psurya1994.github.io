using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NGXLEDApp
{
    public partial class SchedulerForm : Form
    {
        private System.IO.Ports.SerialPort serialPort;

        
   
        public SchedulerForm()
        {
            InitializeComponent();
        }

        public SchedulerForm(System.IO.Ports.SerialPort serialPort)
        {
            this.serialPort = serialPort;
     
            InitializeComponent();
      
           
        }

        private void SchedulerForm_Load(object sender, EventArgs e)
        {
            //InitializeTimePicker();
            GetSchedulerTime();
           
        }

        private void GetSchedulerTime()
        {



            int count=0;
            int checksum = 0;
            short[] now = new short[50];


            byte[] convertToByte = Encoding.ASCII.GetBytes("GET_SCHEDULER"); //GetBytes("FORMAT");
            now[0] = 1;
            now[1] = 0x8e;
            now[2] = 0;
            now[3] = 0;
            for (int i = 4, k = 0; i <= 16; i++, k++)
                now[i] = convertToByte[k];

            now[17] = (short)0;

            for (int k = 0; k < now.Length; k++)
            {
                checksum += now[k];
            }
            byte c1 = (byte)checksum;
            byte c2 = (byte)(checksum >> 8);
            now[2] = c1;
            now[3] = c2;

            byte[] buf = new byte[50];
            for (int i = 0; i < buf.Length; i++)
                buf[i] = (byte)now[i];

            try
            {


                this.serialPort.BaudRate = 115200;
                this.serialPort.Parity = 0;
                this.serialPort.RtsEnable = false;

                this.serialPort.Write(buf, 0, buf.Length);


            }
            catch (TimeoutException)
            {
             
                MessageBox.Show("Com Port Write Fail.");
            }

            Array.Clear(buf, 0, buf.Length);
            System.Threading.Thread.Sleep(500);
            this.serialPort.Read(buf, 0, buf.Length);

            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, buf[22], buf[23], 0);
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, buf[24], buf[25], 0);
            
   
        }
    

        private void InitializeTimePicker()
        {
           
            dateTimePicker1.Format= DateTimePickerFormat.Time;
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.CustomFormat = "hh mm tt";
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.CustomFormat = "hh mm tt";
           // dateTimePicker1.
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime time = dateTimePicker1.Value;
            DateTime time1 = dateTimePicker2.Value;
            int starthour = time.Hour;
            int startminute = time.Minute;
            int starthour1 = time1.Hour;
            int startminute1 = time1.Minute;
            string starttime = "\n\nStartTime\n------------------------\nhour :" + starthour.ToString() + "\tminute :" + startminute.ToString();
            string endtime = "\n\nEndTime\n------------------------\nhour :" + starthour1.ToString() + "\tminute :" + startminute1.ToString();
            DialogResult result1 = MessageBox.Show("Confirm Scheduler Time"+starttime+endtime,
        "Set Scheduler",
        MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result1)
            {
                SetDateTime();
                SetSchedulerTime();
            }

            
        }

        private void SetDateTime()
        {



            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int second = DateTime.Now.Second;

            int checksum = 0;
            short[] now = new short[50];


            byte[] convertToByte = Encoding.ASCII.GetBytes("SET_TIME"); //GetBytes("FORMAT");
            now[0] = 1;
            now[1] = 0x91;
            now[2] = 0;
            now[3] = 0;
            for (int i = 4, k = 0; i <= 11; i++, k++)
                now[i] = convertToByte[k];

            now[12] = (short)0;
            now[13] = (short)0;         //year
            now[14] = (short)0;        //year
            now[15] = (short)0;          //month
            now[16] = (short)0;         //day
            now[17] = (short)0;         //day of year
            now[18] = (short)0;         //day of year
            now[19] = 0;
            now[20] = (short)hour;//day of week
            now[21] = (short)minute;
            now[22] = (short)second;
            for (int k = 0; k < now.Length; k++)
            {
                checksum += now[k];
            }
            byte c1 = (byte)checksum;
            byte c2 = (byte)(checksum >> 8);
            now[2] = c1;
            now[3] = c2;

            byte[] buf = new byte[50];
            for (int i = 0; i < buf.Length; i++)
                buf[i] = (byte)now[i];

            try
            {


                this.serialPort.BaudRate = 115200;
                this.serialPort.Parity = 0;
                this.serialPort.RtsEnable = false;

                this.serialPort.Write(buf, 0, buf.Length);


            }
            catch (TimeoutException)
            {
                //ComPort.Close();
                MessageBox.Show("Com Port Write Fail.");
            }

            catch (InvalidOperationException)
            {
                MessageBox.Show("Com Port Write Fail.");
            }
        }

        private void SetSchedulerTime()
        {
            DateTime time = dateTimePicker1.Value;
            DateTime time1 = dateTimePicker2.Value;
            int starthour = time.Hour;
            int startminute = time.Minute;
            int starthour1 = time1.Hour;
            int startminute1 = time1.Minute;

            int checksum = 0;
            short[] now = new short[50];


            byte[] convertToByte = Encoding.ASCII.GetBytes("SET_SCHEDULER"); //GetBytes("FORMAT");
            now[0] = 1;
            now[1] = 0x8D;
            for (int i = 4, k = 0; i <= 16; i++, k++)
                now[i] = convertToByte[k];

            now[17] = 0x00;
            now[18] = (short)starthour;
            now[19] = (short)startminute;
            now[20] = (short)starthour1;
            now[21] = (short)startminute1;
            now[22] = 0x01;
            for (int k = 0; k < now.Length; k++)
            {
                checksum += now[k];
            }
            byte c1 = (byte)checksum;
            byte c2 = (byte)(checksum >> 8);
            now[2] = c1;
            now[3] = c2;

            byte[] buf = new byte[50];
            for (int i = 0; i < buf.Length; i++)
                buf[i] = (byte)now[i];

            try
            {
           
                
                this.serialPort.BaudRate = 115200;
                this.serialPort.Parity = 0;
                this.serialPort.RtsEnable = false;

                this.serialPort.Write(buf, 0, buf.Length);


            }
            catch (TimeoutException)
            {
                //ComPort.Close();
                MessageBox.Show("Com Port Write Fail.");
            }

            Array.Clear(buf, 0, buf.Length);
            System.Threading.Thread.Sleep(500);
            try
            {
                this.serialPort.Read(buf, 0, buf.Length);
                if (buf[1] == 0x80 && buf[2] == 0x81)
                {
                    MessageBox.Show("Set Scheduler Success.!");
                }
                else
                {
                    MessageBox.Show("Set Scheduler Fail.!");
                }
            }
            catch (TimeoutException te)
            {
                MessageBox.Show("Set Scheduler Fail.!");
            }
        }
    }
}
