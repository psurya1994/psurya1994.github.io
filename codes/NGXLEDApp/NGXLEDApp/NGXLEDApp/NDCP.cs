using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;

namespace NGXLEDApp
{
    public class Utils
    {
         public ushort CalculateChecksum(byte[] byteToCalculate)
        {
            ushort checksum = 0;
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            checksum &= 0xFFff;
            return checksum;
        }

         public ushort CalculateChecksumShort(ushort[] byteToCalculate)
         {
             ushort checksum = 0;
             foreach (ushort chData in byteToCalculate)
             {
                 checksum += chData;
             }
            // checksum &= 0xFFff;
             return checksum;
         }

      
    }

    public class DeviceConnectPacket
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _Packet
        {
            public Byte DisplayId;
            public Byte CommandID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
            public Byte[] CheckSum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 13)]
            public  char[] data;
        }
        _Packet Packet;
        Utils Util=new Utils();
        public DeviceConnectPacket()
        {
         Packet=new _Packet();
         Packet.DisplayId = 0;
         Packet.CommandID = 0x83;
         Packet.CheckSum = new byte[2];
         Packet.CheckSum[0] = 0;
         Packet.CheckSum[1] = 0;
        // char[] data = "DETECT_DEVICE".ToArray();
         Packet.data = "DETECT_DEVICE".ToArray();
      //   Marshal.StructureToPtr(record1, Packet, true);

         int len = Marshal.SizeOf(typeof(_Packet));
         byte[] buffer = new byte[len];
         IntPtr ptr = Marshal.AllocHGlobal(len);
         Marshal.StructureToPtr(Packet, ptr, true);
         Marshal.Copy(ptr, buffer, 0, len);
         ushort cs = Util.CalculateChecksum(buffer);
        Packet.CheckSum[1] = (byte)cs;
        Packet.CheckSum[0] = (byte)(cs >> 8);
        Marshal.StructureToPtr(Packet, ptr, true);
        Marshal.Copy(ptr, buffer, 0, len);
        Marshal.FreeHGlobal(ptr);
        }
        public byte[] ToByteArray()
        {
            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            return buffer;
        }

       
        public int Size()
        {
           return Marshal.SizeOf(Packet);
        }

    }
    public class DeviceFormatPacket
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _Packet
        {
            public Byte DisplayId;
            public Byte CommandID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
            public Byte[] CheckSum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6)]
            public char[] data;
        }
        _Packet Packet;
        Utils Util = new Utils();
        public DeviceFormatPacket()
        {
            Packet = new _Packet();
            Packet.DisplayId = 0;
            Packet.CommandID = 0x82;
            Packet.CheckSum = new byte[2];
            Packet.CheckSum[0] = 0;
            Packet.CheckSum[1] = 0;
            // char[] data = "DETECT_DEVICE".ToArray();
            Packet.data = "FORMAT".ToArray();
            //   Marshal.StructureToPtr(record1, Packet, true);

            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            ushort cs = Util.CalculateChecksum(buffer);
            Packet.CheckSum[1] = (byte)cs;
            Packet.CheckSum[0] = (byte)(cs >> 8);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            Marshal.FreeHGlobal(ptr);
        }

        public byte[] ToByteArray()
        {
            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            return buffer;
        }


        public int Size()
        {
            return Marshal.SizeOf(Packet);
        }

    }
    public class DeviceWriteDataPacket
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct _Packet
        {
            public Byte DisplayId;
            public Byte CommandID;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
            public Byte[] CheckSum;
            [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9)]
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
        }

        public Byte DisplayId;
        public Byte CommandID;
    //    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
    //    public Byte[] CheckSum;
    //    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 9)]
    //    public char[] data;
        public Byte MessageID;
        public Byte Mode;
        public Byte NoofLines;
        public Byte LineId;
        public Byte FontType;
        public Byte EffectType;
        public Byte EffectSpeed;
      //  public Byte TextLength;
        public string Text;


        _Packet Packet;
        Utils Util = new Utils();
        public DeviceWriteDataPacket()
        {
            Packet = new _Packet();
            Packet.DisplayId = 0;
            Packet.CommandID = 0x83;
            Packet.CheckSum = new byte[2];
            Packet.CheckSum[0] = 0;
            Packet.CheckSum[1] = 0;
            // char[] data = "DETECT_DEVICE".ToArray();
            Packet.data = "DETECT_DEVICE".ToArray();
            //   Marshal.StructureToPtr(record1, Packet, true);
            Packet.Text = new char[250];


            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            ushort cs = Util.CalculateChecksum(buffer);
            Packet.CheckSum[0] = (byte)cs;
            Packet.CheckSum[1] = (byte)(cs >> 8);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            Marshal.FreeHGlobal(ptr);
        }

        private void UpdatePacket()
        {
           
            Packet.CommandID = 0x00;
            Packet.DisplayId = DisplayId;
            Packet.data = "DETECT_DEVICE".ToArray();
            Packet.MessageID =MessageID;
            Packet.Mode = Mode;
            Packet.NoofLines = NoofLines;
            Packet.LineId = LineId;
            Packet.FontType = FontType;
            Packet.EffectType = EffectType;
            Packet.EffectType = EffectType;
            Packet.TextLength = (byte)Text.Length;
            char[] array = Text.ToCharArray();

            // Loop through array.
            for (int i = 0; i < array.Length; i++)
            {
                // Get character from array.
                Packet.Text[i] = array[i];
                // Display each letter.
              
            }
            //Packet.Text = Text.ToCharArray();

            int len = Marshal.SizeOf(typeof(_Packet));
            byte[] buffer = new byte[len];
            IntPtr ptr = Marshal.AllocHGlobal(len);
            Marshal.StructureToPtr(Packet, ptr, true);
            Marshal.Copy(ptr, buffer, 0, len);
            ushort cs = Util.CalculateChecksum(buffer);
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


        private int Size()
        {
            UpdatePacket();
            int size =Marshal.SizeOf(Packet);
            size= size-250+Text.Length;
            return size;
        }

    }

    class NDCP
    {
    }
}
