using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScriptS1 : MonoBehaviour
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
        //            ani.SetBool("Move", true);
        //        }
        //    }
        //}
        Vector3 v = transform.InverseTransformDirection(nav.velocity);
        if (true)
        {
            ani.SetFloat("Speed", v.z);
            ani.SetFloat("Angle", v.x);
        }
    }

    private void OnAnimatorMove()
    {
        nav.nextPosition = transform.position;
        ani.ApplyBuiltinRootMotion();
    }
}
