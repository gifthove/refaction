namespace refactor_me.Tests.MockDataStore
{
    using refactor_me.data;
    using System;
    using System.Linq;

    /// <summary>
    /// Class FakeProductSet.
    /// </summary>
    /// <seealso cref="refactor_me.Tests.MockDataStore.FakeDbSet{refactor_me.data.Product}" />
    public class FakeProductSet : FakeDbSet<Product>
    {
        /// <summary>
        /// Finds the specified key values.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns>Product.</returns>
        public override Product Find(params object[] keyValues)
        {
            return this.SingleOrDefault(d => d.Id == (Guid)keyValues.Single());
        }
    }
}
