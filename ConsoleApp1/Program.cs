using SIRI;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


var subscriptionRequest = new SubscriptionRequest();
subscriptionRequest.MessageIdentifier = new MessageQualifierStructure { Value = Guid.NewGuid().ToString() };
subscriptionRequest.RequestTimestamp= DateTime.Now;
subscriptionRequest.SubscriptionContext = new SubscriptionContextStructure()
{
    HeartbeatInterval = "PT1M"
};
subscriptionRequest.Address = "http://loalhost:1234";
subscriptionRequest.RequestorRef = new ParticipantRefStructure { Value = Guid.NewGuid().ToString() };
subscriptionRequest.VehicleMonitoringSubscriptionRequest = new[] {new VehicleMonitoringSubscriptionStructure()
{
    SubscriberRef = new ParticipantRefStructure { Value= Guid.NewGuid().ToString() },
    SubscriptionIdentifier = new SubscriptionQualifierStructure { Value = Guid.NewGuid().ToString() },
    InitialTerminationTime= DateTime.Now.AddDays(1),
}};

var siri = new Siri
{
    Item = subscriptionRequest
};

var serializer = new XmlSerializer(typeof(Siri) );
await using var memoryStream = new MemoryStream();
var streamWriter = XmlWriter.Create(memoryStream, new()
{
    Encoding = Encoding.UTF8,
    Indent = true,
   
    
});
serializer.Serialize(streamWriter, siri);
var result = Encoding.UTF8.GetString(memoryStream.ToArray());

Console.WriteLine(result);