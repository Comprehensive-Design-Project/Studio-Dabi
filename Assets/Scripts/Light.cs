using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Light : MonoBehaviour
{
    public Light2D player_light;
    bool isCorutineStart;

    // Start is called before the first frame update

    private void Awake()
    {
        isCorutineStart = false;
    }
    void Start()
    {
        if (isCorutineStart == false)
        {
            player_light.pointLightOuterRadius = 5f;
            StartCoroutine(LightOff());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BtnClick()
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

        isCorutineStart = false;
        gameObject.SetActive(false);
        yield return null;
    }
}
