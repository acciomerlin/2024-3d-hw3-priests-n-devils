using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    IUserAction userAction;
    public string gameMessage;
    public int time;
    GUIStyle style, bigstyle;
    FirstController controller;

    private bool showStartScreen = true;  
    private Texture2D backgroundTexture;  // 背景蒙版

    void Start()
    {
        time = 60;
        userAction = SSDirector.GetInstance().CurrentSceneController as IUserAction;
        controller = SSDirector.GetInstance().CurrentSceneController as FirstController;

        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 60;

        // 创建背景蒙版，设置透明度和颜色
        backgroundTexture = new Texture2D(1, 1);
        Color overlayColor = new Color(0, 0, 0, 0.5f);  // 黑色，50%透明度
        backgroundTexture.SetPixel(0, 0, overlayColor);
        backgroundTexture.Apply();
    }

    void OnGUI()
    {
        if (showStartScreen)
        {
            // 显示初始蒙版
            GUIStyle startScreenStyle = new GUIStyle(GUI.skin.box);
            startScreenStyle.alignment = TextAnchor.MiddleCenter;
            startScreenStyle.fontSize = 60;
            startScreenStyle.normal.textColor = Color.white;
            startScreenStyle.normal.background = backgroundTexture;  // 设置透明背景

            // 蒙版背景
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", startScreenStyle);

            // title
            GUIStyle titleStyle = new GUIStyle();
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.fontSize = 80;
            titleStyle.normal.textColor = Color.white;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            string title = "Priests and devils";
            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 100, 300, 100), title, titleStyle);

            // 自定义 Start 按钮样式
            GUIStyle startButtonStyle = new GUIStyle(GUI.skin.button);
            startButtonStyle.fontSize = 60;  // 设置字号
            startButtonStyle.fontStyle = FontStyle.BoldAndItalic;
            startButtonStyle.alignment = TextAnchor.MiddleCenter;

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 100), "Start", startButtonStyle))
            {
                showStartScreen = false;  
                controller.RestartGame();  
            }

            return; 
        }

        // 游戏主界面
        userAction.Check();
        GUI.Label(new Rect(400, 20, 50, 200), gameMessage, style);
        GUI.Label(new Rect(40, 20, 100, 50), "Time Left: " + time, style);

        // 自定义按钮样式
        GUIStyle infoButtonStyle = new GUIStyle(GUI.skin.button);
        infoButtonStyle.fontSize = 40;  // 设置按钮的字体大小
        infoButtonStyle.alignment = TextAnchor.MiddleCenter;
        infoButtonStyle.fontStyle = FontStyle.Italic;
        infoButtonStyle.normal.textColor = Color.white;

        GUIStyle restartButtonStyle = new GUIStyle(GUI.skin.button);
        restartButtonStyle.fontSize = 50;  // 设置按钮的字体大小
        restartButtonStyle.alignment = TextAnchor.MiddleCenter;
        restartButtonStyle.fontStyle = FontStyle.BoldAndItalic;
        restartButtonStyle.normal.textColor = Color.yellow;

        // 调整提示按钮位置和大小
        Rect infoButtonRect = new Rect(Screen.width / 2 + 400, Screen.height / 2 -530, 200, 100);
        if (GUI.Button(infoButtonRect, "rules", infoButtonStyle))
        {
            // 可在这里添加提示逻辑
        }

        // 提示内容显示
        string tooltipText = "Game Rules:\n1. Click the gosts with blue hat(priests), the pumpkins(devils) and the grey rock(boat) to move priests and devils across the river.\n" +
                             "2. Ensure priests are never outnumbered by devils on either shore.\n" +
                             "3. Use the boat to transport characters, but at most two can be on it at a time.\n" +
                             "4. Win if all priests reach the opposite shore safely.";
        GUIStyle tooltipStyle = new GUIStyle(GUI.skin.box);
        tooltipStyle.wordWrap = true;
        tooltipStyle.fontSize = 30;  // 调整提示框字体大小

        float maxWidth = 400;
        float textHeight = tooltipStyle.CalcHeight(new GUIContent(tooltipText), maxWidth);
        Vector2 textSize = tooltipStyle.CalcSize(new GUIContent(tooltipText));

        if (infoButtonRect.Contains(Event.current.mousePosition))
        {
            Rect tooltipRect = new Rect(infoButtonRect.x - 150, infoButtonRect.y + 70, maxWidth, textHeight + 20);
            GUI.Box(tooltipRect, tooltipText, tooltipStyle);
        }

        // 调整重新开始按钮位置和大小
        Rect restartButtonRect = new Rect(Screen.width / 2+650, Screen.height / 2 -530, 280, 120);
        if (GUI.Button(restartButtonRect, "RESTART", restartButtonStyle))
        {
            controller.RestartGame();
        }
    }
}