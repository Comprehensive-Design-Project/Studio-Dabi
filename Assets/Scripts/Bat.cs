using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bat : MonoBehaviour
{
    Sequence mySequence;
    bool isFront;


  
    void Start()
    {
        isFront = false;
        mySequence = DOTween.Sequence()
        .SetAutoKill(false) //추가
        .OnStart(() =>
        {
            transform.localScale = Vector3.zero;
            GetComponent<SpriteRenderer>().DOFade(0, 0);
        })
        .InsertCallback(1.0f,() => {
            isFront = true;
        })
        .Append(transform.DOScale(1f, 1.5f).SetEase(Ease.InElastic))
        .Join(GetComponent<SpriteRenderer>().DOFade(1, 1.5f))
        .SetDelay(0.5f)
        .AppendCallback(() => {
            BatManager.inst.StartRed();
            SoundManager.inst.PlayHit();

            
            gameObject.SetActive(false);
            Destroy(gameObject);
        });
    }

    private void OnMouseDown()
    {
        if (isFront == true)
        {
            mySequence.Kill();
            SoundManager.inst.PlayAttack();
            BatManager.inst.clickCount++;
            Debug.Log("타이밍 맞게 클릭했다! 성공 수 : "+ BatManager.inst.clickCount);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }


    void OnEnable()
    {
        StartCoroutine(SoundPlay());
        mySequence.Restart();
    }
    IEnumerator SoundPlay()
    {
        yield return new WaitForSeconds(1.7f);
        SoundManager.inst.PlayBat();
    }
}
