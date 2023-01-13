using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace StoneShop.Utility
{
    public static class SessionExtensions
    {
        public static void Set<T> (this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));  // в некое облако закидываем список товаров корзины в формате json под именем key
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key); // достаем из облака значение по ключу 
            return value == null ? default : JsonSerializer.Deserialize<T>(value); // если значение не пустое то десериализуем его 
        }
    }
}
