using SystemModule;

namespace GameSvr
{
    public class TEvent
    {
        public int EventId;
        public bool Visible
        {
            get
            {
                return FVisible;
            }
        }
        public int Check = 0;
        public TEnvirnoment PEnvir = null;
        public int X = 0;
        public int Y = 0;
        public int EventType = 0;
        public int EventParam = 0;
        public long OpenStartTime = 0;
        public long ContinueTime = 0;
        public long CloseTime = 0;
        public bool Closed = false;
        public int Damage = 0;
        public TCreature OwnCret = null;
        public long runstart = 0;
        public long runtick = 0;
        public bool IsAddToMap = false;
        protected bool FVisible = false;
        protected bool Active = false;

        public TEvent(TEnvirnoment penv, int ax, int ay, int etype, int etime, bool bovisible)
        {
            OpenStartTime  =  HUtil32.GetTickCount();
            EventType = etype;
            EventParam = 0;
            ContinueTime = etime;
            FVisible = bovisible;
            Closed = false;
            PEnvir = penv;
            X = ax;
            Y = ay;
            Active = true;
            Damage = 0;
            OwnCret = null;
            runstart  =  HUtil32.GetTickCount();
            runtick = 500;
            AddToMap();
        }
 
        ~TEvent()
        {
 
        }
        public virtual void AddToMap()
        {
            IsAddToMap = false;
            if ((PEnvir != null) && FVisible)
            {
                if (null != PEnvir.AddToMap(X, Y, Grobal2.OS_EVENTOBJECT, this))
                {
                    IsAddToMap = true;
                }
            }
            else
            {
                FVisible = false;
            }
        }

        public void Close()
        {
            CloseTime  =  HUtil32.GetTickCount();
            if (FVisible)
            {
                FVisible = false;
                if (PEnvir != null)
                {
                    PEnvir.DeleteFromMap(X, Y, Grobal2.OS_EVENTOBJECT, this);
                }
                PEnvir = null;
            }
        }

        public virtual void Run()
        {
            if (HUtil32.GetTickCount() - OpenStartTime > ContinueTime)
            {
                Closed = true;
                Close();
            }
        }

        public long GetTickCount => System.Environment.TickCount;
    }

}

