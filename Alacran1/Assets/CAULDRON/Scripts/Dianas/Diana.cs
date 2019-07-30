using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana : MonoBehaviour
{
    public Animator animator;

    public bool startPos = false;

    public float cooldown = 3f;

    float timecooldown = 0;

    bool up;

    private void Awake()
    {
        if (animator == null)
        {
            enabled = false;
            return;
        }
        up = startPos;
        ActualizarAnimator();
    }

    private void Update()
    {
        if (up) return;
        if (timecooldown < Time.time)
        {
            up = true;
            ActualizarAnimator();
        }
    }

    private void Down()
    {
        up = false;
        ActualizarAnimator();
        timecooldown = Time.time + cooldown;
    }

    void ActualizarAnimator()
    {
        if (!enabled) return;
        animator.SetBool("up", up);
    }

}
