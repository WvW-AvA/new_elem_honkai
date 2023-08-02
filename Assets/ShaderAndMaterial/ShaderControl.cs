using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderControl : MonoBehaviour
{
    public Material tuanzi1;
    public Material tuanzi2;
    public Animator tuanziAnim;
    public SpriteRenderer tuanziSprite;
    public bool startAnimating = false;
    private float timeControlValue = 0f; 

    void Update()
    {
        if (startAnimating)
        {
            timeControlValue += Time.deltaTime;
            tuanziSprite.material.SetFloat("_TimeControl", timeControlValue);
        }
        else
        {
            timeControlValue = 0f;
            tuanziSprite.material.SetFloat("_TimeControl", timeControlValue);
        }
    }

    public void StartGame()
    {
        tuanziSprite.material = tuanzi2;
        tuanziAnim.enabled = true;
        startAnimating = true;
    }
}
