using System;
using SystemModule;

namespace GameSvr
{
    public class TCastleDoor : TGuardUnit
    {
        public long BrokenTime = 0;
        // 何辑柳 矫埃
        public bool BoOpenState = false;
        // 巩牢版快 凯妨脸乐绰瘤
        // --------------------------------------------------------------
        // 己巩, 己寒
        //Constructor  Create()
        public TCastleDoor() : base()
        {
            this.BoAnimal = false;
            this.StickMode = true;
            BoOpenState = false;
            // 摧腮 惑怕
            this.AntiPoison = 200;
            // HideMode := TRUE;  //积己 寸矫绰 救焊捞绰 葛靛烙

        }
        public override void Initialize()
        {
            this.Dir = 0;
            // 檬扁惑怕
            base.Initialize();
            if (this.WAbil.HP > 0)
            {
                if (BoOpenState)
                {
                    ActiveDoorWall(TDoorState.dsOpen);
                }
                else
                {
                    ActiveDoorWall(TDoorState.dsClose);
                }
            }
            else
            {
                ActiveDoorWall(TDoorState.dsBroken);
            }
        }

        // 货肺 绊媚咙
        public void RepairStructure()
        {
            int newdir;
            if (!BoOpenState)
            {
                newdir = 3 - HUtil32.MathRound(this.WAbil.HP / this.WAbil.MaxHP * 3);
                if (!(newdir >= 0 && newdir <= 2))
                {
                    newdir = 0;
                }
                this.Dir = (byte)newdir;
                this.SendRefMsg(Grobal2.RM_ALIVE, this.Dir, this.CX, this.CY, 0, "");
            }
        }

        // 荤合己巩牢 版快俊父 荤侩
        public void ActiveDoorWall(TDoorState dstate)
        {
            bool bomove;
            this.PEnvir.GetMarkMovement(this.CX, this.CY - 2, true);
            this.PEnvir.GetMarkMovement(this.CX + 1, this.CY - 1, true);
            this.PEnvir.GetMarkMovement(this.CX + 1, this.CY - 2, true);
            if (dstate == TDoorState.dsClose)
            {
                bomove = false;
            }
            else
            {
                bomove = true;
            }
            this.PEnvir.GetMarkMovement(this.CX, this.CY, bomove);
            this.PEnvir.GetMarkMovement(this.CX, this.CY - 1, bomove);
            this.PEnvir.GetMarkMovement(this.CX, this.CY - 2, bomove);
            this.PEnvir.GetMarkMovement(this.CX + 1, this.CY - 1, bomove);
            this.PEnvir.GetMarkMovement(this.CX + 1, this.CY - 2, bomove);
            this.PEnvir.GetMarkMovement(this.CX - 1, this.CY, bomove);
            this.PEnvir.GetMarkMovement(this.CX - 2, this.CY, bomove);
            this.PEnvir.GetMarkMovement(this.CX - 1, this.CY - 1, bomove);
            this.PEnvir.GetMarkMovement(this.CX - 1, this.CY + 1, bomove);
            if (dstate == TDoorState.dsOpen)
            {
                this.PEnvir.GetMarkMovement(this.CX, this.CY - 2, false);
                this.PEnvir.GetMarkMovement(this.CX + 1, this.CY - 1, false);
                this.PEnvir.GetMarkMovement(this.CX + 1, this.CY - 2, false);
            }
        }

        // TRUE: 捞悼啊, false:阜塞, 给框流烙
        public void OpenDoor()
        {
            if (!this.Death)
            {
                this.Dir = 7;
                // 救焊捞绰 惑怕
                this.SendRefMsg(Grobal2.RM_DIGUP, this.Dir, this.CX, this.CY, 0, "");
                BoOpenState = true;
                this.BoStoneMode = true;
                // 嘎瘤 臼澜
                ActiveDoorWall(TDoorState.dsOpen);
                // 捞悼啊瓷窍霸
                this.HoldPlace = false;
                // 磊府瞒瘤 救窃
            }
        }

        public void CloseDoor()
        {
            if (!this.Death)
            {
                this.Dir = (byte)(3 - HUtil32.MathRound(this.WAbil.HP / this.WAbil.MaxHP * 3));
                if (!(this.Dir >= 0 && this.Dir <= 2))
                {
                    this.Dir = 0;
                }
                // Dir
                this.SendRefMsg(Grobal2.RM_DIGDOWN, 0, this.CX, this.CY, 0, "");
                BoOpenState = false;
                this.BoStoneMode = false;
                // 嘎澜
                ActiveDoorWall(TDoorState.dsClose);
                // 捞悼 给窍霸
                this.HoldPlace = true;
                // 磊府瞒瘤 窃
            }
        }

        public override void Die()
        {
            base.Die();
            BrokenTime  =  HUtil32.GetTickCount();
            ActiveDoorWall(TDoorState.dsBroken);
            // 捞悼啊瓷窍霸

        }

        public override void Run()
        {
            int newdir;
            if (this.Death && (this.Castle != null))
            {
                this.DeathTime  =  HUtil32.GetTickCount();
                // 绝绢瘤瘤 臼绰促.
            }
            else
            {
                this.HealthTick = 0;
            }
            // 眉仿捞 促矫 瞒瘤 臼绰促.
            if (!BoOpenState)
            {
                newdir = 3 - HUtil32.MathRound(this.WAbil.HP / this.WAbil.MaxHP * 3);
                if ((newdir != this.Dir) && (newdir < 3))
                {
                    // 规氢 0,1,2
                    this.Dir = (byte)newdir;
                    this.SendRefMsg(Grobal2.RM_TURN, this.Dir, this.CX, this.CY, 0, "");
                }
            }
            base.Run();
        }

    }
}