using System;
using UnityEngine;
using UnityEngine.UI;
using MuseDashMirror.UICreate;
using static MuseDashMirror.BattleComponent;

namespace AccDisplay {
    public class AccuracyText : MonoBehaviour {
        private static GameObject _accuracyText;
        private static GameObject _accuracyText2;
        
        private readonly Color _pink = new Color(1f, 0.64f, 0.93f, 1f);
        private readonly Color _pinkDarker = new Color(1f, 0.22f, 0.85f, 1f);

        public AccuracyText(IntPtr intPtr) : base(intPtr)
        {
        }

        private void Start() {
            Fonts.LoadFonts();
        }

        private void Update() {
            if (_accuracyText == null && IsInGame) {
                CanvasCreate.CreateCanvas("Accuracy Canvas", "Camera_2D");
                _accuracyText = Utils.CreateText("Accuracy", "100%", 0, _pink, true, _pinkDarker);
                
                if (AccDisplayMod.DisplayAccText)
                    _accuracyText2 = Utils.CreateText("Accuracy2", AccDisplayMod.CustomText, 70, _pinkDarker);
            }
            
            if (_accuracyText == null) return;
            
            _accuracyText.GetComponent<Text>().text = AccDisplayMod.Accuracy.ToString("F2") + "%";
        }
        
        public static void Remove() {
            Fonts.UnloadFonts();
            Destroy(_accuracyText);
            Destroy(_accuracyText2);
            _accuracyText = null;
            _accuracyText2 = null;
        }
    }
}