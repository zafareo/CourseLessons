using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QovuntonSoftJson.Customs;

public class JsonQovunSerializer
{
    public string Serialize(object obj)
    {
        StringBuilder sb = new StringBuilder();
        SerializeValue(obj, sb);
        return sb.ToString();
    }

    public T Deserialize<T>(string json)
    {
        Dictionary<string, object> jsonObject = ParseObject(json);
        return DeserializeObject<T>(jsonObject);
    }

    /// <summary>
    /// Serialize methods
    /// </summary>
    /// <param name="value"></param>
    /// <param name="sb"></param>

    private void SerializeValue(object value, StringBuilder sb)
    {
        if (value == null)
        {
            sb.Append("null");
        }
        else if (value is string stringValue)
        {
            SerializeString(stringValue, sb);
        }
        else if (value is IDictionary dictionaryValue)
        {
            SerializeDictionary(dictionaryValue, sb);
        }
        else if (value is IEnumerable enumerableValue)
        {
            SerializeArray(enumerableValue, sb);
        }
        else if (IsNumericType(value))
        {
            sb.Append(value.ToString());
        }
        else
        {
            SerializeObject(value, sb);
        }
    }

    private void SerializeString(string value, StringBuilder sb)
    {
        sb.Append("\"");
        sb.Append(value.Replace("\"", "\\\""));
        sb.Append("\"");
    }

    private void SerializeDictionary(IDictionary dictionary, StringBuilder sb)
    {
        sb.Append("{");
        bool first = true;
        foreach (var key in dictionary.Keys)
        {
            if (!first)
            {
                sb.Append(",");
            }
            SerializeString(key.ToString(), sb);
            sb.Append(":");
            SerializeValue(dictionary[key], sb);
            first = false;
        }
        sb.Append("}");
    }

    private void SerializeArray(IEnumerable array, StringBuilder sb)
    {
        sb.Append("[");
        bool first = true;
        foreach (var item in array)
        {
            if (!first)
            {
                sb.Append(",");
            }
            SerializeValue(item, sb);
            first = false;
        }
        sb.Append("]");
    }

    private void SerializeObject(object obj, StringBuilder sb)
    {
        sb.Append("{");
        var properties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        bool first = true;
        foreach (var property in properties)
        {
            if (!first)
            {
                sb.Append(",");
            }
            SerializeString(property.Name, sb);
            sb.Append(":");
            SerializeValue(property.GetValue(obj), sb);
            first = false;
        }
        sb.Append("}");
    }

    private bool IsNumericType(object value)
    {
        return value is sbyte || value is byte || value is short || value is ushort ||
               value is int || value is uint || value is long || value is ulong ||
               value is float || value is double || value is decimal;
    }


    /// <summary>
    /// Deserialize methods
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
  
    private Dictionary<string, object> ParseObject(string json)
    {
        var jsonObject = new Dictionary<string, object>();

        var pairs = json.Trim('{', '}').Split(',');

        foreach (var pair in pairs)
        {
            var parts = pair.Split(':');
            var key = parts[0].Trim('\"');
            var value = parts[1].Trim();

            if (value.StartsWith("\""))
            {
                jsonObject[key] = value.Trim('\"');
            }
            else if (value == "true" || value == "false")
            {
                jsonObject[key] = bool.Parse(value);
            }
            else if (double.TryParse(value, out double number))
            {
                jsonObject[key] = number;
            }
        }

        return jsonObject;
    }

    private T DeserializeObject<T>(Dictionary<string, object> jsonObject)
    {
        T instance = Activator.CreateInstance<T>();
        PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (jsonObject.TryGetValue(property.Name, out object value))
            {
                object deserializedValue = DeserializeValue(property.PropertyType, value);
                property.SetValue(instance, deserializedValue);
            }
        }

        return instance;
    }

    private object DeserializeValue(Type valueType, object jsonValue)
    {
        if (valueType == typeof(string))
        {
            return jsonValue.ToString();
        }
        else if (valueType == typeof(int))
        {
            return int.Parse(jsonValue.ToString());
        }
        else if (valueType == typeof(double))
        {
            return double.Parse(jsonValue.ToString());
        }
        else if (valueType == typeof(bool))
        {
            return bool.Parse(jsonValue.ToString());
        }

        return null;
    }
}
