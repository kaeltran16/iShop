using System;

namespace iShop.Web.Server.Commons.BaseClasses
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
