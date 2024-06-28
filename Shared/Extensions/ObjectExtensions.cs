namespace Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object? obj)
        {
            if (obj == null)
                return true;
            return false;
        }

        public static bool IsNotNull(this object? obj)
        {
            return !obj.IsNull();
        }
    }
}
