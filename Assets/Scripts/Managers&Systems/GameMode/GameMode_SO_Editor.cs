using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GameMode_SO))]
public class GameMode_SO_Editor : Editor
{
    GameMode.EGameMode lastMode = GameMode.EGameMode.Base;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameMode_SO GM_SO = target as GameMode_SO;
        if (GM_SO.gameMode != lastMode)
        {
            lastMode = GM_SO.gameMode;
            var type = Type.GetType("GameMode+" + GM_SO.gameMode.ToString());
            if (type == null)
            {
                Log.Error("{0} is not GameMode Type", GM_SO.gameMode.ToString());
                return;
            }
            GM_SO.modeConfig = Activator.CreateInstance(type) as GameMode.Base;
            GM_SO.modeConfig.modeType = GM_SO.gameMode;
            UnityEditor.EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
        }
    }
}
#endif
