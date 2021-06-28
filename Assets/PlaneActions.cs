using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneActions : MonoBehaviour
{

    public LayerMask clickMask;

    //Attach a cube GameObject in the Inspector before entering Play Mode
    [SerializeField]
    public GameObject m_Cube;

    // Start is called before the first frame update
    void Start()
    {
        //This is how far away from the Camera the plane is placed
        //m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        //Create a new plane with normal (0,0,1) at the position away from the camera you define in the Inspector. This is the plane that you can click so make sure it is reachable.
        //m_Plane = new Plane(Vector3.forward, m_DistanceFromCamera);

    }

    // Update is called once per frame
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
                //Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 spawnPosition = new Vector3 (hit.point.x, hit.point.y + 0.5f, hit.point.z);

                //Move your cube GameObject to the point where you clicked
                //m_Cube.transform.position = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);

                Instantiate(m_Cube, spawnPosition, transform.rotation);

               
                //Debug.Log(hit.point);
            }
        }
    }
}
