using System.Text.Json;


public static class ObjectExtensions
{
    public static string PrettyFormat(this object obj)
    {
        return obj.GetType().Name + " " + JsonSerializer.Serialize(obj, new JsonSerializerOptions()
        {
            WriteIndented = true,
        });
    }
}