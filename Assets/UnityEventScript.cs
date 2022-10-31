using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventScript : MonoBehaviour
{
    RaycastHit raycastHit;

    public static UnityEvent ifSeeing;
    public static UnityEvent ifUnseeing;

    // Start is called before the first frame update
    void Awake()
    {
        ifSeeing = new UnityEvent();
        ifUnseeing = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.forward * 10);

        if (Physics.Raycast(this.transform.position, this.transform.forward, out raycastHit, 10f))
        {
            if (raycastHit.collider.name == "Image")
            {
                ifSeeing.Invoke();
            }
        }
        else
        {
            ifUnseeing.Invoke();
        }
    }
}
