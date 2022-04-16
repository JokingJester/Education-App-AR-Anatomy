using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UI Manager is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Animator anim;
    public int currentAnim;
    public GameObject label;
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

    public void ToggleLabel()
    {
        if (currentAnim == 1)
        {
            label.SetActive(true);
        }
        else if(currentAnim != 1)
        {
            label.SetActive(false);
        }
    }
    private void Update()
    {
        ToggleLabel();
    }
}
