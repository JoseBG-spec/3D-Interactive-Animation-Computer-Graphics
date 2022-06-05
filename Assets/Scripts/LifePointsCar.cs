using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePointsCar : MonoBehaviour
{
    // Start is called before the first frame update
    public int lifePoints;
    public Text textLifePoints;
    void Start()
    {
        lifePoints = 200;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        textLifePoints.text = lifePoints.ToString();
    }
}
