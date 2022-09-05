using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class ProfilerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Profiler.BeginSample("My Test Sample");
        int a = 0;
        for (int i = 0; i < 100000; i++)
        {
            a++;
        }
        Debug.Log(a);
        Profiler.EndSample();
    }
}
