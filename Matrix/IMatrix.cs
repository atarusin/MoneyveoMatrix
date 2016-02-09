namespace Matrix
{
	public interface IMatrix<T>
	{
		int GetLength(int n);

		string Format(string templetMatrix, string templetRow, string templetItem);
	}
}
