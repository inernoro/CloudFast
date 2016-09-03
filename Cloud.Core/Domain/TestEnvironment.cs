using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace Cloud.Domain
{
   
    public class TestEnvironment
    {
        public TestEnvironment()
        {
        }

        public TestEnvironment(bool boo)
        {
            CommandLine = Environment.CommandLine;
            CurrentDirectory = Environment.CurrentDirectory;
            CurrentManagedThreadId = Environment.CurrentManagedThreadId;
            ExitCode = Environment.ExitCode;
            HasShutdownStarted = Environment.HasShutdownStarted;
            Is64BitOperatingSystem = Environment.Is64BitOperatingSystem;
            Is64BitProcess = Environment.Is64BitProcess;
            MachineName = Environment.MachineName;
            NewLine = Environment.NewLine;
            OsVersion = Environment.OSVersion.ToString();
            WorkingSet = Environment.WorkingSet;
            UserName = Environment.UserName;
            UserInteractive = Environment.UserInteractive;
            UserDomainName = Environment.UserDomainName;
            TickCount = Environment.TickCount;
            ProcessorCount = Environment.ProcessorCount;
            SystemPageSize = Environment.SystemPageSize;
            SystemDirectory = Environment.SystemDirectory;
        }

        public string CommandLine { set; get; }
        public string CurrentDirectory { set; get; }
        public int CurrentManagedThreadId { set; get; }
        public int ExitCode { set; get; }
        public bool HasShutdownStarted { set; get; }
        public bool Is64BitOperatingSystem { set; get; }
        public bool Is64BitProcess { set; get; }
        public string MachineName { set; get; }
        public string NewLine { set; get; }
        public string OsVersion { set; get; }
        public long WorkingSet { set; get; }
        public string UserName { set; get; }
        public bool UserInteractive { set; get; }
        public string UserDomainName { set; get; }
        public int TickCount { set; get; }
        public int ProcessorCount { set; get; }
        public int SystemPageSize { set; get; }
        public string SystemDirectory { set; get; }
    }
}