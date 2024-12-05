using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManagerScript : MonoBehaviour
{
    public GameObject theCamera;
    public GameObject activeList;
    public Material nodeMat;

    public Transform rightController; // Assign the right controller transform in the Unity Editor
    public Transform leftController; // Assign the left controller transform in the Unity Editor

    private bool increasing = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        DoClick();
        HandleRotation();
        ModulateNodeMaterial();
    }

    private void ModulateNodeMaterial()
    {
        Color cc = nodeMat.color;
        Color newColor = new Color(cc.r, cc.g, cc.b, cc.a);

        if (increasing)
        {
            if (newColor.a >= 1)
                increasing = false;
            else
                newColor.a += (.5f * Time.smoothDeltaTime);
        }
        else
        {
            if (newColor.a <= 0f)
                increasing = true;
            else
                newColor.a -= (.5f * Time.smoothDeltaTime);
        }

        nodeMat.color = newColor;
    }

    private void HandleRotation()
    {
        // You might want to adjust this for VR head movement instead of mouse input.
    }

    private void DoClick()
    {
        // Replace mouse click detection with Oculus controller input
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            // Create a ray from the right controller's position and forward direction
            Ray ray = new Ray(rightController.position, rightController.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"Hit Object: {hit.collider.gameObject.name}");

                // Change skybox material
                Material theStoredMaterial = hit.collider.gameObject.GetComponent<NodeLoadingScript>().myMaterial;
                RenderSettings.skybox = theStoredMaterial;

                // Switch active list
                GameObject theNodeList = hit.collider.gameObject.GetComponent<NodeLoadingScript>().myNodeList;
                activeList.SetActive(false);
                theNodeList.SetActive(true);
                activeList = theNodeList;
            }
        }
    }
}