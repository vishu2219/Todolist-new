using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Todolist
{

    [Table("task")]
    public class task
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string task2 { get; set; }
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<subtask> subtasks { get; set; } = new List<subtask>();

    }



    [Table("subtasks")]
    public class subtask
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [ForeignKey(typeof(task))]
        public int taskId { get; set; }
        public string sub { get; set; }
        //[OneToOne(CascadeOperations = CascadeOperation.CascadeRead)]
        // public task task { get; set; }
        [ManyToOne]
        public task task { get; set; }
    }

}