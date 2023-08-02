#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Animancer;
public class Tool_AnimationClip2TransitionClip : OdinEditorWindow
{
    [InlineEditor(InlineEditorModes.LargePreview)]
    [ListDrawerSettings(OnTitleBarGUI = "DrawRefreshButton")]
    public List<AnimationClip> animClips;

    [LabelText("存储路径")]
    public string savePath = "";
    [MenuItem("AnimClip2TranClip", menuItem = "Tools/AnimClip2TranClip")]
    private static void OpenWindow()
    {
        var window = GetWindow<Tool_AnimationClip2TransitionClip>();
        window.position = GUIHelper.GetEditorWindowRect().AlignCenter(600, 600);
    }

    public void DrawRefreshButton(List<AnimationClip> animClips)
    {
        if (SirenixEditorGUI.ToolbarButton(EditorIcons.Refresh))
            animClips.Clear();
    }

    [HorizontalGroup("Button")]
    [Button(ButtonSizes.Gigantic, Name = "Covert"), GUIColor(1, 0, 0)]
    public void ConverToTransitionClip()
    {
        if (savePath == "")
        {
            Log.Error("请指定存储路径");
            return;
        }
        if (Directory.Exists(savePath) == false)
            Directory.CreateDirectory(savePath);
        foreach (var anim in animClips)
        {
            var transition = Anim2ClipTransition(anim);
            AssetDatabase.CreateAsset(transition, savePath + "/" + anim.name + ".asset");
        }
        AssetDatabase.SaveAssets();
    }

    public static ClipTransition Anim2ClipTransition(AnimationClip clip)
    {
        ClipTransition transition = (ClipTransition)CreateInstance("ClipTransition");
        transition.Transition = new ClipState.Transition();
        transition.Transition.Clip = clip;
        transition.Transition.FadeDuration = 0f;
        return transition;
    }

}
#endif
