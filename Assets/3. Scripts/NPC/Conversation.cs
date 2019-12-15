using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour
{
    public GameObject mynameis;
    public GameObject speech;
    public Sprite profile;
    public GameObject ProFile;
    public GameObject conversation;

    // Start is called before the first frame update

    Dictionary<string, List<string>> NpcConversation = new Dictionary<string, List<string>>()
        {
            {"PPAP", new List<string>()},
            {"Police", new List<string>()},
            {"Doctor", new List<string>()},
            {"Myam", new List<string>()},
            {"Shopkeeper", new List<string>()},
            {"Chef", new List<string>()},
            {"Teacher", new List<string>()}
    };

    void Start()
    {
        conversation.SetActive(false);
        ProFile.SetActive(false);
        #region 대사지롱
        NpcConversation["PPAP"].Add("우리 마을에서 잘 지내주길 바라네.");
        NpcConversation["PPAP"].Add("자네 밥은 먹고 다니나?");
        NpcConversation["PPAP"].Add("요즘 젊은이들은... 떼잉~ 쯧!");
        NpcConversation["PPAP"].Add("마음이 복잡할 땐 바닷가에 한 번 가보게. 생각을 정리하는 데 도움이 될 걸세.");
        NpcConversation["PPAP"].Add("젊을 때만 할 수 있는 일도 있는 것일세.");
        NpcConversation["PPAP"].Add("마을 사람들과는 많이 친해졌는지 모르겠군. 모두들 알고 보면 따뜻하고 좋은 사람들이니, 모쪼록 친해지길 바라네.");
        NpcConversation["PPAP"].Add("농작물들은 잘 자라고 있나?");
        NpcConversation["PPAP"].Add("필요한 게 있으면 언제든지 말하게. 마을사무소에서는 많은 일을 처리한다네.");
        NpcConversation["PPAP"].Add("내가 이 자리까지 올라오는 데에는 참 많은 일들이 있었지. 언젠가 자네에게 인생이 무엇인지에 대해 제대로 이야기 해 주겠네. 말 나온 김에 내일은 어떤가?");
        NpcConversation["PPAP"].Add("내가 준 도구들은 잘 쓰고 있나? 항상 물건을 소중히 다루는 법을 배우게.");

        NpcConversation["Police"].Add("안녕하십니까! 오늘도 좋은 하루입니다!");
        NpcConversation["Police"].Add("오늘도 순찰이냐구요? 하하하! 맞습니다! 마을의 안전을 위해서라면 이 정도는 거뜬하죠!");
        NpcConversation["Police"].Add("꽃을 가꾸는 일은 참 보람찬 것 같습니다. 매일매일 물을 주다 보면 생명의 소중함을 새롭게 느끼게 됩니다. 농사를 하는 것도 비슷한 느낌이 들겠죠?");
        NpcConversation["Police"].Add("제가 딱~ 지켜보고 있습니다!");
        NpcConversation["Police"].Add("순찰을 하다 보면 마을의 작은 곳까지 볼 수 있게 됩니다.");
        NpcConversation["Police"].Add("혹시 수상한 사람 못 보셨습니까?");
        NpcConversation["Police"].Add("얼른 일을 마치고 맛있는 걸 먹으러 가고 싶습니다. 매일매일 걸어다니다 보니 소화가 너무 잘 됩니다.");
        NpcConversation["Police"].Add("아침에 일찍 일어나서 맡는 공기가 가장 상쾌하고 기분 좋은 것 같습니다.");
        NpcConversation["Police"].Add("제가 뭐 도와드릴 건 없습니까? 불편한 점이 있다면 언제든 찾아오셔도 됩니다!");
        NpcConversation["Police"].Add("배가... 고픕니다...");

        NpcConversation["Doctor"].Add("...안녕.");
        NpcConversation["Doctor"].Add("그 아이... 아냐. 너는 몰라도 돼.");
        NpcConversation["Doctor"].Add("어디 아픈 데는 없지? 없길 바래. 가급적 병원에 손님이 없으면 좋겠으니까.");
        NpcConversation["Doctor"].Add("왜 매일 해가 뜨는 걸까... 해가 뜨면 출근해야 하잖아.");
        NpcConversation["Doctor"].Add("농사 그거 귀찮지 않아? 매일매일 뭔가 돌보고 가꾸고 신경쓰는 일... 나는 힘들어서 못 할 것 같아.");
        NpcConversation["Doctor"].Add("그거 알아? 척추수술 비용은 약 1700만원이라는 거. ...그냥, 알고 있으라고.");
        NpcConversation["Doctor"].Add("출근하기 싫다... 매일매일 주말이었으면 좋겠어.");
        NpcConversation["Doctor"].Add("돈은 함부로 쓰는 게 아냐. 아무 생각 없이 살다가는 네 인생이 망가질 수도 있다는 걸 항상 명심해.");
        NpcConversation["Doctor"].Add("...짜증나. 좀 있다 와줄래?");
        NpcConversation["Doctor"].Add("거북목 조심해.");

        NpcConversation["Myam"].Add("먐먐! 먀먀먐 먐먀~ (안녕! 오늘도 좋아~)");
        NpcConversation["Myam"].Add("먀먀먐 먀먀 먐 먀? 먀먐 먀먐먀~ (의사선생님 못 봤어? 어디 계실까~)");
        NpcConversation["Myam"].Add("먀먐 먐 먀먀먀먀 먀먐먀 먐먀! 먐먀~ (우리 마을에는 예쁜 꽃이 많아! 좋아~!)");
        NpcConversation["Myam"].Add("먀 먀먐 먀먀먐 먀먐먀? 먐묨먐먀먀 먁 먀먐먀먀~ 먀먐먀! (저기 연못 가봤어? 물고기들이 막 돌아다녀~ 귀여워!)");
        NpcConversation["Myam"].Add("먀먀먀 먀먀묘먐 먐 먀먀 먀먐먀 먀먀먀 먀먐먐 먐먐먀먀먀 먀먀. 먐먐먀 먐 먀먐먐 먐 먐먀~ (구름이 흘러가는 걸 보고 있으면 시간이 어떻게 지나가는지 모르겠어. 하늘은 참 재밌는 것 같아~!)");
        NpcConversation["Myam"].Add("먐먀 먀먐먀먐먀 먁먀? 먀먐먀~ (벌레 관찰해본 적 있어? 재밌어~)");
        NpcConversation["Myam"].Add("먀먀 먐 먐먐먀? 먀먐 먀먀 먐먀~ (선생님 참 착하지? 나는 선생님 좋아!)");
        NpcConversation["Myam"].Add("먐먐 먀먀먐 먀먐먐먐 먐 먀먀? 먀먐 먀먐먐 먐 먐미먐미 먀먐먐 먀먀~ 먀먀먀? (비밀 장소라는 거 알아? 나는 비밀 장소 많이많이 가지고 있어~ 부럽지?)");
        
        NpcConversation["Shopkeeper"].Add("안녕하세요~ 오늘도 행복한 하루가 됐으면 좋겠네요~");
        NpcConversation["Shopkeeper"].Add("상점에 새 물건이 들어왔는데 둘러보시고 가세요~ 오늘만 살 수 있는 물건들이랍니다?");
        NpcConversation["Shopkeeper"].Add("하루를 마치고 장부를 정리하는 시간이 저에게는 가장 소중해요~ 소중한 시간~ 소중한 돈~");
        NpcConversation["Shopkeeper"].Add("쌓여 있는 것들을 보면 왠지 모르게 안심이 되지 않나요? 차곡차곡 쌓여 있는 수건... 차곡차곡 쌓여 있는 책... 차곡차곡 모아 놓은 돈...");
        NpcConversation["Shopkeeper"].Add("먼지는 다 어디서 오는 걸까요? 매일매일 청소하는 것도 참 힘든 일이에요~");
        NpcConversation["Shopkeeper"].Add("가게를 관리하다 보면 참 이상한 일이 많이 생기는 것 같아요. 농사를 하면서는 이상한 일이 없나요~?");
        NpcConversation["Shopkeeper"].Add("저희 가게만큼 싸고 좋은 물건들을 파는 곳이 또 없죠~ 밖에서는 이런 물건 못 구하실 거에요~ 다들 그걸 잘 모르시더라~");
        NpcConversation["Shopkeeper"].Add("돈을 모으는 것도 중요하지만, 그만큼 잘 쓰는 것도 중요하다고 생각해요~");

        NpcConversation["Chef"].Add("앗... 안녕. 이런 데서 마주치네... 반가워.");
        NpcConversation["Chef"].Add("나한테 할 말 있어...?");
        NpcConversation["Chef"].Add("나는... 생각을 정리하고 싶을 때 무뎌진 칼들을 갈아. 같은 일을 반복하다 보면 어느 새 날카롭게 칼이 갈려 있고... 그걸 보면 뭔가 충족되는 기분이 들어.");
        NpcConversation["Chef"].Add("이 마을 사람들은 너무 착한 것 같아... 매일매일 내 음식을 맛있다고 해 주는 걸.");
        NpcConversation["Chef"].Add("배가 고플 땐 언제든지 우리 가게로 찾아와... 나는 어차피 할 줄 아는 게 이것뿐이고... 밥 한 끼 해주는 것 정도는 전혀 어렵지 않으니까...응.");
        NpcConversation["Chef"].Add("좋아하는 음식같은 거 있어? 요즘 새로운 메뉴 개발중인데... 아이디어가 모자라.");
        NpcConversation["Chef"].Add("사실 나도 농사를 지어 보고 싶은 마음이 항상 있어. 갓 딴 채소만큼 신선한 게 없거든... ");
        NpcConversation["Chef"].Add("참깨빵 위에 순쇠고기 패티 두 장.. 특별한 소스에 양상추, 치즈, 피클 양파까지... ");
        NpcConversation["Chef"].Add("\"잘 먹었습니다\" 라는 말이 가장 듣기 좋은 것 같아.");

        NpcConversation["Teacher"].Add("아이들은 참 순수한 것 같아요.");
        NpcConversation["Teacher"].Add("모두가 항상 평온하고 행복한 하루를 보냈으면 좋겠습니다.");
        NpcConversation["Teacher"].Add("낚시는 세월을 낚는 것이라는 말이 있잖아요? 그 말을 하루하루 느끼며 살아가고 있습니다.");
        NpcConversation["Teacher"].Add("바쁘게 살아가는 사람들을 보고 있자면, 모두 낚시를 하라고 권하고 싶어요. 삶의 방식이 바뀌니까요.");
        NpcConversation["Teacher"].Add("가장 낚고 싶은 물고기는 뭔가요?");
        NpcConversation["Teacher"].Add("그거 아세요? 오징어와 문어는 심장이 세 개나 있답니다. 대단하지 않나요?");
        NpcConversation["Teacher"].Add("순간들을 소중히 여기다 보면, 긴 세월은 저절로 흘러간답니다.");
        NpcConversation["Teacher"].Add("저는 바다가 참 좋아요.");
        NpcConversation["Teacher"].Add("인내할 수 있는 사람은 그가 바라는 것은 무엇이든지 손에 넣을 수 있다고 책에서 읽었어요. 저는 이 말을 듣고 바로 낚시가 떠올랐답니다.");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator Chat(string narrator, string narration)
    {
        mynameis.GetComponent<Text>().text = narrator;
        string writerText = "";
        for(int i = 0; i < narration.Length; i++)
        {
            writerText += narration[i];
            speech.GetComponent<Text>().text = writerText;

            //yield return new WaitForSeconds(3.0f);
            yield return null;
        }
    }
    
    void Speak()
    {
        conversation.SetActive(true);
        ProFile.SetActive(true);
        System.Random rand = new System.Random();
        string namee = this.name;
        int random = rand.Next(0, NpcConversation[namee].Count - 1); 
        string nayong = NpcConversation[namee][random];

        Debug.Log(namee);
        Debug.Log(nayong);
        Debug.Log(random);
        StartCoroutine(Chat(namee, nayong));
    }
}
