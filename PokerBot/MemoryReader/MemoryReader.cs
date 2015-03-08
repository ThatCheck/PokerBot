using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HandHistories.Objects.GameDescription;
using HandHistories.Objects.Hand;
using HandHistories.Parser.Parsers.Factory;
using HandHistories.Parser.Parsers.Base;
using PokerBot.Entity.Event;

namespace PokerBot.MemoryReader
{
    public class MemoryReader
    {
        // REQUIRED CONSTS

        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x00001000;
        const int PAGE_READWRITE = 0x04;
        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_OPERATION = 0x0008;
        const int MIN_PAGE_SIZE = 100;
        const int MEM_PRIVATE = 0x20000;
        // REQUIRED METHODS

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress,[Out] byte[] lpBuffer,int dwSize,out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // REQUIRED STRUCTS

        [StructLayout(LayoutKind.Sequential)]
        public struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool OpenProcessToken(IntPtr ProcessHandle,
            UInt32 DesiredAccess, out IntPtr TokenHandle);

        private static uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        private static uint STANDARD_RIGHTS_READ = 0x00020000;
        private static uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        private static uint TOKEN_DUPLICATE = 0x0002;
        private static uint TOKEN_IMPERSONATE = 0x0004;
        private static uint TOKEN_QUERY = 0x0008;
        private static uint TOKEN_QUERY_SOURCE = 0x0010;
        private static uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        private static uint TOKEN_ADJUST_GROUPS = 0x0040;
        private static uint TOKEN_ADJUST_DEFAULT = 0x0080;
        private static uint TOKEN_ADJUST_SESSIONID = 0x0100;
        private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        private static uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool LookupPrivilegeValue(string lpSystemName, string lpName,
            out LUID lpLuid);

        public const string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";

        public const string SE_AUDIT_NAME = "SeAuditPrivilege";

        public const string SE_BACKUP_NAME = "SeBackupPrivilege";

        public const string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";

        public const string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";

        public const string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";

        public const string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";

        public const string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";

        public const string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";

        public const string SE_DEBUG_NAME = "SeDebugPrivilege";

        public const string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";

        public const string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";

        public const string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";

        public const string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";

        public const string SE_INC_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";

        public const string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";

        public const string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";

        public const string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";

        public const string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";

        public const string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";

        public const string SE_RELABEL_NAME = "SeRelabelPrivilege";

        public const string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";

        public const string SE_RESTORE_NAME = "SeRestorePrivilege";

        public const string SE_SECURITY_NAME = "SeSecurityPrivilege";

        public const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        public const string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";

        public const string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";

        public const string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";

        public const string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";

        public const string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";

        public const string SE_TCB_NAME = "SeTcbPrivilege";

        public const string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";

        public const string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";

        public const string SE_UNDOCK_NAME = "SeUndockPrivilege";

        public const string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public UInt32 LowPart;
            public Int32 HighPart;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hHandle);

        public const UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        public const UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        public const UInt32 SE_PRIVILEGE_REMOVED = 0x00000004;
        public const UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            public LUID Luid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        // Use this signature if you do not want the previous state
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AdjustTokenPrivileges(IntPtr TokenHandle,
           [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
           ref TOKEN_PRIVILEGES NewState,
           UInt32 Zero,
           IntPtr Null1,
           IntPtr Null2);

        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        /** Event */

        public delegate void DecodeEndMemoryHandler(object sender, DecodedHandEventArgs data);
        public event DecodeEndMemoryHandler DecodeEndMemory;

        public delegate void DecodeErrorMemoryHandler(object sender, DecodedErrorHandEventArgs data);
        public event DecodeErrorMemoryHandler DecodeErrorEndMemory;

        /**
         * Property
         */

        private IntPtr _hwnd;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private IntPtr _processHandle;
        private long _proc_min_address_l;
        private long _proc_max_address_l;
        private IntPtr _proc_min_address;
        private IntPtr _proc_max_address;
        private IHandHistoryParser _parser;
        public static Regex _gameStartRegex = new Regex(@"#Game No :", RegexOptions.Compiled);

        public IntPtr Hwnd
        {
            get { return _hwnd; }
            set { _hwnd = value; }
        }

        public MemoryReader(IntPtr hwnd)
        {
            this._hwnd = hwnd;
            IHandHistoryParserFactory handHistoryParserFactory = new HandHistoryParserFactoryImpl();
            this._parser = handHistoryParserFactory.GetFullHandHistoryParser(SiteName.PartyPokerFr);
            

            IntPtr hToken;
            LUID luidSEDebugNameValue;
            TOKEN_PRIVILEGES tkpPrivileges;

            if (!OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out hToken))
            {
                _logger.Error("OpenProcessToken() failed, error = {0} . SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
                return;
            }
            else
            {
                _logger.Info("OpenProcessToken() successfully");
            }

            if (!LookupPrivilegeValue(null, SE_DEBUG_NAME, out luidSEDebugNameValue))
            {
                _logger.Error("LookupPrivilegeValue() failed, error = {0} .SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
                CloseHandle(hToken);
                return;
            }
            else
            {
                _logger.Info("LookupPrivilegeValue() successfully");
            }

            tkpPrivileges.PrivilegeCount = 1;
            tkpPrivileges.Luid = luidSEDebugNameValue;
            tkpPrivileges.Attributes = SE_PRIVILEGE_ENABLED;

            if (!AdjustTokenPrivileges(hToken, false, ref tkpPrivileges, 0, IntPtr.Zero, IntPtr.Zero))
            {
                _logger.Error("LookupPrivilegeValue() failed, error = {0} .SeDebugPrivilege is not available", Marshal.GetLastWin32Error());
            }
            else
            {
                _logger.Info("SeDebugPrivilege is now available");
            }
            CloseHandle(hToken);

            
        }

        public void analyze()
        {
            /*_logger.Info("Start memory dump");
            var stopwatch = new Stopwatch();
            stopwatch.Start();*/
            IntPtr bytesRead = IntPtr.Zero;  // number of bytes read with ReadProcessMemory
            long savedLongAdress = this._proc_min_address_l;
            IntPtr savedLongAdressPtr = this._proc_min_address;
            Dictionary<String,KeyValuePair<long,String>> tableDictionnary = new Dictionary<String,KeyValuePair<long,String>>();
            SYSTEM_INFO sys_info = new SYSTEM_INFO();
            GetSystemInfo(out sys_info);

            this._proc_min_address = sys_info.minimumApplicationAddress;
            this._proc_max_address = sys_info.maximumApplicationAddress;

            // saving the values as long ints so I won't have to do a lot of casts later
            this._proc_min_address_l = this._proc_min_address.ToInt64();
            this._proc_max_address_l = this._proc_max_address.ToInt64();

            uint processID = 0;
            int threadID = (int)GetWindowThreadProcessId(this._hwnd, out processID);
            this._processHandle = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_WM_READ | PROCESS_VM_OPERATION, false, (int)processID);
            if (this._processHandle == IntPtr.Zero)
            {
                _logger.Error("OpenProcess() failed, error = {0} .OpenProcess is not available", Marshal.GetLastWin32Error());
            }

            while (this._proc_min_address_l < this._proc_max_address_l)
            {
                MEMORY_BASIC_INFORMATION mem_basic_info = new MEMORY_BASIC_INFORMATION();

                if (VirtualQueryEx(this._processHandle, this._proc_min_address, out mem_basic_info, (uint)Marshal.SizeOf(mem_basic_info)) == 0)
                {
                    _logger.Error("VirtualQueryEx() failed, error = {0}.", Marshal.GetLastWin32Error());
                }

                if (mem_basic_info.Type == MEM_PRIVATE && mem_basic_info.AllocationProtect == PAGE_READWRITE && mem_basic_info.RegionSize.ToInt32() >= MIN_PAGE_SIZE && mem_basic_info.Protect == PAGE_READWRITE && mem_basic_info.State == MEM_COMMIT)
                {
                    byte[] buffer = new byte[mem_basic_info.RegionSize.ToInt32()];

                    // read everything in the buffer above
                    ReadProcessMemory(this._processHandle, mem_basic_info.BaseAddress, buffer, mem_basic_info.RegionSize.ToInt32(), out bytesRead);

                    if (bytesRead.ToInt64() >= 15000)
                    {
                        string result = System.Text.Encoding.Unicode.GetString(buffer);
                        String[] splittedResult = result.Split(new[] { "\n\0" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (String split in splittedResult)
                        {
                            if (split.Contains("#Game No :") && !split.Contains("Table Closed") && !split.Contains("wins") && split.Contains("posts") && split.Contains("big blind"))
                            {
                                String[] toAnalyzeList = _gameStartRegex.Split(split);
                                foreach (var toAnalyze in toAnalyzeList)
                                {
                                    if (!toAnalyze.Any(c => (c > 255 && c != '€')) && toAnalyze.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).Count() > 8)
                                    {
                                        //Reformat the correct string 
                                        String formattedNewString = "#Game No :" + toAnalyze;
                                        try
                                        {
                                            string tableName = this._parser.ParseTableName(formattedNewString);
                                            long numberHand = this._parser.ParseHandId(formattedNewString);
                                            if (tableDictionnary.ContainsKey(tableName))
                                            {
                                                if (tableDictionnary[tableName].Key < numberHand)
                                                {
                                                    tableDictionnary[tableName] = new KeyValuePair<long, string>(numberHand, formattedNewString);
                                                }
                                            }
                                            else
                                            {
                                                tableDictionnary.Add(tableName, new KeyValuePair<long, string>(numberHand, formattedNewString));
                                            }
                                        }
                                        catch(Exception ex)
                                        {
                                            _logger.Error("Unable parse hand ID or Table Name" + formattedNewString + " \n Error : " + ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                // move to the next memory chunk
                this._proc_min_address_l += mem_basic_info.RegionSize.ToInt64();
                this._proc_min_address = new IntPtr(this._proc_min_address_l);
            }
            foreach(var table in tableDictionnary)
            {
                try
                {
                    HandHistory handHistory = this._parser.ParseFullHandHistory(table.Value.Value, true);
                    OnDecodeEndMemory(this, new DecodedHandEventArgs(handHistory));
                }
                catch (Exception ex)
                {
                    OnDecodeErrorMemory(this,new DecodedErrorHandEventArgs(table.Value.Value,ex));
                    _logger.Error("Unable to load hand with text" + table.Value.Value + " \n Error : " + ex.Message);
                }
            }
            this._proc_min_address = savedLongAdressPtr;
            this._proc_min_address_l = savedLongAdress;

            /*stopwatch.Stop();
            _logger.Info("Method #1 Total seconds: {0}", stopwatch.Elapsed.TotalMilliseconds);
            _logger.Info("End of memory dump");*/
        }


        protected void OnDecodeEndMemory(object sender, DecodedHandEventArgs args)
        {
            if (DecodeEndMemory != null)
            {
                DecodeEndMemory(this, args);
            }
        }

        protected void OnDecodeErrorMemory(object sender, DecodedErrorHandEventArgs args)
        {
            if (DecodeErrorEndMemory != null)
            {
                DecodeErrorEndMemory(this, args);
            }
        }
    }
}
