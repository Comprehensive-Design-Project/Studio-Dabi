using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonTest : MonoBehaviour
{

    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed()
    {
        if(Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();

            bool isOpen = animator.GetBool("Opened");

            animator.SetBool("Opened", !isOpen);
        }
    }
}
