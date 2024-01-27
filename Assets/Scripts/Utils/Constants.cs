namespace Assets.Scripts.Utils
{
    public static class Constants
    {
        public enum Scores
        {
            /// <summary>
            /// Miss
            /// </summary>
            Diss,
            /// <summary>
            /// Bad
            /// </summary>
            FarOut,
            /// <summary>
            /// Good
            /// </summary>
            Crunk,
            /// <summary>
            /// Perfect
            /// </summary>
            Tubular
        }
    }

    public static class Tags
    {
        public const string PlayerOne = nameof(PlayerOne);
        public const string PlayerTwo = nameof(PlayerTwo);
        public const string Divider = nameof(Divider);
    }
}
