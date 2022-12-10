using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip batSound;
    public AudioClip hitSound;
    public AudioClip attackSound;
    public AudioClip rockSound;


    public static SoundManager inst { get; private set; }

    private void Awake()
    {
        inst = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBat()
    {
        audioSource.PlayOneShot(batSound);
    }

    public void PlayHit()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlayAttack()
    {
        audioSource.PlayOneShot(attackSound);
    }

    public void PlayRock()
    {
        audioSource.PlayOneShot(rockSound);
    }
}
