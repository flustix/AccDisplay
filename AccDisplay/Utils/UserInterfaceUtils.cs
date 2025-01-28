using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace AccDisplay.Utils;

public static class UserInterfaceUtils {
    public static Font SnapsTasteFont { get; private set; }
    public static Font GrooveCoasterFont { get; private set; }
    public static Font LatoFont { get; private set; }
    
    public static void LoadFonts() {
        SnapsTasteFont = Addressables.LoadAssetAsync<Font>("Snaps Taste").WaitForCompletion();
        GrooveCoasterFont = Addressables.LoadAssetAsync<Font>("ScoreGC SDF").WaitForCompletion();
        LatoFont = Addressables.LoadAssetAsync<Font>("Lato-Regular").WaitForCompletion();
    }
    
    public static void UnloadFonts() {
        Addressables.Release(SnapsTasteFont);
    }
    
    public static GameObject CreateText(string id, string text, int yOffset, Color color, Font font, int fontSize = 72) {
        var textObject = new GameObject(id);
            
        var textComponent = textObject.AddComponent<Text>();
        textComponent.text = text;
        textComponent.alignment = TextAnchor.UpperLeft;
        textComponent.font = font;
        textComponent.fontSize = fontSize;
        textComponent.color = color;
        textComponent.transform.localPosition = new Vector3(80, -150 - yOffset, 0);
            
        var rectTransform = textComponent.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1920, 1080);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
            
        return textObject;
    }

    public static GameObject CreateCanvas(string name)
    {
        var canvas = new GameObject(name);
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        canvas.GetComponent<Canvas>().worldCamera = GameObject.Find("Camera_2D").GetComponent<Camera>();
        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        return canvas;
    }

    public static GameObject FindByName(this GameObject gameObject, string name) {
        return gameObject.transform.Find(name)?.gameObject;
    }
    
    public static void AddChild(this GameObject parent, GameObject child)
        => child.transform.SetParent(parent.transform, false);
}