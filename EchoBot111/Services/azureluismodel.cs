using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.Collections.Generic;

namespace EchoBot111.Services
{
    public class azureluismodel
    {


        public Result result { get; set; }
    }

    public class Result
    {
        public string query { get; set; }
        public prediction prediction { get; set; }
    }



    public class prediction
    {

        public string topintent { get; set; }
        public string projectkind { get; set; }
        public List<intent> intents { get; set; }

    }





    public class intent
    {


        public string category { get; set; }

        public double confidencescore { get; set; }
    }
}
