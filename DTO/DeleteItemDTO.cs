using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.DTO
{
    public record DeleteItemDTO 
    {
        [Required]
        public Guid Id { get; init; }
    }
}