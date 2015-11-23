using UnityEngine;
using System.Collections;

public class Matrix : MonoBehaviour {
	public GameObject[,] matrix;
	private GFRectGrid grid;
	private int xMax;
	private int yMax;

	// Use this for initialization
	void Start () {
		grid = GetComponent<GFRectGrid>();
		xMax = Mathf.CeilToInt(Mathf.Abs (grid.renderTo.x) + Mathf.Abs (grid.renderFrom.x));
        yMax = Mathf.CeilToInt(Mathf.Abs (grid.renderTo.y) + Mathf.Abs (grid.renderFrom.y));
		matrix = new GameObject[xMax, yMax];
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public bool InsertIntoMatrix(GameObject objectToInsert){
		grid.AlignTransform(objectToInsert.transform);
		
		int xArrayPosition = CoordinateToPosition(objectToInsert.transform.position.x, grid.renderTo.x, grid.spacing.x);
		int yArrayPosition = CoordinateToPosition(objectToInsert.transform.position.y, grid.renderTo.y, grid.spacing.y);
		if(CanInsertIntoMatrix(objectToInsert)){
			if(matrix[xArrayPosition, yArrayPosition]){
				Destroy (matrix[xArrayPosition, yArrayPosition]);
			}
			
			matrix[xArrayPosition, yArrayPosition] = objectToInsert;
			objectToInsert.GetComponent<GridElement>().SetPosition(xArrayPosition, yArrayPosition);
			return(true);
		}else{
			return(false);
		}
	}
	
	public bool InsertIntoMatrixWithoutDestroy(GameObject objectToInsert){
		grid.AlignTransform(objectToInsert.transform);
		
		int xArrayPosition = CoordinateToPosition(objectToInsert.transform.position.x, grid.renderTo.x, grid.spacing.x);
		int yArrayPosition = CoordinateToPosition(objectToInsert.transform.position.y, grid.renderTo.y, grid.spacing.y);
		if(CanInsertIntoMatrix(objectToInsert)){
			matrix[xArrayPosition, yArrayPosition] = objectToInsert;
			objectToInsert.GetComponent<GridElement>().SetPosition(xArrayPosition, yArrayPosition);
			return(true);
		}else{
			return(false);
		}
	}
	
	public bool CanInsertIntoMatrix(GameObject objectToInsert){
		int xArrayPosition = CoordinateToPosition(objectToInsert.transform.position.x, grid.renderTo.x, grid.spacing.x);
		int yArrayPosition = CoordinateToPosition(objectToInsert.transform.position.y, grid.renderTo.y, grid.spacing.y);
		if(xArrayPosition < xMax && yArrayPosition < yMax && xArrayPosition >= 0 && yArrayPosition >= 0){
			if(matrix[xArrayPosition, yArrayPosition] == null){
				return(true);
			}else{
				GridElement elementAtPosition = matrix[xArrayPosition, yArrayPosition].GetComponent<GridElement>();
				bool canInsert = elementAtPosition.canBeReplaced;
				if(elementAtPosition.Disabled() && objectToInsert.GetComponent<GridElement>().black){
					canInsert = true;
				}else if(elementAtPosition.Disabled()){
					canInsert = false;
				}else{
					canInsert = true;
				}
				return(canInsert);
			}
		}else{
			return(false);
		}
	}
	
	private int CoordinateToPosition(float coordinate, float renderTo, float spacing){
		float position = coordinate * (1/spacing) + (spacing/2) + renderTo - 1;
		return(Mathf.CeilToInt(position));
	}
	
	public Vector3 PositionToCoordinate(int x, int y){
		float newX = (x - RenderTo().x + 1 - (Spacing ().x/2)) * Spacing().x;
		float newY = (y - RenderTo().y + 1 - (Spacing ().y/2)) * Spacing().y;
		return(new Vector3(newX, newY, 0));		
	}
	
	public GameObject ElementAtArrayPosition(int xPosition, int yPosition){
		if(xPosition >= 0 && xPosition < xMax && yPosition >= 0 && yPosition < yMax){
			return(matrix[xPosition, yPosition]);
		}else{
			return(null);
		}		
	}
	
	public GameObject ElementAtVectorPosition(Vector3 position){
		int xArrayPosition = CoordinateToPosition(position.x, RenderTo().x, Spacing().x);
		int yArrayPosition = CoordinateToPosition(position.y, RenderTo().y, Spacing().y);
		if(xArrayPosition < xMax && yArrayPosition < yMax && xArrayPosition >= 0 && yArrayPosition >= 0 && matrix[xArrayPosition, yArrayPosition]){
			return(matrix[xArrayPosition, yArrayPosition]);
		}else{
			return(null);
		}
	}
	
	private Vector3 RenderTo(){
		return(grid.renderTo);
	}
	
	private Vector3 Spacing(){
		return(grid.spacing);
	}
	
}
