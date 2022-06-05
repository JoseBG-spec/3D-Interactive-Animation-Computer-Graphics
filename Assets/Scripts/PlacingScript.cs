using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacingScript : MonoBehaviour
{
    public Text placeInRaceText;
    public AICarBehavior Ai1, Ai2;
    public CarBehavior Car1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Car1.counter > Ai1.counter && Car1.counter > Ai2.counter)
        {
            placeInRaceText.text = "1st";
        }else if (Car1.counter < Ai1.counter && Car1.counter > Ai2.counter || Car1.counter > Ai1.counter && Car1.counter < Ai2.counter)
        {
            placeInRaceText.text = "2nd";
        }else if(Car1.counter < Ai1.counter && Car1.counter < Ai2.counter)
        {
            placeInRaceText.text = "3rd";
        }
    }
}
