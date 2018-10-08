using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SudokuSolver.Core
{
    public class SudokuSolver
    {
		private readonly int[] numberSet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		private readonly int?[,] puzzle;
		private readonly SudokuScratchpad scratchpad;

		public SudokuSolver(int?[,] puzzle)
		{
			this.puzzle = puzzle ?? throw new ArgumentNullException(nameof(puzzle), "The puzzle supplied is null");
			scratchpad = new SudokuScratchpad(puzzle);
		}

		public SudokuResult Solve()
		{
			//TODO: initialize all Candidates



			return null;
		}

		private void InitializeScratchpadWithCandidates()
		{
		}

		public void UpdateCandidates(CellScratchpad cell)
		{
			if(cell == null)
			{
				throw new ArgumentNullException(nameof(cell), "CellScratchpad was not provided.");
			}
			if(cell.Value.HasValue)
			{
				//No point in evaluating this cell since it has a value, so exit.
				return;
			}
			if(cell.Candidates.Count() == 1)
			{
				cell.Value = cell.Candidates.First();
				return;
			}

			var cellRow = scratchpad.GetRow(cell.CellLocation.row)
				.Where(s => s.Value.HasValue)
				.Select(s => s.Value.Value).ToArray();

			var cellColumn = scratchpad.GetColumn(cell.CellLocation.column)
				.Where(s => s.Value.HasValue)
				.Select(s => s.Value.Value).ToArray();

			var cellBox = scratchpad.GetBox(cell.CellLocation.row, cell.CellLocation.column)
				.Where(s => s.Value.HasValue)
				.Select(s => s.Value.Value).ToArray();

			var invalidCandidates = cellRow.Union(cellColumn).Union(cellBox);

			cell.AddCandidates(numberSet.Except(invalidCandidates).ToArray());
		}
    }
}
