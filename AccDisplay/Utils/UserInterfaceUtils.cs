using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace AccDisplay.Utils;

public static class UserInterfaceUtils {
    private static Font _snapsTasteFont;
    
    public static void LoadFonts() {
        _snapsTasteFont = Addressables.LoadAssetAsync<Font>("Snaps Taste").WaitForCompletion();
    }
    
    public static void UnloadFonts() {
        Addressables.Release(_snapsTasteFont);
    }
    
    public static GameObject CreateText(string id, string text, int y, Color color, bool outlined = false, Color? outlineColor = null) {
        var parent = GameObject.Find("Accuracy Canvas");
        var textObject = new GameObject(id);
        textObject.transform.SetParent(parent.transform);
            
        var textComponent = textObject.AddComponent<Text>();
        textComponent.text = text;
        textComponent.alignment = TextAnchor.UpperLeft;
        textComponent.font = _snapsTasteFont;
        textComponent.fontSize = 70;
        textComponent.color = color;
        textComponent.transform.localPosition = new Vector3(80, -150 - y, 0);
            
        var rectTransform = textComponent.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(1920, 1080);
        rectTransform.localScale = new Vector3(1f, 1f, 1f);
            
        if (outlined) {
            var outline = textObject.AddComponent<Outline>();
            outline.effectColor = outlineColor ?? Color.white;
            outline.effectDistance = new Vector2(2, -2);
        }
            
        return textObject;
    }

    public static GameObject CreateCanvas(string name, string camera) {
        var canvas = new GameObject(name);
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();
        canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        canvas.GetComponent<Canvas>().worldCamera = GameObject.Find(camera).GetComponent<Camera>();
        canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1920, 1080);
        canvas.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvas.GetComponent<CanvasScaler>().screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        return canvas;
    }
}