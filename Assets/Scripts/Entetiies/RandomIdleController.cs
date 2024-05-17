using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RandomIdleController : MonoBehaviour
{
    [SerializeField] private int minTimeBetweenActivation;
    [SerializeField] private int maxTimeBetweenActivation;
    [SerializeField] private string[] animationTriggers;


    private Animator Animator;

    public void Awake()
    {
        Animator=GetComponent<Animator>();
    }

    public void invokerRandomIdle()
    {
        Invoke("activateRandomAnimation",Random.Range(minTimeBetweenActivation, maxTimeBetweenActivation));
    }

    private void activateRandomAnimation()
    {
        //Debug.Log("Random Idle activation");
        Animator.SetTrigger(animationTriggers[Random.Range(0,animationTriggers.Length)]);
    }

}
