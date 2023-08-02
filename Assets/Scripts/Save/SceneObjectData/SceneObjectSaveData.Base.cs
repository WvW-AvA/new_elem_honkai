using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
/// <summary>
/// 由于场景中需要存档的物品不同，不同种类的存档数据千变万化
/// 为能够规范，并且可扩展所有场景中物品的存档方式，我们定义
/// SceneObjectSaveData类。
/// </summary>
public partial class SceneObjectSaveData
{
    /// <summary>
    /// 规范所有的需存档的场景物品要继承这个Base基类。
    /// </summary>
    [Serializable]
    public class Base
    {
        public GameObject go;
        public Vector3 savePos;
        public Base(GameObject go) { this.go = go; savePos = go.transform.position; }
        private Base() { }
        public virtual void BeforeSave()
        {
            if (go == null)
                return;
            savePos = go.transform.position;
        }
        public virtual void AfterLoad()
        {
            if (go == null)
                return;
            go.transform.position = savePos;
        }
    }
}
