using System;
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
        static IntPtr baseAddress;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(int hProcess,
            int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        Process wow = Process.GetProcessesByName("WoW")[0]; // Get WoW Process Object
        WoWObject localPlayer = new WoWObject();

        public void init()
        {
            baseAddress = wow.MainModule.BaseAddress;
        }

        public ObjManager()
        {            
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            init();
            textBox1.Text = "0x" + baseAddress.ToString("X");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
