using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{

    public Animator animator;
    private bool Space;
    private int time;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Space = false;
        time = 0;        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartAnimation();
        }

        if (Space)
        {
            SetAnimationTime();
        }
        
    }

    void StartAnimation()
    {
        Space = true;
        animator.SetBool("Space", true);
    }

    void SetAnimationTime()
    {
        time++;
        animator.SetFloat("TIME", time);
    }
}
