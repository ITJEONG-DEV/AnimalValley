using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotAirBalloonFlowing : MonoBehaviour
{
    public float speed = 0.5f;
    public float timeLimit = 1.0f;
    bool isUp;
    float time;

    void Start()
    {
        time = 0.0f;
        isUp = true;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (isUp)
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * speed);
        else
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);

        if(time>=timeLimit)
        {
            time = 0.0f;
            isUp = !isUp;
        }
    }
}
