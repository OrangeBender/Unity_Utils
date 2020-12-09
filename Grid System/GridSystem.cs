using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

    public LayerMask mask;
    public GameObject prefab;
    public float nodeSize = 1f;

    private Grid grid;
    private GameObject currentPrefab;
    private int gridSizeX;
    private int gridSizeZ;
    private Boolean currentPrefabOnMat = false;

    // Use this for initialization
    void Start () {

        //Get Size of the gameMat
        GetGridSize();

        //Create the grid of nodes to the size of the mat
        CreateGrid();
	}
	
	void Update () {

        CheckKeyDown();

        CheckMouseDown();

        currentPrefabOnMat = MovePrefabToMouse();
	}

    private Boolean MovePrefabToMouse() {

        int x;
        int y;

        if (MouseOnTableMat(out x, out y)) {

            if (currentPrefab != null) {

                if (x != -1 && y != -1) {

                    MoveCurrentPrefab(x, y);
                    return prefabOnMat();
                }
            }
        }

        return false;
    }

    private bool prefabOnMat() {

        Boolean onMat = true;

        foreach (Transform child in currentPrefab.transform) {

            int x = Mathf.FloorToInt(currentPrefab.transform.position.x -
                nodeSize / 2 +
                child.localPosition.x / nodeSize +
                gridSizeX / 2 -
                transform.position.x);

            int y = Mathf.FloorToInt(currentPrefab.transform.position.z -
                nodeSize / 2 +
                child.localPosition.z / nodeSize +
                gridSizeZ / 2 -
                transform.position.z);

            if (!(Mathf.Clamp(x, 0, gridSizeX - 1) == x &&
                Mathf.Clamp(y, 0, gridSizeZ - 1) == y)) {

                onMat = false;
            } else {

                if (grid.grid[x, y].occupied) {

                    onMat = false;
                }
            }
        }

        return onMat;
    }

    private void CheckMouseDown() {

        if (Input.GetMouseButtonDown(0) && currentPrefabOnMat) {

            foreach (Transform child in currentPrefab.transform) {

                int x = Mathf.Clamp(
                    Mathf.FloorToInt(currentPrefab.transform.position.x -
                    nodeSize / 2 +
                    child.localPosition.x / nodeSize +                    
                    gridSizeX / 2 -
                    transform.position.x),
                    0, gridSizeX - 1);

                int y = Mathf.Clamp(
                    Mathf.FloorToInt(currentPrefab.transform.position.z -
                    nodeSize / 2 +
                    child.localPosition.z / nodeSize +
                    gridSizeZ / 2 -
                    transform.position.z),
                    0, gridSizeZ - 1);

                grid.grid[x, y].occupied = true;
            }

            currentPrefab = null;
        }
    }

    private void CheckKeyDown() {

        if (Input.GetKeyDown(KeyCode.A)) {

            if (currentPrefab == null) {

                currentPrefab = Instantiate(prefab)
            } else {

                Destroy(currentPrefab);
            }
        }
    }

    private void MoveCurrentPrefab(int x, int y) {

        currentPrefab.transform.position = grid.grid[x,y].centerWorldPosition;
    }

    private bool MouseOnTableMat(out int x, out int y) {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, Mathf.Infinity, mask)) {

            if (hitPoint.collider == GetComponent<Collider>()) {

                x = Mathf.Clamp(
                    Mathf.FloorToInt(hitPoint.point.x - transform.position.x + gridSizeX / 2),
                    0, gridSizeX - 1);

                y = Mathf.Clamp(
                    Mathf.FloorToInt(hitPoint.point.z - transform.position.z + gridSizeZ / 2),
                    0, gridSizeZ - 1);

                return true;
            }            
        }
        x = -1;
        y = -1;
        return false;
    }

    private void GetGridSize() {

        Bounds borders = GetComponent<Renderer>().bounds;
        gridSizeX = Mathf.RoundToInt(borders.size.x);
        gridSizeZ = Mathf.RoundToInt(borders.size.z);

    }
 
    private void CreateGrid() {

        grid = new Grid(transform.position, gridSizeX, gridSizeZ, nodeSize);

    }
}

public class Node{

    public Vector3 centerWorldPosition;
    public bool occupied;

    public Node(Vector3 centerWorldPosition)
    {
        this.centerWorldPosition = centerWorldPosition;
        occupied = false;
    }
}

public class Grid{

    public Node[,] grid;

    public Grid(Vector3 gridCenter, int gridSizeX, int gridSizeZ, float NodeSize){

        grid = new Node[gridSizeX, gridSizeZ];

        for (int row = 0 ; row < gridSizeX; row++){
            for (int collumn = 0; collumn < gridSizeZ ; collumn++){

                Vector3 centerWorldPosition = gridCenter - (Vector3.right * gridSizeX / 2) -
                                     (Vector3.forward * gridSizeZ / 2) + 
                                     (Vector3.right * NodeSize * row) +
                                     (Vector3.forward * NodeSize * collumn) +
                                     (Vector3.right * NodeSize / 2) + 
                                     (Vector3.forward * NodeSize / 2);

                grid[row,collumn] = new Node(centerWorldPosition);
            }
        }
    }

    public String toString() {

        String str = "";

        for (int i = 0; i < 8; i++) {
            for (int y = 0; y < 4; y++) {
                str += $"\n[{i},{y}] : {grid[i, y].centerWorldPosition} - {grid[i, y].occupied}";
            }
        }

        return str;
    }
}