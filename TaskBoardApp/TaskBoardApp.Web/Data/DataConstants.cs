namespace TaskBoardApp.Web.Data
{
    /// <summary>
    /// Data constants for the entities
    /// </summary>
    public static class DataConstants
    {
        /// <summary>
        /// User FirstName length
        /// </summary>
        public const int UserFirstNameMaxLength = 15;
        /// <summary>
        /// User LastName length
        /// </summary>
        public const int UserLastNameMaxLength = 15;

        /// <summary>
        /// Task title min length
        /// </summary>
        public const int TaskTitleMinLength = 5;
        /// <summary>
        /// Task title max length
        /// </summary>
        public const int TaskTitleMaxLength = 75;
        /// <summary>
        /// Task description min length
        /// </summary>
        public const int TaskDescriptionMinLength = 10;
        /// <summary>
        /// Task description max length
        /// </summary>
        public const int TaskDescriptionMaxLength = 1000;
        /// <summary>
        /// Board name min length
        /// </summary>
        public const int BoardNameMinLength = 3;
        /// <summary>
        /// BoardName max length
        /// </summary>
        public const int BoardNameMaxLength = 30;

    }
}
