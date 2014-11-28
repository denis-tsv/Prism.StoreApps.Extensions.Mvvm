using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;

namespace Prism.StoreApps.Extensions.Common
{
	public class EnumWrapper<TEnum> where TEnum : struct
	{
		private readonly TEnum _value;
		private readonly string _name;

		public EnumWrapper(TEnum value)
		{
			_value = value;
			_name = value.ToString();
		}

		public EnumWrapper(TEnum value, string name)
		{
			_value = value;
			_name = name;
		}

		public TEnum Value
		{
			get { return _value; }
		}

		public string Name
		{
			get { return _name; }
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;

			if (GetType() != obj.GetType()) return false;

			return _value.Equals(((EnumWrapper<TEnum>)obj)._value);
		}

		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		public override string ToString()
		{
			return Name;
		}

		public static EnumWrapper<TEnum>[] GetLocalizedValues()
		{
			var enumType = typeof(TEnum);
			var memberInfos = enumType.GetTypeInfo().DeclaredMembers;

			var result = new List<EnumWrapper<TEnum>>();

			foreach (var memberInfo in memberInfos)
			{
				TEnum value;

				var parsed = Enum.TryParse(memberInfo.Name, out value);

				if (parsed)
				{
					var displayAttribute = memberInfo.GetCustomAttribute<DisplayAttribute>();
					Debug.Assert(displayAttribute != null);

					result.Add(new EnumWrapper<TEnum>(value, displayAttribute.GetName()));
				}
			}
			return result.ToArray();
		}
	}
}
