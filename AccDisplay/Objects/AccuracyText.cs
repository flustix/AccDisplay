using System;
using AccDisplay.Utils;
using UnityEngine;
using UnityEngine.UI;
using GameUtils = AccDisplay.Utils.GameUtils;

namespace AccDisplay.Objects;

public class AccuracyText : MonoBehaviour {
    private static GameObject _accuracyText;
    private static GameObject _accuracyText2;
        
    private readonly Color _pink = new(1f, 0.64f, 0.93f, 1f);
    private readonly Color _pinkDarker = new(1f, 0.22f, 0.85f, 1f);

    public AccuracyText(IntPtr intPtr) : base(intPtr)
    {
    }

    private void Start() {
        UserInterfaceUtils.LoadFonts();
    }

    private void Update() {
        if (_accuracyText == null && GameUtils.Playing) {
            UserInterfaceUtils.CreateCanvas("Accuracy Canvas", "Camera_2D");
            _accuracyText = UserInterfaceUtils.CreateText("Accuracy", "100%", 0, _pink, true, _pinkDarker);
                
            if (AccDisplayMod.DisplayAccText)
                _accuracyText2 = UserInterfaceUtils.CreateText("Accuracy2", AccDisplayMod.CustomText, 70, _pinkDarker);
        }
            
        if (_accuracyText == null) return;
            
        _accuracyText.GetComponent<Text>().text = AccDisplayMod.Accuracy.ToString("F2") + "%";
        // _accuracyText.GetComponent<Text>().text = AccDisplayMod.Baller;
        // _accuracyText.GetComponent<Text>().text = $"P{GameUtils.PerfectCount} G{GameUtils.GreatCount} N{GameUtils.NoteCount} H{GameUtils.HeartCount} J{GameUtils.JumpOverCount} M{GameUtils.MissCount}";
    }
        
    public static void Remove() {
        UserInterfaceUtils.UnloadFonts();
        Destroy(_accuracyText);
        Destroy(_accuracyText2);
        _accuracyText = null;
        _accuracyText2 = null;
    }
}