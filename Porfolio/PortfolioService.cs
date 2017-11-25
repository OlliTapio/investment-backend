using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace investment_backend.Portfolio
{
    public class PortfolioService : IPortfolioService
    {
        private readonly string _accountId;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _token;

        public PortfolioService(){
            var apiValues = (JObject)JsonConvert.DeserializeObject(System.IO.File.ReadAllText(Directory.GetCurrentDirectory() + "/json/nordea_api.json"));

            _accountId = (string)apiValues["api"]["accountId"];
            _clientId = (string)apiValues["api"]["clientId"];
            _clientSecret = (string)apiValues["api"]["clientSecret"];
            _token = (string)apiValues["api"]["token"];
        }

        public async Task<Account> GetAcccount(){
            var accountTransactions = await GetAccountTransactionsFromApi();
            ParseTransactions(accountTransactions);

            var accountDetails = await GetAccountDetailsFromApi();
            var returnValue = ParseAccountDetails(accountDetails);
            return returnValue;
        }

        private async Task<JObject> GetAccountDetailsFromApi(){
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-IBM-Client-Id", _clientId);
            client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", _clientSecret);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            Console.Write(client);

            var stringTask = client.GetStringAsync("https://api.hackathon.developer.nordeaopenbanking.com/v2/accounts/" + _accountId);
 
            var msg = await stringTask;
            var accountDetails = (JObject)JsonConvert.DeserializeObject(msg);
            

            return accountDetails;
        }

        private Account ParseAccountDetails(JObject accountDetails){
            Console.Write(accountDetails);
            var account = new Account();
            account.AvailableBalance = (double)accountDetails["response"]["availableBalance"];
            account.BookedBalance = (double)accountDetails["response"]["bookedBalance"];
            return account;
        }

        private async Task<JObject> GetAccountTransactionsFromApi(){
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-IBM-Client-Id", _clientId);
            client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", _clientSecret);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var stringTask = client.GetStringAsync("https://api.hackathon.developer.nordeaopenbanking.com/v2/accounts/" + _accountId + "/transactions");
 
            var msg = await stringTask;
            var accountTransactions = (JObject)JsonConvert.DeserializeObject(msg);
            
            return accountTransactions;
        }
        private List<Transaction> ParseTransactions(JObject accountTransactions)
        {
            var transactions = new List<Transaction>();
            foreach(var accountTransaction in accountTransactions["response"]["transactions"]){
                var transaction = new Transaction();
                transaction.Amount = (double)accountTransaction["amount"];
                transaction.TransactionId = (double)accountTransaction["transactionId"];                
                transaction.Currency = (string)accountTransaction["currency"];
                
                transaction.BookingDate = (DateTime)accountTransaction["bookingDate"];
                transaction.ValueDate = (DateTime)accountTransaction["valueDate"];
                transaction.TypeDescription = (string)accountTransaction["typeDescription"];
                transactions.Add(transaction);
            }
            return transactions;
        }

    }
}
