using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using Model;


public class Rotate : MonoBehaviour
{
    private float update;
	private bool rotate;
    private bool firstGrab;
    private BoardGame boardGame;
    private Square[,] matrix;
    private GameObject[,] cubesMatrix;
	MeshRenderer rend;
	Color original;
    XRIDefaultInputActions actions;
    
    // Start is called before the first frame update
    void Start()
    {



    	rotate = true;
        firstGrab = false;
    	rend = GetComponentInChildren<MeshRenderer>();
    	original = rend.material.color;

        // DRAW DE MATRIX 
        
        actions.XRILeftHandLocomotion.MoveSnake.performed += moveSnake;
   


        //
    }

    private void startGame(){

        boardGame = new BoardGame();
        cubesMatrix = new GameObject[boardGame.getBoardGameSize(), boardGame.getBoardGameSize()];
        matrix = boardGame.getBoardGame();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Square cube = matrix[i, j];
                GameObject cubeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cubeObject.transform.localPosition = new Vector3(cube.getPosX(), cube.getPosY(), -1);
                cubeObject.transform.localScale = new Vector3(cube.getSize(), cube.getSize(), cube.getSize());
                cubeObject.GetComponent<MeshRenderer>().material.color = getColor(cube.getCurrentColor());
                cubesMatrix[i, j] = cubeObject;
            }
        }
    }
    private void OnEnable()
    {
        actions.Enable();
    }

    private void Awake()
    {
        update = 0.0f;
        actions = new XRIDefaultInputActions();
    }

    private void moveSnake(InputAction.CallbackContext obj)
    {
        float x = obj.ReadValue<Vector2>().x;
        float y = obj.ReadValue<Vector2>().y;
        Debug.Log("X:"+x+" y:"+y);

        if (x >= 0.9) {
            boardGame.changeDirection(Square.LEFT);
          
        }
        if (x <= -0.9)
        {
            boardGame.changeDirection(Square.RIGHT);
       
        }
        if (y >= 0.9)
        {
            boardGame.changeDirection(Square.DOWN);
         
        }
        if (y <= -0.9)
        {
            boardGame.changeDirection(Square.UP);
     
        }

    }

    // Update is called once per frame
    void Update()
    {


        update += Time.deltaTime;
        if (update > 0.3f)
        {
            update = 0.0f;
            if(!rotate)
             boardGame.move();
        }


        if(rotate){
    		 transform.Rotate(transform.up * Time.deltaTime * 45f, Space.Self);
    	}

        if(cubesMatrix!=null)
        {
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
       
        
       
    }

    public Color getColor(char c){
        if(c==Square.BLACK)
            return Color.black;

        if (c == Square.DARK_GRAY)
            return new Color((20/255.0f), (20 / 255.0f), (20 / 255.0f));

        if (c == Square.DARK_GREEN)
            return new Color((53 / 255.0f), (121 / 255.0f), (33 / 255.0f));

        if (c == Square.GREEN)
            return new Color((158 / 255.0f), (178 / 255.0f), (55 / 255.0f));

        if (c == Square.RED)
            return new Color((185 / 255.0f), (46 / 255.0f), (77 / 255.0f));

        return Color.red;
    }

    public void InterruptRotation(){

        if(!firstGrab){
            firstGrab = true;
            startGame();
        }
        Debug.Log(boardGame.getIsGameOver());
        if (boardGame.getIsGameOver())
        {
            Debug.Log("start again");
            destroyMatrix();
            startGame();
        }

        if (rotate){
    		rend.material.color = Color.green;
    	}else{
    		rend.material.color = original;
    	}
    	rotate = !rotate;
    }

    public void destroyMatrix()
    {
        for (int i = 0; i < cubesMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < cubesMatrix.GetLength(1); j++)
            {
         
                GameObject cubeObject = cubesMatrix[i, j];
                Destroy(cubeObject);

            }
        }

        cubesMatrix = null;
    }
}
