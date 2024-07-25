using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 g = Physics.gravity;
        Vector3 f = transform.parent.forward;

        Vector3 gravAbs = new Vector3(Mathf.Abs(Physics.gravity.normalized.x), Mathf.Abs(Physics.gravity.normalized.y), Mathf.Abs(Physics.gravity.normalized.z));
        Vector3 c = Vector3.Cross(f, gravAbs);
        Vector3 nonGravdir = Vector3.one - (gravAbs);

        Debug.Log("G"+g+" f"+f+"c"+c);
        transform.localPosition = f * dist* Input.GetAxis("Horizontal")  + c * Input.GetAxis("Vertical") * dist;

        //transform.localPosition = new Vector3(Input.GetAxis("Horizontal")*dist,0, Input.GetAxis("Vertical")*dist);
        //transform.eulerAngles = new Vector3(_player.eulerAngles.x,transform.eulerAngles.y,_player.eulerAngles.z);
    }
}
