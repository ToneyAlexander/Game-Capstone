﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace CCC.Inputs
{
    /// <summary>
    /// Represents a Component that detects the position of the mouse in world 
    /// space for a given LayerMask.
    /// </summary>
    public sealed class MousePositionDetector : MonoBehaviour
    {
        /// <summary>
        /// The LayerMask that this MousePositionDetector should look at.
        /// </summary>
        [SerializeField]
        private LayerMask layerMask;

        /// <summary>
        /// Calculates the world position of the mouse during the frame that this 
        /// method was called as long as the mouse pointer was not over an 
        /// EventSystem object.
        /// </summary>
        /// <returns>
        /// The world position of the mouse pointer if it wasn't over an 
        /// EventSystem object, Vector3.negativeInfinity otherwise.
        /// </returns>
        public Vector3 CalculateWorldPosition()
        {
            Vector3 worldSpacePosition = Vector3.negativeInfinity;

            if (!EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 mouseScreenSpacePosition = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouseScreenSpacePosition);

                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, layerMask))
                {
                    worldSpacePosition = hit.point;
                }
            }

            return worldSpacePosition;
        }

        /// <summary>
        /// Tests whether a given Vector3 is valid or not.
        /// </summary>
        /// <returns><c>true</c>, if v is valid, <c>false</c> otherwise.</returns>
        /// <param name="v">V.</param>
        public bool IsValid(Vector3 v)
        {
            bool isValid = true;

            if (float.IsNegativeInfinity(v.x) &&
                float.IsNegativeInfinity(v.y) &&
                float.IsNegativeInfinity(v.z))
            {
                isValid = false;
            }

            return isValid;
        }

        #region MonoBehaviour Messages
        private void Awake()
        {
            // We have to bit shift 1 by the layer mask
            layerMask = 1 << layerMask;
            layerMask = ~layerMask;
        }
        #endregion
    }
}
