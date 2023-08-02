using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLayerTrigger : EnemyFSMBaseTrigger
{
    public LayerMask Layer;
    public override bool IsTriggerReachOnTriggerEnter(EnemyFSM EnemyFSM, Collider2D collision)
    {
        return ((1 << collision.gameObject.layer & Layer) != 0);
    }
    public override bool IsTriggerReachOnCollisionEnter(EnemyFSM EnemyFSM, Collision2D collision)
    {
        return ((1 << collision.gameObject.layer) & Layer) != 0;
    }
}

public class TouchTagTrigger : EnemyFSMBaseTrigger
{
    public string Tag;
    public override bool IsTriggerReachOnTriggerEnter(EnemyFSM EnemyFSM, Collider2D collision)
    {
        return (collision.gameObject.tag == Tag);
    }
    public override bool IsTriggerReachOnCollisionEnter(EnemyFSM EnemyFSM, Collision2D collision)
    {
        return (collision.gameObject.tag == Tag);
    }
}

public class TouchGameObjectTrigger : EnemyFSMBaseTrigger
{
    public string GameObjectName;
    public override bool IsTriggerReachOnTriggerEnter(EnemyFSM EnemyFSM, Collider2D collision)
    {
        return (collision.gameObject.name.Contains(GameObjectName));
    }
    public override bool IsTriggerReachOnCollisionEnter(EnemyFSM EnemyFSM, Collision2D collision)
    {
        return (collision.gameObject.name.Contains(GameObjectName));
    }
}