using System;
using UnityEngine;
using UnityEngine.UI;
using static MuseDashMirror.BattleComponent;
using static MuseDashMirror.UICreate;

namespace AccDisplay {
    public class AccuracyText : MonoBehaviour {
        private static GameObject _accuracyText;

        public AccuracyText(IntPtr intPtr) : base(intPtr)
        {
        }

        private void Start() {
            LoadFonts();
        }

        private void Update() {
            if (_accuracyText == null && IsInGame) {
                CreateCanvas("Accuracy Canvas", "Camera_2D");
                _accuracyText = CreateText("Accuracy", "100%", new Color(1, .49f, .839f), 70);
                CreateText("Accuracy2", "ACCURACY", new Color(1, .18f, .592f), 60, 70);
            }
            
            if (_accuracyText == null) return;
            
            _accuracyText.GetComponent<Text>().text = AccDisplayMod.Accuracy.ToString("F2") + "%";
        }

        private GameObject CreateText(string id, string text, Color color, int size, int offset = 0) {
            return CreateTextGameObject("Accuracy Canvas", id, text, TextAnchor.UpperLeft, true,
                new Vector3(-400, 280 - offset, 0), new Vector2(960, 216), size, SnapsTasteFont, color);
        }
        
        public static void Remove() {
            UnloadFonts();
            Destroy(_accuracyText);
            _accuracyText = null;
        }
    }
}