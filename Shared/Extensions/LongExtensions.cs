namespace Shared.Extensions
{
    public static class LongExtensions
    {
        // Megabytes
        public static long ToMb(this long l)
        {
            return (l / 1024 / 1024);
        }

        // Kilobytes
        public static long ToKb(this long l)
        {
            return (l / 1024);
        }

        public static bool IsBiggerZero(this long l)
        {
            return (l > 0);
        }

        public static bool IsZero(this long l)
        {
            return (l == 0);
        }
    }
}
