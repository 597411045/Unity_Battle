using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScript : MonoBehaviour
{
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(DestroySelf());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 1 * Time.deltaTime, 0);
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
