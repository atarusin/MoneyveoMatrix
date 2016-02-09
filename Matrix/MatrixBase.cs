using System;
using System.Text;

namespace Matrix
{
	public abstract class MatrixBase<T> : IMatrix<T>
	{
		protected T[,] matrix;

		public MatrixBase(int[] length)
		{
			matrix = new T[length[0], length[1]];
		}

		public int GetLength(int n)
		{
			return ((matrix == null) ? 0 : matrix.GetLength(n));
		}

		public string Format(string templetMatrix, string templetRow, string templetItem)
		{
			var result = String.Empty;
			if (matrix != null)
			{
				StringBuilder rows = new StringBuilder();
				StringBuilder row = new StringBuilder();
				var l = matrix.GetLength(0);
				var h = matrix.GetLength(1);
				for (int i = 0; i < h; i++)
				{
					for (int j = 0; j < l; j++)
					{
						row.AppendFormat(templetItem, matrix[j, i]);
					}
					rows.AppendFormat(templetRow, row.ToString());
					row.Length = 0;
				}
				return String.Format(templetMatrix, rows.ToString());
			}

			return result;
		}

	}
}
