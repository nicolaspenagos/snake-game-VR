using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Model;


public class Rotate : MonoBehaviour
{
	private bool rotate;
    private BoardGame boardGame;
    private Square[,] matrix;
    private GameObject[,] cubesMatrix;
	MeshRenderer rend;
	Color original;
    // Start is called before the first frame update
    void Start()
    {



    	rotate = true;
    	rend = GetComponentInChildren<MeshRenderer>();
    	original = rend.material.color;

        // DRAW DE MATRIX 
        
        boardGame = new BoardGame();
        cubesMatrix = new GameObject[boardGame.getBoardGameSize(), boardGame.getBoardGameSize()];
        matrix = boardGame.getBoardGame();

        for(int i=0; i<matrix.GetLength(0); i++){
            for(int j=0; j<matrix.GetLength(1); j++){
                Square cube = matrix[i,j];
                GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeObject.transform.localPosition = new Vector3(cube.getPosX(), cube.getPosY(), -1);
                cubeObject.transform.localScale = new Vector3(cube.getSize(), cube.getSize(), cube.getSize());
                cubeObject.GetComponent<MeshRenderer>().material.color = getColor(cube.getCurrentColor());
                cubesMatrix[i,j] = cubeObject;
            }
        }

        //
    }

    // Update is called once per frame
    void Update()
    {
    	if(rotate){
    		 transform.Rotate(transform.up * Time.deltaTime * 45f, Space.Self);
    	}

        for (int i = 0; i < cubesMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cubesMatrix.GetLength(1); j++)
            {
                Square cube = matrix[i, j];
                GameObject cubeObject = cubesMatrix[i, j];
                cubeObject.GetComponent<MeshRenderer>().material.color = getColor(cube.getCurrentColor());

              
            }
        }
        
       
    }

    public Color getColor(char c){
        if(c==Square.BLACK)
            return Color.black;

        if (c == Square.DARK_GRAY)
            return new Color((20/255.0f), (20 / 255.0f), (20 / 255.0f));

        if (c == Square.DARK_GREEN)
            return new Color((158 / 255.0f), (178 / 255.0f), (55 / 255.0f));

        if (c == Square.GREEN)
            return new Color((158 / 255.0f), (178 / 255.0f), (55 / 255.0f));

        if (c == Square.RED)
            return new Color((185 / 255.0f), (46 / 255.0f), (77 / 255.0f));

        return Color.red;
    }

    public void InterruptRotation(){
    	if(rotate){
    		rend.material.color = Color.green;
    	}else{
    		rend.material.color = original;
    	}
    	rotate = !rotate;
    }
}
