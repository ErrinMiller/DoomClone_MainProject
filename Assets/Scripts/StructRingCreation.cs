using UnityEngine;
using System.Collections;

public class StructRingCreation : MonoBehaviour
{
    public GameObject structPivot;
    public int sections;
    public float rotation;
    public float currentRotation;

	void Start ()
    {
	    for (int i = sections; i > 0; i--)
        {
            GameObject newStruct = structPivot;
            currentRotation += rotation;

            Instantiate(newStruct, Vector3.zero, Quaternion.Euler(0,currentRotation,0));
        }
	}
	
	void Update ()
    {
	
	}
}
