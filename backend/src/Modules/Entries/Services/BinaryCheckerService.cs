using Common.Exceptions;
using Entries.DTOs;

namespace Entries.Services;

public class BinaryCheckerService : IBinaryChecker
{

    public BinaryCheckResponseDTO ValidateBinaryString(string input)
    {
        if (input == null || input == "") throw new BadRequestException("Error: Binary String is required");
        // Equal number of 0's and 1's

        //// Two counters to store number of each 1 or 0

        var zerosCount = 0;
        var onesCount = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != '0' && input[i] != '1')
            {
                return new BinaryCheckResponseDTO
                {
                    valid = false,
                    error = "Error: Invalid characters, please provide a binary string"
                };
            }
            if (input[i] == '0') zerosCount++;
            if (input[i] == '1') onesCount++;

            // For every portion the number of 1's is not less than 0's
            if (zerosCount > onesCount)
            {
                return new BinaryCheckResponseDTO
                {
                    valid = false,
                    error = "Error: ones number should not be less than the number of zeros in any portion"
                };
            };
        }

        if (zerosCount != onesCount)
        {
            return new BinaryCheckResponseDTO
            {
                valid = false,
                error = "Error: the number of Zeros and Ones are not equal"
            };
        }

        return new BinaryCheckResponseDTO
        {
            valid = true,
        };
    }
}
