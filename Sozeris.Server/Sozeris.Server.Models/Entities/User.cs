namespace Sozeris.Server.Models.Entities;

public class User : FilterBase
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}