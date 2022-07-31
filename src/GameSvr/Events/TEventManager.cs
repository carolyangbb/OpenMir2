using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TEventManager
    {
        public ArrayList EventList = null;
        public ArrayList ClosedList = null;

        public TEventManager()
        {
            EventList = new ArrayList();
            ClosedList = new ArrayList();
        }

        public void AddEvent(TEvent __event)
        {
            EventList.Add(__event);
        }

        public TEvent FindEvent(TEnvirnoment penvir, int x, int y, int evtype)
        {
            TEvent result;
            int i;
            TEvent __event;
            result = null;
            for (i = 0; i < EventList.Count; i++)
            {
                __event = EventList[i] as TEvent;
                if ((__event.PEnvir == penvir) && (__event.X == x) && (__event.Y == y) && (__event.EventType == evtype))
                {
                    result = __event;
                    break;
                }
            }
            return result;
        }

        public void Run()
        {
            int i;
            TEvent __event;
            i = 0;
            try
            {
                while (true)
                {
                    if (i >= EventList.Count)
                    {
                        break;
                    }
                    __event = EventList[i] as TEvent;
                    if (__event.Active && (HUtil32.GetTickCount() - __event.runstart > __event.runtick))
                    {
                        __event.runstart = HUtil32.GetTickCount();
                        __event.Run();
                        if (__event.Closed)
                        {
                            ClosedList.Add(__event);
                            EventList.RemoveAt(i);
                        }
                        else
                        {
                            i++;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("Except:TEventManager.Run[1]");
            }
            try
            {
                for (i = 0; i < ClosedList.Count; i++)
                {
                    if (HUtil32.GetTickCount() - (ClosedList[i] as TEvent).CloseTime > 5 * 60 * 1000)
                    {
                        try
                        {
                            (ClosedList[i] as TEvent).Free();
                        }
                        finally
                        {
                            ClosedList.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
            catch
            {
                M2Share.MainOutMessage("Except:TEventManager.Run[2]");
            }
        }
    }
}

