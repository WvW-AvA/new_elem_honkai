using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;


public class SavePoint : Interactible
{
    protected override void OnPlayerTouch()
    {
        base.OnPlayerTouch();
        SaveSystem.CurrentFile.playerData.pos = transform.position;
        SaveSystem.Save();
    }
}