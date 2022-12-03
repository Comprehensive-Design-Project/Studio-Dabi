using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BatManager : MonoBehaviour
{
    [SerializeField]
    GameObject background;

    [SerializeField]
    GameObject[] batsPos;

    [SerializeField]
    GameObject batPrefabs;

    List<int> numList = new List<int>();
    public int clickCount;
    public GameObject red;
    public static BatManager inst { get; private set; }

    private void Awake()
    {
        inst = this;
        clickCount = 0;

    }

    private void Start()
    {
        CreateUnDuplicateRandom(0, 7);
    }

    IEnumerator SpawnBatPos()
    {

        for (int i = 0; i < numList.Count; i++)
        {
           SpawnBat(batsPos[i].transform.position);
            yield return new WaitForSeconds(0.5f);
        }

        //이 이후 7-클릭카운트 *5 만큼 최대 행동력 감소 하면 될듯

        numList.Clear();
    }

    public void BtnClick()
    {
        background.GetComponent<SpriteRenderer>().DOFade(1, 1f);

        StartCoroutine(SpawnBatPos());
    }
    // Start is called before the first frame update

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
}
