using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlindEffect : MonoBehaviour
{
    public Image blindImage;
    public Image image2;
    public bool isBlind = false;
    private float blindDuration = 3f;
    private Color originalImageColor;
    private Color originalImage2Color;
    private Color originalTextColor;
    public Text text1;
    public Text text2;

    private Coroutine reduceTransparencyCoroutine;

    void Start()
    {
        originalImageColor = blindImage.color;
        originalImageColor.a = 0f;
        blindImage.color = originalImageColor;

        originalImage2Color = image2.color;
        originalImage2Color.a = 0f;
        image2.color = originalImage2Color;

        originalTextColor = text1.color;
        originalTextColor.a = 0f;
        text1.color = originalTextColor;
        text2.color = originalTextColor;
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Blind_Bullet")
        {
            isBlind = true;
            SetFullTransparency();
            if (reduceTransparencyCoroutine != null)
            {
                StopCoroutine(reduceTransparencyCoroutine);
            }
            reduceTransparencyCoroutine = StartCoroutine(ReduceTransparency());
        }
    }

    void SetFullTransparency()
    {
        Color fullTransparencyColor = originalImageColor;
        fullTransparencyColor.a = 1f;
        blindImage.color = fullTransparencyColor;

        fullTransparencyColor = originalImage2Color;
        fullTransparencyColor.a = 1f;
        image2.color = fullTransparencyColor;

        fullTransparencyColor = originalTextColor;
        fullTransparencyColor.a = 1f;
        text1.color = fullTransparencyColor;
        text2.color = fullTransparencyColor;
    }

    IEnumerator ReduceTransparency()
    {
        yield return new WaitForSeconds(2f);

        float elapsedTime = 0f;
        while (elapsedTime < blindDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, elapsedTime / blindDuration);

            Color newImageColor = originalImageColor;
            newImageColor.a = newAlpha;
            blindImage.color = newImageColor;

            Color newImage2Color = originalImage2Color;
            newImage2Color.a = newAlpha;
            image2.color = newImage2Color;

            Color newTextColor = originalTextColor;
            newTextColor.a = newAlpha;
            text1.color = newTextColor;
            text2.color = newTextColor;

            yield return null;
        }
    }
}
