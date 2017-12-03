using System;

namespace ExampleWebApi.Models.General
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}