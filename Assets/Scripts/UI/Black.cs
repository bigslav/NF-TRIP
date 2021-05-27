using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Black : MonoBehaviour
{
    public Image FadeImg;
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;
    private bool fading = false;

    void Awake()
    {
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
    }
    
    void Start()
    {
        FadeOut();
    }
    
    public IEnumerator FadeOutObject()
    {
        while (FadeImg.color.a > 0.001)
        {
            if (fading)
                break;
            Debug.Log("a");
            FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator FadeInObject()
    {
        while (FadeImg.color.a < 0.999)
        {
            if (!fading)
                fading = true;
            Debug.Log(FadeImg.color.a);
            FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        fading = false;
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutObject());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInObject());
    }
}