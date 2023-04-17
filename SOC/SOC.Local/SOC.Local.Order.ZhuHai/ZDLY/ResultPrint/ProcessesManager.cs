using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDLY.ResultPrint
{
    /// <summary>
    /// ���̹�����
    /// create by lijp 2011-04-25
    /// </summary>
    public partial class ProcessesManager
    {

        /// <summary>
        /// ���캯��
        /// </summary>
        public ProcessesManager()
        {
            //
            // Do Nothing.
            //
        }

        /// <summary>
        /// �г����пɷ��ʽ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public Process[] ListProcesses()
        {
            Process[] processList = null;
            try
            {
                processList = Process.GetProcesses();
                return processList;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///  ���ҽ���
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public Process FindProcesses(string processName)
        {
            if (processName.Length > 0)
            {
                Process[] arrP = Process.GetProcesses();
                foreach (Process p in arrP)
                {
                    try
                    {
                        if (p.ProcessName.ToLower() == processName.ToLower())
                        {
                            return p;
                        }
                    }
                    catch
                    { 
                        //
                        // Do Nothing.
                        //
                    }
                }

                return null;
            }
            return null;
        }

        /// <summary>
        /// ɱ������
        /// </summary>
        /// <param name="processName"></param>
        /// <returns></returns>
        public int KillProcess(string processName)
        {
            if (processName.Length > 0)
            {
                int pid = -1;
                Process[] arrP = Process.GetProcesses();
                foreach (Process p in arrP)
                {
                    try
                    {
                        if (p.ProcessName.ToLower() == processName.ToLower())
                        {
                            p.Kill();
                            break;
                        }
                    }
                    catch 
                    {
                        p.Kill();
                    }
                }
                return 0;
            }
            return -1;
        }


        /// <summary>   
        /// ����DOS����   
        /// DOS�رս�������(ntsd -c q -p PID )PIDΪ���̵�ID   
        /// </summary>   
        /// <param name="command"></param>   
        /// <returns></returns>   
        public string RunCmd(string command)
        {

            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + command;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;

            p.Start();

            return p.StandardOutput.ReadToEnd();

        }

        /// <summary>
        /// ShellExecute API����
        /// add by lijp 2011-04-25
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="lpOperation"></param>
        /// <param name="lpFile"></param>
        /// <param name="lpParameters"></param>
        /// <param name="lpDirectory"></param>
        /// <param name="nShowCmd"></param>
        /// <returns></returns>
        [DllImport("shell32.dll")]
        public extern static IntPtr ShellExecute(IntPtr hwnd,
                                                 string lpOperation,
                                                 string lpFile,
                                                 string lpParameters,
                                                 string lpDirectory,
                                                 int nShowCmd
                                                );
        /// <summary>
        /// ������ʾ��ʽö��
        /// add by lijp 2011-04-25
        /// </summary>
        public enum ShowWindowCommands : int
        {

            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }

        /// <summary>
        /// �������Ƿ��Ѿ���װ
        /// </summary>
        /// <param name="fileDisplayName"></param>
        /// <returns></returns>
        private bool CheckSoftwareInstalled(string fileDisplayName)   
        {   
            Microsoft.Win32.RegistryKey uninstallNode = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");   
            foreach (string subKeyName in uninstallNode.GetSubKeyNames())   
            {   
                Microsoft.Win32.RegistryKey subKey = uninstallNode.OpenSubKey(subKeyName);   
                object displayName = subKey.GetValue("DisplayName");   
                if (displayName != null)   
                {
                    if (displayName.ToString().Contains(fileDisplayName))   
                    {   
                        return true;   
                    }   
                }   
            }   
            return false;   
        }

        /// <summary>
        /// ���ð�װ����
        /// </summary>
        /// <param name="fullFilePath"></param>
        private void InvokeInstall(string fullFilePath)
        {
            try
            {
                
            }
            catch
            { }
        }

    }
}
