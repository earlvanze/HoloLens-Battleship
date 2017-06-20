using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour {

	private string gameState;
	private int activeShip;

	private int orientation;

	private int[,] playerBoard;
	private int[,] computerBoard;

	private int[][] playerShipStatus;
	private int[][] computerShipStatus;

	public void SetGameState(string _gameState){
		gameState = _gameState;
	}

	public string GetGameState(){
		return gameState;
	}

	private void Update(){
		switch (gameState) 
		{
		case "init":
				clearGameArrays ();
				activeShip = -1;
				SetGameState ("TitleScreen");
				break;
			case "TitleScreen":
				// Show Title Screen
				SetGameState ("TitleScreenWait");
				break;
			case "TitleScreenWait":
				break;
			case "StartGame":
				// Hide Title Screen
				SetGameState ("PlayerPlaceShips");
				break;
			case "PlayerPlaceShips":
				break;
			case "ComputerPlaceShips":
				break;
			case "PlayerFire":
				break;
			case "PlayerFireWait":
				break;
			case "ComputerFire":
				break;
			case "ComputerFireWait":
				break;
		}
	}

	private void clearGameArrays(){
		playerBoard = new int[10, 10];
		computerBoard = new int[10, 10];
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				playerBoard [i,j] = 0;
				computerBoard [i,j] = 0;
			}
		}
		playerShipStatus = new int[][] { new int[2] {0,0}, new int[3] {0,0,0}, new int[3] {0,0,0}, new int[4] {0,0,0,0}, new int[5]{0,0,0,0,0} };
		computerShipStatus = new int[][] { new int[2] {0,0}, new int[3] {0,0,0}, new int[3] {0,0,0}, new int[4] {0,0,0,0}, new int[5]{0,0,0,0,0} };
	}

	public void selectShip (int _shipType){
		orientation = 0;
		activeShip = _shipType;
	}

	private void placeShip (int[,] _grid, int _shipType, int _gridX, int _gridY){
		// horizontal
		if (orientation == 0) {
			if (_grid [_gridX, _gridY] == 0) {
				_grid [_gridX, _gridY] = _shipType;
			}
			if (_gridX + 1 < 10 && _grid [_gridX + 1, _gridY]==0) {
				if (_grid [_gridX + 1, _gridY] == 0) {
					_grid [_gridX + 1, _gridY] = _shipType;
				}
			}
			if (_shipType > 2) {
				if (_gridX - 1 > -1) {
					if (_grid [_gridX - 1, _gridY] == 0) {	
						_grid [_gridX - 1, _gridY] = _shipType;
					}
				}
			}
			if (_shipType > 3) {
				if (_gridX + 2 < 10) {
					if (_grid [_gridX + 2, _gridY] == 0) {
						_grid [_gridX + 2, _gridY] = _shipType;
					}
				}
			}
			if (_shipType > 4) {
				if (_gridX - 2 > -1) {
					if (_grid [_gridX - 2, _gridY] == 0) {
						_grid [_gridX - 2, _gridY] = _shipType;
					}
				}
			}
		}
		// vertical
		if (orientation == 1) {
			if (_grid [_gridX, _gridY] == 0) {
				if (_grid [_gridX, _gridY] == 0) {
					_grid [_gridX, _gridY] = _shipType;
				}
			}
			if (_gridY + 1 < 10) {
				if (_grid [_gridX, _gridY + 1] == 0) {
					_grid [_gridX, _gridY + 1] = _shipType;
				}
			}
			if (_shipType > 2) {
				if (_gridY - 1 > -1) {
					if (_grid [_gridX, _gridY - 1] == 0) {
						_grid [_gridX, _gridY - 1] = _shipType;
					}
				}
			}
			if (_shipType > 3) {
				if (_gridY + 2 < 10) {
					if (_grid [_gridX, _gridY + 2] == 0) {
						_grid [_gridX, _gridY + 2] = _shipType;
					}
				}
			}
			if (_shipType > 4) {
				if (_gridY - 2 > -1) {
					if (_grid [_gridX, _gridY - 2] == 0) {
						_grid [_gridX, _gridY - 2] = _shipType;
					}
				}
			}
		}
	}

	private void removeShip (int[,] _grid, int _shipType){
		for (int i = 0; i < 10; i++) {
			for (int j = 0; j < 10; j++) {
				if (_grid [i,j] == _shipType){
					_grid [i,j] = 0;
				}
			}
		}
	}

	public bool playerFire(int _gridX, int _gridY){
		bool fireResult = false;
		if (gameState == "PlayerFire") {
			//if (checkGridArea (playerBoard, _gridX, _gridY)){
				//fireResult = true;
			//}
			SetGameState("PlayerFireWait");
		}
		return fireResult;
	}

	public bool computerFire(int _gridX, int _gridY){
		bool fireResult = false;
		if (gameState == "ComputerFire") {
			//if (checkGridArea (computerBoard, _gridX, _gridY)){
			//	fireResult = true;
			//}
			SetGameState("ComputerFireWait");
		}
		return fireResult;
	}

	private void shootGridArea (int[,] _grid,int _gridX, int _gridY){
		
	}

	private bool checkGridArea (int[,] _grid,int _gridX, int _gridY){
		bool fireResult = false;
		if (_grid [_gridX, _gridY] == 0){
			// miss
			splashGridArea (_grid, _gridX, _gridY);
		}
		if (_grid [_gridX, _gridY] != 0 && _grid [_gridX, _gridY] < 10){
			fireResult = true;
			// hit
			explodeGridArea (_grid, _gridX, _gridY);
		}
		return fireResult;
	}
		
	private void explodeGridArea (int[,] _grid,int _gridX, int _gridY){
		int _shipType = _grid [_gridX, _gridY] ;
		_grid [_gridX, _gridY] = _shipType + 10;
		// Animate Explosion

		// Update ShipStatus
		if (gameState == "ComputerFireWait") {
			for (int i=0;i<playerShipStatus[_shipType-1].Length;i++){
				if (playerShipStatus [_shipType - 1] [i] == 0) {
					playerShipStatus [_shipType - 1] [i] = 1;
					break;
				}
			}
			checkBoat (playerShipStatus, _shipType);
		}
		if (gameState == "PlayerFireWait") {
			for (int j=0;j<computerShipStatus[_shipType-1].Length;j++){
				if (computerShipStatus [_shipType - 1] [j] == 0) {
					computerShipStatus [_shipType - 1] [j] = 1;
					break;
				}
			}
			checkBoat (computerShipStatus, _shipType);
		}
	}

	private void splashGridArea (int[,] _grid,int _gridX, int _gridY){
		// Animate Splash
	}

	private void checkBoat(int[][] _shipStatus, int _shipType){
		// Set Fire 
		//
		int sinkboatFlag = 1;
		for (int i = 0; i < _shipStatus[_shipType - 1].Length; i++) {
			if (_shipStatus [_shipType - 1] [i] == 0) {
				sinkboatFlag = 0;
			}
		}
		if (sinkboatFlag == 1){
			sinkBoat(_shipType);
		}
	}

	private void sinkBoat(int _shipType){
		if (gameState == "PlayerFireWait") {
			// sink computer boat
		}
		if (gameState == "ComputerFireWait") {
			// sink player boat
		}
	}

	private void checkEndGame(int[][] _shipStatus){
		int endGameFlag = 1;
		for (int i = 0; i < _shipStatus.Length; i++) {
			for (int j = 0; j < _shipStatus.Length; j++) {
				if (_shipStatus [i] [j] == 0) {
					endGameFlag = 0;
				}
			}
		}
		if (gameState == "PlayerFireWait") {
			// player wins
			if (endGameFlag==1) {
				SetGameState("PlayerWins");
			}
		}
		if (gameState == "ComputerFireWait") {
			// computer wins
			if (endGameFlag==1) {
				SetGameState("ComputerWins");
			}
		}
	}

}
