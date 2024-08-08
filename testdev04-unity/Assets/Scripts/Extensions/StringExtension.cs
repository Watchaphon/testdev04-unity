public static class StringExtension
{
    /// <summary>
    /// Check string is empty or null.
    /// </summary>
    /// <param name="str"></param>
    /// <returns>True if null or empty</returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }
}
