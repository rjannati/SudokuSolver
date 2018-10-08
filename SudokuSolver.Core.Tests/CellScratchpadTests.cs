using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;

namespace SudokuSolver.Core.Tests
{
    public class CellScratchpadTests
    {
		[Fact]
		public void Constructor_WhenCellHasNoOriginalValue_CreatesMutableCell()
		{
			var scratchPadCell = new CellScratchpad(3, 4, null);
			var (row, column) = scratchPadCell.CellLocation;

			Assert.NotNull(scratchPadCell);
			Assert.NotNull(scratchPadCell.Candidates);
			Assert.Equal(3, row);
			Assert.Equal(4, column);
			Assert.False(scratchPadCell.Immutable);
		}

		[Fact]
		public void Constructor_WhenCellHasOriginalValue_CreatesImmutableCell()
		{
			var scratchPadCell = new CellScratchpad(3, 4, 5);
			var (row, column) = scratchPadCell.CellLocation;

			Assert.NotNull(scratchPadCell);
			Assert.NotNull(scratchPadCell.Candidates);
			Assert.Equal(3, row);
			Assert.Equal(4, column);
			Assert.True(scratchPadCell.Immutable);
		}

		[Fact]
		public void ValueProperty_WhenCellScratchpadIsImmutable_ThrowsException()
		{
			var scratchPadCell = new CellScratchpad(3, 4, 5);
			Assert.Throws<InvalidOperationException>(() => scratchPadCell.Value = 2);
		}

		[Fact]
		public void ValueProperty_WhenCellScratchpadIsMutable_ValueIsChanged()
		{
			var scratchPadCell = new CellScratchpad(3, 4);
			scratchPadCell.Value = 5;

			Assert.Equal(5, scratchPadCell.Value);
		}

		[Fact]
		public void AddCandidates_WhenAddingValues_IsSuccessful()
		{
			var addedValues = new int[] { 3, 4, 5 };
			var scratchPadCell = new CellScratchpad(2, 2);
			scratchPadCell.AddCandidates(addedValues[0], addedValues[1], addedValues[2]);

			Assert.True(scratchPadCell.Candidates.Count() == 3 && 
				scratchPadCell.Candidates.All(a => addedValues.Contains(a)));
		}

		[Fact]
		public void AddCandidates_WhenRemovingValues_IsSuccessful()
		{
			var addedValues = new int[] { 3, 4, 5 };
			var scratchPadCell = new CellScratchpad(2, 2);
			scratchPadCell.AddCandidates(addedValues);

			scratchPadCell.RemoveCandidates(3, 5);

			Assert.True(scratchPadCell.Candidates.Count() == 1 && scratchPadCell.Candidates.First() == 4);
		}
	}
}
