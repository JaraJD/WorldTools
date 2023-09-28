using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldTools.Domain.Entities
{
    public class StoredEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventId { get; set; }

        public string StoredName { get; set; }

        public string EventBody { get; set; }

        public StoredEvent(string storedName, string eventBody)
        {
            StoredName = storedName;
            EventBody = eventBody;
        }

        public StoredEvent() { }
    }
}
