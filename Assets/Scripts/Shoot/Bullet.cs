using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TODO:�п�������
/// </summary>
public class Bullet : MonoBehaviour
{

    public string bulletName;
    public EElement element;

    private void Awake()
    {
        if (gameObject.tag != "Bullet")
            gameObject.tag = "Bullet";
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
