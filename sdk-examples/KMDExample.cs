using Algorand.Algod.Model;
using Algorand.KMD;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;


namespace sdk_examples
{

    /// <summary>
    /// This example uses the KMD api to access Sandbox's default wallet to get the three predefined test accounts.
    /// </summary>
    internal class KMDExample
    {

        private static Api kmdApi;
        private static Account account1;
        private static Account account2;
        private static Account account3;
        private const string walletName = "unencrypted-default-wallet";

        public static async Task Main(string[] args)
        {

            //make a connection to the Algod node
            SetUpAlgodConnection();

            //Set up accounts based on the Sandbox KMD
            await SetUpAccounts();

            Console.WriteLine($"Account 1 address is {account1.Address}");
            Console.WriteLine($"Account 2 address is {account2.Address}");
            Console.WriteLine($"Account 3 address is {account3.Address}");
        }


        private static void SetUpAlgodConnection()
        {
            //A standard sandbox connection
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-KMD-API-Token", "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

            kmdApi=new Api(client);
            kmdApi.BaseUrl = @"http://localhost:4002";
            

        }

        private static async Task SetUpAccounts()
        {
            var accounts = await getDefaultWallet();

            //get accounts based on the above private keys using the .NET SDK
            account1 = accounts[0];
            account2 = accounts[1];
            account3 = accounts[2];
        }

        private static async Task<List<Account>> getDefaultWallet()
        {
            string handle = await getWalletHandleToken();
            var accs = await kmdApi.ListKeysInWalletAsync(new ListKeysRequest() { Wallet_handle_token  = handle });
            if (accs.Addresses.Count < 3) throw new Exception("Sandbox should offer minimum of 3 demo accounts.");

            List<Account> accounts = new List<Account>();
            foreach (var a in accs.Addresses)
            {
                
                var resp = await kmdApi.ExportKeyAsync(new ExportKeyRequest() { Address = a, Wallet_handle_token = handle, Wallet_password = "" });
                Account account = new Account(resp.Private_key);
                accounts.Add(account);
            }
            return accounts;

        }

        private static async Task<string> getWalletHandleToken()
        {
            var wallets = await kmdApi.ListWalletsAsync(null);
            var wallet = wallets.Wallets.Where(w => w.Name == walletName).FirstOrDefault();
            var handle = await kmdApi.InitWalletHandleTokenAsync(new InitWalletHandleTokenRequest() { Wallet_id = wallet.Id, Wallet_password = "" });
            return handle.Wallet_handle_token;
        }


    }
}
