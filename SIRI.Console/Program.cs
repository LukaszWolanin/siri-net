using SIRI;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


var subscriptionRequest = new SubscriptionRequest();
subscriptionRequest.MessageIdentifier = new MessageQualifierStructure { Value = Guid.NewGuid().ToString() };
subscriptionRequest.RequestTimestamp= DateTime.UtcNow;
subscriptionRequest.SubscriptionContext = new SubscriptionContextStructure()
{
    HeartbeatInterval = "PT1M"
};
subscriptionRequest.Address = "https://db80-195-136-19-28.eu.ngrok.io";
subscriptionRequest.RequestorRef = new ParticipantRefStructure { Value = Guid.NewGuid().ToString() };
subscriptionRequest.VehicleMonitoringSubscriptionRequest = new[] {new VehicleMonitoringSubscriptionStructure()
{
    SubscriberRef = new ParticipantRefStructure { Value= Guid.NewGuid().ToString() },
    SubscriptionIdentifier = new SubscriptionQualifierStructure { Value = Guid.NewGuid().ToString() },
    InitialTerminationTime= DateTime.UtcNow.AddMinutes(10),
    VehicleMonitoringRequest = new VehicleMonitoringRequestStructure
    {
        RequestTimestamp= DateTime.UtcNow,
    }
    
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


var httpClient = new HttpClient();
var response = await httpClient.PostAsync("https://api.entur.io/realtime/v1/subscribe", new StringContent(Encoding.UTF8.GetString(memoryStream.ToArray()), Encoding.UTF8, "application/xml"));
Console.WriteLine(result);