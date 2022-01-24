using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crusherController : MonoBehaviour
{
    Animator animator;
    bool state  =  false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("State", false);

        // animate crusher on key press
        if (Input.GetKeyDown("space") == true)
        {
            animator.SetBool("State", true);
        }

        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

    }

    
}
