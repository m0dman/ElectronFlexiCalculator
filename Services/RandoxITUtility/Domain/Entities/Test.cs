using System;
using RandoxITUtility.Domain;

namespace RandoxITUtility.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        /// <summary>
        /// private, parameterless constructor used by EF Core
        /// </summary>
        private Test() { }

        /// <summary>
        /// public constructor - support property setting for a single Test record
        /// </summary>
        /// <param name="name">the name of the test</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the name is null or whitespace</exception>
        public Test(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        #region Public Update 

        /// <summary>
        /// Public accessor for updating a test name
        /// </summary>
        /// <param name="name">The new test name</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the name is null or whitespace</exception>
        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }

        #endregion
    }
}