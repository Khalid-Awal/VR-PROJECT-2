using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node forward;
    public Node backward;
    public Node left;
    public Node right;

    // Optional: Add other properties or methods specific to nodes
    public void HighlightNode(bool isActive)
    {
        // Example: Highlight this node when active
        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = isActive ? Color.green : Color.white;
        }
    }
}