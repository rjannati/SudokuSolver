using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SudokuSolver.Core.Tests
{
    public class SudokuScratchpadTests
    {
		private readonly int?[,] puzzle = new int?[9, 9]
		{
			{ 5,    3,    null, /**/  null, 7,    null, /**/  null, null, null },
			{ 6,    null, null, /**/  1,    9,    5,    /**/  null, null, null },
			{ null, 9,    8,    /**/  null, null, null, /**/  null, 6,    null },
			/**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**/
			{ 8,    null, null, /**/  null, 6,    null, /**/  null, null, 3    },
			{ 4,    null, null, /**/  8,    null, 3,    /**/  null, null, 1    },
			{ 7,    null, null, /**/  null, 2,    null, /**/  null, null, 6    },
			/**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**//**/
			{ null, 6,    null, /**/  null, null, null, /**/  2,    8,    null },
			{ null, null, null, /**/  4,    1,    9,    /**/  null, null, 5    },
			{ null, null, null, /**/  null, 8,    null, /**/  null, 7,    9    }
		};

		private SudokuScratchpad scratchpadUnderTest;

		public SudokuScratchpadTests()
		{
			scratchpadUnderTest = new SudokuScratchpad(puzzle);
		}

		[Fact]
		public void Constructor_EnsureScratchpadCellsAreConstructedCorrectly_IsSuccessful()
		{
			Assert.Equal(5, scratchpadUnderTest.GetCellScratchpad(0, 0).Value);
			Assert.Equal(9, scratchpadUnderTest.GetCellScratchpad(8, 8).Value);
			Assert.Null(scratchpadUnderTest.GetCellScratchpad(2, 4).Value);
		}

		[Fact]
		public void GetRow_WhenRowIsValid_ReturnsCorrectRow()
		{
			var expected = new int[] { 7, 2, 6 };
			var row = scratchpadUnderTest.GetRow(5).ToList();

			Assert.Equal(3, row.Count(c => c.Value.HasValue));
			Assert.Equal(expected, row.Where(w => w.Value.HasValue).Select(s => s.Value.Value).ToArray());
		}

		[Fact]
		public void GetColumn_WhenColumnIsValid_ReturnsCorrectColumn()
		{
			var expected = new int[] { 7, 9, 6, 2, 1, 8 };
			var column = scratchpadUnderTest.GetColumn(4);

			Assert.NotNull(column);
			Assert.Equal(6, column.Count(c => c.Value.HasValue));
			Assert.Equal(expected, column.Where(w => w.Value.HasValue).Select(s => s.Value.Value).ToArray());
		}

		[Fact]
		public void GetBox_WhenCoordinateIsValid_ReturnsCorrectBox()
		{
			var expected = new int[] { 3,1,6 };
			var box = scratchpadUnderTest.GetBox(3, 8);

			Assert.NotNull(box);
			Assert.Equal(3, box.Count(c => c.Value.HasValue));
			Assert.Equal(expected, box.Where(w => w.Value.HasValue).Select(s => s.Value.Value).ToArray());
		}

		[Fact]
		public void GetBox_ByIndex_ReturnsCorrectValues()
		{
			var box = scratchpadUnderTest.GetBox(8);

			Assert.Equal(9, box.Count());
			var values = box.Where(w => w.Value != null).Select(s => s.Value.Value);
			Assert.Equal(new int[] { 2, 8, 5, 7, 9 }, values.ToArray());
		}

    }
}
