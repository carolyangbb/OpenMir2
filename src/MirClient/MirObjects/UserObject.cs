using MirClient.MirScenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirClient.MirObjects
{
    public class UserObject : PlayerObject
    {
        public uint Id;
        public int HP, MP;
        public int AttackSpeed;
        public int CurrentHandWeight,
                      CurrentWearWeight,
                      CurrentBagWeight;
        public long Experience, MaxExperience;
        public bool TradeLocked;
        public uint TradeGoldAmount;
        public bool AllowTrade;
        public bool RentalGoldLocked;
        public bool RentalItemLocked;
        public uint RentalGoldAmount;
        public SpecialItemMode ItemMode;
        public int BeltIdx = 6, HeroBeltIdx = 2;
        public bool HasExpandedStorage = false;
        public DateTime ExpandedStorageExpiryTime;
        public bool CreatureSummoned;
        public int PearlCount = 0;
        public List<int> CompletedQuests = new List<int>();
        public bool Slaying, Thrusting, HalfMoon, CrossHalfMoon, DoubleSlash, TwinDrakeBlade, FlamingSword;
        public Point NextMagicLocation;
        public MapObject NextMagicObject;
        public MirDirection NextMagicDirection;
        public QueuedAction QueuedAction;

        public UserObject() { }

        public UserObject(uint objectID) : base(objectID)
        {
            
        }

        public override void SetLibraries()
        {
            base.SetLibraries();
        }

        public override void SetEffects()
        {
            base.SetEffects();
        }

        public void RefreshStats()
        {
            RefreshLevelStats();
            RefreshBagWeight();
            RefreshEquipmentStats();
            RefreshItemSetStats();
            RefreshMirSetStats();
            RefreshSkills();
            SetLibraries();
            SetEffects();
            if (this == User && Light < 3) Light = 3;
            if (AttackSpeed < 550) AttackSpeed = 550;
            GameScene.Scene.Redraw();
        }

        private void RefreshLevelStats()
        {
            Light = 0;
        }

        private void RefreshBagWeight()
        {
            CurrentBagWeight = 0;
        }

        private void RefreshEquipmentStats()
        {
          
        }

        private void RefreshItemSetStats()
        {
             
        }

        private void RefreshMirSetStats()
        {
            
        }

        private void RefreshSkills()
        {
            
        }
         
        public override void SetAction()
        {
            if (QueuedAction != null)
            {
                if ((ActionFeed.Count == 0) || (ActionFeed.Count == 1 && NextAction.Action == MirAction.Stance))
                {
                    ActionFeed.Clear();
                    ActionFeed.Add(QueuedAction);
                    QueuedAction = null;
                }
            }

            base.SetAction();
        }

        public override void ProcessFrames()
        {
            bool clear = GameFrm.Time >= NextMotion;

            base.ProcessFrames();

            if (clear) QueuedAction = null;
            if ((CurrentAction == MirAction.Standing || CurrentAction == MirAction.MountStanding || CurrentAction == MirAction.Stance || CurrentAction == MirAction.Stance2 || CurrentAction == MirAction.DashFail) && (QueuedAction != null || NextAction != null))
                SetAction();
        }

        public void ClearMagic()
        {
 
        }
    }

}
