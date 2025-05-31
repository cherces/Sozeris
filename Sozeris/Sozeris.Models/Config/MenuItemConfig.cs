namespace Sozeris.Models.Config;

public class MenuItemConfig
{
    public string Title { get; set; }
    public Type PageType { get; set; }
    public string Route { get; set; }
    public int Order { get; set; }
    public string Icon { get; set; }
}