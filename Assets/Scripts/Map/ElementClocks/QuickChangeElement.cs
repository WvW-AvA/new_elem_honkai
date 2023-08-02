using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickChangeElement : MonoBehaviour
{
    public EElement changeElement;
    private MapObjectFSM mFSM;
    void Start()
    {
        if (changeElement == EElement.NONE) return;
        mFSM = GetComponentInParent<MapObjectFSM>();
        if(mFSM!=null)
        {
            Debug.Log("Success1");
            FSMBaseState_T ENS;
            mFSM.statesDic.TryGetValue("ElementClock_None",out ENS);
            if(ENS as ElementClock_None_State!=null)
            {
                (ENS as ElementClock_None_State).quickChangeElement = changeElement;
                Debug.Log("Success2");
            }
        }
    }
}
