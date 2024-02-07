using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotBeat : MonoBehaviour
{
    public GameObject text;
    public AudioSource audioSource;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.gameObject.name == "Player")
        {
            this.audioSource.Play();
            this.text.gameObject.SetActive(true);
            StartCoroutine(HideText());
        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(5);
        this.text.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
