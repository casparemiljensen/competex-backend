namespace competex_backend.Helpers
{
    internal static class EnumMapper
    {
        public static TEnum? MapEnumValueTo<TEnum>(object fromEnum)
            where TEnum : struct, Enum
        {
            if (fromEnum == null) return null;

            return Enum.TryParse(fromEnum.ToString(), true, out TEnum outPutEnumValue) ? outPutEnumValue : null;
        }
    }
}
