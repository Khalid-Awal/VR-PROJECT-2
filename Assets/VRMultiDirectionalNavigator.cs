using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMultiDirectionalNavigator : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3.0f;
    private Node currentNode;

    public float inputCooldown = 0.5f;
    private float lastInputTime = 0f;

    void Update()
    {
        HandleInput();
        MoveToCurrentNode();
    }

    private void HandleInput()
    {
        if (Time.time - lastInputTime < inputCooldown) return;

        Vector2 thumbstickInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        if (thumbstickInput.y > 0.7f && currentNode.forward != null) // Forward
        {
            MoveToNode(currentNode.forward);
        }
        else if (thumbstickInput.y < -0.7f && currentNode.backward != null) // Backward
        {
            MoveToNode(currentNode.backward);
        }
        else if (thumbstickInput.x > 0.7f && currentNode.right != null) // Right
        {
            MoveToNode(currentNode.right);
        }
        else if (thumbstickInput.x < -0.7f && currentNode.left != null) // Left
        {
            MoveToNode(currentNode.left);
        }

        lastInputTime = Time.time;
    }

    private void MoveToNode(Node targetNode)
    {
        if (targetNode != null)
        {
            currentNode = targetNode;
        }
    }

    private void MoveToCurrentNode()
    {
        if (player != null && currentNode != null)
        {
            player.position = Vector3.Lerp(player.position, currentNode.transform.position, Time.deltaTime * moveSpeed);
            player.rotation = Quaternion.Slerp(player.rotation, currentNode.transform.rotation, Time.deltaTime * moveSpeed);

            // Optional: Highlight the active node
            currentNode.HighlightNode(true);
        }
    }
}