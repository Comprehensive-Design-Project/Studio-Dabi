using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    public Light2D player_light;
    public GameObject shake_event;

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
        yield return new WaitForSeconds(5f);

        player_light.pointLightOuterRadius = 2f;
        yield return new WaitForSeconds(0.5f);

        player_light.pointLightOuterRadius = 5f;
        yield return new WaitForSeconds(0.5f);

        player_light.pointLightOuterRadius = 2f;
        yield return new WaitForSeconds(0.5f);

        player_light.pointLightOuterRadius = 5f;
        yield return new WaitForSeconds(0.5f);

        //선을 그리는 중이라면 선을 제거, 클릭 막음
        //이동 중이라면 선 부수는 건 그냥 넘어가고 이미 false지만 또 fasle해주고
        //이동 중지
        Player_Move_Draw.inst.DestroyLine();
        Player_Move_Draw.inst.canClick = false;
        Player_Move_Draw.inst.StopMove();
        isCorutineStart = false;
        gameObject.SetActive(false);
        shake_event.SetActive(true);
        yield return null;
    }


}
