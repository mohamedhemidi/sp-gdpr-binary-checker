namespace Entries.DTOs;

public class BinaryCheckResponseDTO
{
    public bool valid { get; set; }
    public string? error { get; set; } = null;
}
