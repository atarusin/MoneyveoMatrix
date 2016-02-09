namespace Matrix
{
	public class SquareMatrix<T> : MatrixBase<T>, ISquareMatrix<T>
	{
		public SquareMatrix(int length) : base(new[] { length, length })
		{
		}

		public int GetLength()
		{
			return GetLength(0);
		}

		public void RightRotation()
		{
			var length = GetLength();
			int halfLengthX = length / 2;
			int halfLengthY = halfLengthX + length % 2;
			length--;
			T cache;
			for (var y = 0; y < halfLengthY; y++)
			{
				for (var x = 0; x < halfLengthX; x++)
				{
					cache = matrix[x, y];
					matrix[x, y] = matrix[y, length - x];
					matrix[y, length - x] = matrix[length - x, length - y];
					matrix[length - x, length - y] = matrix[length - y, x];
					matrix[length - y, x] = cache;
				}
			}
		}
	}
}
