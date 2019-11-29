using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float speed = 3f;
    public float runWeight = 2f;

    float walkingTime = 0.0f;
    bool isWalk = false;
    bool isRun = false;
    bool left = false;
    bool right = false;
    bool forward = false;
    bool back = false;

    void Start()
    {
    }

    void FixedUpdate()
    {
        Move();

        if (isWalk)
        {
            walkingTime += Time.fixedDeltaTime;

            if (walkingTime >= 1.5f) isRun = true;

            if (forward || back)
                GetComponent<Animator>().SetInteger("animation", 15);
            else if (right)
                GetComponent<Animator>().SetInteger("animation", 16);
            else if (left)
                GetComponent<Animator>().SetInteger("animation", 17);
        }
        else
        {
            isRun = false;
            walkingTime = 0.0f;
            GetComponent<Animator>().SetInteger("animation", 1);
        }
    }

    void Move()
    {

        // real : right, key : down
        if (Input.GetKeyDown(KeyCode.S)) right = true;
        else if (Input.GetKeyUp(KeyCode.S)) right = false;

        // real : left, key : up
        if (Input.GetKeyDown(KeyCode.W)) left = true;
        else if (Input.GetKeyUp(KeyCode.W)) left = false;

        // real : forward, key : right
        if (Input.GetKeyDown(KeyCode.D)) forward = true;
        else if (Input.GetKeyUp(KeyCode.D)) forward = false;

        // real : back, key : back
        if (Input.GetKeyDown(KeyCode.A)) back = true;
        else if (Input.GetKeyUp(KeyCode.A)) back = false;

        isWalk = false;

        if (left && !right)
        {
            isWalk = true;
            transform.Translate(Vector3.left * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (right && !left)
        {
            isWalk = true;
            transform.Translate(Vector3.right * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (left && right)
        {
            left = false;
            right = false;
        }

        if (forward && !back)
        {
            isWalk = true;
            transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (back && !forward)
        {
            isWalk = true;
            transform.Translate(Vector3.back * speed * Time.fixedDeltaTime * (isRun ? runWeight : 1));
        }
        else if (forward && back)
        {
            forward = false;
            back = false;
        }
    }
}
