using System;
using System.ComponentModel;
using System.Linq;

namespace CBD.Model.Enums
{
    public static class Enums
    {
        public static string Description(this Enum value)
        {
            var descriptionAttribute = (DescriptionAttribute)value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(false)
                .Where(a => a is DescriptionAttribute)
                .FirstOrDefault();

            return descriptionAttribute != null ? descriptionAttribute.Description : value.ToString();
        }
    }

    public enum USED_STATE : int
    {
        [Description("Actived")]
        Actived = 1,
        [Description("Inactived")]
        Inactived = 2,
        [Description("Deleted")]
        Deleted = 3,
    }

    public enum PAGE_USED_STATE : int
    {
        [Description("Actived")]
        Actived = 1,
        [Description("Inactived")]
        Inactived = 2,
        [Description("New")]
        New = 3,
        [Description("Demo")]
        Demo = 4,
    }
}
