using System;

namespace DataAccess.Entities
{
    public abstract class AbstractEntity
    {
        public AbstractEntity()
        {
            Id = Guid.NewGuid().ToString();
        }
        
        public string Id { get; set; }
    }
}