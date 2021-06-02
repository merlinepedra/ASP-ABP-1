using System;
using Volo.Abp.Domain.Entities;

namespace MyCompanyName.MyProjectName.Users
{
    public class MyEntity : AggregateRoot<Guid>
    {
        public decimal Price { get; set; }

        protected MyEntity()
        {

        }

        public MyEntity(Guid id)
            : base(id)
        {

        }
    }
}
