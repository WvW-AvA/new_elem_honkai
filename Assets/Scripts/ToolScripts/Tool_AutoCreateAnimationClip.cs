#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;


public class Tool_AutoCreateAnimationClip : OdinEditorWindow
{
    public bool isCreateNewDir = false;
    [LabelText("前缀")]
    public string prefix = "";
    [LabelText("名称")]
    public string animName = "";
    [LabelText("帧率")]
    public int frameRate = 24;
    [LabelText("AnimationClip保存路径")]
    public string AnimSavePath = "";
    [LabelText("是否生成TransitionClip")]
    public bool isCreateTransitionClip;
    [LabelText("TransitionClip保存路径")]
    public string TransitionClipSavePath = "";

    [InlineEditor(InlineEditorModes.LargePreview)]
    [ListDrawerSettings(OnTitleBarGUI = "DrawRefreshButton")]
    public List<Sprite> sprites;

    [MenuItem("Tools/Sprite2Anim")]
    private static void OpenWindow()
    {
        var window = GetWindow<Tool_AutoCreateAnimationClip>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 600);
        window.titleContent = new GUIContent("Create Sprite Anim");
    }

    public void DrawRefreshButton()
    {
        if (SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
            sprites.Clear();
    }
    [HorizontalGroup("Button")]
    [Button(ButtonSizes.Gigantic, Name = "创建动画"), GUIColor(0, 1, 0)]
    public void CreateAnimationClip()
    {
        if (sprites.Count == 0)
        {
            Log.Error("请选择贴图");
            return;
        }
        if (prefix == "" || animName == "" || AnimSavePath == "" || (isCreateTransitionClip == true && TransitionClipSavePath == ""))
        {
            Log.Error("请填写前缀，名称和路径");
            return;
        }
        AnimationClip clip = new AnimationClip();
        EditorCurveBinding curveBinding = new EditorCurveBinding();
        curveBinding.type = typeof(SpriteRenderer);
        curveBinding.path = "";
        curveBinding.propertyName = "m_Sprite";
        ObjectReferenceKeyframe[] keyframes = new ObjectReferenceKeyframe[sprites.Count];
        for (int i = 0; i < sprites.Count; i++)
        {
            keyframes[i] = new ObjectReferenceKeyframe();
            keyframes[i].time = i / (float)frameRate;
            keyframes[i].value = sprites[i];
        }
        clip.frameRate = frameRate;
        AnimationClipSettings clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
        clipSettings.loopTime = true;
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, keyframes);

        string name = prefix + "_" + animName;
        string dir = AnimSavePath;
        if (isCreateNewDir)
        {
            dir += "/" + prefix;
            if (Directory.Exists(dir) == false)
                Directory.CreateDirectory(dir);
        }
        AssetDatabase.CreateAsset(clip, dir + "/" + name + ".anim");
        if (isCreateTransitionClip)
        {
            dir = TransitionClipSavePath;
            if (isCreateNewDir)
            {
                dir += "/" + prefix;
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);
            }
            var transition = Tool_AnimationClip2TransitionClip.Anim2ClipTransition(clip);
            AssetDatabase.CreateAsset(transition, dir + "/" + name + ".asset");
        }
        AssetDatabase.SaveAssets();
    }
}

#endif
