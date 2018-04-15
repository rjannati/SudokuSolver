using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver.Core
{
	public class SudokuResult
	{
		public bool Solved { get; set; }

		public string Message { get; set; }

		public int[,] Solution { get; set; }
    }
}
