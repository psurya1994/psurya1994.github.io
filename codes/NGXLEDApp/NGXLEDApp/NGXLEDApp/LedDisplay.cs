using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace NGXLEDApp
{
    class LedDisplay
    {

              [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _Packet
        {
            public Byte DisplayId;
            public Byte CommandID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
            public Byte[] CheckSum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 10)]
            public char[] data;
            public Byte MessageID;
            public Byte Mode;
            public Byte NoofLines;
            public Byte LineId;

            public Byte FontType;
            public Byte EffectType;
            public Byte EffectSpeed;
            public Byte TextLength;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 250)]
            public char[] Text;

            public Byte FontType1;
            public Byte EffectType1;
            public Byte EffectSpeed1;
            public Byte TextLength1;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 250)]
            public char[] Text1;
        }

              public Byte DisplayId;
              public Byte CommandID;
              public Byte MessageID;
              public Byte Mode;
              public Byte NoofLines;
              public Byte LineId;
              public Byte FontType;
              public Byte EffectType;
              public Byte EffectSpeed;
              public Byte FontType1;
              public Byte EffectType1;
              public Byte EffectSpeed1;

              public string Text;
              public string Text1;
  

        _Packet Packet;
        Utils Util = new Utils();
        public LedDisplay()
        {
            Packet = new _Packet();
            Packet.DisplayId = 1;
            Packet.CommandID = 0;
            Packet.CheckSum = new byte[2];
            Packet.CheckSum[0] = 0;
            Packet.CheckSum[1] = 0;
            // char[] data = "DETECT_DEVICE".ToArray();
            Packet.data = "WRITE_DATA".ToArray();
            //   Marshal.StructureToPtr(record1, Packet, true);
            Packet.Text = new char[250];
            Packet.Text1 = new char[250];

            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            ushort[] tmp = new ushort[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            for (int i = 0; i < len; i++)
                tmp[i] = buffer[i];
            ushort cs = Util.CalculateChecksumShort(tmp);
            Packet.CheckSum[0] = (byte)cs;
            Packet.CheckSum[1] = (byte)(cs >> 8);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            Marshal.FreeHGlobal(ptr);
        }

        private void UpdatePacket()
        {
           
            Packet.CommandID = 0x00;
            Packet.DisplayId = 1;
            Packet.CheckSum[0] = 0;
            Packet.CheckSum[1] = 0;
            Packet.data = "WRITE_DATA".ToArray();
            Packet.MessageID = MessageID;
            Packet.Mode = Mode;
            if (Packet.Mode == 1)
            {
                Packet.NoofLines = NoofLines;
                Packet.LineId = LineId;
                Packet.FontType = FontType;
                Packet.EffectType = EffectType;
                Packet.EffectSpeed = EffectSpeed;
                Packet.TextLength = (byte)Text.Length;
                char[] array = Text.ToCharArray();


                for (int i = 0; i < array.Length; i++)
                {

                    Packet.Text[i] = array[i];
                }
            }
            else if (Packet.Mode == 2)
            {
        
                Packet.NoofLines = NoofLines;
                Packet.LineId = LineId;
                Packet.FontType = FontType;
                Packet.EffectType = EffectType;
                Packet.EffectSpeed = EffectSpeed;
                Packet.FontType1 = FontType1;
                Packet.EffectType1 = EffectType1;
                Packet.EffectSpeed1 = EffectSpeed1;

                Packet.TextLength = (byte)Text.Length;
                Packet.TextLength1 = (byte)Text1.Length;

                char[] array = Text.ToCharArray();
                char[] array1 = Text1.ToCharArray();

                for (int i = 0; i < array.Length; i++)
                {

                    Packet.Text[i] = array[i];
                }

                for (int i = 0; i < array1.Length; i++)
                {

                    Packet.Text1[i] = array1[i];
                }

            }

            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            ushort[] tmp = new ushort[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            for (int i = 0; i < len; i++)
                tmp[i] = buffer[i];
            ushort cs = Util.CalculateChecksumShort(tmp);
            Packet.CheckSum[0] = (byte)cs;
            Packet.CheckSum[1] = (byte)(cs >> 8);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            Marshal.FreeHGlobal(ptr);


        }

        public byte[] ToByteArray()
        {
            UpdatePacket();
            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            len = Size();
            Marshal.Copy(ptr, buffer, 0, len);
            byte[] NewBuffer = new byte[len];
            Array.Copy(buffer, NewBuffer, len);

            return NewBuffer;
        }


        public int Size()
        {
            UpdatePacket();
            int size =Marshal.SizeOf(Packet);
            size= size-250+Text.Length;
            return size;
        }
    }
}
