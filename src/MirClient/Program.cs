using System.Reflection;
using System.Runtime.InteropServices;

namespace MirClient
{
    static public class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string dllToLoad);
    }


    internal static class Program
    {
        public static GameFrm Form;
        const string DxResName = "MirClient.Resources.D3DX9_43.dll";

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            LoadUnmanagedLibraryFromResource(Assembly.GetExecutingAssembly(), DxResName, "D3DX9_43.dll");
            Settings.UseTestConfig = true;
            Application.Run(Form = new GameFrm());
        }

        public static string LoadUnmanagedLibraryFromResource(Assembly assembly, string libraryResourceName,
            string libraryName)
        {
            string tempDllPath = string.Empty;

            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(libraryResourceName))
            {
                byte[] data = new BinaryReader(s).ReadBytes((int)s.Length);

                string assemblyPath = Path.GetDirectoryName(assembly.Location);
                tempDllPath = Path.Combine(assemblyPath, libraryName);
                File.WriteAllBytes(tempDllPath, data);
            }

            NativeMethods.LoadLibrary(libraryName);
            return tempDllPath;
        }
    }
}