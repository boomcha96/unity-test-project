using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneActions : MonoBehaviour
{

    public LayerMask clickMask;

    //Attach a cube GameObject in the Inspector before entering Play Mode
    [SerializeField]
    public GameObject m_Cube;
    
    void Update()
    {
        //Detect when there is a mouse click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Create a ray from the Mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, clickMask))
            {
                //Get the point that is clicked
                Vector3 spawnPosition = new Vector3 (hit.point.x, hit.point.y + 0.5f, hit.point.z);

                Instantiate(m_Cube, spawnPosition, transform.rotation);
            }
        }
    }
}
