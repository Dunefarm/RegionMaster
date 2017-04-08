using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public Transform UpperLeftCorner;
    public Transform LowerRightCorner;
    public int GridSize = 8;

    public Vector3 GridCoordinateToVector3(Finite2DCoord coord)
    {
        return GridCoordinateToVector3(coord.x, coord.y);
    }

    public Vector3 GridCoordinateToVector3(int i, int j)
    {
        float x = Mathf.Lerp(UpperLeftCorner.position.x, LowerRightCorner.position.x, i / (float)(GridSize - 1));
        float y = Mathf.Lerp(UpperLeftCorner.position.y, LowerRightCorner.position.y, j / (float)(GridSize - 1));
        return new Vector3(x, y, 0);
    }
}
