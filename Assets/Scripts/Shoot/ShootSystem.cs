using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

[Serializable]
public class ShootSystem : SerializedMonoBehaviour
{
    public enum ShootMethod
    {
        ShootOnce = 0,
        ShootTogether = 1,
        ShootOneByOne = 2,
        ShootRandomly = 3,
        SpecialShoot = 4
    }
    public enum Pattern
    {
        Cycle = 0,
        Square = 1,
        Triangle = 2
    }
    public enum ShootDir
    {
        fromThisPos = 0,
        toTarget = 1,
        Random = 2
    }
    [Serializable]
    public struct shootParam
    {
        public string shootModeName;
        public GameObject bullet;
        public int bulletNum;
        public ShootMethod shootMethod;
        public ShootDir shotDir;
        public bool isShotToTarget() { return shotDir == ShootDir.toTarget; }
        [ShowIf("isShotToTarget")]
        public GameObject target;
        public Transform createPos;
        public float bulletDelayTime;
        public float bulletSpeed;
        public float shootOffsetAngle;
    }



    public List<shootParam> shootModes;
    private Vector2 mainDir;
    void Start()
    {
        for (int i = 0; i < shootModes.Count; i++)
        {
            var tem = shootModes[i];
            if (tem.bullet == null)
                return;
            if (tem.target == null)
                tem.target = Player.instance.gameObject;
            if (tem.createPos == null)
                tem.createPos = this.transform;
            shootModes[i] = tem;
        }
    }



    public void Shoot(string shootMode)
    {
        foreach (var value in shootModes)
        {
            if (value.shootModeName.Equals(shootMode))
            {
                Fire(value);
                return;
            }
        }
        Debug.LogError("Shoot_mode_list could not find shoot mode:" + shootMode);
    }

    private void Fire(shootParam Param)
    {
        switch (Param.shootMethod)
        {
            case ShootMethod.ShootOnce: ShootOnce(Param); break;
            case ShootMethod.ShootOneByOne: StartCoroutine(ShootOneByOne(Param)); break;
            case ShootMethod.ShootRandomly: ShootRandomly(Param); break;
            case ShootMethod.ShootTogether: ShootTogether(Param); break;
        }

    }

    private Vector2 AddOffset(Vector2 a, float offsetAngle)
    {
        float o = (UnityEngine.Random.value - 0.5f) * offsetAngle * Mathf.Deg2Rad;
        Vector2 tem = new Vector2(Mathf.Cos(o), Mathf.Sin(o));
        return new Vector2(tem.x * a.x - tem.y * a.y, tem.x * a.y + tem.y * a.x).normalized;
    }

    private void CreateBullet(GameObject bullet, Vector2 CreatePos, Vector2 ShootVector, Transform target)
    {
        GameObject tem = ResourceManager.GetInstance(bullet, CreatePos);
        tem.GetComponent<Rigidbody2D>().velocity = ShootVector;
        float angle = Mathf.Sign(ShootVector.y) * Mathf.Acos(Vector2.Dot(Vector2.right, ShootVector.normalized)) * Mathf.Rad2Deg;
        tem.transform.Rotate(new Vector3(0, 0, angle));
    }
    private void ShootOnce(shootParam Param)
    {
        if (Param.target == null)
            Param.target = Player.instance.gameObject;
        if (Param.createPos == null)
            Param.createPos = gameObject.transform;

        switch (Param.shotDir)
        {
            case ShootDir.toTarget: mainDir = (Param.target.transform.position - Param.createPos.position).normalized; break;
            case ShootDir.fromThisPos: mainDir = (Param.createPos.position - transform.position).normalized; break;
            case ShootDir.Random: mainDir = UnityEngine.Random.insideUnitCircle; break;
        }

        if (mainDir == Vector2.zero)
            mainDir = UnityEngine.Random.insideUnitCircle.normalized;
        mainDir = AddOffset(mainDir, Param.shootOffsetAngle);
        CreateBullet(Param.bullet, Param.createPos.position, mainDir * Param.bulletSpeed, Param.target.transform);
    }
    private void ShootTogether(shootParam Param)
    {
        for (int i = 0; i < Param.bulletNum; i++)
            ShootOnce(Param);
    }
    private IEnumerator ShootOneByOne(shootParam Param)
    {
        for (int i = 0; i < Param.bulletNum; i++)
        {
            ShootOnce(Param);
            yield return new WaitForSeconds(Param.bulletDelayTime + 0.2f);
        }
    }


    private void ShootRandomly(shootParam Param)
    {

    }

    private void Update()
    {

    }
}
