using System;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;
namespace Model{
public class BoardGame
    {
        private Square[,] boardGame;
        private Queue<Pair> snake;
        private int boardGameSize = 15;
        private float cubeSize = 0.3f;
        private Thread movementThread;
        private bool continueMoving;
        private char direction;



        public BoardGame()
        {
            
            boardGame = new Square[boardGameSize, boardGameSize];
            snake = new Queue<Pair>();
            snake.Enqueue(new Pair(7, 7));
        
            continueMoving = true;
            direction = Square.DOWN;

            loadBoardGame();
            setEnemy();

           // movementThread = new Thread(new ThreadStart(moveThread));
           // movementThread.Start();

   
      
        }

        public void changeDirection(char direction)
        {

            if (this.direction == Square.UP && direction != Square.DOWN && direction != Square.UP)
            {
                this.direction = direction;
            }
            else if (this.direction == Square.DOWN && direction != Square.UP && direction != Square.DOWN)
            {
                this.direction = direction;
            }
            else if (this.direction == Square.RIGHT && direction != Square.LEFT && direction != Square.RIGHT)
            {
                this.direction = direction;
            }
            else if (this.direction == Square.LEFT && direction != Square.RIGHT && direction != Square.LEFT)
            {
                this.direction = direction;
            }


        }

      

        public void loadBoardGame()
        {

            for (float i = 0f; i < boardGame.GetLength(0); i++)
            {
                for (float j = 0f; j < boardGame.GetLength(1); j++)
                {

                    char color;

                    if (i % 2 == 0)
                    {
                        if (j % 2 == 0)
                        {
                            color = Square.BLACK;
                        }
                        else
                        {
                            color = Square.DARK_GRAY;
                        }
                    }
                    else
                    {
                        if (j % 2 == 0)
                        {
                            color = Square.DARK_GRAY;
                        }
                        else
                        {
                            color = Square.BLACK;
                        }
                    }

                    
                    Square sq = new Square(color, cubeSize, i * cubeSize, j * cubeSize);
                    boardGame[(int)i,(int)j] = sq;
                }
            }

            
            Pair pair = snake.Peek();
            int _i = pair.getFirst();
            int _j = pair.getSecond();
            boardGame[_i,_j].setSnake(true);
            boardGame[_i,_j].setCurrentColor(Square.DARK_GREEN);

        }


        int ft = 0;
        public void setEnemy()
        {
          
            bool contains = true;

            while (contains)
            {

                System.Random rand = new System.Random();
                int i = rand.Next(0, boardGameSize);
                int j = rand.Next(0, boardGameSize);

                Pair enemy = new Pair(i, j);

                contains = snakeContains(enemy);

                if (!contains)
                {
                     boardGame[i, j].setCurrentColor(Square.RED);
                }
            }
            
        }

        public bool snakeContains(Pair enemy)
        {

            bool contains = false;

            foreach (Pair pair in snake)
            {

                if (pair.Equals(enemy))
                    contains = true;

            }

            return contains;

        }

        public void moveThread(){

            while(continueMoving){
                Thread.Sleep(400);
                move();
            }
           
        }

        public void move(){


            if(continueMoving){

                try{

                    Queue<Pair> temp = new Queue<Pair>();
                    Pair p = snake.Peek();
                    int i = p.getFirst();
                    int j = p.getSecond();

                    switch (direction)
                    {

                        case Square.UP:
                            j--;
                            break;

                        case Square.DOWN:
                            j++;
                            break;

                        case Square.LEFT:
                            i--;
                            break;

                        case Square.RIGHT:
                            i++;
                            break;

                    }

                    int limit = 1;

                    if (boardGame[i, j].getCurrentColor() == Square.RED)
                    {
                        limit = 0;
                        setEnemy();
                    }
                    else if (boardGame[i, j].getCurrentColor() == Square.GREEN)
                    {
                        continueMoving = false;
                        //PERDIOOO --------------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    }

                    temp.Enqueue(new Pair(i, j));

                    int tailI = 0;
                    int tailJ = 0;

                    while (snake.Count > limit)
                    {

                        p = snake.Dequeue();
                        temp.Enqueue(p);
                        tailI = p.getFirst();
                        tailJ = p.getSecond();

                    }

                    if (snake.Count > 0)
                    {

                        p = snake.Dequeue();
                        i = p.getFirst();
                        j = p.getSecond();
                        boardGame[i, j].setSnake(false);

                    }
                    else
                    {
                        if (tailI < (boardGameSize - 1) && tailI > 0 && tailJ < (boardGameSize - 1) && tailJ > 0)
                        {

                            if (direction == Square.UP)
                            {
                                tailJ++;
                                boardGame[tailI, tailJ].setSnake(false);
                            }
                            else if (direction == Square.DOWN)
                            {
                                tailJ--;
                                boardGame[tailI, tailJ].setSnake(false);
                            }
                            else if (direction == Square.RIGHT)
                            {
                                tailI--;
                                boardGame[tailI, tailJ].setSnake(false);
                            }
                            else
                            {
                                tailI++;
                                boardGame[tailI, tailJ].setSnake(false);
                            }

                        }

                    }

                    snake = temp;



                    foreach (Pair pair in snake)
                    {
                        i = pair.getFirst();
                        j = pair.getSecond();

                        boardGame[i, j].setSnake(true);
                    }
                    p = snake.Peek();
                    i = p.getFirst();
                    j = p.getSecond();

                    boardGame[i, j].setCurrentColor(Square.DARK_GREEN);

                }catch(IndexOutOfRangeException ex)
                {
                    continueMoving = false;
                    gameOver();
                }

            }
            
        }

    
        public void gameOver(){
            for (float i = 0f; i < boardGame.GetLength(0); i++)
            {
                for (float j = 0f; j < boardGame.GetLength(1); j++)
                {
                    boardGame[(int)i,(int)j].setCurrentColor(Square.RED);
               
                }
            }
        }

        public Square[,] getBoardGame()
        {
            return boardGame;
        }

        public int getBoardGameSize(){
            return boardGameSize;
        }



    }

}
