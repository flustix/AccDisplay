using AccDisplay.Objects;
using AccDisplay.Objects.Styles;
using AccDisplay.Utils;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;
using UnityEngine;
using GameUtils = AccDisplay.Utils.GameUtils;

namespace AccDisplay;

public class AccDisplayMod : MelonMod {
    private static MelonPreferences_Category _category;
    private static MelonPreferences_Entry<bool> _displayAccText;
    private static MelonPreferences_Entry<string> _customText;
    private static MelonPreferences_Entry<bool> _smoothAccuracy;
        
    public static bool DisplayAccText => _displayAccText.Value;
    public static string CustomText => _customText.Value;
    public static bool SmoothAccuracy => _smoothAccuracy.Value;
    
    private AccuracyManager _manager;
    
    public override void OnInitializeMelon() {
        _category = MelonPreferences.CreateCategory("AccDisplay", "AccDisplay");
        _displayAccText = _category.CreateEntry("DisplayAccText", true, "Display the \"accuracy\" text");
        _customText = _category.CreateEntry("CustomText", "ACCURACY", "Replace the \"accuracy\" text with your own");
        _smoothAccuracy = _category.CreateEntry("SmoothAccuracy", true, "Smooth out the accuracy using lerp");
        _category.LoadFromFile(false);
        _category.SaveToFile(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buildIndex"></param>
    /// <param name="sceneName"></param>
    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        if (sceneName == "GameMain")
        {
            GameUtils.Reload();
        
            ClassInjector.RegisterTypeInIl2Cpp<DebugHud>();
            ClassInjector.RegisterTypeInIl2Cpp<DefaultStyle>();
            ClassInjector.RegisterTypeInIl2Cpp<DJMaxStyle>();
            ClassInjector.RegisterTypeInIl2Cpp<AccuracyManager>();
            
            var layer = UserInterfaceUtils.CreateCanvas(AccuracyManager.LayerName);

            var managerObject = new GameObject("AccuracyManager");
            layer.AddChild(managerObject);

            DebugHud.Text.Clear();
            
            var editorObject = new GameObject("DebugInterface");
            editorObject.AddComponent<DebugHud>();
            layer.AddChild(editorObject);
            
            _manager = managerObject.AddComponent<AccuracyManager>();
            _manager.Setup(layer);

        } else {
            if (_manager != null)
                _manager.Cleanup();
            
            GameUtils.Reset();
        }
    }
}