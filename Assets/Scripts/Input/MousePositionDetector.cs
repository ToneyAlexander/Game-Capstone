using UnityEngine;

/// <summary>
/// Represents a Component that detects the position of the mouse in world 
/// space.
/// </summary>
public sealed class MousePositionDetector : MonoBehaviour
{
    public Vector3 CalculateWorldPosition()
    {
        Vector3 mouseScreenSpacePosition = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouseScreenSpacePosition);

        RaycastHit hit;
        Vector3 worldSpacePosition = Vector3.zero;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            worldSpacePosition = hit.point;
        }

        return worldSpacePosition;
    }
}
