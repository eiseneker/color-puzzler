using UnityEngine;
using System.Collections;

public class GridElement : MonoBehaviour {
	
	public int xPosition;
	public int yPosition;
	public Matrix matrix;
	private bool alive = true;
	public Color color;
	public int colorIndex;
	public bool insertedIntoMatrix;
	private Color[] colors = {
		Color.red,
		new Color(1, .5f, 0),
		Color.yellow,
		Color.green,
		Color.blue,
		new Color(1, 0, 1)
	};
	public bool canChain = true;
	private int permanentColorIndex;
	private Direction explosionDirection;
	private bool readyToExplode = false;
	private float maxExplosionTimer = .1f;
	private float currentExplosionTimer;
	public bool colorSet = false;
	private bool permanentColorSet = false;
	public bool survivesExplosion = false;
	public bool agnostic = false;
	public bool countable = true;
	private int refundValue = 0;
	public bool disabled = false;
	private float colorDelay;
	private bool delayedColorSet = true;
	public bool canBeReplaced;
	
	public enum Direction { None, Forward, Backward };

	// Use this for initialization
	void Start () {
		matrix = GameObject.Find ("Grid").GetComponent<Matrix>();
		
		if(!colorSet){
			colorIndex = RandomizedColorIndex();
			colorSet = true;
		}
		
		UpdateColorByIndex (colorIndex);
		GetComponent<MeshRenderer>().material.SetColor ("_Color", color);
	}
	
	// Update is called once per frame
	void Update () {
		if(!permanentColorSet) {
			permanentColorIndex = colorIndex;
			permanentColorSet = true;
		}
		
		if(insertedIntoMatrix){
			if(permanentColorSet) permanentColorIndex = colorIndex;
			if(canBeReplaced) canBeReplaced = !disabled;
		}else{
			ManageHoverState ();
		}
		
		if(!delayedColorSet){
			if(colorDelay > 0){
				colorDelay -= Time.deltaTime;
			}else{
				GetComponent<MeshRenderer>().material.SetColor ("_Color", color);
			}
		}
		
		HandleExplosion ();
	}
	
	public void SetExplode(Direction direction, int newRefundValue){
		refundValue = newRefundValue;
		explosionDirection = direction;
		readyToExplode = true;
	}
	
	public void UpdateColorByIndex(int inputColorIndex){
		UpdateColorByIndex (inputColorIndex, 0);
	}
	
	public void UpdateColorByIndex(int inputColorIndex, float delay){
		colorIndex = inputColorIndex;
		color = colors[inputColorIndex];
		UpdateColor (delay);
	}
	
	public void SetPosition(int xPositionIn, int yPositionIn){
		xPosition = xPositionIn;
		yPosition = yPositionIn;
		Vector3 newPosition = transform.position;
		newPosition.z = 2;
		transform.position = newPosition;
		insertedIntoMatrix = true;
	}
	
	public ArrayList CardinalNeighbors(){
		ArrayList neighbors = new ArrayList();
		GameObject neighbor;
		neighbor = matrix.ElementAtArrayPosition (xPosition - 1, yPosition);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition + 1, yPosition);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition, yPosition - 1);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition, yPosition + 1);
		neighbors.Add (neighbor);
		return(neighbors);
	}
	
	public ArrayList AllNeighbors(){
		ArrayList neighbors = new ArrayList();
		GameObject neighbor;
		neighbor = matrix.ElementAtArrayPosition (xPosition, yPosition + 1);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition + 1, yPosition + 1);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition + 1, yPosition);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition + 1, yPosition - 1);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition, yPosition - 1);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition - 1, yPosition - 1);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition - 1, yPosition);
		neighbors.Add (neighbor);
		neighbor = matrix.ElementAtArrayPosition (xPosition - 1, yPosition + 1);
		neighbors.Add (neighbor);
		return(neighbors);
	}
	
	public void UpdateColor(){
		UpdateColor (0);
	}
	
	public void UpdateColor(float delay){
		colorDelay = delay;
		delayedColorSet = false;
		if(disabled){
			color = Color.gray;
		}else if(agnostic){
			color = Color.white;
		}
	}
	
	private void ManageHoverState(){
		Transform lockIcon = transform.Find("X");
		
		if(matrix.CanInsertIntoMatrix(gameObject)){
			UpdateHoverColor();
			if(lockIcon) lockIcon.gameObject.SetActive(false);
		}else{
			if(lockIcon) lockIcon.gameObject.SetActive(true);
		}
	}
	
	private void UpdateHoverColor(){
		GameObject objectAtPosition = matrix.ElementAtVectorPosition(transform.position);
		disabled = false;
		if(objectAtPosition){
			int foundColorIndex = objectAtPosition.GetComponent<GridElement>().colorIndex;
			int mixedColorIndex = MixColorIndexes(permanentColorIndex, foundColorIndex);
			disabled = (mixedColorIndex >= colors.Length);
			if(disabled){
				UpdateColor ();
			}else{
				UpdateColorByIndex (mixedColorIndex);
			}
		}else{
			UpdateColorByIndex (permanentColorIndex);
		}
	}	
	
	private void HandleExplosion(){
		if(readyToExplode){
			if(currentExplosionTimer < maxExplosionTimer){
				currentExplosionTimer += Time.deltaTime;
			}else{
				Explode ();
			}
		}
	}
	
	
	private void Explode(){
		int newValue = refundValue;
		if(AbleToExplode ()){
			readyToExplode = false;
			alive = survivesExplosion;
			ArrayList neighbors = GetComponent<GridElement>().CardinalNeighbors ();
			foreach(GameObject neighborObject in neighbors){
				if(neighborObject){
					GridElement neighbor = neighborObject.GetComponent<GridElement>();
					if(!neighbor.survivesExplosion){
						if(explosionDirection == Direction.None){
							HandleExplosionWithNoDirection(neighbor, newValue);
						}else{
							HandleExplosionWithDirection (neighbor, newValue);
						}
					}
				}
			}
			
			GameController.ResetEventTimer();
			if(!survivesExplosion) {
				UpdateGameValues ();
			}
		}
	}
	
	private bool AbleToExplode(){
		return(alive && (agnostic || colorIndex != colors.Length));
	}
	
	private void HandleExplosionWithNoDirection(GridElement neighbor, int inputRefundValue){
		bool eitherIsAgnostic = agnostic || neighbor.agnostic;
		bool colorMatchesNeighbor = ColorMatches(neighbor);
	
		if(canChain){
			Direction newDirection = GetDirection(colorIndex, neighbor.colorIndex);
			bool newCascade = newDirection != Direction.None;
			bool neighborIsSame = ColorMatches (neighbor) && newDirection == Direction.None;
			if(eitherIsAgnostic || newCascade || neighborIsSame){
				if(newDirection != Direction.None && !colorMatchesNeighbor){
					print("increased value!");
					refundValue = inputRefundValue + 1;
					print ("new refund value " + refundValue);
				}
				neighbor.SetExplode(newDirection, refundValue);
			}
		}else{
			if(eitherIsAgnostic || colorMatchesNeighbor){
				neighbor.SetExplode(Direction.None, refundValue);
			}
		}
	}
	
	private void HandleExplosionWithDirection(GridElement neighbor, int inputRefundValue){
		bool eitherIsAgnostic = agnostic || neighbor.agnostic;
	
		if(eitherIsAgnostic || ColorMatches (neighbor) || DirectionMatchesNeighbor(explosionDirection, neighbor)){
			if(colorIndex != neighbor.colorIndex){
				print("increased value!");
				refundValue = inputRefundValue + 1;
				print ("new refund value " + refundValue);
			}
			neighbor.SetExplode(explosionDirection, refundValue);
		}
	}
	
	private bool ColorMatches(GridElement neighbor){
		return(colorIndex == neighbor.GetComponent<GridElement>().colorIndex);
	}
	
	private bool DirectionMatchesNeighbor(Direction direction, GridElement neighbor){
		return(GetDirection(colorIndex, neighbor.colorIndex) == direction);
	}
	
	private void UpdateGameValues(){
		print("countable " + countable);
		if(countable) GameController.remainingEnergy += refundValue;
		print ("refunded" + refundValue);
		if(GetComponent<Target>()){
			GameController.targetCount--;
			GameController.remainingEnergy += 10;
		}
		Destroy (gameObject);
	}
	
	private Direction GetDirection(int colorFrom, int colorTo){
		if(colorTo == colors.Length - 1 && colorFrom == 0){
			return(Direction.Backward);
		}else if(colorFrom == colors.Length - 1 && colorTo == 0){
			return(Direction.Forward);
		}else if(colorTo - colorFrom == 1){
			return(Direction.Forward);
		}else if(colorTo - colorFrom == -1){
			return(Direction.Backward);
		}else{
			return(Direction.None);
		}
	}
	
	public int MixColorIndexes(int color1, int color2){
		if(color1 > color2){
			int tempColor = color1;
			color1 = color2;
			color2 = tempColor;
		}
		if(color2 - color1 > 3){
			color1 += 6;
		}
		if(color1 == color2){
			return(color1);
		}else if(Mathf.Abs (color2 - color1) == 3){
			return(colors.Length);
		}else if((color2 + color1) % 2 == 1){
			if(color2 % 2 == 0){
				return(color2 % 6);
			}else{
				return(color1 % 6);
			}		
		}else{
			return(((color1 + color2) / 2) % 6);
		}
	}
	
	private int RandomizedColorIndex(){
		int index;
		float randomValue = Random.value;
		if(randomValue > .833f){
			index = 0;
		}else if(randomValue > .666f){
			index = 1;
		}else if(randomValue > .499f){
			index = 2;
		}else if(randomValue > .332f){
			index = 3;
		}else if(randomValue > .165f){
			index = 4;
		}else{
			index = 5;
		}
		return(index);
	}
	
	
}
