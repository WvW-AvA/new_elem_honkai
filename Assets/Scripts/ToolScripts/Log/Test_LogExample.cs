using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LogExample : MonoBehaviour
{
    int num = 0;
    // Start is called before the first frame update
    void Start()
    {
        Log.Debug("Start Debug log");
    }

    // Update is called once per frame
    void Update()
    {
        num++;
        Log.Debug("{0} Debug log", num);

        if (num % 10 == 0)
        {
            Log.Info("{0} Info log", num);
        }
    }
}
