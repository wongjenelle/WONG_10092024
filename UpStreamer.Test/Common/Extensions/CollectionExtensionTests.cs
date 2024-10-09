using FluentAssertions;
using UpStreamer.Server.Common.Extensions;
using Xunit;

namespace UpStreamer.Test.Common.Extensions
{
    public class CollectionExtensionTests
    {
        [Fact]
        public void When_EnumerableIsNull_Return_True()
        {
            IEnumerable<string>? enumerable = null;

            enumerable.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void When_EnumerableIsEmpty_Return_True()
        {
            IEnumerable<string>? enumerable = Enumerable.Empty<string>();

            enumerable.IsNullOrEmpty().Should().BeTrue();
        }

        [Fact]
        public void When_EnumerableIsNotEmpty_Return_False()
        {
            IEnumerable<string>? enumerable = [""];

            enumerable.IsNullOrEmpty().Should().BeFalse();
        }
    }
}
