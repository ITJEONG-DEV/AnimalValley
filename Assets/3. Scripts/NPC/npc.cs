using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    public GameObject PATH;
    public string name; //npc 이름 설정

    private int day, hour, minute;
    bool isMove;

    private float speed = 0.3f;

    Vector3 unitVector;
    Vector3 destPoint = new Vector3(-1, -1, -1);

    string currentPlace;
    int numberOfMovement;
    Queue<string> movePoint;

    bool isReady = false;
    
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Ready && !isReady)
        {
            day = GameManager.Day;
            hour = GameManager.Hour;
            minute = GameManager.Minute;

            Debug.Log("day : " + day + " hour : " + hour + "minute : " + minute);

            isMove = false;

            unitVector = Vector3.zero;

            movePoint = new Queue<string>();

            switch (name)
            {
                case "ppap":
                    SetPPAP();
                    break;
                case "doctor":
                    SetDoctor();
                    break;
                case "myam":
                    SetMyam();
                    break;
                case "chef":
                    SetChef();
                    break;
                case "police":
                    SetPolice();
                    break;
                case "shopkeeper":
                    SetShopKeeper();
                    break;
                case "teacher":
                    SetTeacher();
                    break;
            }

            SetInitialPosition(currentPlace);

            isReady = true;
        }

        if(isReady)
        {
            // 움직이고 있지 않음
            if (!isMove)
            {
                GetComponent<Animator>().SetBool("walk", false);
            }
            // 움직이는 중임
            else
            {
                GetComponent<Animator>().SetBool("walk", true);

                if (destPoint.x == -1 && destPoint.y == -1 && destPoint.z == -1)
                {

                    string point = movePoint.Dequeue();
                    destPoint = GetPointPosition(point);


                    Vector3 currentPoint = transform.position;

                    float xGap = destPoint.x - currentPoint.x;
                    if (Mathf.Abs(xGap) <= 0.1f)
                    {
                        transform.position = new Vector3(destPoint.x, currentPoint.y, currentPoint.z);

                        unitVector.x = 0;
                    }
                    else if(xGap > 0.1f)
                    {
                        unitVector.x = 1;
                    }
                    else
                    {
                        unitVector.x = -1;
                    }

                    float zGap = destPoint.z - currentPoint.z;
                    if (Mathf.Abs(zGap) <= 0.1f)
                    {
                        transform.position = new Vector3(currentPoint.x, currentPoint.y, destPoint.z);

                        unitVector.z = 0;
                    }
                    else if(zGap > 0.1f)
                    {
                        unitVector.z = 1;
                    }
                    else
                    {
                        unitVector.z = -1;
                    }

                    if (unitVector.x == 0 && unitVector.z == 1)
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    else if (unitVector.x == 0 && unitVector.z == -1)
                        transform.rotation = Quaternion.Euler(0, 180, 0);

                    else if (unitVector.x == -1 && unitVector.z == 0)
                        transform.rotation = Quaternion.Euler(0, -90, 0);
                    else if (unitVector.x == 1 && unitVector.z == 0)
                        transform.rotation = Quaternion.Euler(0, 90, 0);

                    Debug.Log(name + " walk : " + currentPlace + " point : " + point + " unit vector : " + unitVector);
                }

                transform.Translate(unitVector * Time.deltaTime * speed, Space.World);

                float length = Mathf.Sqrt(
                    Mathf.Pow(Mathf.Abs(destPoint.x - transform.position.x), 2) +
                    Mathf.Pow(Mathf.Abs(destPoint.z - transform.position.z), 2)
                    );

                if(length <= 0.01)
                {
                    transform.position = new Vector3(destPoint.x, transform.position.y, destPoint.z);
                    destPoint = new Vector3(-1, -1, -1);
                    unitVector = new Vector3(0, 0, 0);

                    if (movePoint.Count == 0)
                    {
                        isMove = false;
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
            }

            day = GameManager.Day;
            hour = GameManager.Hour;
            minute = GameManager.Minute;

            // 목적지까지 이동한 경우
            if (movePoint.Count == 0)
            {
                switch (name)
                {
                    case "ppap":
                        GetPPAPPath();
                        break;
                    case "doctor":
                        GetDoctorPath();
                        break;
                    case "myam":
                        GetMyamPath();
                        break;
                    case "chef":
                        GetChefPath();
                        break;
                    case "police":
                        GetPolicePath();
                        break;
                    case "shopkeeper":
                        GetShopKeeperPath();
                        break;
                    case "teacher":
                        GetTeacherPath();
                        break;
                }
            }
            else
            {
                isMove = true;
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
                movePoint.Enqueue("B");
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
                movePoint.Enqueue("A");
                break;
            case "BC":
                movePoint.Enqueue("11");
                movePoint.Enqueue("10");
                movePoint.Enqueue("12");
                movePoint.Enqueue("13");
                movePoint.Enqueue("14");
                movePoint.Enqueue("15");
                movePoint.Enqueue("C");
                break;
            case "CB":
                movePoint.Enqueue("15");
                movePoint.Enqueue("14");
                movePoint.Enqueue("13");
                movePoint.Enqueue("16");
                movePoint.Enqueue("17");
                movePoint.Enqueue("11");
                movePoint.Enqueue("B");
                break;
            case "CO":
                movePoint.Enqueue("15");
                movePoint.Enqueue("14");
                movePoint.Enqueue("70");
                movePoint.Enqueue("O");
                break;
            case "DE":
                movePoint.Enqueue("18");
                movePoint.Enqueue("22");
                movePoint.Enqueue("03");
                movePoint.Enqueue("02");
                movePoint.Enqueue("19");
                movePoint.Enqueue("20");
                movePoint.Enqueue("21");
                movePoint.Enqueue("E");
                break;
            case "ED":
                movePoint.Enqueue("21");
                movePoint.Enqueue("20");
                movePoint.Enqueue("19");
                movePoint.Enqueue("02");
                movePoint.Enqueue("03");
                movePoint.Enqueue("22");
                movePoint.Enqueue("18");
                movePoint.Enqueue("D");
                break;
            case "EG":
                movePoint.Enqueue("21");
                movePoint.Enqueue("23");
                movePoint.Enqueue("24");
                movePoint.Enqueue("25");
                movePoint.Enqueue("26");
                movePoint.Enqueue("27");
                movePoint.Enqueue("28");
                movePoint.Enqueue("29");
                movePoint.Enqueue("30");
                movePoint.Enqueue("G");
                break;
            case "GE":
                movePoint.Enqueue("30");
                movePoint.Enqueue("29");
                movePoint.Enqueue("28");
                movePoint.Enqueue("27");
                movePoint.Enqueue("26");
                movePoint.Enqueue("25");
                movePoint.Enqueue("24");
                movePoint.Enqueue("23");
                movePoint.Enqueue("21");
                movePoint.Enqueue("E");
                break;
            case "FH":
                movePoint.Enqueue("23");
                movePoint.Enqueue("74");
                movePoint.Enqueue("36");
                movePoint.Enqueue("37");
                movePoint.Enqueue("38");
                movePoint.Enqueue("20");
                movePoint.Enqueue("19");
                movePoint.Enqueue("02");
                movePoint.Enqueue("03");
                movePoint.Enqueue("04");
                movePoint.Enqueue("05");
                movePoint.Enqueue("06");
                movePoint.Enqueue("07");
                movePoint.Enqueue("31");
                movePoint.Enqueue("32");
                movePoint.Enqueue("H");
                break;
            case "FI":
                movePoint.Enqueue("23");
                movePoint.Enqueue("74");
                movePoint.Enqueue("36");
                movePoint.Enqueue("37");
                movePoint.Enqueue("38");
                movePoint.Enqueue("27");
                movePoint.Enqueue("19");
                movePoint.Enqueue("02");
                movePoint.Enqueue("03");
                movePoint.Enqueue("04");
                movePoint.Enqueue("05");
                movePoint.Enqueue("06");
                movePoint.Enqueue("07");
                movePoint.Enqueue("31");
                movePoint.Enqueue("08");
                movePoint.Enqueue("35");
                movePoint.Enqueue("34");
                movePoint.Enqueue("I");
                break;
            case "FJ":
                movePoint.Enqueue("23");
                movePoint.Enqueue("74");
                movePoint.Enqueue("36");
                movePoint.Enqueue("37");
                movePoint.Enqueue("38");
                movePoint.Enqueue("39");
                movePoint.Enqueue("00");
                movePoint.Enqueue("40");
                movePoint.Enqueue("41");
                movePoint.Enqueue("42");
                movePoint.Enqueue("43");
                movePoint.Enqueue("J");
                break;
            case "HF":
                movePoint.Enqueue("32");
                movePoint.Enqueue("31");
                movePoint.Enqueue("07");
                movePoint.Enqueue("06");
                movePoint.Enqueue("05");
                movePoint.Enqueue("04");
                movePoint.Enqueue("03");
                movePoint.Enqueue("02");
                movePoint.Enqueue("01");
                movePoint.Enqueue("40");
                movePoint.Enqueue("00");
                movePoint.Enqueue("39");
                movePoint.Enqueue("38");
                movePoint.Enqueue("37");
                movePoint.Enqueue("36");
                movePoint.Enqueue("74");
                movePoint.Enqueue("23");
                movePoint.Enqueue("F");
                break;
            case "HN":
                movePoint.Enqueue("32");
                movePoint.Enqueue("31");
                movePoint.Enqueue("07");
                movePoint.Enqueue("06");
                movePoint.Enqueue("67");
                movePoint.Enqueue("69");
                movePoint.Enqueue("N");
                break;
            case "IF":
                movePoint.Enqueue("34");
                movePoint.Enqueue("35");
                movePoint.Enqueue("08");
                movePoint.Enqueue("31");
                movePoint.Enqueue("07");
                movePoint.Enqueue("06");
                movePoint.Enqueue("05");
                movePoint.Enqueue("04");
                movePoint.Enqueue("03");
                movePoint.Enqueue("02");
                movePoint.Enqueue("01");
                movePoint.Enqueue("40");
                movePoint.Enqueue("00");
                movePoint.Enqueue("39");
                movePoint.Enqueue("38");
                movePoint.Enqueue("37");
                movePoint.Enqueue("36");
                movePoint.Enqueue("74");
                movePoint.Enqueue("23");
                movePoint.Enqueue("F");
                break;
            case "IK":
                movePoint.Enqueue("34");
                movePoint.Enqueue("46");
                movePoint.Enqueue("47");
                movePoint.Enqueue("48");
                movePoint.Enqueue("49");
                movePoint.Enqueue("50");
                movePoint.Enqueue("51");
                movePoint.Enqueue("52");
                movePoint.Enqueue("53");
                movePoint.Enqueue("54");
                movePoint.Enqueue("14");
                movePoint.Enqueue("13");
                movePoint.Enqueue("12");
                movePoint.Enqueue("10");
                movePoint.Enqueue("11");
                movePoint.Enqueue("17");
                movePoint.Enqueue("16");
                movePoint.Enqueue("55");
                movePoint.Enqueue("56");
                movePoint.Enqueue("57");
                movePoint.Enqueue("58");
                movePoint.Enqueue("30");
                movePoint.Enqueue("29");
                movePoint.Enqueue("28");
                movePoint.Enqueue("27");
                movePoint.Enqueue("26");
                movePoint.Enqueue("25");
                movePoint.Enqueue("60");
                movePoint.Enqueue("61");
                movePoint.Enqueue("62");
                movePoint.Enqueue("63");
                movePoint.Enqueue("64");
                movePoint.Enqueue("65");
                movePoint.Enqueue("66");
                movePoint.Enqueue("44");
                movePoint.Enqueue("K");
                break;
            case "KL":
                movePoint.Enqueue("44");
                movePoint.Enqueue("45");
                movePoint.Enqueue("37");
                movePoint.Enqueue("38");
                movePoint.Enqueue("20");
                movePoint.Enqueue("19");
                movePoint.Enqueue("02");
                movePoint.Enqueue("03");
                movePoint.Enqueue("04");
                movePoint.Enqueue("05");
                movePoint.Enqueue("06");
                movePoint.Enqueue("07");
                movePoint.Enqueue("31");
                movePoint.Enqueue("08");
                movePoint.Enqueue("35");
                movePoint.Enqueue("34");
                movePoint.Enqueue("46");
                movePoint.Enqueue("L");
                break;
            case "LI":
                movePoint.Enqueue("I");
                break;
            case "NM":
                movePoint.Enqueue("69");
                movePoint.Enqueue("67");
                movePoint.Enqueue("68");
                movePoint.Enqueue("M");
                break;
            case "MH":
                movePoint.Enqueue("68");
                movePoint.Enqueue("67");
                movePoint.Enqueue("06");
                movePoint.Enqueue("07");
                movePoint.Enqueue("31");
                movePoint.Enqueue("33");
                movePoint.Enqueue("H");
                break;
            case "OC":
                movePoint.Enqueue("70");
                movePoint.Enqueue("14");
                movePoint.Enqueue("15");
                movePoint.Enqueue("C");
                break;
            case "P28":
                movePoint.Enqueue("72");
                movePoint.Enqueue("25");
                movePoint.Enqueue("26");
                movePoint.Enqueue("27");
                movePoint.Enqueue("28");
                break;
            case "QP":
                movePoint.Enqueue("71");
                movePoint.Enqueue("62");
                movePoint.Enqueue("61");
                movePoint.Enqueue("60");
                movePoint.Enqueue("72");
                movePoint.Enqueue("P");
                break;
            case "2873":
                movePoint.Enqueue("29");
                movePoint.Enqueue("30");
                movePoint.Enqueue("58");
                movePoint.Enqueue("57");
                movePoint.Enqueue("56");
                movePoint.Enqueue("73");
                break;
            case "73Q":
                movePoint.Enqueue("73");
                movePoint.Enqueue("56");
                movePoint.Enqueue("57");
                movePoint.Enqueue("58");
                movePoint.Enqueue("30");
                movePoint.Enqueue("29");
                movePoint.Enqueue("28");
                movePoint.Enqueue("27");
                movePoint.Enqueue("59");
                movePoint.Enqueue("60");
                movePoint.Enqueue("61");
                movePoint.Enqueue("62");
                movePoint.Enqueue("71");
                movePoint.Enqueue("Q");
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
    void SetDoctor()
    {
        if (hour == 6 || (hour == 7 && minute < 30))
        {
            currentPlace = "D";
            numberOfMovement = 1;
        }
        else if(hour>=7 && hour<=10)
        {
            currentPlace = "E";
            numberOfMovement = 2;
        }
        else if(hour>=11 && hour<=18)
        {
            currentPlace = "G";
            numberOfMovement = 3;
        }
        else if(hour>19 && hour<=21)
        {
            currentPlace = "E";
            numberOfMovement = 4;
        }
        else if(hour>=22)
        {
            currentPlace = "D";
            numberOfMovement = 5;
        }
    }
    void SetMyam()
    {
        if(hour==6&&minute<20)
        {
            currentPlace = "F";
            numberOfMovement = 1;
        }
        else if(hour==6&&(hour==7||minute<10))
        {
            currentPlace = "H";
            numberOfMovement = 2;
        }
        else if((hour>=7&&hour<12) || (hour==13&&minute<30))
        {
            currentPlace = "F";
            numberOfMovement = 3;
        }
        else if(hour>=13&&hour<16)
        {
            currentPlace = "I";
            numberOfMovement = 4;
        }
        else if(hour>=16&&hour<21)
        {
            currentPlace = "F";
            numberOfMovement = 5;
        }
        else if(hour<23)
        {
            currentPlace = "J";
            numberOfMovement = 6;
        }
        else
        {
            currentPlace = "F";
            numberOfMovement = 7;
        }
    }
    void SetChef()
    {
        if(hour>=6&&hour<14)
        {
            currentPlace = "N";
            numberOfMovement = 1;
        }
        else if(hour>=14&&hour<20)
        {
            currentPlace = "M";
            numberOfMovement = 2;
        }
        else if(hour>20&&hour<22)
        {
            currentPlace = "H";
            numberOfMovement = 3;
        }
        else if(hour>=22)
        {
            currentPlace = "N";
            numberOfMovement = 4;
        }
    }
    void SetPolice()
    {
        if (hour >= 6 && hour <= 8)
        {
            currentPlace = "K";
            numberOfMovement = 1;
        }
        else if (hour >= 8 && hour < 10)
        {
            currentPlace = "L";
            numberOfMovement = 2;
        }
        else if(hour>=10 && hour<11)
        {
            currentPlace = "I";
            numberOfMovement = 3;
        }
        else if(hour>=11 && hour<20)
        {
            currentPlace = "L";
            numberOfMovement = 4;
        }
        else if(hour>=20)
        {
            currentPlace = "K";
            numberOfMovement = 5;
        }
    }
    void SetShopKeeper()
    {
        if(hour<9)
        {
            currentPlace = "O";
            numberOfMovement = 1;
        }
        else if(hour<12)
        {
            currentPlace = "C";
            numberOfMovement = 2;
        }
        else if(hour<14)
        {
            currentPlace = "O";
            numberOfMovement = 3;
        }
        else if(hour<21)
        {
            currentPlace = "C";
            numberOfMovement = 4;
        }
        else
        {
            currentPlace = "O";
            numberOfMovement = 5;
        }
    }
    void SetTeacher()
    {
        if(hour<8)
        {
            currentPlace = "Q";
            numberOfMovement = 1;
        }
        else if(hour<15)
        {
            currentPlace = "P";
            numberOfMovement = 2;
        }
        else if(hour<18)
        {
            currentPlace = "28";
            numberOfMovement = 3;
        }
        else if(hour<21)
        {
            currentPlace = "73";
            numberOfMovement = 4;
        }
        else
        {
            currentPlace = "Q";
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
    void GetDoctorPath()
    {
        if (numberOfMovement == 1 && (hour >= 7))
        {
            numberOfMovement++;
            GetPath("D", "E");
        }
        else if (numberOfMovement == 2 && (hour>=11))
        {
            numberOfMovement++;
            GetPath("E", "G");
        }
        else if(numberOfMovement==3 && (hour>=18))
        {
            numberOfMovement++;
            GetPath("G", "E");
        }
        else if(numberOfMovement==4 && (hour>=21))
        {
            numberOfMovement++;
            GetPath("E", "D");
        }
    }
    void GetMyamPath()
    {
        if(numberOfMovement==1&&(hour>=6))
        {
            numberOfMovement++;
            GetPath("F", "H");
        }
        else if(numberOfMovement==2&&(hour>=7))
        {
            numberOfMovement++;
            GetPath("H", "F");
        }
        else if(numberOfMovement==3&&(hour>=12))
        {
            numberOfMovement++;
            GetPath("F", "I");
        }
        else if(numberOfMovement==4&&(hour>=14))
        {
            numberOfMovement++;
            GetPath("I", "F");
        }
        else if(numberOfMovement==5&&(hour>=21))
        {
            numberOfMovement++;
            GetPath("F", "J");
        }
        else if(numberOfMovement==6&&(hour>=23))
        {
            numberOfMovement++;
            GetPath("J", "F");
        }
    }
    void GetChefPath()
    {
        if (numberOfMovement==1 && (hour>=14&&hour<20))
        {
            numberOfMovement++;
            GetPath("N","M");
        }
        else if (numberOfMovement==2 && (hour>=20&&hour<22))
        {
            numberOfMovement++;
            GetPath("M", "H");
        }
        else if (numberOfMovement==3 && hour >= 22)
        {
            numberOfMovement++;
            GetPath("H", "N");
        }
    }
    void GetPolicePath()
    {
        if (numberOfMovement==1&& (hour >= 7 || (hour <= 8 && minute<30)))
        {
            numberOfMovement++;
            GetPath("K", "L");
        }
        else if (hour >= 9 || (hour < 10 && minute<30))
        {
            numberOfMovement++;
            GetPath("L", "I");
        }
        else if (hour >= 11 && hour < 21)
        {
            numberOfMovement++;
            GetPath("I", "K");
        }
    }
    void GetShopKeeperPath()
    {
        if (numberOfMovement == 1 && hour >= 8)
        {
            numberOfMovement++;
            GetPath("O", "C");
        }
        else if (numberOfMovement == 2 && hour>=11)
        {
            numberOfMovement++;
            GetPath("C", "O");
        }
        else if (numberOfMovement == 3 && hour >= 13)
        {
            numberOfMovement++;
            GetPath("O", "C");
        }
        else if (numberOfMovement == 4 && hour >= 20)
        {
            numberOfMovement++;
            GetPath("C", "O");
        }
    }
    void GetTeacherPath()
    {
        if(numberOfMovement==1 && (hour>=7))
        {
            numberOfMovement++;
            GetPath("Q", "P");
        }
        else if (numberOfMovement == 2 && (hour >= 14))
        {
            numberOfMovement++;
            GetPath("P", "28");
        }
        else if (numberOfMovement == 3 && (hour >= 17))
        {
            numberOfMovement++;
            GetPath("28", "73");
        }
        else if (numberOfMovement == 4 && (hour >= 20))
        {
            numberOfMovement++;
            GetPath("73", "Q");
        }
    }
}