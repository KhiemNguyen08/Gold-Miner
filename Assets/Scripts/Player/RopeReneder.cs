using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeReneder : MonoBehaviour
{
    LineRenderer lineRenderer;
    [SerializeField]
    Transform startPosition;
    float line_Width = 0.1f;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = line_Width;
        lineRenderer.enabled = false;
    }
    public void ReanderLine(Vector3 endPosition , bool enableRender)
    {
        if (enableRender)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }

        }
            if (lineRenderer.enabled)
            {
                Vector3 temp = startPosition.position;
                temp.z = -2f;
                startPosition.position = temp;
            temp = endPosition;
            temp.z = 0;
            endPosition = temp;
            lineRenderer.SetPosition(0, startPosition.position);
            lineRenderer.SetPosition(1, endPosition);
            }
        }
    }

