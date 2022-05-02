using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chasing.Utils;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int testNum = 1000;
        using (CustomTimer timer = new CustomTimer("bob¼ì²é", testNum))
        {
            for (int i = 0; i < testNum; i++)
            {
                this.DoSome();
            }
        }
    }

    public void DoSome()
    {
        for (int i = 0; i < 1000; i++)
        {
            
        }
    }
}
