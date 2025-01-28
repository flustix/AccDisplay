using System.Collections.Generic;

namespace AccDisplay.Scoring;

public class ScoreInfo
{
    public Dictionary<int, byte> Results { get; } = new();
    
    public int TotalCount
    {
        get
        {
            var count = NormalPerfectCount;
            count += NormalGreatCount;
            count += NormalHitCount;
            count += NormalMissCount;
            
            count += GearDodgeCount;
            count += GearHitCount;

            count += SheetPerfectCount;
            count += SheetGreatCount;
            count += SheetMissCount;
            
            count += GhostPerfectCount;
            count += GhostGreatCount;
            count += GhostMissCount;
            
            count += BossPerfectCount;
            count += BossGreatCount;
            count += BossHitCount;
            
            count += HeartCollectCount;
            count += HeartMissCount;
            
            count += NoteCollectCount;
            count += NoteMissCount;
            
            count += MasherPerfectCount;
            count += MasherGreatCount;
            count += MasherHitCount;
            return count;
        }
    }

    public int TotalPerfectCount => NormalPerfectCount + SheetPerfectCount + GhostPerfectCount + BossPerfectCount + MasherPerfectCount;
    public int TotalGreatCount => NormalGreatCount + SheetGreatCount + GhostGreatCount + BossGreatCount + MasherGreatCount;

    public int NormalPerfectCount { get; set; }
    public int NormalGreatCount { get; set; }
    public int NormalHitCount { get; set; }
    public int NormalMissCount { get; set; }
    
    public int GearDodgeCount { get; set; }
    public int GearHitCount { get; set; }
    
    public int SheetPerfectCount { get; set; }
    public int SheetGreatCount { get; set; }
    public int SheetTickCount { get; set; }
    public int SheetMissCount { get; set; }
    
    public int GhostPerfectCount { get; set; }
    public int GhostGreatCount { get; set; }
    public int GhostMissCount { get; set; }
    
    public int BossPerfectCount { get; set; }
    public int BossGreatCount { get; set; }
    public int BossHitCount { get; set; }
    
    public int HeartCollectCount { get; set; }
    public int HeartMissCount { get; set; }
    
    public int NoteCollectCount { get; set; }
    public int NoteMissCount { get; set; }
    
    public int MasherPerfectCount { get; set; }
    public int MasherGreatCount { get; set; }
    public int MasherTickCount { get; set; }
    public int MasherHitCount { get; set; }
}