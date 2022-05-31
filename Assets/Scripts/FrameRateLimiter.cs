using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimiter : MonoBehaviour
{
    public int Rate;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Rate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
