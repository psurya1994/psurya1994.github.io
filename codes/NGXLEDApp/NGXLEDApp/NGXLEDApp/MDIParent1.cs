using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace NGXLEDApp
{
    public partial class MDIParent1 : Form
    {
        private int childFormNumber = 0;
        SerialPort ComPort = new SerialPort();
        private SchedulerForm SchedulerForm1;
        
        
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        public MDIParent1()
        {
            
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form1();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
        //    statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        private void UpdateLanguageCombo()
        {
            LEDLanguage Languages = new LEDLanguage();
            string[] LanguageList = Languages.GetLaguages();
            int index = 0;
            foreach (string s in LanguageList)
            {
                index++;
                ComboboxItem item = new ComboboxItem();
                item.Text = s;
                item.Value = index;
                Line1LanguageComboBox.Items.Add(item);
                Line1LanguageComboBox.SelectedIndex = 0;
                Line2LanguageComboBox.Items.Add(item);
                Line2LanguageComboBox.SelectedIndex = 0;
            }
        }

        public void clearallmsg()
        {

            int checksum = 0;
            short[] now = new short[10];


            byte[] convertToByte = Encoding.ASCII.GetBytes("FORMAT"); //GetBytes("FORMAT");
            now[0] = 1;
            now[1] = 0x82;
            for (int i = 4, k = 0; i <= 9; i++, k++)
                now[i] = convertToByte[k];

            for (int k = 0; k < now.Length; k++)
            {
                checksum += now[k];
            }
            byte c1 = (byte)checksum;
            byte c2 = (byte)(checksum >> 8);
            now[2] = c1;
            now[3] = c2;

            byte[] buf = new byte[10];
            for (int i = 0; i < buf.Length; i++)
                buf[i] = (byte)now[i];

         
            try
            {
                ComPort.BaudRate = 115200;
                ComPort.Parity = 0;
                ComPort.RtsEnable = false;

                ComPort.Write(buf, 0, buf.Length);


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
                ComPort.Read(buf, 0, buf.Length);
                if (buf[1] == 0x80 && buf[2] == 0x81)
                {
                    MessageBox.Show("Format Display Success.!");
                }
                else
                {
                    MessageBox.Show("Format Display Fail.!");
                }
            }
            catch (TimeoutException )
            {
                MessageBox.Show("Format Display Fail.!");
            }

        }

        private void UpdateEffectCombo()
        {
            LEDEffect Effects = new LEDEffect();
            string[] List = Effects.GetEffects();
            int index = 0;
            foreach (string s in List)
            {
                index++;
                ComboboxItem item = new ComboboxItem();
                item.Text = s;
                item.Value = index;
                Line1EffectComboBox.Items.Add(item);
                Line1EffectComboBox.SelectedIndex = 0;
                Line2EffectComboBox.Items.Add(item);
                Line2EffectComboBox.SelectedIndex = 0;
            }
        }

        private void UpdateSpeedCombo()
        {
            LEDSpeed Speed = new LEDSpeed();
            string[] List = Speed.GetSpeeds();
            int index = 0;
            foreach (string s in List)
            {
                index++;
                ComboboxItem item = new ComboboxItem();
                item.Text = s;
                item.Value = index;
                Line1SpeedComboBox.Items.Add(item);
                Line1SpeedComboBox.SelectedIndex = 0;
                Line2SpeedComboBox.Items.Add(item);
                Line2SpeedComboBox.SelectedIndex = 0;
            }
        }

        private void UpdateFontCombo()
        {
            LEDFont Font = new LEDFont();
            LEDLanguage ledLang=new LEDLanguage();
            var Lang = ledLang.StringToEnum(Line1LanguageComboBox.Text);
            string[] List = Font.GetFonts(Lang);
            int index = 0;
            Line1FontComboBox.Items.Clear();
            foreach (string s in List)
            {
                index++;
                ComboboxItem item = new ComboboxItem();
                item.Text = s;
                item.Value = index;
                Line1FontComboBox.Items.Add(item);
                Line1FontComboBox.SelectedIndex = 0;
                Line2FontComboBox.Items.Add(item);
                Line2FontComboBox.SelectedIndex = 0;
            }


             Lang = ledLang.StringToEnum(Line2LanguageComboBox.Text);
            List = Font.GetFonts(Lang);
            index = 0;
            Line2FontComboBox.Items.Clear();
            foreach (string s in List)
            {
                index++;
                ComboboxItem item = new ComboboxItem();
                item.Text = s;
                item.Value = index;
                Line2FontComboBox.Items.Add(item);
                Line2FontComboBox.SelectedIndex = 0;
            }

            if (Line1LanguageComboBox.Text == "Kannada")
            {

                textBox2.Font = new Font("BRH Kannada", 14, FontStyle.Regular);
            }
 
            else 
                if (Line1LanguageComboBox.Text == "Tamil")
                {

                    textBox2.Font = new Font("BRH Tamil", 14, FontStyle.Regular);
                }
                else
                    if (Line1LanguageComboBox.Text == "English")
                    {

                        textBox2.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
                    }
                    else
                        if (Line1LanguageComboBox.Text == "Hindi")
                        {

                            textBox2.Font = new Font("BRH Devanagari", 14, FontStyle.Regular);
                        }
                        else
                            if (Line1LanguageComboBox.Text == "Punjabi")
                            {

                                textBox2.Font = new Font("BRH Gurumukhi", 14, FontStyle.Regular);
                            }




            if (Line2LanguageComboBox.Text == "Kannada")
            {

                textBox1.Font = new Font("BRH Kannada", 14, FontStyle.Regular);
            }
         
            
            else if (Line2LanguageComboBox.Text == "Tamil")
                {

                    textBox1.Font = new Font("BRH Tamil", 14, FontStyle.Regular);
                }
                else
                    if (Line2LanguageComboBox.Text == "English")
                    {

                        textBox1.Font = new Font("Microsoft Sans Serif", 14, FontStyle.Regular);
                    }
                    else
                        if (Line2LanguageComboBox.Text == "Hindi")
                        {

                            textBox1.Font = new Font("BRH Devanagari", 14, FontStyle.Regular);
                        }
                        else
                            if (Line2LanguageComboBox.Text == "Punjabi")
                            {

                                textBox1.Font = new Font("BRH Gurumukhi", 14, FontStyle.Regular);
                            }




        }
       
        private void MDIParent1_Load(object sender, EventArgs e)
        {

            string[] row1 = new string[] { "Message" };
            for (int i = 0; i < 25; i++)
            {
                string str = "MESSAGE " + i;
                MessageSelectGrid.Rows.Add(str);
               
            }


            UpdateLanguageCombo();
            UpdateEffectCombo();
            UpdateSpeedCombo();
            UpdateFontCombo();
            ScanComPort();
            splitContainer1.Panel2.Enabled = false;
            toolStripButton6.Enabled = false;
            toolStripButton5.Enabled = false;

            comboBox1.SelectedIndex=0;

          
           
          //  splitContainer1.Panel2.Enabled = true;
           //  SchedulerForm1 = new SchedulerForm();
          

         
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void MessageGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void ScanComPort()
        {
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;
            //serialPort1.Ge


            ArrayComPortsNames = SerialPort.GetPortNames();
            //  ArrayComPortsNames = SerialPort.GetPortNames(
            toolStripComboBox1.Items.Clear();


            foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
            {
                //     if (comPort.Manufacturer == "NGX")
                {
                    toolStripComboBox1.Items.Add(comPort.Name);
                }
                //  Console.WriteLine(string.Format("{0} – {1}-{2}", comPort.Name, comPort.Description, comPort.Manufacturer));
            }


            //// comboBox1.Items.Add(
            //int Count = ArrayComPortsNames.Count();
            //if (Count > 0)
            //{
            //    do
            //    {
            //        index += 1;
            //        //  comboBox1.Items[index]=ArrayComPortsNames[index];
            //        toolStripComboBox1.Items.Add(ArrayComPortsNames[index]);
            //        //rtbIncoming.Text += ArrayComPortsNames[index] + "\n";
            //    }
            //    while (!((ArrayComPortsNames[index] == ComPortName) ||
            //                        (index == ArrayComPortsNames.GetUpperBound(0))));
            //}
            toolStripComboBox1.Text = "";
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ScanComPort();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
          //  if (ComPort.IsOpen == false)
            {

                if (toolStripComboBox1.SelectedItem != null)
                {
                    string Str = toolStripComboBox1.SelectedItem.ToString();
                    byte[] convertToByte = Encoding.ASCII.GetBytes("DETECT_DEVICE");
                    byte[] arr = new byte[50];
              
                    ComPort.PortName = toolStripComboBox1.SelectedItem.ToString();
                    ComPort.WriteTimeout = 4000;
                    ComPort.ReadTimeout = 4000;
                    try
                    {
                        ComPort.Open();
                  
                        Array.Clear(arr, 0, arr.Length);
                        arr[0] = 1;
                        arr[1] = 0x83;
                        arr[2] = 0;
                        arr[3] = 0;
                        for (int i = 4, k = 0; i <= 16; i++, k++)
                            arr[i] = convertToByte[k];
                        int checksum = 0;
                        for (int k = 0; k < arr.Length; k++)
                        {
                            checksum += arr[k];
                        }
                        byte c1 = (byte)checksum;
                        byte c2 = (byte)(checksum >> 8);
                        arr[2] = c1;
                        arr[3] = c2;


                        try
                        {


                            ComPort.BaudRate = 115200;
                            ComPort.Parity = 0;
                            ComPort.RtsEnable = false;

                            ComPort.Write(arr, 0, arr.Length);


                        }
                        catch (TimeoutException)
                        {

                            MessageBox.Show("Com Port Write Fail.");
                        }



                        Array.Clear(arr, 0, arr.Length);
                        System.Threading.Thread.Sleep(500);
                        try
                        {
                            ComPort.Read(arr, 0, arr.Length);
                        }
                        catch (TimeoutException )
                        {
                            MessageBox.Show("Device Not Connected");
                            ComPort.Close();
                            return;
                        }

                        MessageBox.Show("Com Port Open Success!");
                            toolStripButton2.Enabled = true;
                        toolStripButton1.Enabled = false;
                        toolStripComboBox1.Enabled = false;
                        toolStripButton3.Enabled = false;
                        toolStripButton5.Enabled = true;

                        splitContainer1.Panel2.Enabled = true;
                        toolStripButton6.Enabled = true;
                       
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Com Port Open Fail.");
                        return;
                    }


                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

            try
            {
                ComPort.Close();
                toolStripButton2.Enabled = false;
                toolStripButton1.Enabled = true;
                toolStripComboBox1.Enabled = true;
                toolStripButton3.Enabled = true;
                splitContainer1.Panel2.Enabled = false;
                toolStripButton6.Enabled = false;
                toolStripButton5.Enabled = false;

            }
            catch (Exception)
            {
                splitContainer1.Panel2.Enabled = false;
                MessageBox.Show("Com Port close Fail.");
                return;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                groupBox2.Enabled = false;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                groupBox2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int MessageId = 0;
            foreach (DataGridViewCell cell in MessageSelectGrid.SelectedCells)
            {
                MessageId = cell.RowIndex;

            }
            ReadPacket(MessageId);
        }

        private void ReadPacket(int MessageId)
        {

       
            int checksum = 0;
            short[] now = new short[200];


            byte[] convertToByte = Encoding.ASCII.GetBytes("READE_DATA"); //GetBytes("FORMAT");
            now[0] = 1;
            now[1] = 0x1;
            now[2] = 0;
            now[3] = 0;
            for (int i = 4, k = 0; i <= 13; i++, k++)
                now[i] = convertToByte[k];

            now[14] = (short)MessageId;

            for (int k = 0; k < now.Length; k++)
            {
                checksum += now[k];
            }
            byte c1 = (byte)checksum;
            byte c2 = (byte)(checksum >> 8);
            now[2] = c1;
            now[3] = c2;

            byte[] buf = new byte[200];
            for (int i = 0; i < buf.Length; i++)
                buf[i] = (byte)now[i];

            try
            {


                ComPort.BaudRate = 115200;
                ComPort.Parity = 0;
                ComPort.RtsEnable = false;

                ComPort.Write(buf, 0, buf.Length);


            }
            catch (TimeoutException)
            {

                MessageBox.Show("Com Port Write Fail.");
            }
            catch (InvalidOperationException)
            {

                MessageBox.Show("Com Port Closed\n\nReconnect Device & Try Again!.");
            }
            Array.Clear(buf, 0, buf.Length);
            System.Threading.Thread.Sleep(500);
            try
            {
                ComPort.Read(buf, 0, buf.Length);
                AssignValues(buf);
                MessageBox.Show("Data Read Success!");
                
            }
            catch (TimeoutException )
            {
                MessageBox.Show("Com Port\nRead Data Failed!");
            }

            
          
   
        }

        private void AssignValues(byte[] buf)
        {
            int MessageId, NoOfLines, LineId, LineId1, Font, Font1, Effect, Effect1, Speed, Speed1, TextLength, TextLength1, Count = 0, l = 0,Secondline;
            string s;
    
            byte[] tmp = new byte[200];
         while(1==1)
         {
            if (buf[Count] == 1 && buf[Count + 1] == 1)
            {
                break;
            }
            Count++;
        }
         MessageId = buf[14 + Count];
         NoOfLines = buf[16 + Count];

         LineId = buf[17 + Count];
         Font = buf[18 + Count];
         Effect = buf[19 + Count];
         Speed = buf[20 + Count];
         TextLength = buf[21 + Count];
         Array.Clear(tmp, 0, tmp.Length);
         for (int i = (22 + Count); i <=( TextLength + 22 + Count); i++)
         {
            
             tmp[l++] = buf[i];
             
             
         }
         Secondline = (TextLength + 22 + Count);
         //s = Encoding.ASCII.GetString(tmp);
         s = Encoding.Default.GetString(tmp);
            textBox2.Text = s;

            if (Line1LanguageComboBox.Text != "English")
            {
                Font = 0;
            }
 
            if (Font == 23 || Font == 24 || Font ==25 || Font==26)
            {
                Line1FontComboBox.SelectedIndex = 0;
            }
 
            else
            {
                Line1FontComboBox.SelectedIndex = Font;
            }
            Line1EffectComboBox.SelectedIndex = Effect;
            if (Speed == 255)
            {
                Line1SpeedComboBox.SelectedIndex = 10;
            }
            else
            {
                Line1SpeedComboBox.SelectedIndex = Speed-1;
            }

            if (NoOfLines == 2)
            {
                comboBox1.SelectedIndex = 1;
                groupBox2.Enabled = true;

                LineId1 = buf[Secondline+0];
                Font1 = buf[Secondline + 1];
                Effect1 = buf[Secondline + 2];
                Speed1 = buf[Secondline + 3];
                TextLength1 = buf[Secondline + 4];
                Array.Clear(tmp, 0, tmp.Length);
                l = 0;
                for (int i = (Secondline + 5); i <= (TextLength1 + Secondline + 5); i++)
                {

                    tmp[l++] = buf[i];


                }
           
               // s = Encoding.ASCII.GetString(tmp);
                s = Encoding.Default.GetString(tmp);
                textBox1.Text = s;

                if (Line1LanguageComboBox.Text != "English")
                {
                    Font1 = 0;
                }

                if (Font1 == 23 || Font1 == 24 || Font1 == 25 || Font1 == 26)
                {
                    Line1FontComboBox.SelectedIndex = 0;
                }
                else
                {
                    Line2FontComboBox.SelectedIndex = Font1;
                }
                Line2EffectComboBox.SelectedIndex = Effect1;
                if (Speed1 == 255)
                {
                    Line2SpeedComboBox.SelectedIndex = 10;
                }
                else
                {
                    Line2SpeedComboBox.SelectedIndex = Speed1-1;
                }
            }
        }

        private string HexAsciiConvert(string hex)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= hex.Length - 2; i += 2)
            {

                sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),

                System.Globalization.NumberStyles.HexNumber))));

            }

            return sb.ToString();

        }
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat(" 0X{0:x2}", b);
            return hex.ToString();
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int messageid = 0, NoOfLines = 0, Language = 0, Font= 0,Font1=0,Effect=0,Effect1=0,Speed=0,Speed1=0;
              Dictionary<string, int> fonts = new Dictionary<string, int>();
              Dictionary<string, int> effectid = new Dictionary<string, int>();
              Dictionary<string, int> speedid = new Dictionary<string, int>();
              string Message1 = textBox2.Text;
              string Message2 = textBox1.Text;
              fonts.Add("Font 7x5", 0);
            fonts.Add("Tohama Small", 1);
            fonts.Add("Verdana", 2);
            fonts.Add("Courier", 3);
            fonts.Add("Tahoma Large", 4);
            fonts.Add("Georgia", 5);
            fonts.Add("RockWell", 6);
            fonts.Add("Thremin Gothic", 7);
            fonts.Add("Batang", 8);
            fonts.Add("Bitwise", 9);
            fonts.Add("Pirule", 10);
            fonts.Add("Venus Rising", 11);
            fonts.Add("Engravers", 12);
            fonts.Add("Impact H15", 13);
            fonts.Add("Engravers H16", 14);
            fonts.Add("Venus Rising H16", 15);
            fonts.Add("Impact H16", 16);
            fonts.Add("Steelfish H16", 17);
            fonts.Add("Impact H32", 18);
            fonts.Add("Steelfish H32", 19);
            fonts.Add("Steelfish Reg H32", 20);
            fonts.Add("Impact Mid H32", 21);
            fonts.Add("Steelfish MidReg H32", 22);
            fonts.Add("Hindi", 23);
            fonts.Add("BrH Kannada", 24);
            fonts.Add("Punjabi", 25);
            fonts.Add("BRH Tamil", 26);
           
           
            effectid.Add("Horizontal Scroll", 0);
            effectid.Add("Still", 1);
            effectid.Add("Horizontal Scroll Wavy", 2);
            effectid.Add("Flash", 3);
            effectid.Add("Drop", 4);
            effectid.Add("Up Down", 5);
            effectid.Add("Matrix", 6);
            effectid.Add("Diagonal", 7);
            effectid.Add("Right slide", 8);
            effectid.Add("Blooming", 9);
            effectid.Add("Left Slide", 10);
            effectid.Add("Vertical Up", 11);
            effectid.Add("Verticle Down", 12);
            effectid.Add("Jumping", 13);

            speedid.Add("Speed 1", 1);
            speedid.Add("Speed 2", 2);
            speedid.Add("Speed 3", 3);
            speedid.Add("Speed 4", 4);
            speedid.Add("Speed 5", 5);
            speedid.Add("Speed 6", 6);
            speedid.Add("Speed 7", 7);
            speedid.Add("Speed 8", 8);
            speedid.Add("Speed 9", 9);
            speedid.Add("Speed 10", 10);
            speedid.Add("Default", 255);


            LedDisplay FormatPacket = new LedDisplay();          
            foreach (DataGridViewCell cell in MessageSelectGrid.SelectedCells)
            {
                messageid = cell.RowIndex;
              
            }
            if (comboBox1.SelectedIndex == 0)
            {
                NoOfLines = 1;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                NoOfLines = 2;
            }
            Font = fonts[Line1FontComboBox.Text];
            Effect = effectid[Line1EffectComboBox.Text];
            Speed = speedid[Line1SpeedComboBox.Text];
            

         

            if (NoOfLines == 1)
            {
                FormatPacket.NoofLines = (byte)NoOfLines;
                FormatPacket.Mode = (byte)NoOfLines;
                FormatPacket.MessageID = (byte)messageid;
                FormatPacket.FontType = (byte)Font;
                FormatPacket.EffectSpeed = (byte)Speed;
                FormatPacket.EffectType = (byte)Effect;
                FormatPacket.Text = Message1;
                int bytes = FormatPacket.Size();
                // richTextBox1.Text = bytes.ToString();
                byte[] ba = FormatPacket.ToByteArray();

               

                try
                {
                    ComPort.BaudRate = 115200;
                    ComPort.Parity = 0;
                    ComPort.RtsEnable = false;

                    ComPort.Write(ba, 0, ba.Length);


                }
                catch (TimeoutException)
                {
                    //ComPort.Close();
                    MessageBox.Show("Data Write Fail.");
                }

                Array.Clear(ba, 0, ba.Length);
                System.Threading.Thread.Sleep(500);
                try
                {
                    ComPort.BaudRate = 115200;
                    ComPort.Parity = 0;
                    ComPort.RtsEnable = false;
                    ComPort.Read(ba, 0, ba.Length);
                    if (ba[1] == 0x80 && ba[2] == 0x81)
                    {
                        MessageBox.Show("Data Write Success!");
                    }
                    else
                    {
                        MessageBox.Show("Data Write Fail!");
                    }
                    
                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Data Write Fail!");
                }
            }
            else if (NoOfLines == 2)
            {
                Font1 = fonts[Line2FontComboBox.Text];
                Effect1 = effectid[Line2EffectComboBox.Text];
                Speed1 = speedid[Line2SpeedComboBox.Text];

                ushort[] Tmp = new ushort[(5 + Message1.Length) + (5 + Message2.Length) + 17];
                int checksum = 0;
                byte[] convertToByte = Encoding.ASCII.GetBytes("WRITE_DATA");// GetBytes("WRITE_DATA");
                byte[] convertMessage = Encoding.Default.GetBytes(Message1); //GetBytes(Message1);
                byte[] convertMessage1 = Encoding.Default.GetBytes(Message2); //GetBytes(Message2);
                int j = 0, l = 0, line2offset;

                Tmp[0] = 1;										//display ID
                Tmp[1] = 00;										//command ID
                for (int i = 4, k = 0; i <= 13; i++, k++)
                    Tmp[i] = convertToByte[k];						//message data string
                Tmp[14] = (ushort)messageid;										//message ID
                Tmp[15] = (ushort)NoOfLines;										//mode 2 for two lines 1 for 1 line
                Tmp[16] = 2;										//no of lines
                Tmp[17] = 0;										//line
                Tmp[18] = (ushort)Font;									//font
                Tmp[19] = (ushort)Effect;										//effect

                Tmp[20] = (ushort)Speed;								//effect speed
                Tmp[21] = (ushort)Message1.Length;				//length
                for (j = 22, l = 0; j < (22 + Message1.Length); j++, l++)
                    Tmp[j] = convertMessage[l];

                line2offset = j - 1;
                Tmp[line2offset + 1] = 1;										//line
                Tmp[line2offset + 2] = (ushort)Font1;									//font
                Tmp[line2offset + 3] = (ushort)Effect1;										//effect
                Tmp[line2offset + 4] = (ushort)Speed1;								//effect speed
                Tmp[line2offset + 5] = (ushort)Message2.Length;				//length
                for (j = line2offset + 6, l = 0; j < ((line2offset + 6) + Message2.Length); j++, l++)
                    Tmp[j] = convertMessage1[l];


                for (int k = 0; k < Tmp.Length; k++)
                {
                    checksum += Tmp[k];
                }
                byte c1 = (byte)checksum;
                byte c2 = (byte)(checksum >> 8);
                Tmp[2] = c1;
                Tmp[3] = c2;


                // Reset out string buffer to zero and clear the edit text field

                byte[] buf = new byte[(5 + Message1.Length) + (5 + Message2.Length) + 17];
                for (int i = 0; i < buf.Length; i++)
                    buf[i] = (byte)Tmp[i];

            

                try
                {
                    ComPort.BaudRate = 115200;
                    ComPort.Parity = 0;
                    ComPort.RtsEnable = false;

                    ComPort.Write(buf, 0, buf.Length);


                }
                catch (TimeoutException)
                {
                    //ComPort.Close();
                    MessageBox.Show("Data Write Fail.");
                }

                Array.Clear(buf, 0, buf.Length);
                System.Threading.Thread.Sleep(500);
                try
                {
                    ComPort.BaudRate = 115200;
                    ComPort.Parity = 0;
                    ComPort.RtsEnable = false;
                    ComPort.Read(buf, 0, buf.Length);
                    if (buf[1] == 0x80 && buf[2] == 0x81)
                    {
                        MessageBox.Show("Data Write Success!");
                    }
                    else
                    {
                        MessageBox.Show("Data Write Fail!");
                    }

                }
                catch (TimeoutException)
                {
                    MessageBox.Show("Data Write Fail!");
                }

            }
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeviceWriteDataPacket WriteDataPacket = new DeviceWriteDataPacket();
           
            WriteDataPacket.DisplayId = 0;
            WriteDataPacket.MessageID = 1;
            WriteDataPacket.Mode = 1;
            WriteDataPacket.NoofLines = 1;
            WriteDataPacket.LineId = 0;
            WriteDataPacket.FontType = 1;
            WriteDataPacket.EffectType = 1;
            WriteDataPacket.EffectSpeed = 0xff;
            WriteDataPacket.Text = "NGX TECHNOLOGIES";
           

            // richTextBox1.Text = bytes.ToString();

            byte[] ba = WriteDataPacket.ToByteArray();

           

            try
            {
                ComPort.BaudRate = 38400;
                ComPort.Parity = 0;
                ComPort.RtsEnable = false;

                ComPort.Write(ba, 0, ba.Length);


            }
            catch (TimeoutException)
            {
                //ComPort.Close();
                MessageBox.Show("Data Write Fail.");
            }
        }

        private void Line2EffectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Line1LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFontCombo();
     
       
        }

        private void Line2LanguageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFontCombo();
    
        }
        private DateTimePicker timePicker;
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
        
            SchedulerForm s = new SchedulerForm(this.ComPort);

            s.Show();
            //SchedulerForm1.Show();
        }

        private void InitializeTimePicker()
        {
            timePicker = new DateTimePicker();
            timePicker.Format = DateTimePickerFormat.Time;
            timePicker.ShowUpDown = true;
            timePicker.Location = new Point(10, 10);
            timePicker.Width = 100;
            Controls.Add(timePicker);
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void MessageSelectGrid_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {

        }

        private void Line1FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolTip_Popup(object sender, System.Windows.Forms.PopupEventArgs e)
        {

        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show("Do you want to Format display?",
             "Format Display",
             MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result1)
            {
                clearallmsg();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(this.ComPort);
            f.Show();
        }

        private void Line2FontComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Line2SpeedComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }

      
    }
}
