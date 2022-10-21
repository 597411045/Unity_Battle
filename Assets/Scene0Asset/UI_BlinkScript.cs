using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BlinkScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve curve;
    Material material;
    float timer;

    void Start()
    {
        material = this.gameObject.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        material.SetColor("_Color", new Color(1, 1, 1, curve.Evaluate(timer)));
    }
}
