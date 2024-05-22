using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera Camera;
    private Vector3 Target;
    private float Speed;
    private bool AtTarget = true;
    // Start is called before the first frame update
    void Start()
    {
        Speed = 200;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Target = hit.point;
            }
            AtTarget = false;
        }
        if(!AtTarget)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Target, step);
            if (Vector3.Distance(transform.position, Target) < 0.001f)
            {
                AtTarget = true;
            }
        }
    }
}
