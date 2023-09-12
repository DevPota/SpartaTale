using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    public float fadeTime;
    public Image image;

    public void Awake()
    {
        image = GetComponent<Image>();

        StartCoroutine(Fade(1, 0));
    }
    public IEnumerator Fade(float start, float end)
    {
        float currentTime = 0f;
        float percent = 0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, percent);
            image.color = color;

            yield return null;
        }
        image.gameObject.SetActive(false);
        yield return null;
    }
}