using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public GameObject PATH;
    public string name; //npc 이름 설정

    private int day, hour, minute;
    bool isMove;

    Vector3 unitVector;
    Vector3 destPoint = new Vector3(-1, -1, -1);

    string currentPlace;
    int numberOfMovement;
    Queue<string> movePoint;

    // Start is called before the first frame update
    void Start()
    {
        day = GameManager.Day;
        hour = GameManager.Hour;
        minute = GameManager.Minute;

        isMove = false;

        unitVector = Vector3.zero;

        movePoint = new Queue<string>();

        switch (name)
        {
            case "ppap":
                SetPPAP();
                break;
        }

        SetInitialPosition(currentPlace);
    }

    // Update is called once per frame
    void Update()
    {
        // 움직이고 있지 않음
        if(!isMove)
        {
            GetComponent<Animator>().SetBool("move", false);
        }
        // 움직이는 중임
        else
        {
            if(destPoint.x == -1 && destPoint.y == -1 && destPoint.z == -1)
            {
                destPoint = GetPointPosition(movePoint.Dequeue());

                Vector3 currentPoint = transform.position;

                if(Mathf.Abs(currentPoint.x - destPoint.x) <= 0.1f)
                {
                    transform.position = new Vector3(destPoint.x, currentPoint.y, currentPoint.z);

                    unitVector.x = 0;
                }
                else
                {
                    unitVector.x = 1;
                }

                if(Mathf.Abs(currentPoint.x - destPoint.z) <= 0.1f)
                {
                    transform.position = new Vector3(currentPoint.x, currentPoint.y, destPoint.z);

                    unitVector.z = 0;
                }
                else
                {
                    unitVector.z = 1;
                }

            }
            transform.Translate(unitVector * Time.deltaTime, Space.World);

            GetComponent<Animator>().SetBool("move", true);
        }

        day = GameManager.Day;
        hour = GameManager.Hour;
        minute = GameManager.Minute;

        // 목적지까지 이동한 경우
        if(movePoint.Count==0)
        {
            switch (name)
            {
                case "ppap":
                    GetPPAPPath();
                    break;
            }
        }
    }

    void SetInitialPosition(string placeName)
    {
        Vector3 place = GetPlacePosition(placeName);
        transform.position = new Vector3(place.x, transform.position.y, place.z);
    }

    Vector3 GetPlacePosition(string placeName)
    {
        Transform place = PATH.transform.Find(placeName) as Transform;

        return place.position;
    }

    Vector3 GetPointPosition(string pointName)
    {
        Transform point = PATH.transform.Find(pointName) as Transform;

        return point.position;
    }

    void GetPath(string dep, string arr)
    {
        switch(dep + arr)
        {
            case "AB":
                movePoint.Enqueue("00");
                movePoint.Enqueue("01");
                movePoint.Enqueue("02");
                movePoint.Enqueue("03");
                movePoint.Enqueue("04");
                movePoint.Enqueue("05");
                movePoint.Enqueue("06");
                movePoint.Enqueue("07");
                movePoint.Enqueue("08");
                movePoint.Enqueue("09");
                movePoint.Enqueue("10");
                movePoint.Enqueue("11");
                break;
            case "BA":
                movePoint.Enqueue("11");
                movePoint.Enqueue("10");
                movePoint.Enqueue("09");
                movePoint.Enqueue("08");
                movePoint.Enqueue("07");
                movePoint.Enqueue("06");
                movePoint.Enqueue("05");
                movePoint.Enqueue("04");
                movePoint.Enqueue("03");
                movePoint.Enqueue("02");
                movePoint.Enqueue("01");
                movePoint.Enqueue("00");
                break;
            case "BC":
                movePoint.Enqueue("11");
                movePoint.Enqueue("10");
                movePoint.Enqueue("12");
                movePoint.Enqueue("13");
                movePoint.Enqueue("14");
                movePoint.Enqueue("15");
                break;
            case "CB":
                movePoint.Enqueue("15");
                movePoint.Enqueue("14");
                movePoint.Enqueue("13");
                movePoint.Enqueue("16");
                movePoint.Enqueue("17");
                movePoint.Enqueue("11");
                break;
        }
    }

    // set initial position
    void SetPPAP()
    {
        if (hour == 6 || (hour == 7 && minute < 30))
        {
            currentPlace = "A";
            numberOfMovement = 1;
        }
        else if ((hour >= 7 && hour <= 10) || (hour == 11 && minute == 0))
        {
            currentPlace = "B";
            numberOfMovement = 2;
        }
        else if (hour == 11 || hour == 12)
        {
            currentPlace = "C";
            numberOfMovement = 3;
        }
        else if (hour >= 13 && hour <= 16)
        {
            currentPlace = "B";
            numberOfMovement = 4;
        }
        else
        {
            currentPlace = "A";
            numberOfMovement = 5;
        }
    }

    // get current path
    void GetPPAPPath()
    {
        if (numberOfMovement == 1 &&
            ((hour >= 7 && hour <= 10) || (hour == 11 && minute == 0)))
        {
            numberOfMovement++;
            GetPath("A", "B");
        }
        else if (numberOfMovement == 2 && (hour == 11 || hour == 12))
        {
            numberOfMovement++;
            GetPath("B", "C");
        }
        else if (numberOfMovement == 3 && (hour >= 13 && hour <= 16))
        {
            numberOfMovement++;
            GetPath("C", "B");
        }
        else if (numberOfMovement == 4 && (hour > 16))
        {
            numberOfMovement++;
            GetPath("B", "A");
        }
    }
}