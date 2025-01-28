using System;
using AccDisplay.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace AccDisplay.Objects.Styles;

// ReSharper disable once InconsistentNaming
public class DJMaxStyle : MonoBehaviour
{
    private readonly Color _grey = new(.62f, .62f, .62f, 1f);
    
    private static GameObject _percentText;
    private static GameObject _customText;
    private Text _text;
    
    public DJMaxStyle(IntPtr intPtr)
        : base(intPtr)
    {
    }
    
    private void Start()
    {
        UserInterfaceUtils.LoadFonts();
        var layer = GameObject.Find(AccuracyManager.LayerName);
        
        _percentText = UserInterfaceUtils.CreateText("AccuracyPercent", ",", 0, Color.white, UserInterfaceUtils.LatoFont);
        _text = _percentText.GetComponent<Text>();
        layer.AddChild(_percentText);

        if (AccDisplayMod.DisplayAccText)
        {
            _customText = UserInterfaceUtils.CreateText("AccuracyText", AccDisplayMod.CustomText, 70, _grey, UserInterfaceUtils.LatoFont, 45);
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