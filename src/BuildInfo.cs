namespace RevitDevX
{
    public class BuildInfo
    {
        private static readonly int _buildId = 0;
        public static readonly int BUILD_ID = _buildId;
        public static string BUILD_VERSION = _buildId.ToString();
        public static string COMMIT_ID = "{sha}";
        public static int MAJOR_BUILD_VERSION_ID = 1;
        public static int MINOR_BUILD_VERSION_ID = 0;
    }
}