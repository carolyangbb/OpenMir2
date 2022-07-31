using System.Collections;
using System.IO;

namespace GameSvr
{
    public class TMission
    {
        public string MissionName = string.Empty;
        public bool BoPlay = false;

        public TMission(string mission)
        {
            string flname;
            BoPlay = false;
            MissionName = mission;
            flname = Mission.MISSIONBASE + mission + ".txt";
            if (File.Exists(flname))
            {
                if (LoadMissionFile(flname))
                {
                    BoPlay = true;
                }
            }
        }

        private bool LoadMissionFile(string flname)
        {
            bool result;
            ArrayList strlist;
            strlist = new ArrayList();
            //strlist.LoadFromFile(flname);
            //strlist.Free();
            result = true;
            return result;
        }

        public void Run()
        {
        }

    }
}

namespace GameSvr
{
    public class Mission
    {
        public const string MISSIONBASE = ".\\MissionBase\\";
    }
}

