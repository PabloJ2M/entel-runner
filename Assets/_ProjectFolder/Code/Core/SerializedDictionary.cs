using System.Linq;
using UnityEngine.Rendering;

namespace System.Collections.Generic
{
    public static class SerializedDictionaryExtensions
    {
        public static Dictionary<K, V> ToDictionary<K, V>(this SerializedDictionary<K, V> sDict) => new(sDict);
        public static Dictionary<K, Dictionary<K2, V2>> ToDictionary<K, K2, V2>(this SerializedDictionary<K, SerializedDictionary<K2, V2>> sDict)
        {
            var result = new Dictionary<K, Dictionary<K2, V2>>();
            foreach (var kv in sDict) result[kv.Key] = kv.Value.ToDictionary();
            return result;
        }

        public static SerializedDictionary<K, V> ToSerializable<K, V>(this Dictionary<K, V> dict)
        {
            var sDict = Activator.CreateInstance<SerializedDictionary<K, V>>();
            foreach (var kv in dict) sDict[kv.Key] = kv.Value;
            return sDict;
        }
        public static SerializedDictionary<K, SerializedDictionary<K2, V2>> ToSerializable<K, K2, V2>(this Dictionary<K, Dictionary<K2, V2>> dict)
        {
            var sDict = Activator.CreateInstance<SerializedDictionary<K, SerializedDictionary<K2, V2>>>();
            foreach (var kv in dict) sDict[kv.Key] = kv.Value.ToSerializable();
            return sDict;
        }

        public static bool DeepEquals(object a, object b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a == null || b == null) return false;

            if (IsDictionary(a) && IsDictionary(b)) return CompareDicts(ToIDictionary(a), ToIDictionary(b));
            if (a is IEnumerable ea && b is IEnumerable eb && !(a is string)) return CompareEnumerable(ea, eb);
            return a.Equals(b);
        }
        private static bool CompareDicts(IDictionary a, IDictionary b)
        {
            if (a.Count != b.Count) return false;

            foreach (var key in a.Keys) {
                if (!b.Contains(key)) return false;
                if (!DeepEquals(a[key], b[key])) return false;
            }
            return true;
        }
        private static bool CompareEnumerable(IEnumerable a, IEnumerable b)
        {
            var A = a.Cast<object>().ToList();
            var B = b.Cast<object>().ToList();

            if (A.Count != B.Count) return false;

            for (int i = 0; i < A.Count; i++)
                if (!DeepEquals(A[i], B[i])) return false;

            return true;
        }

        private static bool IsDictionary(object o)
        {
            if (o == null) return false;
            if (o is IDictionary) return true;
            return IsSerializableDict(o);
        }
        private static bool IsSerializableDict(object o)
        {
            var t = o.GetType();

            if (!t.IsGenericType) return false;

            var g = t.GetGenericTypeDefinition();
            return g == typeof(SerializedDictionary<,>) ||
                   (t.BaseType?.IsGenericType == true &&
                    t.BaseType.GetGenericTypeDefinition() == typeof(SerializedDictionary<,,,>));
        }
        private static IDictionary ToIDictionary(object o)
        {
            if (o is IDictionary normal) return normal;
            if (IsSerializableDict(o)) return ConvertSerializedDictionary(o);
            throw new InvalidOperationException("No es un diccionario válido.");
        }

        private static IDictionary ConvertSerializedDictionary(object sDict)
        {
            var kType = sDict.GetType().GetGenericArguments()[0];
            var vType = sDict.GetType().GetGenericArguments()[1];

            var dictType = typeof(Dictionary<,>).MakeGenericType(kType, vType);
            var newDict = (IDictionary)Activator.CreateInstance(dictType);

            foreach (DictionaryEntry kv in (IDictionary)sDict) {
                var value = kv.Value;
                if (IsDictionary(value)) value = ToIDictionary(value);
                newDict.Add(kv.Key, value);
            }

            return newDict;
        }
    }
}