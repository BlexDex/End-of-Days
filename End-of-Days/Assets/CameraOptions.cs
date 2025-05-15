using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOptions : MonoBehaviour
{
    public void ChangeWASDControl (bool isActive)
    {
        RTSCameraController.instance.SetWASDBool(isActive);
    }

    public void ChangeEdgeScrollControl(bool isActive)
    {
        RTSCameraController.instance.SetEdgeScrollBool(isActive);
    }
    public void ChangeDragClickControl(bool isActive)
    {
        RTSCameraController.instance.SetDragClickBool(isActive);
    }
    
}
