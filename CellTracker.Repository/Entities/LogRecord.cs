using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CellTracker.Repository.Entities
{
    [Table("LogRecords")]
    public abstract class LogRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [NotMapped]
        public CellActionType CellActionType
        {
            get => Enum.Parse<CellActionType>(CellActionTypeString);
            set => CellActionTypeString = value.ToString();
        }

        [Column("CellActionType")]
        internal string CellActionTypeString { get; set; }

        public string SubscriberId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}