using System;
using System.ComponentModel.DataAnnotations;

namespace Corporate.MyFilmes.Schedule.Domain.Base
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
