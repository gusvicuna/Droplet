using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPanel : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("StartFadeOut");
    }

    public IEnumerator StartFadeOut(){
        animator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }

    public void StartFadeIn(){
        animator.SetTrigger("FadeIn");
    }
}
