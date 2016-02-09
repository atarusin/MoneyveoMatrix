using System;
using System.Text;
using System.IO;
using Matrix;
using Core.Interfaces;

namespace Core.Models
{
	public class SquareMatrixModel : SquareMatrix<string>, ISquareMatrixModel
	{
		public SquareMatrixModel() : base(0) { }

		public void LoadRandom(int length)
		{
			var rnd = new Random();
			matrix = new string[length, length];
			for (var i = 0; i < length; i++)
			{
				for (var j = 0; j < length; j++)
				{
					matrix[j, i] = rnd.Next(-999,999).ToString();
				}
			}
		}

		public void Load(Stream stream)
		{
			if (stream != null && stream.Length > 0)
			{
				stream.Position = 0;
				string matrix;
				using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8))
				{
					matrix = reader.ReadToEnd();
				}
				Load(matrix);
			}
		}

		public void Load(string value)
		{
			if (!String.IsNullOrWhiteSpace(value))
			{
				var rows = value.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
				if (rows.Length > 0)
				{
					matrix = new string[rows.Length, rows.Length];
					for (var i = 0; i < rows.Length; i++)
					{
						var items = rows[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
						for (var j = 0; j < items.Length && j < rows.Length; j++)
						{
							matrix[j, i] = items[j];
						}
					}
				}
			}
		}

		public string GetValue()
		{
			StringBuilder sb = new StringBuilder();
			var l = GetLength();
			for (var i = 0; i < l; i++)
			{
				for (var j = 0; j < l; j++)
				{
					sb.AppendFormat("{0},", matrix[j, i]);
				}
				sb.Length--;
				sb.Append('\n');
			}
			if (sb.Length > 0) sb.Length--;
			return sb.ToString();
		}
	}
}