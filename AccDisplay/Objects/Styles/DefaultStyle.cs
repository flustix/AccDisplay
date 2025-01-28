using System;
using AccDisplay.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace AccDisplay.Objects.Styles;

public class DefaultStyle : MonoBehaviour
{
    private readonly Color _pink = new(1f, 0.64f, 0.93f, 1f);
    private readonly Color _pinkDarker = new(1f, 0.22f, 0.85f, 1f);
    
    private static GameObject _percentText;
    private static GameObject _customText;
    private Text _text;
    
    public DefaultStyle(IntPtr intPtr)
        : base(intPtr)
    {
    }
    
    private void Start()
    {
        UserInterfaceUtils.LoadFonts();
        var layer = GameObject.Find(AccuracyManager.LayerName);
        
        _percentText = UserInterfaceUtils.CreateText("AccuracyPercent", ",", 0, _pink, UserInterfaceUtils.SnapsTasteFont);
        _text = _percentText.GetComponent<Text>();
        layer.AddChild(_percentText);
        
        var outline = _percentText.AddComponent<Outline>();
        outline.effectColor = _pinkDarker;
        outline.effectDistance = new Vector2(2, 2);

        if (AccDisplayMod.DisplayAccText)
        {
            _customText = UserInterfaceUtils.CreateText("AccuracyText", AccDisplayMod.CustomText, 70, _pinkDarker, UserInterfaceUtils.SnapsTasteFont);
            layer.AddChild(_customText);
        }
    }
    
    private void Update()
    {
        _text.text = AccuracyManager.AccuracyStr;
    }

    public static void Cleanup()
    {
        Destroy(_percentText);
        Destroy(_customText);
    }
}