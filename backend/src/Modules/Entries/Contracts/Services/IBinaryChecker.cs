using Entries.DTOs;

public interface IBinaryChecker
{
    BinaryCheckResponseDTO ValidateBinaryString(string input);
}
