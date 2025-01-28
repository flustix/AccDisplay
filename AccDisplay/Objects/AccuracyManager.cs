using System;
using AccDisplay.Objects.Styles;
using AccDisplay.Utils;
using UnityEngine;

namespace AccDisplay.Objects;

public class AccuracyManager : MonoBehaviour
{
    private GameObject _layer;
    private bool _initialized;
    
    public const string LayerName = "AccuracyLayer";
    
    public static float Accuracy { get; private set; }
    public static string AccuracyStr => $"{Accuracy:00.00}%".Replace(",", ".");
    
    public void Setup(GameObject layer)
    {
        _layer = layer;
        UserInterfaceUtils.LoadFonts();
    }

    private void Update()
    {
        if (!_initialized)
        {
            if (!GameUtils.Playing)
                return;
        
            Initialize();
        }

        var calculated = GameUtils.Accuracy;
        Accuracy = AccDisplayMod.SmoothAccuracy ? Mathf.Lerp(Accuracy, calculated, 0.1f) : calculated;
    }

    private void Initialize()
    {
        var stage = GetStage();
        
        switch (stage)
        {
            case StageType.DjMax:
                _layer.AddComponent<DJMaxStyle>();
                break;
            
            default:
                _layer.AddComponent<DefaultStyle>();
                break;
        }
        
        _initialized = true;
    }
    
    private StageType GetStage()
    {
        var battleUI = GameObject.Find("PnlBattleOthers");
        var scores = battleUI.FindByName("Score");
        
        var groove = scores.FindByName("GC");
        var djmax = scores.FindByName("Djmax");
        
        if (djmax?.activeSelf ?? false)
            return StageType.DjMax;
        
        if (groove?.activeSelf ?? false)
            return StageType.GrooveCoaster;
        
        return StageType.Default;
    }
    
    public void Cleanup()
    {
        UserInterfaceUtils.UnloadFonts();
        DefaultStyle.Cleanup();
        DJMaxStyle.Cleanup();
    }
}