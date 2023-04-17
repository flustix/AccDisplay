using MuseDashMirror.UICreate;
using UnityEngine;
using UnityEngine.UI;

namespace AccDisplay {
    public abstract class Utils {
        public static GameObject CreateText(string id, string text, int y, Color color, bool outlined = false, Color? outlineColor = null) {
            var parent = GameObject.Find("Accuracy Canvas");
            var textObject = new GameObject(id);
            textObject.transform.SetParent(parent.transform);
            
            var textComponent = textObject.AddComponent<Text>();
            textComponent.text = text;
            textComponent.alignment = TextAnchor.UpperLeft;
            textComponent.font = Fonts.SnapsTasteFont;
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
    }
}