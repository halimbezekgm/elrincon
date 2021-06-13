using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elrincon.web.Utils
{
    public class ConvertHelper
    {
        public bool ToBoolean(object value, bool defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToBoolean(value);
        }

        public bool ToBoolean(object value)
        {
            return ToBoolean(value, false);
        }

        public bool? ToNullableBoolean(object value)
        {
            return IsDbNull(value) ? (bool?)null : Convert.ToBoolean(value);
        }

        public byte ToByte(object value, byte defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToByte(value);
        }

        public byte ToByte(object value)
        {
            return ToByte(value, 0);
        }

        public byte? ToNullableByte(object value)
        {
            return IsDbNull(value) ? (byte?)null : Convert.ToByte(value);
        }

        public char ToChar(object value)
        {
            return ToChar(value, char.MinValue);
        }

        public char ToChar(object value, char defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToChar(value);
        }

        public char? ToNullableChar(object value)
        {
            return IsDbNull(value) ? (char?)null : Convert.ToChar(value);
        }

        public DateTime ToDateTime(object value)
        {
            return ToDateTime(value, DateTime.MinValue);
        }

        public DateTime ToDateTime(object value, DateTime defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToDateTime(value);
        }

        public DateTime? ToNullableDateTime(object value)
        {
            return IsDbNull(value) ? (DateTime?)null : Convert.ToDateTime(value);
        }

        public double ToDouble(object value, double defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToDouble(value);
        }

        public double ToDouble(object value)
        {
            return ToDouble(value, 0);
        }

        public double? ToNullableDouble(object value)
        {
            return IsDbNull(value) ? (double?)null : Convert.ToDouble(value);
        }

        public decimal ToDecimal(object value, decimal defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToDecimal(value);
        }

        public decimal ToDecimal(object value)
        {
            return ToDecimal(value, 0);
        }

        public decimal? ToNullableDecimal(object value)
        {
            return IsDbNull(value) ? (decimal?)null : Convert.ToDecimal(value);
        }

        public short ToInt16(object value, short defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToInt16(value);
        }

        public short ToInt16(object value)
        {
            return ToInt16(value, 0);
        }

        public short? ToNullableInt16(object value)
        {
            return IsDbNull(value) ? (short?)null : Convert.ToInt16(value);
        }

        public int ToInt32(object value, int defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToInt32(value);
        }

        public int ToInt32(object value)
        {
            return ToInt32(value, 0);
        }

        public int? ToNullableInt32(object value)
        {
            return IsDbNull(value) ? (int?)null : Convert.ToInt32(value);
        }

        public long ToInt64(object value, long defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToInt64(value);
        }

        public long ToInt64(object value)
        {
            return ToInt64(value, 0);
        }

        public long? ToNullableInt64(object value)
        {
            return IsDbNull(value) ? (long?)null : Convert.ToInt64(value);
        }

        public object ToObject(object value)
        {
            return Convert.IsDBNull(value) ? null : value;
        }

        public float ToSingle(object value, float defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToSingle(value);
        }

        public float ToSingle(object value)
        {
            return ToSingle(value, 0);
        }

        public float? ToNullableSingle(object value)
        {
            return IsDbNull(value) ? (float?)null : Convert.ToSingle(value);
        }

        public string ToSqlSafeString(string value)
        {
            if (value == null)
                return null;

            string result = value;
            if (result.Contains("'"))
                result = result.Replace("'", "''");
            return result;
        }

        public string ToString(object value, string defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToString(value);
        }

        public string ToString(object value)
        {
            return ToString(value, string.Empty);
        }

        public ushort ToUInt16(object value, ushort defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToUInt16(value);
        }

        public ushort ToUInt16(object value)
        {
            return ToUInt16(value, 0);
        }

        public ushort? ToNullableUInt16(object value)
        {
            return IsDbNull(value) ? (ushort?)null : Convert.ToUInt16(value);
        }

        public uint ToUInt32(object value, uint defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToUInt32(value);
        }

        public uint ToUInt32(object value)
        {
            return ToUInt32(value, 0);
        }

        public uint? ToNullableUInt32(object value)
        {
            return IsDbNull(value) ? (uint?)null : Convert.ToUInt32(value);
        }

        public ulong ToUInt64(object value, ulong defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToUInt64(value);
        }

        public ulong ToUInt64(object value)
        {
            return ToUInt64(value, 0);
        }

        public ulong? ToNullableUInt64(object value)
        {
            return IsDbNull(value) ? (ulong?)null : Convert.ToUInt64(value);
        }

        public Guid ToGuid(object value, Guid defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : new Guid(Convert.ToString(value));
        }

        public Guid ToGuid(object value)
        {
            return ToGuid(value, Guid.Empty);
        }

        public Guid? ToNullableGuid(object value)
        {
            return IsDbNull(value) ? (Guid?)null : new Guid(Convert.ToString(value));
        }

        public object FromString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DBNull.Value;

            return value;
        }

        public bool IsDbNull(object value)
        {
            return value == null || value == DBNull.Value;
        }
    }
}