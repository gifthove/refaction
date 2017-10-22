namespace refactor_me.core.Models
{
    /// <summary>
    /// Class Data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Data<T>
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public T Items { get; set; }
    }
}
