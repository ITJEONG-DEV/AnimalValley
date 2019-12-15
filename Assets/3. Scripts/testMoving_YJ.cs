using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum PLAYERSTATE
{
    PLAYERSTATE_IDLE,
    PLAYERSTATE_RUN,
    PLAYERSTATE_USE_ITEM,    //상호작용
    PLAYERSTATE_ACTION,  //도구
    PLAYERSTATE_ATTACK,
    PLAYERSTATE_DIE
}
public class testMoving_YJ : MonoBehaviour
{
    GameObject you;

    Collider co;
    public static string itemCode;
    [SerializeField] float gravity = 9.81f;      //중력
    [SerializeField] float runSpeed = 5.0f;      //달리는 속도
    [SerializeField] float mouseSensitivity = 2.0f;  //카메라 마우스 감도
    [SerializeField]
    float jumpPower = 5.0f;
    GameObject bullet;
    [SerializeField] GameObject[] bulletPrefab;
    [SerializeField] Transform bulletSponeLoca;
    [SerializeField] public int player_HP;
    [SerializeField] int bullet_Speed;
    Transform myTransform;
    Transform model;
    int Weapon_case;
    Animator ani;
    string temp = null;
    string temp_1 = null;
    Vector3 mouseMove;
    Vector3 move;
    Transform cameraParentTransform;
    Transform cameraTransform;
    CharacterController cc;
    private PLAYERSTATE playerState;
    private Vector3 playerDir;
    GameObject trunk_temp;
    private float verticalVelocity;
    public AudioSource treeChoppingSound;
    bool hasShovel = false;
    bool hasWatering = false;
    bool tree_near=false;
    Vector3 trunkPos;
    // Use this for initialization
    void Awake()
    {    
        //bullet = bulletPrefab[0];
        myTransform = transform;
        model = transform.GetChild(0);
        ani = model.GetComponent<Animator>();
        // cameraTransform = Camera.main.transform;
        cameraTransform = transform.parent.GetChild(0).GetChild(0);
        cameraParentTransform = cameraTransform.parent;
        cc = GetComponent<CharacterController>();
        treeChoppingSound = GetComponent<AudioSource>();

    }
    GameObject tree_temp;
    bool wetCheck = false;
    float angle_y;
    GameObject plowGround;
    public static string groundName=null;
    string baseName = null;
    string dir = null;
    bool trunk_near = false;
    bool hasTool = false;
    int treeCount = 0;
    private void OnTriggerEnter(Collider other)   //보는 방향이랑 구간으로 개간하는 땅 정한다.
    {

        if (other.tag == "ground" || other.tag == "wetGround")
        {
      
            Debug.Log("밑의 지반 이름" + other.gameObject.name);

            //Debug.Log("트리거는 됨");
            if (other.gameObject.name == "(3,0)")
            {

                //Debug.Log(angle_y);
                if (dir=="D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(4,0)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(2,0)";
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = "(3,1)";
                }
                                                                                     
            }
            else if (other.gameObject.name == "(3,1)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
                
                //Debug.Log(angle_y);
                if (dir=="D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(4,1)";

                }
                else if(dir=="S") //캐릭터 기준 아래 보는중
                {
                    groundName = "(3,0)";
                   
                }
                else if (dir=="A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(2,1)";
                }
                else if (dir=="W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = null;
                }
            }
            else if (other.gameObject.name == "(4,0)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
      
                //Debug.Log(angle_y);
                if (dir=="D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;

                }
                else if (dir=="S") //캐릭터 기준 아래 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir=="A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(3,0)";
                }
                else if (dir=="W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = "(4,1)";
                }

            }
            else if (other.gameObject.name == "(4,1)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
 
                //Debug.Log(angle_y);
                if (dir=="D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = null; 
                    hasShovel = false;
                }
                else if (dir=="S") //캐릭터 기준 아래 보는중
                {
                    groundName = "(4,0)";
                    
                }
                else if (dir=="A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(3,1)";
                    
                }
                else if (dir=="W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }

            }
            else if (other.gameObject.name == "(2,0)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
 
                //Debug.Log(angle_y);
                if (dir=="D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(3,0)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(1,0)";
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = "(2,1)";
                }

            }
            else if (other.gameObject.name == "(2,1)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
              
                //Debug.Log(angle_y);
                if (dir == "D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(3,1)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName = "(2,0)";
                    
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(1,1)";
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }

            }
            else if (other.gameObject.name == "(1,0)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
            
                //Debug.Log(angle_y);
                if (dir == "D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(2,0)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(0,0)";
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = "(1,1)";
                }

            }
            else if (other.gameObject.name == "(1,1)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
           
                //Debug.Log(angle_y);
                if (dir == "D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(2,1)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName = "(1,0)";
                    
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = "(0,1)";
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }

            }
            else if (other.gameObject.name == "(0,1)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
  
                //Debug.Log(angle_y);
                if (dir == "D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName= "(1,1)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName ="(0,0)";
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }

            }
            else if (other.gameObject.name == "(0,0)")
            {
                //Debug.Log(this.transform.GetChild(0).forward);
        
                //Debug.Log(angle_y);
                if (dir == "D")  // 캐릭터 기준 오른쪽 보는중
                {
                    groundName = "(1,0)";

                }
                else if (dir == "S") //캐릭터 기준 아래 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir == "A")   //캐릭터 기준 왼쪽 보는중
                {
                    groundName = null;
                    hasShovel = false;
                }
                else if (dir == "W")//캐릭터 기준 위쪽 보는중
                {
                    groundName = "(0,1)";
                }

            }


        }
        else if(other.tag=="tree")
        {
            tree_temp = other.gameObject;
            Debug.Log(tree_temp.transform.name);
            tree_near = true;
            trunkPos = other.transform.position;
        }
        else if(other.tag=="trunk")
        {
            trunk_temp = other.gameObject;
            trunk_near = true;
        }

    }
    string seed_name=null;
    bool alive = true;

    public void faint()
    {
        StartCoroutine("dead");
    }

    IEnumerable dead()
    {
        ani.SetBool("die", true);
        yield return new WaitForSeconds(0.5f);
        alive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            Balance();
            CameraDistanceCtrl();
            showTool(itemCode);

            move.y -= gravity * Time.deltaTime;
            MoveCalc(1.0f);

            cc.Move(move * Time.deltaTime);

            #region 캐릭터 이동 애니메이션
            //캐릭터 회전
            if (Input.GetKey(KeyCode.D))
            {
                dir = "D";
                ani.SetBool("walk", true);
                playerState = PLAYERSTATE.PLAYERSTATE_RUN;
            }
            if (Input.GetKey(KeyCode.A))
            {
                dir = "A";
                ani.SetBool("walk", true);
                playerState = PLAYERSTATE.PLAYERSTATE_RUN;
            }
            if (Input.GetKey(KeyCode.W))
            {
                dir = "W";
                ani.SetBool("walk", true);
                playerState = PLAYERSTATE.PLAYERSTATE_RUN;
            }
            if (Input.GetKey(KeyCode.S))
            {
                dir = "S";
                ani.SetBool("walk", true);
                playerState = PLAYERSTATE.PLAYERSTATE_RUN;
            }

            //키를 눌렀다 뗐을 경우
            if (Input.GetKeyUp(KeyCode.W))
            {

                ani.SetBool("walk", false);
                playerState = PLAYERSTATE.PLAYERSTATE_IDLE;

            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                ani.SetBool("walk", false);
                playerState = PLAYERSTATE.PLAYERSTATE_IDLE;

            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                ani.SetBool("walk", false);
                playerState = PLAYERSTATE.PLAYERSTATE_IDLE;

            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                ani.SetBool("walk", false);
                playerState = PLAYERSTATE.PLAYERSTATE_IDLE;

            }

            //캐릭터 점프(GetKeyDown은 최초 누르는 한 번만 호출됨)
            //if (playerState != PLAYERSTATE.PLAYERSTATE_JUMP)
            //{
            //    if (Input.GetKeyDown(KeyCode.Space))
            //    {
            //        Vector3 moveVec = new Vector3(0, jumpPower, 0);
            //        cc.Move(moveVec * Time.deltaTime);
            //        ani.SetBool("Jump", true);
            //        playerState = PLAYERSTATE.PLAYERSTATE_JUMP;
            //    }
            //}
            //if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    ani.SetBool("Jump", false);
            //    playerState = PLAYERSTATE.PLAYERSTATE_IDLE;
            //}

            //캐릭터 구르기
            //if (playerState != PLAYERSTATE.PLAYERSTATE_Roll)
            //{
            //    if (Input.GetKeyDown(KeyCode.E))
            //    {
            //        StartCoroutine(Rolling());
            //    }
            //}
            #endregion

            #region 스킬
            if (Input.GetKey(KeyCode.Mouse1))       //마우스 오른쪽 키
            {

                if (itemCode == "RSG0")
                {
                    seed_name = "RSG0";
                }
                else if (itemCode == "RSG1")
                {
                    seed_name = "RSG1";
                }
                else if (itemCode == "RSV0")
                {
                    seed_name = "RSV0";
                }
                else if (itemCode == "RSV1")
                {
                    seed_name = "RSV1";
                }
                else if (itemCode == "RSV2")
                {
                    seed_name = "RSV2";
                }
                else if (itemCode == "RSV3")
                {
                    seed_name = "RSV3";
                }
                else if (itemCode == "RSV4")
                {
                    seed_name = "RSV4";
                }
                else if (itemCode == "RSV8")
                {
                    seed_name = "RSV8";
                }
                else if (itemCode == "RSCR")
                {
                    seed_name = "RSCR";
                }
                else if (itemCode == "RSCW")
                {
                    seed_name = "RSCW";
                }
                else if (itemCode == "RSCS")
                {
                    seed_name = "RSCS";
                }


                if (itemCode.Substring(0, 2) == "RS")
                {
                    string[] names = new string[2];
                    names[0] = groundName;
                    names[1] = seed_name;
                    ani.SetBool("block", true);
                    temp_1 = "block";
                    if (groundName != null)
                        GameObject.Find("House_6").GetComponent<cropManager>().SendMessage("sowSeed", names);
                }


            }
            if (Input.GetKey(KeyCode.Mouse0))       //마우스 왼쪽 키
            {

                if (groundName != null)
                    GameObject.Find("House_6").GetComponent<cropManager>().SendMessage("growingCrops", groundName);
                playerState = PLAYERSTATE.PLAYERSTATE_ACTION;

                if (itemCode == null)  //액션, 상호작용
                {
                    if (trunk_near)
                        StartCoroutine("trunkPicking");

                }
                else if (itemCode.Substring(0, 3) == "TTS")    // 낫
                {
                    ani.SetBool("sickle", true);
                    temp = "sickle";
                    GameObject.Find("House_6").GetComponent<cropManager>().SendMessage("harvestCrops", groundName);

                }
                else if (itemCode.Substring(0, 3) == "TTH")    //괭이
                {
                    ani.SetBool("axe(pick)", true);
                    temp = "axe(pick)";
                    hasShovel = true;
                    if (hasShovel == true && groundName != null)
                    {
                        Debug.Log(groundName);
                        GameObject.Find("House_6").GetComponent<cropManager>().SendMessage("plowGround", groundName);
                        hasShovel = false;
                        groundName = null;
                    }

                }
                else if (itemCode.Substring(0, 3) == "TTP")    //곡괭이
                {
                    ani.SetBool("axe(pick)", true);
                    temp = "axe(pick)";

                }
                else if (itemCode.Substring(0, 3) == "TTW")    //물뿌리개
                {
                    ani.SetBool("watering", true);
                    temp = "watering";
                    hasWatering = true;
                    if (hasWatering == true && groundName != null)
                    {
                        GameObject.Find("House_6").GetComponent<cropManager>().SendMessage("waterGround", groundName);
                        hasWatering = false;
                    }

                }
                else if (itemCode.Substring(0, 3) == "TTA")     //도끼
                {
                    StartCoroutine("chopping");

                }
                else if (itemCode.Substring(0, 3) == "TTF")     //낚싯대
                {
                    ani.SetBool("fishing", true);
                    temp = "fishing";
                }
                else if (itemCode.Substring(0, 3) == "THW")      //사냥,무기
                {
                    ani.SetBool("attack1", true);
                    temp = "attack1";
                    playerState = PLAYERSTATE.PLAYERSTATE_ATTACK;
                }


            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                ani.SetBool(temp, false);
            }

            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                ani.SetBool(temp_1, false);

            }


            //if (playerState == PLAYERSTATE.PLAYERSTATE_ATTACK)
            //{
            //    if (Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 왼쪽 클릭
            //    {
            //        StartCoroutine(Attack(Weapon_case));   //무기의 종류에따라 
            //    }
            //}

            #endregion
        }
    }

    public void showTool(string itemCode)
    {   
        //GameObject tool = transform.GetChild(0).GetComponent<character_helper>().weapon_locator;
        GameObject tool = transform.GetChild(0).transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Chest_M").transform.Find("Scapula_R").transform.Find("Shoulder_R").transform.Find("Elbow_R").transform.Find("Wrist_R").transform.Find("WeaponR_locator").gameObject;
        Vector3 toolPos = transform.GetChild(0).transform.Find("Root_M").transform.Find("Spine1_M").transform.Find("Chest_M").transform.Find("Scapula_R").transform.Find("Shoulder_R").transform.Find("Elbow_R").transform.Find("Wrist_R").transform.Find("WeaponR_locator").transform.position;
        //위는 무기 위치
        Vector3 playerPos = transform.position;
        Vector3 difference = toolPos - playerPos;
        GameObject newTool;
        Debug.Log(tool.name);
        GameObject[] family;
        Debug.Log(tool.transform.position);
        if (itemCode == null)  //액션, 상호작용
        {
            if(hasTool==true)
            {
                for (int i= 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true)
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                }
                hasTool = false;
            }
        }
        else if (itemCode.Substring(0, 3) == "TTS")    // 낫
        {
            if(hasTool==true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "TTSM Variant")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "TTSM Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {

                    if (tool.transform.GetChild(i).gameObject.name == "TTSM Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }

        }
        else if (itemCode.Substring(0, 3) == "TTH")    //괭이
        {
            if (hasTool == true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "TTHM (1) Variant")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "TTHM (1) Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {

                    if (tool.transform.GetChild(i).gameObject.name == "TTHM (1) Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }

        }
        else if (itemCode.Substring(0, 3) == "TTP")    //곡괭이
        {
            if (hasTool == true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "TTPM Variant")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "TTPM Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {

                    if (tool.transform.GetChild(i).gameObject.name == "TTPM Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }
        }
        else if (itemCode.Substring(0, 3) == "TTW")    //물뿌리개
        {
            if (hasTool == true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "WateringCanPrefab Variant")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "WateringCanPrefab Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {

                    if (tool.transform.GetChild(i).gameObject.name == "WateringCanPrefab Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }


        }
        else if (itemCode.Substring(0, 3) == "TTA")     //도끼  
        {

            if (hasTool == true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "axe (2) Variant")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "axe (2) Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
           
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                
                    if (tool.transform.GetChild(i).gameObject.name == "axe (2) Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }
           
            //GetComponent<character_helper>().weapon_locator.transform.position, transform.GetComponent<character_helper>().weapon_locator.transform.rotation
        }
        else if (itemCode.Substring(0, 3) == "TTF")     //낚싯대
        {
            if (hasTool == true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "TTFM")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "TTFM")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {

                    if (tool.transform.GetChild(i).gameObject.name == "TTFM")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }
        }
        else if (itemCode.Substring(0, 3) == "THW")      //사냥,무기
        {
            if (hasTool == true)
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {
                    if (tool.transform.GetChild(i).gameObject.activeSelf == true && tool.transform.GetChild(i).gameObject.name != "SwordToyB Variant")
                    {
                        tool.transform.GetChild(i).gameObject.SetActive(false);
                    }
                    if (tool.transform.GetChild(i).gameObject.name == "SwordToyB Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }
            }
            else
            {
                for (int i = 0; i < tool.transform.childCount; i++)
                {

                    if (tool.transform.GetChild(i).gameObject.name == "SwordToyB Variant")
                        tool.transform.GetChild(i).gameObject.SetActive(true);

                }

            }

        }


    }

    IEnumerator trunkPicking()
    {
        ani.SetBool("pickUp", true);
        yield return new WaitForSeconds(1.5f);
        Destroy(trunk_temp);
        temp = "pickUp";
        trunk_near = false;
        ani.SetBool(temp, false);
    }
    IEnumerator chopping()
    {
        ani.SetBool("axe(pick)", true);
        temp = "axe(pick)";


        yield return new WaitForSeconds(1.7f);
        if (tree_near)
        {
            if (treeCount == 3)
            {
                Destroy(tree_temp);
                GameObject.Find("House_6").GetComponent<cropManager>().SendMessage("choppingTree", trunkPos);
                treeCount = 0;
            }
            else
            {
                treeCount++;
            }
            Debug.Log(treeCount);
            treeChoppingSound.Play();
        }
        tree_near = false;
    }

    //IEnumerator Attack(int weapon)
    //{
    //    playerState = PLAYERSTATE.PLAYERSTATE_ATTACK;
    //    ani.SetBool("Attack", true);

    //    if (weapon == 1) // dagger
    //        yield return new WaitForSeconds(1);
    //    else
    //    {   // wand
    //        yield return new WaitForSeconds(0.3f);
    //        GameObject fire = Instantiate(bullet, bulletSponeLoca);
    //        fire.transform.parent = null;
    //        // fire.GetComponent<Rigidbody>().MovePosition(bulletSponeLoca.transform.position + new Vector3(300, 0, 0));
    //        yield return new WaitForSeconds(0.3f);
    //        fire.GetComponent<Rigidbody>().AddForce(bulletSponeLoca.forward * bullet_Speed * Time.deltaTime, ForceMode.Impulse);
    //        yield return new WaitForSeconds(0.4f);
    //    }
    //    ani.SetBool("Attack", false);
    //    playerState = PLAYERSTATE.PLAYERSTATE_IDLE;
    //}

    //IEnumerator Rolling()   // 구르기 바로 변수를 바꾸면 애니메이션이 멈춰버려서 시간차를 두고 멈추게 함.
    //{
    //    PLAYERSTATE past = playerState;
    //    ani.SetBool("Rolling", true);
    //    playerState = PLAYERSTATE.;

    //    yield return new WaitForSeconds(0.8f);

    //    ani.SetBool("Rolling", false);
    //    playerState = past;
    //}
    void LateUpdate()
    {
        cameraParentTransform.position = myTransform.position;  //캐릭터의 머리 높이쯤
        //mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity, Input.GetAxisRaw("Mouse X") * mouseSensitivity, 0);   //마우스의 움직임을 가감
        //if (mouseMove.x < -20)  //높이는 제한을 둔다. 슈팅 게임이라면 거의 90에 가깝게 두는게 좋을수도 있다.
        //    mouseMove.x = -20;
        //else if (50 < mouseMove.x)
        //    mouseMove.x = 50;
        ////여기서 헷갈리면 안 되는게 GetAxisRaw("Mouse XY") 는 실제 마우스의 움직임의 x좌표 y좌표를 가져오지만 회전은 축 기준이라 x가 위아래고 y가 좌우이다.

        //cameraParentTransform.localEulerAngles = mouseMove;

        
        // raycast
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
            {
                if (hit.collider.gameObject.tag == "NPC")
                {
                    you = hit.collider.gameObject;
                    you.SendMessage("Speak");
                }
            }
        }
    }

    void Balance()
    {
        if (myTransform.eulerAngles.x != 0 || myTransform.eulerAngles.z != 0)   //모종의 이유로 기울어진다면 바로잡는다.
            myTransform.eulerAngles = new Vector3(0, myTransform.eulerAngles.y, 0);
    }

    void CameraDistanceCtrl()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f); //휠로 카메라의 거리를 조절한다.
        if (-1 < Camera.main.transform.localPosition.z)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -1);    //최대로 가까운 수치
        else if (Camera.main.transform.localPosition.z < -5)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -5);    //최대로 먼 수치
    }

    void MoveCalc(float ratio)
    {
        float tempMoveY = move.y;
        move.y = 0; //이동에는 xz값만 필요하므로 잠시 저장하고 빼둔다.
        Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //대각선 이동이 루트2 배의 속도를 갖는 것을 막기위해 속도가 1 이상 된다면 노말라이즈 후 속도를 곱해 어느 방향이든 항상 일정한 속도가 되게 한다.
        float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude; //sqrMagnitude연산을 두 번 할 필요 없도록 따로 저장
        inputMoveXZ = myTransform.TransformDirection(inputMoveXZ);
        if (inputMoveXZMgnitude <= 1)
            inputMoveXZ *= runSpeed;
        else
            inputMoveXZ = inputMoveXZ.normalized * runSpeed;

        //조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록 한다.
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraParentTransform.rotation;
            cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.
            //자연스러움을 위해 Slerp로 회전시킨다.

            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, cameraRotation, 10.0f * Time.deltaTime);
            if (move != Vector3.zero)//Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                model.rotation = Quaternion.Slerp(model.rotation, characterRotation, 10.0f * Time.deltaTime);
            }

            //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
            move = Vector3.MoveTowards(move, inputMoveXZ, ratio * runSpeed);
        }
        else
        {
            //조작이 없으면 서서히 멈춘다.
            move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * runSpeed * ratio);
        }
        //  float speed = move.sqrMagnitude;    //현재 속도를 애니메이터에 세팅한다.
        //   ani.SetFloat("Speed", speed);
        move.y = tempMoveY; //y값 복구
        
    }

    void GradientCheck()
    {
        ani.SetBool("isGrounded", true);
        if (Physics.Raycast(myTransform.position, Vector3.down, 0.2f))
        //경사로를 구분하기 위해 밑으로 레이를 쏘아 땅을 확인한다.
        //CharacterController는 밑으로 지속적으로 Move가 일어나야 땅을 체크하는데 -y값이 너무 낮으면 조금만 경사져도 공중에 떠버리고 너무 높으면 절벽에서 떨어질때 추락하듯 바로 떨어진다.
        //완벽하진 않지만 캡슐 모양의 CharacterController에서 절벽에 떨어지기 직전엔 중앙에서 밑으로 쏘아지는 레이에 아무것도 닿지 않으므로 그때만 -y값을 낮추면 경사로에도 잘 다니고
        //절벽에도 자연스럽게 천천히 떨어지는 효과를 줄 수 있다.
        {
            move.y = -5;
        }
        else
            move.y = -1;
    }

}
