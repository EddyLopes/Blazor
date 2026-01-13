using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GestaoSpaceEdu.Client.Libraries.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue) 
    {
        var enumType = enumValue.GetType();
        var memberInfo = enumType.GetMember(enumValue.ToString());

        if(memberInfo.Length > 0)
        {
            var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

            if(displayAttribute is not null && displayAttribute.Name is not null)
            {
                return displayAttribute.Name; 
            }
        }

        return enumValue.ToString();    
    }
}
