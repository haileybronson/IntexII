using System.Text.Json;

namespace IntexII.Infrastructure;

//keeps track of the data, pulling and setting json 
//static for a session, stays only within the life of the program running
public static class SessionExtensions
{
    public static void SetJson (this ISession session, string key, object value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetJson<T>(this ISession session, string key)
    {
        var sessionData = session.GetString(key);

        return sessionData == null ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
    }
}