using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    Image image;
    float timer;
    void Start()
    {
        image = this.gameObject.GetComponent<Image>();
        timer = 3;

        UnityEventScript.ifSeeing.AddListener(ImageFillAmountDesc);
        UnityEventScript.ifUnseeing.AddListener(ImageFillAmountAsc);
    }

    public void ImageFillAmountDesc()
    {
        if (timer <= 0) return;
        timer -= Time.deltaTime;
        image.fillAmount = timer / 3;
    }

    public void ImageFillAmountAsc()
    {
        if (timer >= 3) return;
        timer += Time.deltaTime;
        image.fillAmount = timer / 3;
    }
}
