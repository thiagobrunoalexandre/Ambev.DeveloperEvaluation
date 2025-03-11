using System;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Branch
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Location { get; private set; }

        public Branch(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }
}
