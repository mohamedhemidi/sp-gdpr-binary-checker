using Entries.Services;
using Entries.DTOs;
namespace UnitTests;

public class BinaryStringsTests
{
    [Theory]
    [InlineData("1100", true, null)]
    [InlineData("1001", false, "Error: ones number should not be less than the number of zeros in any portion")]
    [InlineData("111000", true, null)]
    [InlineData("1010", true, null)]
    [InlineData("000111", false, "Error: ones number should not be less than the number of zeros in any portion")]
    [InlineData("1111", false, "Error: the number of Zeros and Ones are not equal")]
    [InlineData("0000", false, "Error: ones number should not be less than the number of zeros in any portion")]
    [InlineData("hello", false, "Error: Invalid characters, please provide a binary string")]
    public void IsGoodBinaryString_Test(string binaryString, bool expectedValid, string expectedError)
    {
        // Arrange
        var binaryStringEvaluator = new BinaryCheckerService();

        // Act
        BinaryCheckResponseDTO result = binaryStringEvaluator.ValidateBinaryString(binaryString);

        // Assert
        /*Assert.Equal(expected, result);*/

        Assert.Equal(expectedValid, result.valid);
        // Uncomment to test return error message:
        Assert.Equal(expectedError, result.error);
    }
}
