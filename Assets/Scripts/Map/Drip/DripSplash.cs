using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSplash : MonoBehaviour
{
    public float lifeTime;
    private float timer;
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer > lifeTime) Destroy(this.gameObject);
    }
}
