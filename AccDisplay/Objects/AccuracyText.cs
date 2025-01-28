/*using System;
using AccDisplay.Utils;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using GameUtils = AccDisplay.Utils.GameUtils;

namespace AccDisplay.Objects;

public class AccuracyText : MonoBehaviour {
    private static GameObject _accuracyText;
    private static GameObject _accuracyText2;
        
    private readonly Color _pink = new(1f, 0.64f, 0.93f, 1f);
    private readonly Color _pinkDarker = new(1f, 0.22f, 0.85f, 1f);
    
    private bool _init;

    private void Start() {
        UserInterfaceUtils.LoadFonts();
    }

    private void Update() {
        if (!GameUtils.Playing)
            return; // dont do anything yet
        
        if (!_init)
            Init();
        
        if (_accuracyText == null)
            return;
            
        // _accuracyText.GetComponent<Text>().text = AccDisplayMod.Accuracy.ToString("F2") + "%";
        // _accuracyText.GetComponent<Text>().text = AccDisplayMod.Baller;
        // _accuracyText.GetComponent<Text>().text = $"P{GameUtils.PerfectCount} G{GameUtils.GreatCount} N{GameUtils.NoteCount} H{GameUtils.HeartCount} J{GameUtils.JumpOverCount} M{GameUtils.MissCount}";
    }

    private void Init()
    {
        var uiOther = GameObject.Find("PnlBattleOthers");
        var scores = uiOther.FindByName("Score");
        var canvas = UserInterfaceUtils.CreateCanvas("AccuracyLayer");
        
        var stage = GetStage(scores);
        MelonLogger.Msg($"StageType: {stage}");
        
        _accuracyText = UserInterfaceUtils.CreateText("AccuracyPercent", stage, "100%", 0, _pink, true, _pinkDarker);
        canvas.AddChild(_accuracyText);

        if (AccDisplayMod.DisplayAccText)
        {
            _accuracyText2 = UserInterfaceUtils.CreateText("AccuracyText", stage, AccDisplayMod.CustomText, 70, _pinkDarker);
            canvas.AddChild(_accuracyText2);
        }

        _init = true;
    }

    private StageType GetStage(GameObject scores)
    {
        var groove = scores.FindByName("GC");
        var djmax = scores.FindByName("Djmax");
        
        if (djmax?.activeSelf ?? false)
            return StageType.DjMax;
        
        if (groove?.activeSelf ?? false)
            return StageType.GrooveCoaster;
        
        return StageType.Default;
    }

    public static void Remove() {
        UserInterfaceUtils.UnloadFonts();
        Destroy(_accuracyText);
        Destroy(_accuracyText2);
        _accuracyText = null;
        _accuracyText2 = null;
    }
}*/