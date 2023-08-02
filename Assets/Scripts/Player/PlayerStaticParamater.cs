using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class PlayerStaticParamater
{
    public const int maxFireEnergy = 100;
    public const int maxWaterEnergy = 100;
    public const int maxTerrainEnergy = 100;
    public static Vector3 playerBornPos
    {
        get
        {
            if (GameObject.FindGameObjectWithTag("PlayerBorn"))
            {
                var pos = GameObject.FindGameObjectWithTag("PlayerBorn").transform.position;
                pos.z = 0;
                return pos;
            }
            else
                return Vector3.zero;
        }
    }
}
