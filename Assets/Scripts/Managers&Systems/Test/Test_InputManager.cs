using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_InputManager : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        Log.Info("{0} catch {1} value {2}", LogColor.InputManager, LogColor.Dye(LogColor.EColor.blue, "Horizontal"), InputManager.Axises[EAxis.Horizontal].value);
        Log.Info("{0} catch {1} value {2}", LogColor.InputManager, LogColor.Dye(LogColor.EColor.red, "Vertical"), InputManager.Axises[EAxis.Vertical].value);
    }
}
