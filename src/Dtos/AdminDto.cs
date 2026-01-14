namespace E_Commerce.Dtos;

public class AdminDto : UserDto
{
    public DateTimeOffset LastActionAtUtc { get; set; }
}