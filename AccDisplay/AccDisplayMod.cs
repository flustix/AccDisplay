using AccDisplay.Objects;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using UnityEngine;
using GameUtils = AccDisplay.Utils.GameUtils;

namespace AccDisplay;

public class AccDisplayMod : MelonMod {
    public static float Accuracy { get; private set; }
    public static string Baller { get; set; } // debuggery
        
    private static MelonPreferences_Category _category;
    private static MelonPreferences_Entry<bool> _displayAccText;
    private static MelonPreferences_Entry<string> _customText;
    private static MelonPreferences_Entry<bool> _smoothAccuracy;
        
    public static bool DisplayAccText => _displayAccText.Value;
    public static string CustomText => _customText.Value;
    private static bool SmoothAccuracy => _smoothAccuracy.Value;

    public override void OnInitializeMelon() {
        _category = MelonPreferences.CreateCategory("AccDisplay", "AccDisplay");
        _displayAccText = _category.CreateEntry("DisplayAccText", true, "Display the \"accuracy\" text");
        _customText = _category.CreateEntry("CustomText", "ACCURACY", "Replace the \"accuracy\" text with your own");
        _smoothAccuracy = _category.CreateEntry("SmoothAccuracy", true, "Smooth out the accuracy using lerp");
        _category.LoadFromFile(false);
        _category.SaveToFile(false);
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (sceneName == "GameMain")
        {
            GameUtils.Reload();
            ClassInjector.RegisterTypeInIl2Cpp<AccuracyText>();
            new GameObject("AccuracyDisplay").AddComponent<AccuracyText>();
        } else {
            AccuracyText.Remove();
            GameUtils.Reset();
        }
    }

    public override void OnUpdate() {
        base.OnUpdate();
        
        if (!GameUtils.Playing) return;

        var calculated = GameUtils.Accuracy;
        Accuracy = SmoothAccuracy ? Mathf.Lerp(Accuracy, calculated, 0.1f) : calculated;
    }
}