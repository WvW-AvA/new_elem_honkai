using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drip : MonoBehaviour
{
    public float ignoreTime;
    public GameObject splash;
    private float timer;
    private ElementDamageBuff EDB = new ElementDamageBuff();
    private void Awake()
    {
        EDB.buffName = "Drip";
        EDB.element = EElement.WATER;
        EDB.times = 1;
        EDB.damageValue = 1;
    }
    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (timer > ignoreTime)
            if (col.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Instantiate(splash, this.transform.position, this.transform.rotation, this.transform.parent);
                Destroy(this.gameObject);
            }
        if (col.gameObject.layer == LayerMask.NameToLayer("ElementHittable"))
        {
            Instantiate(splash, this.transform.position, this.transform.rotation, this.transform.parent);
            BuffManager.AddBuff(EDB, this.gameObject, col.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("ElementHittable"))
        {
            BuffManager.AddBuff(EDB, this.gameObject, col.gameObject);
        }
    }
}
