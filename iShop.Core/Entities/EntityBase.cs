using System;

namespace iShop.Core.Entities
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        public EntityBase()
        {
            if (Id == Guid.Empty)
                Id = Guid.NewGuid();
        }
    }
}
