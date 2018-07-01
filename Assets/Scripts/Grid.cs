using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public GameObject GridDividerPrefab;

    public Transform UpperLeftCorner;
    public Transform LowerRightCorner;
    public int GridSize = 8;
    public float CellSize = 1;

    private Transform _transform;

    void Awake()
    {
        _transform = transform;
        UpperLeftCorner.gameObject.SetActive(false);
        LowerRightCorner.gameObject.SetActive(false);
        //CreateGrid();
    }

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

    public void CreateGrid()
    {
        float gridDividerThickness = GridDividerPrefab.transform.localScale.x * 0.5f;
        float dividerSpace = gridDividerThickness + CellSize;

        LowerRightCorner.position = UpperLeftCorner.position + new Vector3(GridSize * CellSize, -GridSize * CellSize, 0);
        Vector3 startPos = UpperLeftCorner.position + new Vector3(-1, 1, 0) * CellSize * 0.5f;

        return;

        //float rowLength = CellSize * (GridSize + 1);
        //Vector3 rowPos = startPos + Vector3.right * (rowLength * 0.5f);
        

        //float columnHeight = CellSize * (GridSize + 1);
        //Vector3 columnPos = startPos + Vector3.down * (columnHeight * 0.5f);

        //Vector3 rowEnd = rowPos + Vector3.down * columnHeight;
        //Transform rowEndObj = (Instantiate(GridDividerPrefab, rowEnd, Quaternion.identity, _transform) as GameObject).transform;
        //rowEndObj.name = "RowEnd";

        //GameObject startPosObj = Instantiate(GridDividerPrefab, startPos, Quaternion.identity, _transform) as GameObject;
        //startPosObj.name = "Start pos";

        //for (int i = 0; i < GridSize + 1; i++)
        //{
        //    Vector3 pos = Vector3.Lerp(rowPos, rowEnd, (float)i / (float)GridSize);
        //    Transform row = (Instantiate(GridDividerPrefab, pos, Quaternion.identity, _transform) as GameObject).transform;
        //    row.localScale = new Vector3(rowLength, row.localScale.y, row.localScale.z);
        //    row.name = "GridRow" + (i + 1);
        //}
        //for (int j = 0; j < GridSize + 1; j++)
        //{
        //    Transform column = (Instantiate(GridDividerPrefab, columnPos + Vector3.right * j * CellSize, Quaternion.identity, _transform) as GameObject).transform;
        //    column.localScale = new Vector3(column.localScale.x, columnHeight, column.localScale.z);
        //}
    }
}
