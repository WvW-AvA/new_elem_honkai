using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class Gate : Interactible
{
    [SerializeField]
    public String Scene;
    public Vector3 pos;
    public bool isPlayerMoveAfterTransport;
    [ShowIf("isPlayerMoveAfterTransport"), Range(-1, 1)]
    public float moveDir;
    [ShowIf("isPlayerMoveAfterTransport")]
    public float moveTime;
    // Start is called before the first frame update

    protected override void OnPlayerTouch()
    {
        base.OnPlayerTouch();
        if (Scene == String.Empty)
        {
            Debug.Log(gameObject + " Gate No Scene to enter!");
        }
        GameManager.LoadSceneAsync(Scene, AfterLoad);

    }
    private void AfterLoad()
    {
        CameraManager.AutoSetCameraBoundary();
        GameManager.ChangeGameMode(GameMode.EGameMode.NormalMode);
        Player.instance.transform.position = pos;
        if (isPlayerMoveAfterTransport && moveDir != 0)
        {
            if (moveDir > 0)
            {
                InputManager.MockKey(EKey.D, moveTime);
            }
            else if (moveDir < 0)
            {
                InputManager.MockKey(EKey.A, moveTime);
            }
            InputManager.MockAxis(EAxis.Horizontal, moveDir, moveTime);
        }
    }
}
