using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    NavMeshAgent nav;
    RaycastHit result;
    Animator ani;

    Vector3 path1;
    Vector3 path2;

    public void Awake()
    {
        nav = this.gameObject.GetComponent<NavMeshAgent>();
        ani = this.gameObject.GetComponent<Animator>();
        path1 = new Vector3(-9.12f, -25.06f, 27.61f);
        path2 = new Vector3(-63.9f, -25.06f, -0.9f);
    }

    private void Start()
    {
        nav.updatePosition = false;
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out result))
        //    {
        //        Debug.Log(result.collider.gameObject);
        //        if (result.collider.gameObject.name == "Plane")
        //        {
        //            nav.SetDestination(result.point);
        //        }
        //    }
        //}
        Vector3 v = transform.InverseTransformDirection(nav.velocity);
        ani.SetFloat("Speed", v.z);
        ani.SetFloat("Angle", v.x);
        //ani.SetFloat("Speed", nav.velocity.magnitude);

        if (Vector3.Distance(this.transform.position, path1) < 1f)
        {
            nav.SetDestination(path2);
        }
        if (Vector3.Distance(this.transform.position, path2) < 1f)
        {
            nav.SetDestination(path1);
        }
    }

    private void OnAnimatorMove()
    {
        //Vector3 v = ani.rootPosition;
        //v.y = nav.nextPosition.y;
        //transform.position = v;
        nav.nextPosition = transform.position;
        ani.ApplyBuiltinRootMotion();
    }
}
