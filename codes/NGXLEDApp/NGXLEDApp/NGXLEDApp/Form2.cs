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
    public partial class Form2 : Form
    {
        private System.IO.Ports.SerialPort serialPort;

        public Form2()
        {
            InitializeComponent();
        }

        public Form2(System.IO.Ports.SerialPort serialPort)
        {
            // TODO: Complete member initialization
            this.serialPort = serialPort;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.CustomFormat = "hh mm tt";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            DateTime dt1 = dateTimePicker2.Value;
  
            int hour = dt1.Hour;
            int minute = dt1.Minute;
            int second = dt1.Second;
  
           
            string buffer1 = "\n\nTime\n--------------------------------------------\nHour :" + hour.ToString() + "\tMinute :" + minute.ToString() + "\tSecond :" + second.ToString();
            DialogResult result1 = MessageBox.Show("Set Time\n" + buffer1+"\n\nAre you Sure?", "Set Time",
 MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result1)
            {
                SetDateTime();
            }
         
        }

        public void SetDateTime()
        {
          

     
        }
    }
}
