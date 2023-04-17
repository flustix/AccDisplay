using System;
using Assets.Scripts.GameCore.HostComponent;
using Assets.Scripts.PeroTools.Commons;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace AccDisplay {
    public class AccDisplayMod : MelonMod {
        public static float Accuracy { get; private set; }
        
        /// <summary>
        /// Misses that aren't tracked by _task.m_MissResult
        /// </summary>
        public static int ExtraMisses { get; set; }

        private TaskStageTarget _task;
        
        // calculation stuff
        private const float GreatFactor = 0.5f;

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            if (sceneName == "GameMain")
            {
                _task = Singleton<TaskStageTarget>.instance;
                ClassInjector.RegisterTypeInIl2Cpp<AccuracyText>();
                new GameObject("AccuracyDisplay").AddComponent<AccuracyText>();
            } else {
                AccuracyText.Remove();
                ExtraMisses = 0;
                _task = null;
            }
        }

        public override void OnUpdate() {
            base.OnUpdate();
            
            if (_task == null) return;
            
            var perfect = _task.m_PerfectResult;
            var pass = _task.m_JumpOverResult;
            var note = _task.m_MusicCount;
            var heart = _task.m_Blood;
            var great = _task.m_GreatResult;
            var miss = _task.m_MissResult + ExtraMisses;

            var total = perfect + pass + note + heart + great + miss;
            
            if (total == 0) {
                Accuracy = 100;
            } else {
                var counted = perfect + pass + note + heart + great * GreatFactor;
                // lerp to smooth out the accuracy
                Accuracy = Mathf.Lerp(Accuracy, counted / total * 100, 0.1f);
            }
        }
    }
}