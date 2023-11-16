using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	//-- set start time 00:00
	public int seconds = 0;
	public bool realTime=false;
	
	public GameObject pointerSeconds;

    
    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    float msecs=0;

void Update() 
{
    //-- calculate time
    msecs += Time.deltaTime * clockSpeed;
    if(msecs >= 1.0f)
    {
        msecs -= 1.0f;
        seconds++;
        if(seconds >= 60)
        {
            seconds = 0;
         
        }
    }


    //-- calculate pointer angles
    float rotationSeconds = (360.0f / 60.0f)  * seconds;

    //-- draw pointers
    pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);


}
}
