using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SwitchToCards()
    {
        animator.SetBool("ViewCard", true);
    }

    public void SwitchFromCards()
    {
        animator.SetBool("ViewCard", false);
    }
}
