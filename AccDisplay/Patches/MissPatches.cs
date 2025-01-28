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
            var score = GameUtils.Score;
            
            if (score == null)
                return;
            
            int lane = Singleton<BattleEnemyManager>.instance.GetPlayResult(idx);
            var noteData = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
            var noteType = noteData.noteData.type;
            var isDouble = noteData.isDouble;
            
            if (score.Results.ContainsKey(idx))
                return;
            
            DebugHud.Text.Add($"idx={idx}, type={noteType}, double={isDouble}, lane={lane}");

            switch (noteType)
            {
                case 1:
                    score.NormalMissCount++;
                    break;
                
                case 4:
                    score.GhostMissCount++;
                    break;
                
                case 6:
                    score.HeartMissCount++;
                    break;
                
                case 7:
                    score.NoteMissCount++;
                    break;
            }

            /*switch (lane)
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
            }*/
        }
    }
    
    [HarmonyPatch(typeof(BattleEnemyManager), "SetPlayResult")]
    internal static class SetPlayResultPatch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx">index of the note</param>
        /// <param name="result">result itself</param>
        /// <param name="isMulStart">whether this is a masher start/tick</param>
        /// <param name="isMulEnd">whether this is a masher end</param>
        /// <param name="isLeft">whether this note was hit early or not</param>
        private static void Postfix(int idx, byte result, bool isMulStart = false, bool isMulEnd = false, bool isLeft = false)
        {
            var score = GameUtils.Score;
            
            if (score == null)
                return;
            
            var musicData = Singleton<StageBattleComponent>.instance.GetMusicDataByIdx(idx);
            var noteData = musicData.noteData;
            var noteType = noteData.type;

            // var time = (int)(musicData.configData.time * 1000);
            
            // DebugHud.Text.Add($"type={noteType}, res={result}, isMulStart={isMulStart}, isMulEnd={isMulEnd}, double={musicData.isDouble}");
            DebugHud.Text.Add($"hit {idx}");
            score.Results.Add(idx, result);
            
            /*
             * results:
             *  1 - miss (in this case its "got hit by note")
             *  2 - ???
             *  3 - great (counts as hold tick when noteData.isLongPress)
             *  4 - perfect
             */
            
            /*
             * types:
             *  1 - hit / gemini
             *  2 - gear
             *  3 - sheet
             *  4 - ghosts
             *  5 - boss hit
             *  6 - heart
             *  7 - note
             *  8 - masher
             */

            switch (noteType)
            {
                case 1: // normal
                    if (result == 1)
                        score.NormalHitCount++;
                    else if (result == 3)
                        score.NormalGreatCount++;
                    else if (result == 4)
                        score.NormalPerfectCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");

                    break;
                
                case 2: // gear
                    if (result == 1)
                        score.GearHitCount++;
                    else if (result == 4)
                        score.GearDodgeCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");

                    break;
                
                case 3: // sheet
                    if (result == 1)
                        score.SheetMissCount++;
                    else if (result == 3)
                    {
                        if (musicData.isLongPressing)
                            score.SheetTickCount++;
                        else
                            score.SheetGreatCount++;
                    }
                    else if (result == 4)
                        score.SheetPerfectCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}! {musicData.isLongPressing}");

                    break;
                
                case 4: // ghosts
                    if (result == 3)
                        score.GhostGreatCount++;
                    else if (result == 4)
                        score.GhostPerfectCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");

                    break;
                
                case 5: // boss hit
                    if (result == 1)
                        score.BossHitCount++;
                    else if (result == 3)
                        score.BossGreatCount++;
                    else if (result == 4)
                        score.BossPerfectCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");

                    break;
                
                case 6: // heart
                    if (result == 4)
                        score.HeartCollectCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");
                    
                    break;
                
                case 7: // note
                    if (result == 4)
                        score.NoteCollectCount++;
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");
                    
                    break;
                
                case 8: // masher
                    if (result == 1)
                        score.MasherHitCount++;
                    else if (result == 3)
                    {
                        if (isMulEnd)
                            score.MasherGreatCount++;
                        else
                            score.MasherTickCount++;
                    }
                    else if (result == 4)
                    {
                        if (isMulEnd)
                            score.MasherPerfectCount++;
                        else
                            score.MasherTickCount++;
                    }
                    else
                        DebugHud.Text.Add($"unknown result {result} for type {noteType}!");
                    
                    break;
                
                default:
                    DebugHud.Text.Add($"unknown type {noteType}! res is {result}");
                    break;
            }
        }
    }
}