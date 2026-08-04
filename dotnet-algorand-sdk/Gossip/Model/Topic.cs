namespace Algorand.Gossip.Model
{
    /// <summary>
    /// https://github.com/algorand/go-algorand/blob/d9e8f58b7065158565cfe9b98734b86998003c6b/network/topics.go
    /// </summary>
    [MessagePack.MessagePackObject]
    public class Topic
    {

        [MessagePack.Key("key")]
        public string Key { get; set; }
        [MessagePack.Key("data")]
        public byte[] Data { get; set; }
             
    }
}
