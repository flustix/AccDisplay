using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;

namespace AccDisplay.Utils;

public static class GameUtils {
    private static StageBattleComponent _stage;
    private static TaskStageTarget _task;
    
    public static bool Playing => _stage?.isInGame ?? false;
    
    // normal judgements
    public static int PerfectCount => _task?.m_PerfectResult ?? 0;
    public static int GreatCount => _task?.m_GreatResult ?? 0;
    public static int NoteCount => _task?.m_MusicCount ?? 0;
    public static int HeartCount => _task?.m_Blood ?? 0;
    public static int MissCount => NormalMissCount + GhostMissCount + CollectableMissCount;
    
    // from patches
    public static int JumpOverCount { get; internal set; }
    public static int NormalMissCount { get; internal set; }
    public static int GhostMissCount { get; internal set; }
    public static int CollectableMissCount { get; internal set; }

    public static float Accuracy {
        get {
            var total = PerfectCount + JumpOverCount + NoteCount + HeartCount + GreatCount + MissCount;
            
            if (total == 0)
                return 100;

            var counted = PerfectCount + JumpOverCount + NoteCount + HeartCount + GreatCount * .5f;
            return counted / total * 100;
        }
    }

    internal static void Reload() {
        _stage = Singleton<StageBattleComponent>.instance;
        _task = Singleton<TaskStageTarget>.instance;
    }
    
    internal static void Reset() {
        _stage = null;
        _task = null;
        
        JumpOverCount = 0;
        NormalMissCount = 0;
        GhostMissCount = 0;
        CollectableMissCount = 0;
    }
}