using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver.Core
{
	public class CellScratchpad
    {
		private readonly bool immutable;
		private int? cellValue = null;
		private List<int> candidates = new List<int>();

		public (int row, int column) CellLocation { get; private set; }
		public int? Value
		{
			get { return cellValue; }
			set
			{
				if(immutable)
				{
					throw new System.InvalidOperationException("This cell has a value that is part of the puzzle.");
				}
				cellValue = value;
			}
		}
		public IEnumerable<int> Candidates => candidates;
		public bool Immutable => immutable;

		public CellScratchpad(int row, int column, int? value = null)
		{
			CellLocation = (row: row, column: column);
			if(value.HasValue)
			{
				immutable = true;
				cellValue = value.Value;
			}
		}

		public void AddCandidates(params int[] values)
		{
			if (!immutable)
			{
				candidates.AddRange(values);
			}
		}

		public void RemoveCandidates(params int[] values)
		{
			if(!immutable)
			{
				candidates.RemoveAll(v => values.Contains(v));
			}
		}
    }
}
