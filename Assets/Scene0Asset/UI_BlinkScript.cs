using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BlinkScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static Material defaultImageMaterial;
    public Material material;
    float timer1;
    float timer2;

    bool ifRecycle;
    bool ifFade;
    bool ifAscend;
    bool isTextMode;

    TextMeshPro textMeshPro;

    void Awake()
    {
        if (this.gameObject.name.Contains("TMP"))
        {
            textMeshPro = this.GetComponent<TextMeshPro>();
            isTextMode = true;
        }
        else if (this.gameObject.name == "ExitBlackScreen")
        {
            material = this.gameObject.GetComponent<Image>().material;
            ifAscend = true;
        }
        else if (this.gameObject.name == "EnterBlackScreen")
        {
            material = this.gameObject.GetComponent<Image>().material;
            ifFade = true;
            defaultImageMaterial = material;
        }
        else
        {
            material = this.gameObject.GetComponent<SpriteRenderer>().material;
            ifRecycle = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ifRecycle)
        {
            timer1 += Time.deltaTime;
            material.SetColor("_Color", new Color(1, 1, 1, Base_SGM.RecycleCurve.Evaluate(timer1)));
        }
        if (ifFade)
        {
            timer2 += Time.deltaTime;
            material.SetColor("_Color", new Color(1, 1, 1, Base_SGM.FadeCurve.Evaluate(timer2)));
            if (timer2 >= 1)
            {
                ifFade = false;
            }
        }
        if (ifAscend)
        {
            timer2 += Time.deltaTime;
            material.SetColor("_Color", new Color(1, 1, 1, Base_SGM.AscendCurve.Evaluate(timer2)));
            if (timer2 >= 1)
            {
                ifAscend = false;
            }
        }
        if (isTextMode)
        {
            timer1 += Time.deltaTime;
            textMeshPro.color = new Color(1, 1, 1, Base_SGM.RecycleCurve.Evaluate(timer1));
        }
    }

    public void FadeModeOn()
    {
        ifRecycle = !ifRecycle;
        ifFade = !ifFade;
    }

    public void AscendModeOn()
    {
        ifRecycle = !ifRecycle;
        ifAscend = !ifAscend;
    }

}
