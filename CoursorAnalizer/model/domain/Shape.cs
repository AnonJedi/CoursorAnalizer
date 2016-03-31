namespace CursorAnalyzer.model.domain
{
    /// <summary>
    /// POJO class
    /// </summary>
    public class Shape
    {
        private int x, y, size, shapeNumber;

        public Shape(int x, int y, int size, int shapeNumber)
        {
            this.x = x;
            this.y = y;
            this.size = size;
            this.shapeNumber = shapeNumber;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public int ShapeNumber
        {
            get { return shapeNumber; }
            set { shapeNumber = value; }
        }
    }
}