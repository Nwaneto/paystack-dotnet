# PayStack API for .Net
This library makes it easy to consume the [Payment API](http://developers.paystack.co/docs) from .Net projects.

## How to Install  
From Nuget
```
Install-Package PayStack.Net
```
# Usage
The most important type in this library is the **[PayStackApi](https://github.com/adebisi-fa/paystack-dotnet/blob/master/src/main/PayStackApi.cs)** class.  This can be created as follows:
```c#
var testOrLiveSecret = ConfigurationManager.AppSettings["PayStackSecret"];
var api = new PayStackApi(testOrLiveSecret);
```
To enhance discovery, all types are exposed under the **PayStack.Net** namespace. So, remember to include:
```c#
...
using PayStack.Net;
...
```

## Transactions API
To consume the Transactions API, use methods from the **[ITransactionsApi](https://github.com/adebisi-fa/paystack-dotnet/blob/master/src/main/Apis/Transactions/ITransactionsApi.cs)** interface (available via the **Transactions** property of **PayStackApi**, viz:
```c#

// Initializing a transaction
var response = api.Transactions.Initialize("user@somewhere.net", "5000000");
if (response.Status)
  // use response.Data
else
  // show response.Message
  
// Verifying a transaction
var verifyResponse = api.Transactions.Verify("transaction-reference"); // auto or supplied when initializing;
if (verifyResponse.Status)
  /* 
      You can save the details from the json object returned above so that the authorization code 
      can be used for charging subsequent transactions
      
      // var authCode = verifyResponse.Data.Authorization.AuthorizationCode
      // Save 'authCode' for future charges!
  */
```

The **ITransactionsApi** is defined as follows:
```c#
public interface ITransactionsApi
{
    TransactionInitializeResponse Initialize(string email, string amount);
    TransactionInitializeResponse Initialize(TransactionInitializeRequest request);
    TransactionVerifyResponse Verify(string reference);
    TransactionListResponse List(TransactionListRequest request = null);
    TransactionFetchResponse Fetch(string transactionId);
    TransactionTimelineResponse Timeline(string transactionIdOrReference);
    TransactionTotalsResponse Totals(DateTime? from = null, DateTime? to = null);

    TransactionExportResponse Export(DateTime? from = null, DateTime? to = null,
        bool settled = false, string paymentPage = null);
}
```

## Other APIs

Other APIs are implemented in like manner and exposed via the **PayStack** type as given below:
```c#
// Customer APIs

var request = new CustomerCreateRequest { ... };
var response = api.Customers.Create(request);

var listRequest = new CustomerListRequest { ... };
var listResponse = api.Customers.List(listRequest);  // api.Customers is of type ICustomersApi 

// Sub Accounts APIs

var saRequest = new SubAccountCreateRequest { ... };
var response = api.SubAccounts.Create(saRequest); // api.SubAccounts is of type ISubAccountsApi
// response.Status, response.Message, response.Data are available

// etc
