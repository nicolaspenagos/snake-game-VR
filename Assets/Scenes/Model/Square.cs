using System;

namespace  Model
{
    public class Square
    {
        // Start is called before the first frame update
        public const char BLACK = 'B';
        public const char DARK_GRAY = 'D';
        public const char GREEN = 'G';
        public const char DARK_GREEN = 'E';
        public const char RED = 'R';
        public const char UP = 'u';
        public const char DOWN = 'd';
        public const char LEFT = 'l';
        public const char RIGHT = 'r';
        private char currentColor;
        private char color;
        private float size;
        private float posX;
        private float posY;
        private bool snake;
        private bool move;
        private char direction;

        public Square(char color, float size, float posX, float posY)
        {

            this.color = color;
            this.currentColor = color;
            this.size = size;
            this.posX = posX;
            this.posY = posY;
            this.snake = false;
            this.move = false;
            this.direction = UP;

        }

        public char getColor()
        {
            return color;
        }

        public float getSize()
        {
            return size;
        }

        public void setSize(float size)
        {
            this.size = size;
        }

        public char getCurrentColor()
        {
            return currentColor;
        }

        public void setCurrentColor(char currentColor)
        {
            this.currentColor = currentColor;
        }

        public float getPosX()
        {
            return posX;
        }

        public void setPosX(float posX)
        {
            this.posX = posX;
        }

        public float getPosY()
        {
            return posY;
        }

        public void setPosY(float posY)
        {
            this.posY = posY;
        }

        public bool isSnake()
        {
            return snake;
        }

        public void setSnake(bool snake)
        {
            this.snake = snake;
            if (snake)
                currentColor = GREEN;
            else
                currentColor = color;
        }

        public bool isMove()
        {
            return move;
        }

        public void setMove(bool move)
        {
            this.move = move;
        }

        public char getDirection()
        {
            return direction;
        }

        public void setDirection(char direction)
        {
            this.direction = direction;
        }

    }


}
