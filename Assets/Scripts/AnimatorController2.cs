using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController2 : MonoBehaviour
{

    public Animator animator;
    private bool space;
    private int time;
    Vector3 position;


    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        space = false;
        time = 0;       
        position = transform.position; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !space)
        {
            StartAnimation();
        }

        // Aquí puedes controlar la posición del objeto como desees
        if (space)
        {
            SetAnimationTime();
        }
       
    }

    void StartAnimation()
    {
        space = true;
        animator.SetBool("Space", true);
        //start position -95.32 0.13 19.79
        //end position -84.0 0.13 19.79
        if (transform.position.x > -84.0)
        {
            transform.position = new Vector3(transform.position.x - 0.018f, transform.position.y, transform.position.z);
        }
    }

    void SetAnimationTime()
    {
        time++;
        animator.SetFloat("TIME", time);
    }
}
