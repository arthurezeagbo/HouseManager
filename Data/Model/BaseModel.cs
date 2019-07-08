using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class BaseModel
    {
        public BaseModel()
        {
            DateCreated = DateUpdated = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool MarkAsDeleted { get; set; } = false;
    }
}
