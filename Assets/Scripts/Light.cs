using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    public Light2D player_light;
    public GameObject shake_event;
    public GameObject pStateBar;
    bool isCorutineStart;

    // Start is called before the first frame update

    private void Awake()
    {
        isCorutineStart = false;
        
    }
    void Start()
    {
            player_light.pointLightOuterRadius = 5f;
    }

    void Update()
    {
       
    }

   public void TurnOnEvent()
    {
        shake_event.SetActive(true);
        pStateBar.SetActive(false);
    }

    public void LightCorutine()
    {
        if (isCorutineStart == false)
        {
            player_light.pointLightOuterRadius = 5f;
            StartCoroutine(LightOff());
        }
    }


    IEnumerator LightOff()
    {
        isCorutineStart = true;
        Invoke("TurnOnEvent", 8);
        yield return new WaitForSeconds(5f);

        player_light.pointLightOuterRadius = 2f;
        yield return new WaitForSeconds(0.5f);

        player_light.pointLightOuterRadius = 5f;
        yield return new WaitForSeconds(0.5f);

        player_light.pointLightOuterRadius = 2f;
        yield return new WaitForSeconds(0.5f);

        player_light.pointLightOuterRadius = 5f;
        yield return new WaitForSeconds(0.5f);

        //���� �׸��� ���̶�� ���� ����, Ŭ�� ����
        //�̵� ���̶�� �� �μ��� �� �׳� �Ѿ�� �̹� false���� �� fasle���ְ�
        //�̵� ����
        Player_Move_Draw.inst.DestroyLine();
        Player_Move_Draw.inst.canClick = false;
        Player_Move_Draw.inst.StopMove();
        isCorutineStart = false;
        gameObject.SetActive(false);
        
        yield return null;
    }


}
