using System;
namespace Model{
    public class Pair{

        private int first;
        private int second;
     
        public Pair(int first, int second){
            this.first = first;
            this.second = second;
        }

     
        public int getFirst()
        {
            return first;
        }

      
        public int getSecond()
        {
            return second;
        }

        public void setFirst(int f)
        {
            first = f;
        }
      
        public void setSecond(int s)
        {
            second = s;
        }

        public override int GetHashCode()
        {
            return (first << 2) ^ second;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Pair p = (Pair)obj;
                return (first == p.getFirst()) && (second == p.getSecond());
            }
        }

    }
}


