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

        public string StoredName { get; set; }

        public Guid AggregateId { get; set; }

        public string EventBody { get; set; }

        public StoredEvent(string storedName ,Guid aggregateId ,string eventBody)
        {
            StoredName = storedName;
            AggregateId = aggregateId;
            EventBody = eventBody;
        }

        public StoredEvent() { }
    }
}
