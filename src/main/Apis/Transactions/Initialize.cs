﻿using Newtonsoft.Json;
using XtremeIT.Library.Pins;

namespace PayStack.Net
{
    public class TransactionInitializeRequest : RequestMetadataExtender
    {
        public string Reference { get; set; }

        [JsonProperty("amount")]
        public string AmountInKobo { get; set; }

        public string Email { get; set; }

        public string Plan { get; set; }

        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        public string SubAccount { get; set; }

        [JsonProperty("transaction_charge")]
        public int TransactionCharge { get; set; }

        public string Bearer { get; set; }

        public override void Prepare()
        {
            base.Prepare();
            Reference =
                $"{Reference}-{Generator.NewPin(new GeneratorSettings {Domain = GeneratorCharacterDomains.AlphaNumerics, PinLength = 7})}";
        }
    }

    public static class PayStackChargesBearer
    {
        public static string Account = nameof(Account).ToLower();
        public static string SubAccount = nameof(SubAccount).ToLower();
    }

    public class TransactionInitialize
    {
        public class Data
        {
            [JsonProperty("authorization_url")]
            public string AuthorizationUrl { get; set; }

            [JsonProperty("access_code")]
            public string AccessCode { get; set; }

            [JsonProperty("reference")]
            public string Reference { get; set; }
        }
    }

    public class TransactionInitializeResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public TransactionInitialize.Data Data { get; set; }
    }
}