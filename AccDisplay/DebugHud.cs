using System;
using System.Collections.Generic;
using System.Text;
using AccDisplay.Utils;
using UnityEngine;

namespace AccDisplay;

public class DebugHud : MonoBehaviour
{
    public static bool Render { get; set; }
    public static List<string> Text { get; } = new();

    private bool comboPressed = true;

    private void OnGUI()
    {
        if (!Render)
            return;
        
        const int height = 28;
        const int spacing = 4;
        var y = 20 + height;

        var count = 0;

        GUI.s_Skin.box.alignment = TextAnchor.UpperLeft;

        for (var i = Text.Count - 1; i >= 0; i--)
        {
            if (count >= 20)
                break;
            
            var t = Text[i];

            GUI.Box(new Rect(20, Screen.height - y, 380, height), t);
            count++;

            y += height + spacing;
        }

        var score = GameUtils.Score;
        
        if (score == null)
            return;

        var sb = new StringBuilder();
        sb.AppendLine("total:");
        sb.AppendLine($"  count:   {score.TotalCount}");
        sb.AppendLine($"  perfect: {score.TotalPerfectCount}");
        sb.AppendLine($"  great:   {score.TotalGreatCount}");
        sb.AppendLine($"  pass:    {score.GearDodgeCount}");
        sb.AppendLine("normal:");
        sb.AppendLine($"  perfect: {score.NormalPerfectCount}");
        sb.AppendLine($"  great:   {score.NormalGreatCount}");
        sb.AppendLine($"  hit:     {score.NormalHitCount}");
        sb.AppendLine($"  miss:    {score.NormalMissCount}");
        sb.AppendLine("gear:");
        sb.AppendLine($"  dodge:   {score.GearDodgeCount}");
        sb.AppendLine($"  hit:     {score.GearHitCount}");
        sb.AppendLine("sheet:");
        sb.AppendLine($"  perfect: {score.SheetPerfectCount}");
        sb.AppendLine($"  great:   {score.SheetGreatCount}");
        sb.AppendLine($"  tick:    {score.SheetTickCount}");
        sb.AppendLine($"  miss:    {score.SheetMissCount}");
        sb.AppendLine("ghost:");
        sb.AppendLine($"  perfect: {score.GhostPerfectCount}");
        sb.AppendLine($"  great:   {score.GhostGreatCount}");
        sb.AppendLine($"  miss:    {score.GhostMissCount}");
        sb.AppendLine("boss:");
        sb.AppendLine($"  perfect: {score.BossPerfectCount}");
        sb.AppendLine($"  great:   {score.BossGreatCount}");
        sb.AppendLine($"  hit:     {score.BossHitCount}");
        sb.AppendLine("heart:");
        sb.AppendLine($"  collect: {score.HeartCollectCount}");
        sb.AppendLine($"  miss:    {score.HeartMissCount}");
        sb.AppendLine("note:");
        sb.AppendLine($"  collect: {score.NoteCollectCount}");
        sb.AppendLine($"  miss:    {score.NoteMissCount}");
        sb.AppendLine("masher:");
        sb.AppendLine($"  perfect: {score.MasherPerfectCount}");
        sb.AppendLine($"  great:   {score.MasherGreatCount}");
        sb.AppendLine($"  tick:    {score.MasherTickCount}");
        sb.AppendLine($"  hit:     {score.MasherHitCount}");
        
        GUI.Box(new Rect(Screen.width - 320, 20, 300, 600), sb.ToString());
    }

    private void Update()
    {
        var shiftDown = Input.GetKey(KeyCode.LeftShift);
        var f10Down = Input.GetKey(KeyCode.F10);

        if (comboPressed)
        {
            if (!shiftDown || !f10Down)
                comboPressed = false;
            else
                return;
        }

        if (shiftDown && f10Down)
        {
            Render = !Render;
            comboPressed = true;
        }
    }
}