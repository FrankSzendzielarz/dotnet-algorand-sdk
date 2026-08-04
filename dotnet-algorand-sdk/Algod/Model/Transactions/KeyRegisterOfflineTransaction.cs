using MessagePack;

namespace Algorand.Algod.Model.Transactions
{
    [MessagePackObject(AllowPrivate = true)]
    public partial class KeyRegisterOfflineTransaction : KeyRegistrationTransaction
    {
    
    }
}
