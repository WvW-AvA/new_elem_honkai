using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripCreator : MonoBehaviour
{
    public GameObject drip;
    public float creatCD;
    private float timer;
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if(timer>creatCD)
        {
            timer = 0;
            Instantiate(drip,this.transform);
        }
    }
}
