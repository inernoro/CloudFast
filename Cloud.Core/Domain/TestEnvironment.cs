using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;

namespace Cloud.Domain
{
    public static class StaticEnvironment
    {
        public static TestEnvironment TestEnvironment = new TestEnvironment();


    }
    ///  
    /// 系统信息类 - 获取CPU、内存、磁盘、进程信息 
    ///   
    public class TestEnvironment
    {
        public string CommandLine => Environment.CommandLine;
        public string CurrentDirectory => Environment.CurrentDirectory;
        public int CurrentManagedThreadId => Environment.CurrentManagedThreadId;
        public int ExitCode => Environment.ExitCode;
        public bool HasShutdownStarted => Environment.HasShutdownStarted;
        public bool Is64BitOperatingSystem => Environment.Is64BitOperatingSystem;
        public bool Is64BitProcess => Environment.Is64BitProcess;
        public string MachineName => Environment.MachineName;
        public string NewLine => Environment.NewLine;
        public OperatingSystem OsVersion => Environment.OSVersion;
        public long WorkingSet => Environment.WorkingSet;
        public string UserName => Environment.UserName;
        public bool UserInteractive => Environment.UserInteractive;
        public string UserDomainName => Environment.UserDomainName;
        public string StackTrace => Environment.StackTrace;
        public int TickCount => Environment.TickCount;
        public int ProcessorCount => Environment.ProcessorCount;
        public int SystemPageSize => Environment.SystemPageSize;
        public string SystemDirectory => Environment.SystemDirectory;
    }
}