namespace TestApplication.Classes
{
    public class MainConfig
    {
        /// <summary>
        /// Максимальное количество "выпадающих" записей
        /// </summary>
        public int MaxSuggestCount { get; set; }

        /// <summary>
        /// Строка подключения к бд
        /// </summary>
        public string DbSuggestConnectionString { get; set; }
    }
}
