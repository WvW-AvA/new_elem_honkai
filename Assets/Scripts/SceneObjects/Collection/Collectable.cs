using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactible
{
    public virtual string CollectionName { get { return ""; } }


    protected sealed override void OnPlayerTouch()
    {

    }

    protected virtual void OnCollect()
    {

    }
}
