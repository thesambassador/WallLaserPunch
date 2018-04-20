using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public enum WallDir
{
        Front,
        Back,
        Left,
        Right
}

public class WallInfo : MonoBehaviour {

    public Transform ScaledWallPiece;

    public WallDir Direction;
    public float Width;
    public float Height;

    public UnityEvent OnWallPropertiesChanged;

    public void SetWallProperties(Vector3 centerPos, float width, float height, WallDir dir)
    {
        transform.position = centerPos;
        ScaledWallPiece.localScale = new Vector3(width, height, 1);
        Direction = dir;
        Width = width;
        Height = height;

        if (OnWallPropertiesChanged != null)
        {
            OnWallPropertiesChanged.Invoke();
        }
    }
    

}
