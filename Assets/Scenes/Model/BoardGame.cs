using System;
using System.Collections.Generic;

namespace Model{
public class BoardGame
    {
        private Square[,] boardGame;
        private Queue<Pair> snake;
        private int boardGameSize = 15;
        private float cubeSize = 0.3f;


        public BoardGame()
        {
            boardGame = new Square[boardGameSize, boardGameSize];
            snake = new Queue<Pair>();
            snake.Enqueue(new Pair(7, 7));
            loadBoardGame();
      
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

        public void setEnemy()
        {

            bool contains = true;

            while (contains)
            {

                Random rand = new Random();
                int i = rand.Next(0, boardGameSize);
                int j = rand.Next(0, boardGameSize);
  
                Pair enemy = new Pair(i, j);

                contains = snakeContains(enemy);

                if (!contains)
                {
                    boardGame[i,j].setCurrentColor(Square.RED);
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

    

        public Square[,] getBoardGame()
        {
            return boardGame;
        }



    }

}
