namespace Matrix
{
	public interface ISquareMatrix<T> : IMatrix<T>
	{
		int GetLength();

		void RightRotation();
	}
}
