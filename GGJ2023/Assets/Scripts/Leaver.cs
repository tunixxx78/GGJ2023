using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaver : MonoBehaviour
{
    [SerializeField] GameObject objToRemove;
    [SerializeField] Animator leverAnimator;

    SfxManager sfx;

    private void Awake()
    {
        leverAnimator = GetComponentInChildren<Animator>();
        sfx = FindObjectOfType<SfxManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objToRemove.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            leverAnimator.SetTrigger("IsPulled");
            sfx.buttoPress();
        }
    }
}
