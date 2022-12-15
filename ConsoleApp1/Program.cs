using SIRI;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


var subscriptionRequest = new SubscriptionRequest();
subscriptionRequest.MessageIdentifier = new MessageQualifierStructure { Value = Guid.NewGuid().ToString() };
subscriptionRequest.RequestTimestamp= DateTime.Now;
subscriptionRequest.VehicleMonitoringSubscriptionRequest = new[] {new VehicleMonitoringSubscriptionStructure()
{
    SubscriberRef = new ParticipantRefStructure { Value= Guid.NewGuid().ToString() },
    
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