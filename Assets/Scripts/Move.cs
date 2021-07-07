using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private GameObject targetHolder;
	private GameObject target;

	public float speed;
	private Transform blackShapeObject;
	
	// Start is called before the first frame update
	public void Start()
	{
        targetHolder = GameObject.Find("Target Holder");
        if(targetHolder != null) SetTarget();
	}

    public void SetTarget()
    {
        target = GetNearestTarget();
    }

    GameObject GetNearestTarget()
    {
        int count = targetHolder.transform.childCount;
        float min = 1000000f;
        Vector2 pos = gameObject.transform.position;
        float dis;
        int index = 0;

        for(int i=0; i<count; i++)
        {
            dis = Vector2.Distance(targetHolder.transform.GetChild(i).gameObject.transform.position, pos);
            if(dis <= min)
            {
                min = dis;
                index = i;
            }
        }

        return targetHolder.transform.GetChild(index).gameObject;
    }
	
	// Update is called once per frame
	void Update()
	{
        Vector3 deltaPosition = target.transform.position - transform.position;           // get the direction vector from bug to target
        float angle = Mathf.Atan2(deltaPosition.y, deltaPosition.x) * Mathf.Rad2Deg;      // calculate tan inverse to find appropriate angle and convert to degree
        transform.eulerAngles = new Vector3(                                              // sets this angle on z axis. and thus it faces towards the target
            transform.eulerAngles.x,
            transform.eulerAngles.y,
            angle
        );
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
	}
}