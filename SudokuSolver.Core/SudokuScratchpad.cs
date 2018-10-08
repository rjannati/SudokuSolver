using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSolver.Core
{
	/// <summary>
	/// SudokuScratchPad is a data structure that flattens a sudoku structure
	/// </summary>
    public class SudokuScratchpad
    {
		private SortedDictionary<(int row, int column), CellScratchpad> cellScratchpads;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="puzzle"></param>
		public SudokuScratchpad(int? [,] puzzle)
		{
			if(puzzle == null)
			{
				throw new ArgumentNullException(nameof(puzzle), "The array was not supplied.");
			}
			if(puzzle.GetLength(0) != 9 || puzzle.GetLength(1) !=9)
			{
				throw new ArgumentException("A 9x9 multidimensional array was not supplied.", nameof(puzzle));
			}

			cellScratchpads = new SortedDictionary<(int row, int column), CellScratchpad>();
			ConstructCellScratchpads(puzzle);
		}

		public IEnumerable<CellScratchpad> CellScratchpads => cellScratchpads.Select(s => s.Value);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public CellScratchpad GetCellScratchpad(int row, int column)
		{
			if(row < 0 || row > 8)
			{
				throw new ArgumentOutOfRangeException(nameof(row), "The row should be between 0 and 8");
			}
			if(column <0 || column > 8)
			{
				throw new ArgumentOutOfRangeException(nameof(column), "The column should be between 0 and 8");
			}

			return cellScratchpads[(row, column)];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <returns></returns>
		public IEnumerable<CellScratchpad> GetRow(int row)
		{
			return cellScratchpads.Where(w => w.Key.row == row).Select(s => s.Value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="column"></param>
		/// <returns></returns>
		public IEnumerable<CellScratchpad> GetColumn(int column)
		{
			return cellScratchpads.Where(w => w.Key.column == column).Select(s => s.Value);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public IEnumerable<CellScratchpad> GetBox(int row, int column)
		{
			var rowLowerLimit = (row / 3) * 3;
			var rowUpperLimit = rowLowerLimit + 2;

			var columnLowerLimit = (column / 3) * 3;
			var columnUpperLimit = columnLowerLimit + 2;

			var results = cellScratchpads
				.Where(w =>
					(w.Key.row >= rowLowerLimit & w.Key.row <= rowUpperLimit) &
					(w.Key.column >= columnLowerLimit & w.Key.column <= columnUpperLimit))
				.Select(s => s.Value);

			return results;
		}

		/// <summary>
		/// 012
		/// 345
		/// 678
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public IEnumerable<CellScratchpad> GetBox(int index)
		{
			var rowLowerLimit = (index / 3) * 3;
			var rowUpperLimit = rowLowerLimit + 2;

			var columnLowerLimit = (index / 3) * 3;
			var columnUpperLimit = columnLowerLimit + 2;

			var results = cellScratchpads
				.Where(w =>
					(w.Key.row >= rowLowerLimit & w.Key.row <= rowUpperLimit) &
					(w.Key.column >= columnLowerLimit & w.Key.column <= columnUpperLimit))
				.Select(s => s.Value);

			return results;	
		}

		private void ConstructCellScratchpads(int?[,] puzzle)
		{
			for (var row = 0; row < 9; row++)
			{
				for (var column = 0; column < 9; column++)
				{
					var key = (row: row, column: column);
					var value = puzzle[row, column];

					if (!cellScratchpads.ContainsKey(key))
					{
						var cellScratchpad = new CellScratchpad(row, column, value);
						cellScratchpads.Add(key, cellScratchpad);
					}
				}
			}
		}
	}
}
