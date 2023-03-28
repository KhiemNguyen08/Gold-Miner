using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charector : MonoBehaviour
{
    // Start is called before the first frame update
    public HookMove hookmove;
    public Animator animator;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hookmove.canRotate == false)
        {
           // animator.SetBool("rotate", true);
            //Debug.Log("rotate");
        }
    }
}
