using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator anim;
    public int currentAnim;
    public void Next()
    {
        //if current anim == 2 return
        if (currentAnim == 2)
            return;
        currentAnim++;
        anim.SetInteger("NextAnim", currentAnim);
    }

    public void Previous()
    {
        if (currentAnim == 0)
            return;
        currentAnim--;
        anim.SetInteger("NextAnim", currentAnim);
    }
}
