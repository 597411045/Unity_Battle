using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    Material bodyMaterial;
    Material topMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "SampleA")
        {
            bodyMaterial = MyUtil.FindTransformInChildren(this.transform, "Body").GetComponent<SkinnedMeshRenderer>().materials[0];
            topMaterial = MyUtil.FindTransformInChildren(this.transform, "Body").GetComponent<SkinnedMeshRenderer>().materials[3];
        }
        else if (this.gameObject.name == "SampleB")
        {
            bodyMaterial = MyUtil.FindTransformInChildren(this.transform, "Body").GetComponent<SkinnedMeshRenderer>().materials[0];
            topMaterial = MyUtil.FindTransformInChildren(this.transform, "Body").GetComponent<SkinnedMeshRenderer>().materials[2];
        }
        else if (this.gameObject.name == "SampleC")
        {
            bodyMaterial = MyUtil.FindTransformInChildren(this.transform, "Body").GetComponent<SkinnedMeshRenderer>().materials[0];
            topMaterial = MyUtil.FindTransformInChildren(this.transform, "Body").GetComponent<SkinnedMeshRenderer>().materials[3];
        }
    }


    public void EnableOutline()
    {
        bodyMaterial.SetFloat("_OutlineWidth", 1);
        topMaterial.SetFloat("_OutlineWidth", 1);
    }

    public void DisableOutline()
    {
        bodyMaterial.SetFloat("_OutlineWidth", 0);
        topMaterial.SetFloat("_OutlineWidth", 0);
    }
}
