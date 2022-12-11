using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BatManager : MonoBehaviour
{
    public static BatManager inst { get; private set; }

    [SerializeField]
    GameObject background;
    [SerializeField]
    GameObject[] batsPos;
    [SerializeField]
    GameObject batPrefabs;

    List<int> numList = new List<int>();

    public int clickCount;

    public GameObject red;
    public GameObject batGame;
    public GameObject gaugeUI;
    public GameObject text;

    public Light2D global_light;
    public Light2D player_light;

    private void Awake()
    {
        inst = this;
        clickCount = 0;
    }

    private void OnEnable()
    {
        clickCount = 0;
        CreateUnDuplicateRandom(0, 7);
        InvokeGame();
    }

    IEnumerator SpawnBatPos()
    {

        text.SetActive(true);
        for (int i = 0; i < numList.Count; i++)
        {
           SpawnBat(batsPos[i].transform.position);
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(4f);
        StartCoroutine(LightControl(false));
        yield return new WaitForSeconds(1f);
        background.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        player_light.gameObject.SetActive(true);

        text.SetActive(false);

        yield return new WaitForSeconds(0.9f);
        gaugeUI.gameObject.SetActive(true);

        //이 이후 7-클릭카운트 *2 만큼 최대 행동력 감소
        StartCoroutine(DecreaseStamina((7-clickCount)*2));

        numList.Clear();

        RandomEvent.inst.isCorutineStart = false;
        RandomEvent.inst.EventCorutine();

        batGame.SetActive(false);
        gameObject.SetActive(false);
    }

    public void InvokeGame()
    {
        Invoke("GameStart", 1f);
    }

    public void GameStart()
    {
        player_light.gameObject.SetActive(false);
        gaugeUI.gameObject.SetActive(false);
        batGame.SetActive(true);

        background.GetComponent<SpriteRenderer>().DOFade(1, 1f);
        StartCoroutine(LightControl(true));
        
        StartCoroutine(SpawnBatPos());
    }
    // Start is called before the first frame update

    IEnumerator LightControl(bool control)
    {

        if (control)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 10;i++)
            {
                global_light.intensity += 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (int i = 0; i < 10;i++)
            {
                global_light.intensity -= 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }

    }

    public void SpawnBat(Vector3 pos)
    {
        GameObject bat = Instantiate(batPrefabs);
        bat.transform.position = pos;
    }

    void CreateUnDuplicateRandom(int min, int max)
    {
        int currentNumber = Random.Range(min, max);

        for (int i = 0; i < max;)
        {
            if (numList.Contains(currentNumber))
            {
                currentNumber = Random.Range(min, max);
            }
            else
            {
                numList.Add(currentNumber);
                i++;
            }
        }
    }

    IEnumerator hitEffect()
    {
        red.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        red.SetActive(false);
    }

    public void StartRed()
    {
        StartCoroutine(hitEffect());
    }

    IEnumerator DecreaseStamina(float penalty)
    {
        if (penalty == 0)
            yield return null;

        Player_Move_Draw.inst.maxStamina -= penalty;

        EventStartTrigger.inst.StartBatText();

        yield return new WaitForSeconds(15f);

        Player_Move_Draw.inst.maxStamina = 80;

    }
}
