using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
	public class CellScratchpad
    {
		private List<int> candidates = new List<int>();

		public (int row, int column) CellLocation { get; private set; }
		public int? Value { get; set; }
		public IEnumerable<int> Candidates => candidates;

		public CellScratchpad(int row, int column)
		{
			CellLocation = (row: row, column: column);
		}

		public void AddCandidates(params int[] values)
		{
			candidates.AddRange(values);
		}

		public void RemoveCandidates(params int[] values)
		{
			candidates.RemoveAll(v => values.Contains(v));
		}
    }
}
