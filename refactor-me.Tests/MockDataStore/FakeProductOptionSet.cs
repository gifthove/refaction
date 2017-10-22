namespace refactor_me.Tests.MockDataStore
{
    using refactor_me.data;
    using System;
    using System.Linq;

    /// <summary>
    /// Class FakeProductOptionSet.
    /// </summary>
    /// <seealso cref="refactor_me.Tests.MockDataStore.FakeDbSet{refactor_me.data.ProductOption}" />
    public class FakeProductOptionSet : FakeDbSet<ProductOption>
    {
        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>ProductOption.</returns>
        public override ProductOption Find(params object[] keyValues)
        {
            return this.SingleOrDefault(e => e.Id == (Guid)keyValues.Single());
        }
    }
}
