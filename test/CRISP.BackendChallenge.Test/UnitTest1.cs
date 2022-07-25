

using FluentAssertions;
using Xunit;

namespace CRISP.BackendChallenge.Test;

public class Tests
{

    [Fact]
    public void Test1()
    {
        true.Should().Be(true);
    }
}