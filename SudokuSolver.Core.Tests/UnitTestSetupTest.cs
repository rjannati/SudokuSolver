using NSubstitute;
using Xunit;

namespace SudokuSolver.Core.Tests
{
	public class UnitTestSetupTest
	{
		private ITestMockable testMockable;

		public UnitTestSetupTest()
		{
			testMockable = Substitute.For<ITestMockable>();
		}

		[Fact]
		public void Ensure_XUnit_Works()
		{
			Assert.True(true);
		}

		[Fact]
		public void Ensure_NSubstitute_Works()
		{
			testMockable.GetValue().Returns(5);
			Assert.Equal(5, testMockable.GetValue());
		}
	}

	public interface ITestMockable
	{
		int GetValue();
	}
}
