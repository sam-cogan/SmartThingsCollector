using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SmartThingsCollector
{
    [DataContract(Name = "EventData", Namespace = "Microsoft.ServiceBus.Messaging")]
    public class EventData
    {
        [DataMember(Name = "SequenceNumber")]
        public long SequenceNumber { get; set; }

        [DataMember(Name = "Offset")]
        public string Offset { get; set; }

        [DataMember(Name = "EventEnqueuedUtcTime")]
        public DateTime EventEnqueuedUtcTime { get; set; }

        [DataMember(Name = "SystemProperties")]
        public Dictionary<string, object> SystemProperties { get; set; }

        [DataMember(Name = "Properties")]
        public Dictionary<string, object> Properties { get; set; }

        [DataMember(Name = "sensorId")]
        public string sensorId { get; set; }

        [DataMember(Name = "sensorName")]
        public string sensorName { get; set; }

        [DataMember(Name = "SensorType")]
        public string sensorType { get; set; }

        [DataMember(Name = "value")]
        public string value { get; set; }


        public EventData(dynamic record)
        {
            SequenceNumber = (long)record.SequenceNumber;
            Offset = (string)record.Offset;
            DateTime.TryParse((string)record.EnqueuedTimeUtc, out var enqueuedTimeUtc);
            EventEnqueuedUtcTime = enqueuedTimeUtc;
            SystemProperties = (Dictionary<string, object>)record.SystemProperties;
            Properties = (Dictionary<string, object>)record.Properties;
            var bodyText = Encoding.UTF8.GetString((byte[])record.Body);

            var sensordata = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(bodyText);
            sensorId = sensordata.sensorId;
            sensorName = sensordata.sensorName;
            sensorType = sensordata.sensorType;
            value = sensordata.value;


        }

    }
}
