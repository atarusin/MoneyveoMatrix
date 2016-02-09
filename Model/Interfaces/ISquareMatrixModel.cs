using Matrix;
using System.IO;

namespace Core.Interfaces
{
	public interface ISquareMatrixModel : ISquareMatrix<string>
	{
		void LoadRandom(int length);

		void Load(Stream file);

		void Load(string value);

		string GetValue();
	}
}
