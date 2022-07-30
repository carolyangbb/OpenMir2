using System;
using SystemModule;

namespace GameSvr
{
    public class ItemUnitSystem
    {
        public byte GetUpgrade(int count, int ran)
        {
            byte result = 0;
            for (var i = 0; i < count; i++)
            {
                if (new System.Random(ran).Next() == 0)
                {
                    result = (byte)(result + 1);
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        public int GetUpgrade2(int x, int a)
        {
            int iProb;
            int result = 0;
            for (var i = x; i >= 1; i--)
            {
                if (i > x / 2)
                {
                    iProb = Convert.ToInt32(Math.Sqrt(Math.Pow(a, 2.0) - Math.Pow(i, 2.0)) / (a * i + Math.Pow(i, 2.0)) * 100);
                }
                else
                {
                    iProb = Convert.ToInt32(Math.Sqrt(1 - (Math.Pow(i, 2.0) / Math.Pow(a, 2.0))) * 100 / Math.Sqrt(i));
                }
                if (new System.Random(100).Next() < iProb)
                {
                    result = i / 3;
                    break;
                }
            }
            return result;
        }

        // TUserItem의 Desc의 업그레이드 맵 0:DC 1:MC 2:SC
        public void UpgradeRandomWeapon(TUserItem pu)
        {
            int n;
            int incp;
            int up = GetUpgrade(12, 15);
            if (new System.Random(15).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            up = GetUpgrade(12, 15);
            if (new System.Random(20).Next() == 0)
            {
                incp = (1 + up) / 3;
                if (incp > 0)
                {
                    if (new System.Random(3).Next() != 0)
                    {
                        pu.Desc[6] = (byte)incp;
                    }
                    else
                    {
                        pu.Desc[6] = (byte)(10 + incp);
                    }
                }
            }
            up = GetUpgrade(12, 15);
            if (new System.Random(15).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            up = GetUpgrade(12, 15);
            if (new System.Random(15).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            up = GetUpgrade(12, 15);
            if (new System.Random(24).Next() == 0)
            {
                pu.Desc[5] = (byte)(1 + (up / 2));
            }
            up = GetUpgrade(12, 12);
            if (new System.Random(3).Next() < 2)
            {
                n = (1 + up) * 2000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
            up = GetUpgrade(12, 15);
            if (new System.Random(10).Next() == 0)
            {
                pu.Desc[7] = (byte)(1 + (up / 2));
            }
        }

        // 무기를 랜덤하게 업그레이드 한다.
        public void UpgradeRandomDress(TUserItem pu)
        {
            int n;
            int up;
            // 방어
            up = GetUpgrade(6, 15);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            // AC
            // 마항
            up = GetUpgrade(6, 15);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            // MAC
            // 파괴
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            // DC
            // 마법
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            // MC
            // 도력
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            // SC
            // 내구
            up = GetUpgrade(6, 10);
            if (new System.Random(8).Next() < 6)
            {
                n = (1 + up) * 2000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        // 옷을 랜덤하게 업그레이드함.
        public void UpgradeRandomNecklace(TUserItem pu)
        {
            int n;
            int up;
            // 정확
            up = GetUpgrade(6, 30);
            if (new System.Random(60).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            // AC(HIT)
            // 민첩
            up = GetUpgrade(6, 30);
            if (new System.Random(60).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            // MAC(SPEED)
            // 파괴
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            // DC
            // 마법
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            // MC
            // 도력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            // SC
            // 내구
            up = GetUpgrade(6, 12);
            if (new System.Random(20).Next() < 15)
            {
                // 내구
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        public void UpgradeRandomBarcelet(TUserItem pu)
        {
            int n;
            int up;
            // 방어
            up = GetUpgrade(6, 20);
            if (new System.Random(20).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            // AC
            // 마항
            up = GetUpgrade(6, 20);
            if (new System.Random(20).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            // MAC
            // 파괴
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            // DC
            // 마법
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            // MC
            // 도력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            // SC
            // 내구
            up = GetUpgrade(6, 12);
            if (new System.Random(20).Next() < 15)
            {
                // 내구
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        public void UpgradeRandomNecklace19(TUserItem pu)
        {
            int n;
            int up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            up = GetUpgrade(6, 10);
            if (new System.Random(4).Next() < 3)
            {
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        public void UpgradeRandomRings(TUserItem pu)
        {
            int n;
            int up;
            // 파괴
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            // DC
            // 마력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            // MC
            // 도력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            // SC
            // 내구
            up = GetUpgrade(6, 12);
            if (new System.Random(4).Next() < 3)
            {
                // 내구성 업그레이드
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        public void UpgradeRandomRings23(TUserItem pu)
        {
            int n;
            int up;
            // 중독저항
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            // 중독저항
            // 중독회복
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            // 중독회복
            // 파괴
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            // DC
            // 마력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            // MC
            // 도력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            // SC
            // 내구
            up = GetUpgrade(6, 12);
            if (new System.Random(4).Next() < 3)
            {
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        public void UpgradeRandomHelmet(TUserItem pu)
        {
            int n;
            int up;
            // 방어
            up = GetUpgrade(6, 20);
            if (new System.Random(40).Next() == 0)
            {
                pu.Desc[0] = (byte)(1 + up);
            }
            // AC
            // 마항
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[1] = (byte)(1 + up);
            }
            // MAC
            // 파괴
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[2] = (byte)(1 + up);
            }
            // DC
            // 마법
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[3] = (byte)(1 + up);
            }
            // MC
            // 도력
            up = GetUpgrade(6, 20);
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[4] = (byte)(1 + up);
            }
            // SC
            // 내구
            up = GetUpgrade(6, 12);
            if (new System.Random(4).Next() < 3)
            {
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
        }

        // -------------------------------------------------------------
        // 미지의 아이템 (투구)
        // 투구
        public void RandomSetUnknownHelmet(TUserItem pu)
        {
            int n;
            int sum;
            byte up = (byte)(GetUpgrade(4, 3) + GetUpgrade(4, 8) + GetUpgrade(4, 20));
            if (up > 0)
            {
                pu.Desc[0] = up;
            }
            // AC
            sum = up;
            // 마항
            up = (byte)(GetUpgrade(4, 3) + GetUpgrade(4, 8) + GetUpgrade(4, 20));
            if (up > 0)
            {
                pu.Desc[1] = up;
            }
            // MAC
            sum = sum + up;
            // 파괴
            up = (byte)(GetUpgrade(3, 15) + GetUpgrade(3, 30));
            if (up > 0)
            {
                pu.Desc[2] = up;
            }
            // DC
            sum = sum + up;
            // 마법
            up = (byte)(GetUpgrade(3, 15) + GetUpgrade(3, 30));
            if (up > 0)
            {
                pu.Desc[3] = up;
            }
            // MC
            sum = sum + up;
            // 도력
            up = (byte)(GetUpgrade(3, 15) + GetUpgrade(3, 30));
            if (up > 0)
            {
                pu.Desc[4] = up;
            }
            // SC
            sum = sum + up;
            // 내구
            up = GetUpgrade(6, 30);
            if (up > 0)
            {
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
            // 떨어지지 않는 아이템
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[7] = 1;
            }
            // 떨어지지 않는 속성
            pu.Desc[8] = 1;
            // 미지의 속성
            // 착용 필요치가 붙음
            if (sum >= 3)
            {
                if (pu.Desc[0] >= 5)
                {
                    // 방어가 큼
                    pu.Desc[5] = 1;
                    // 필파
                    pu.Desc[6] = (byte)(25 + pu.Desc[0] * 3);
                    return;
                }
                if (pu.Desc[2] >= 2)
                {
                    // 파괴가 큼
                    pu.Desc[5] = 1;
                    // 필파
                    pu.Desc[6] = (byte)(35 + pu.Desc[2] * 4);
                    return;
                }
                if (pu.Desc[3] >= 2)
                {
                    // 마력 큼
                    pu.Desc[5] = 2;
                    // 필마
                    pu.Desc[6] = (byte)(18 + pu.Desc[3] * 2);
                    return;
                }
                if (pu.Desc[4] >= 2)
                {
                    // 도력 큼
                    pu.Desc[5] = 3;
                    // 필도
                    pu.Desc[6] = (byte)(18 + pu.Desc[4] * 2);
                    return;
                }
                pu.Desc[6] = (byte)(18 + sum * 2);
            }
        }

        // 미지의 아이템 (반지)
        public void RandomSetUnknownRing(TUserItem pu)
        {
            int n;
            int up;
            int sum;
            // 파괴
            up = GetUpgrade(3, 4) + GetUpgrade(3, 8) + GetUpgrade(6, 20);
            if (up > 0)
            {
                pu.Desc[2] = (byte)up;
            }
            // DC
            sum = up;
            // 마력
            up = GetUpgrade(3, 4) + GetUpgrade(3, 8) + GetUpgrade(6, 20);
            if (up > 0)
            {
                pu.Desc[3] = (byte)up;
            }
            // MC
            sum = sum + up;
            // 도력
            up = GetUpgrade(3, 4) + GetUpgrade(3, 8) + GetUpgrade(6, 20);
            if (up > 0)
            {
                pu.Desc[4] = (byte)up;
            }
            // SC
            sum = sum + up;
            // 내구
            up = GetUpgrade(6, 30);
            if (up > 0)
            {
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[7] = 1;
            }
            pu.Desc[8] = 1;
            if (sum >= 3)
            {
                if (pu.Desc[2] >= 3)
                {
                    pu.Desc[5] = 1;
                    pu.Desc[6] = (byte)(25 + pu.Desc[2] * 3);
                    return;
                }
                if (pu.Desc[3] >= 3)
                {
                    pu.Desc[5] = 2;
                    pu.Desc[6] = (byte)(18 + pu.Desc[3] * 2);
                    return;
                }
                if (pu.Desc[4] >= 3)
                {
                    pu.Desc[5] = 3;
                    pu.Desc[6] = (byte)(18 + pu.Desc[4] * 2);
                    return;
                }
                pu.Desc[6] = (byte)(18 + sum * 2);
            }
        }

        public void RandomSetUnknownBracelet(TUserItem pu)
        {
            int n;
            int sum;
            byte up = (byte)(GetUpgrade(3, 5) + GetUpgrade(5, 20));
            if (up > 0)
            {
                pu.Desc[0] = up;
            }
            sum = up;
            up = (byte)(GetUpgrade(3, 5) + GetUpgrade(5, 20));
            if (up > 0)
            {
                pu.Desc[1] = up;
            }
            sum = sum + up;
            up = (byte)(GetUpgrade(3, 15) + GetUpgrade(5, 30));
            if (up > 0)
            {
                pu.Desc[2] = up;
            }
            sum = sum + up;
            up = (byte)(GetUpgrade(3, 15) + GetUpgrade(5, 30));
            if (up > 0)
            {
                pu.Desc[3] = up;
            }
            sum = sum + up;
            up = (byte)(GetUpgrade(3, 15) + GetUpgrade(5, 30));
            if (up > 0)
            {
                pu.Desc[4] = up;
            }
            sum = sum + up;
            up = GetUpgrade(6, 30);
            if (up > 0)
            {
                n = (1 + up) * 1000;
                pu.DuraMax = (ushort)HUtil32._MIN(65000, pu.DuraMax + n);
                pu.Dura = (ushort)HUtil32._MIN(65000, pu.Dura + n);
            }
            if (new System.Random(30).Next() == 0)
            {
                pu.Desc[7] = 1;
            }
            pu.Desc[8] = 1;
            if (sum >= 2)
            {
                if (pu.Desc[0] >= 3)
                {
                    pu.Desc[5] = 1;
                    pu.Desc[6] = (byte)(25 + pu.Desc[0] * 3);
                    return;
                }
                if (pu.Desc[2] >= 2)
                {
                    pu.Desc[5] = 1;
                    pu.Desc[6] = (byte)(30 + pu.Desc[2] * 3);
                    return;
                }
                if (pu.Desc[3] >= 2)
                {
                    pu.Desc[5] = 2;
                    pu.Desc[6] = (byte)(20 + pu.Desc[3] * 2);
                    return;
                }
                if (pu.Desc[4] >= 2)
                {
                    pu.Desc[5] = 3;
                    pu.Desc[6] = (byte)(20 + pu.Desc[4] * 2);
                    return;
                }
                pu.Desc[6] = (byte)(18 + sum * 2);
            }
        }

        public int GetUpgradeStdItem(TUserItem ui, ref TStdItem std)
        {
            int result;
            int UCount;
            UCount = 0;
            switch (std.StdMode)
            {
                case 5:
                case 6:
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[0]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[1]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[2]));
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC) + ui.Desc[3], HUtil32.HiByte(std.AC) + ui.Desc[5]);
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC) + ui.Desc[4], HUtil32.HiByte(std.MAC));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), GetAttackSpeed(HUtil32.HiByte(std.MAC), ui.Desc[6]));
                    if (ui.Desc[7] >= 1 && ui.Desc[7] <= 10)
                    {
                        if (std.SpecialPwr >= 0)
                        {
                            std.SpecialPwr = ui.Desc[7];
                        }
                    }
                    if (ui.Desc[10] != 0)
                    {
                        std.ItemDesc = (byte)(std.ItemDesc | Grobal2.IDC_UNIDENTIFIED);
                    }
                    std.Slowdown = (byte)(std.Slowdown + ui.Desc[12]);
                    std.Tox = (byte)(std.Tox + ui.Desc[13]);
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[5] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[6] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[7] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[12] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[13] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 10:
                case 11:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[2]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[3]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[4]));
                    std.Agility = (byte)(std.Agility + ui.Desc[11]);
                    std.MgAvoid = (byte)(std.MgAvoid + ui.Desc[12]);
                    std.ToxAvoid = (byte)(std.ToxAvoid + ui.Desc[13]);
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[11] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[12] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[13] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 15:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[2]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[3]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[4]));
                    std.Accurate = (byte)(std.Accurate + ui.Desc[11]);
                    std.MgAvoid = (byte)(std.MgAvoid + ui.Desc[12]);
                    std.ToxAvoid = (byte)(std.ToxAvoid + ui.Desc[13]);
                    if (ui.Desc[5] > 0)
                    {
                        std.Need = ui.Desc[5];
                    }
                    if (ui.Desc[6] > 0)
                    {
                        std.NeedLevel = ui.Desc[6];
                    }
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[11] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[12] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[13] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 19:
                case 20:
                case 21:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[2]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[3]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[4]));
                    std.AtkSpd = (byte)(std.AtkSpd + ui.Desc[9]);
                    std.Slowdown = (byte)(std.Slowdown + ui.Desc[12]);
                    std.Tox = (byte)(std.Tox + ui.Desc[13]);
                    if (std.StdMode == 19)
                    {
                        std.Accurate = (byte)(std.Accurate + ui.Desc[11]);
                    }
                    else if (std.StdMode == 20)
                    {
                        std.MgAvoid = (byte)(std.MgAvoid + ui.Desc[11]);
                    }
                    else if (std.StdMode == 21)
                    {
                        std.Accurate = (byte)(std.Accurate + ui.Desc[11]);
                        std.MgAvoid = (byte)(std.MgAvoid + ui.Desc[7]);
                    }
                    if (ui.Desc[5] > 0)
                    {
                        // 필요(렙,파괴,마법,도력)
                        std.Need = ui.Desc[5];
                    }
                    if (ui.Desc[6] > 0)
                    {
                        std.NeedLevel = ui.Desc[6];
                    }
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[9] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[11] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[12] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[13] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 22:
                case 23:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[2]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[3]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[4]));
                    std.AtkSpd = (byte)(std.AtkSpd + ui.Desc[9]);
                    std.Slowdown = (byte)(std.Slowdown + ui.Desc[12]);
                    std.Tox = (byte)(std.Tox + ui.Desc[13]);
                    if (ui.Desc[5] > 0)
                    {
                        std.Need = ui.Desc[5];
                    }
                    if (ui.Desc[6] > 0)
                    {
                        std.NeedLevel = ui.Desc[6];
                    }
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[9] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[12] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[13] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 24:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[2]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[3]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[4]));
                    if (ui.Desc[5] > 0)
                    {
                        std.Need = ui.Desc[5];
                    }
                    if (ui.Desc[6] > 0)
                    {
                        std.NeedLevel = ui.Desc[6];
                    }
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 26:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.DC = HUtil32.MakeWord(HUtil32.LoByte(std.DC), HUtil32._MIN(255, HUtil32.HiByte(std.DC) + ui.Desc[2]));
                    std.MC = HUtil32.MakeWord(HUtil32.LoByte(std.MC), HUtil32._MIN(255, HUtil32.HiByte(std.MC) + ui.Desc[3]));
                    std.SC = HUtil32.MakeWord(HUtil32.LoByte(std.SC), HUtil32._MIN(255, HUtil32.HiByte(std.SC) + ui.Desc[4]));
                    std.Accurate = (byte)(std.Accurate + ui.Desc[11]);
                    std.Agility = (byte)(std.Agility + ui.Desc[12]);
                    if (ui.Desc[5] > 0)
                    {
                        std.Need = ui.Desc[5];
                    }
                    if (ui.Desc[6] > 0)
                    {
                        std.NeedLevel = ui.Desc[6];
                    }
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[4] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[11] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[12] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 52:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.Agility = (byte)(std.Agility + ui.Desc[3]);
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    break;
                case 54:
                    std.AC = HUtil32.MakeWord(HUtil32.LoByte(std.AC), HUtil32._MIN(255, HUtil32.HiByte(std.AC) + ui.Desc[0]));
                    std.MAC = HUtil32.MakeWord(HUtil32.LoByte(std.MAC), HUtil32._MIN(255, HUtil32.HiByte(std.MAC) + ui.Desc[1]));
                    std.Accurate = (byte)(std.Accurate + ui.Desc[2]);
                    std.Agility = (byte)(std.Agility + ui.Desc[3]);
                    std.ToxAvoid = (byte)(std.ToxAvoid + ui.Desc[13]);
                    if (ui.Desc[0] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[1] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[2] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[3] > 0)
                    {
                        UCount++;
                    }
                    if (ui.Desc[13] > 0)
                    {
                        UCount++;
                    }
                    break;
            }
            result = UCount;
            return result;
        }

        // 공속 변환 함수
        // 양수 공속값을 실제 공속(-10~15)값으로 변환해주는 함수.
        public int RealAttackSpeed(short wAtkSpd)
        {
            int result;
            if (wAtkSpd <= 10)
            {
                result = -wAtkSpd;
            }
            else
            {
                result = wAtkSpd - 10;
            }
            return result;
        }

        // -10~15의 값을 갖는 실제 공속
        // 실제 공속(-10~15)값을 양수 공속값으로 변환해주는 함수.
        public ushort NaturalAttackSpeed(int iAtkSpd)
        {
            ushort result;
            if (iAtkSpd <= 0)
            {
                result = (ushort)-iAtkSpd;
            }
            else
            {
                result = (ushort)(iAtkSpd + 10);
            }
            return result;
        }

        public byte GetAttackSpeed(byte bStdAtkSpd, byte bUserAtkSpd)
        {
            int iTemp = RealAttackSpeed(bStdAtkSpd) + RealAttackSpeed(bUserAtkSpd);
            byte result = (byte)NaturalAttackSpeed(iTemp);
            return result;
        }

        public byte UpgradeAttackSpeed(byte bUserAtkSpd, int iUpValue)
        {
            int iTemp = RealAttackSpeed(bUserAtkSpd) + iUpValue;
            byte result = (byte)NaturalAttackSpeed(iTemp);
            return result;
        }
    }
}
