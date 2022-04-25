namespace MirClient
{
    internal static class Program
    {
        public static GameFrm Form;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Settings.UseTestConfig = true;
            Application.Run(Form = new GameFrm());
        }
    }
}