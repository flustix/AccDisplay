using Assets.Scripts.GameCore.HostComponent;
using Assets.Scripts.PeroTools.Commons;
using FormulaBase;
using GameLogic;
using HarmonyLib;

namespace AccDisplay {
    [HarmonyPatch(typeof(GameMissPlay), "MissCube")]
    internal static class MissCubePatch
    {
        private static void Postfix(int idx, decimal currentTick)
        {
            // credits to lxy for all of this
            // i removed some stuff that i didn't need and reformatted it
            
            int result = Singleton<BattleEnemyManager>.instance.GetPlayResult(idx);
            MusicData musicDataByIdx = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
            
            if (result == 0)
            {
                // air ghost miss
                if (musicDataByIdx.noteData.type == 4)
                    AccDisplayMod.ExtraMisses++;
                // air collectable note miss
                else if (musicDataByIdx.noteData.type == 7)
                    AccDisplayMod.ExtraMisses++;
                // normal miss
                else if (musicDataByIdx.noteData.type != 2 && !musicDataByIdx.isDouble)
                    AccDisplayMod.ExtraMisses++;
            }
            if (result == 1)
            {
                // ground ghost miss
                if (musicDataByIdx.noteData.type == 4)
                    AccDisplayMod.ExtraMisses++;
                // ground collectable note miss
                else if (musicDataByIdx.noteData.type == 7)
                    AccDisplayMod.ExtraMisses++;
                // normal miss
                else
                    AccDisplayMod.ExtraMisses++;
            }
        }
    }
}