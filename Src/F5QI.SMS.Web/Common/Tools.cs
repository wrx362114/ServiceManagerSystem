using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F5QI.SMS.Web.Common
{
    public static class Tools
    {
        public static IEnumerable<KeyValuePair<string, string>> Enum2NameValueList(Type typeInfo)
        {
            if (!typeInfo.IsEnum)
            {
                yield break;
            }
            foreach (var item in typeInfo.GetEnumNames())
            {
                var field = typeInfo.GetField(item);
                var attrs= field.GetCustomAttributesData();
                if (!attrs.Any())
                {
                    yield break;
                } //(typeof(DisplayAttribute), false)
                var name = attrs.First().NamedArguments.First(a => a.MemberName == "Name");
                yield return new KeyValuePair<string, string>(item, name.TypedValue.Value.ToString());
            }
        }
    }

}