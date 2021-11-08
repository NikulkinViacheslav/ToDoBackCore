using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListReact.Domain
{
    public class TodoItem
    {
        [Key]        
        public int Id { get; set; }
                
        [Column(TypeName ="nvarchar(100)")]
        public string Note { get; set; }

        [Column(TypeName ="bit")]
        public bool Completed { get; set; }
    }
}
