using HarmonyLib;
using Il2CppAssets.Scripts.GameCore.HostComponent;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppFormulaBase;
using Il2CppGameLogic;
using GameUtils = AccDisplay.Utils.GameUtils;

namespace AccDisplay.Patches;

public static class MissPatches {
    [HarmonyPatch(typeof(GameMissPlay), "MissCube")]
    internal static class MissCubePatch
    {
        private static void Postfix(int idx, decimal currentTick)
        {
            int lane = Singleton<BattleEnemyManager>.instance.GetPlayResult(idx);
            var noteData = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
            var noteType = noteData.noteData.type;
            var isDouble = noteData.isDouble;

            switch (lane)
            {
                // air
                case 0:
                    switch (noteType)
                    {
                        case 4:
                            GameUtils.GhostMissCount++;
                            break;

                        case 6 or 7:
                            GameUtils.CollectableMissCount++;
                            break;

                        default:
                        if (noteType != 2 && !isDouble)
                                GameUtils.NormalMissCount++;
                        break;
                    }

                    break;

                // ground
                case 1:
                    switch (noteType)
                    {
                        case 4:
                            GameUtils.GhostMissCount++;
                            break;

                        case 6 or 7:
                            GameUtils.CollectableMissCount++;
                            break;

                        default:
                            GameUtils.NormalMissCount++;
                            break;
                    }

                    break;
            }
        }
    }
    
    [HarmonyPatch(typeof(BattleEnemyManager), "SetPlayResult")]
    internal static class SetPlayResultPatch
    {
        private static void Postfix(int idx, byte result, bool isMulStart = false, bool isMulEnd = false, bool isLeft = false)
        {
            var noteData = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
            var noteType = noteData.noteData.type;

            AccDisplayMod.Baller = $"T{noteType} R{result}";
            
            switch (result) {
                case 4 when noteType == 2:
                    GameUtils.JumpOverCount++;
                    break;
                case 1 when noteType == 3:
                    GameUtils.NormalMissCount++;
                    break;
            }
        }
    }
}