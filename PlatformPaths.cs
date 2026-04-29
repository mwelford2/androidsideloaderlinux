using System;
using System.IO;
using System.Runtime.InteropServices;

namespace AndroidSideloader
{
    internal static class PlatformPaths
    {
        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static string AdbExecutableName => IsWindows ? "adb.exe" : "adb";
        public static string RcloneExecutableName => IsWindows ? "rclone.exe" : "rclone";
        public static string PlatformToolsDir => Path.Combine(Environment.CurrentDirectory, "platform-tools");
        public static string RcloneDir => Path.Combine(Environment.CurrentDirectory, "rclone");

        public static string AdbPath => Path.Combine(PlatformToolsDir, AdbExecutableName);
        public static string RclonePath => Path.Combine(RcloneDir, RcloneExecutableName);

        public static string NormalizeExecutablePath(string path)
        {
            if (IsWindows)
            {
                return path;
            }

            try
            {
                if (File.Exists(path))
                {
                    return path;
                }

                var noExe = path.EndsWith(".exe", StringComparison.OrdinalIgnoreCase)
                    ? path.Substring(0, path.Length - 4)
                    : path;

                if (File.Exists(noExe))
                {
                    return noExe;
                }

                return path;
            }
            catch
            {
                return path;
            }
        }
    }
}
