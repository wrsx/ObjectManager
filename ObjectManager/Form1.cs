﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; // Required for Process objects
using System.Runtime.InteropServices; // DLL SetLastError?


namespace ObjectManager
{
    public partial class ObjManager : Form
    {
        static uint baseAddress;
        static uint clientConnection;
        uint firstObject;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        Process wow;
        WoWObject localPlayer = new WoWObject();

        public void init()
        {      
            wow = Process.GetProcessesByName("WoW")[0]; // Get WoW Process Object
            baseAddress = (uint)wow.MainModule.BaseAddress;
            clientConnection = readUInt(wow.Handle, (baseAddress + (uint)Pointers.ObjectManager.CurMgrPointer));
            firstObject = readUInt(wow.Handle, (clientConnection + (uint)Pointers.ObjectManager.FirstObject));
            localPlayer.Guid = readUInt(wow.Handle, baseAddress + (uint)Pointers.StaticPointers.LocalPlayerGUID);
            localPlayer.BaseAddress = GetObjectBaseByGuid(localPlayer.Guid);
            localPlayer.UnitFieldsAddress = readUInt(wow.Handle, localPlayer.BaseAddress + (uint)Pointers.WoWObject.DataPTR);
            localPlayer.MaxHealth = readUInt(wow.Handle, localPlayer.UnitFieldsAddress + (uint)Descriptors.WoWUnitFields.Health * 4);
        }

        public ObjManager()
        {            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            textBox1.Text = "0x" + baseAddress.ToString("X");

			dataGridView1.Rows[0].Cells[0].Value = localPlayer.MaxHealth.ToString(); //guid
			dataGridView1.Rows[0].Cells[1].Value = "n/a"; //obj name 
			dataGridView1.Rows[0].Cells[2].Value = "0x" + localPlayer.BaseAddress.ToString("X"); //obj base
			dataGridView1.Rows[0].Cells[3].Value = "n/a"; //type
			dataGridView1.Rows[0].Cells[4].Value = "n/a"; //x
			dataGridView1.Rows[0].Cells[5].Value = "n/a"; //y
			dataGridView1.Rows[0].Cells[6].Value = "n/a"; //z


			textBox2.Text = "0x" + clientConnection.ToString("X");
			//textBox3.Text = "0x" + firstObject.ToString("X");
			textBox4.Text = localPlayer.Guid.ToString();
            textBox6.Text = localPlayer.MaxHealth.ToString();
        }

        private uint GetObjectBaseByGuid(ulong Guid)
        {
            WoWObject TempObject = new WoWObject();
            TempObject.BaseAddress = firstObject;
            while (TempObject.BaseAddress != 0)
            {
                try
                {
                    TempObject.Guid = readUInt(wow.Handle, TempObject.BaseAddress + (uint)Pointers.WoWObject.Guid);
                    if (TempObject.Guid == Guid)
                    {
                        return TempObject.BaseAddress;
                    }
                    TempObject.BaseAddress = readUInt(wow.Handle, TempObject.BaseAddress + (uint)Pointers.ObjectManager.NextObject);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            return 0;
        }
        
        byte[] ReadMemory(IntPtr handle, uint address, int size)
        {
            byte[] dataBuffer = new byte[size];
            IntPtr bytesRead = IntPtr.Zero;
            ReadProcessMemory(handle, (IntPtr)address, dataBuffer, dataBuffer.Length, out bytesRead);
            if (bytesRead.ToInt32() != size)
            {
                return null;
            }
            return dataBuffer;
        }

        private uint readUInt(IntPtr handle, uint address)
        {
            byte[] res = ReadMemory(handle, address, sizeof(uint));
            if (res == null)
            {
                return 0;
            }
            return BitConverter.ToUInt32(res, 0);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

		private void label6_Click(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}
	}
}
