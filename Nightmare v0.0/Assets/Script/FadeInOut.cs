using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    public bool fadeIn = true;
    public float fadeTime = 1f;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();

        image.color = new Color(1f, 1f, 1f, 0f);
    }

    private void Start()
    {
        if (fadeIn)
            StartCoroutine(FadeIn(fadeTime));
        else
            StartCoroutine(FadeOut(fadeTime));
    }

    private void Update()
    {
        Color color = image.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / 1f;
            image.color = color;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        Color color = image.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / time;
            image.color = color;
            yield return null;
        }
    }

    public IEnumerator FadeOut(float time)
    {
        Color color = image.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / time;
            image.color = color;
            yield return null;
        }
    }
}
