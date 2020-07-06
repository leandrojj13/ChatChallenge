using ChatChallenge.Services.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatChallenge.Services.Services.Stock
{
    public interface IStookService
    {
        Task<StookRequestReponse> GetStockInfoByCodeAsync(string code);
    }

    public class StookService : IStookService
    {
        public string ServiceUrl = "https://stooq.com/q/l/?s={{stock_code}}&f=sd2t2ohlcv&h&e=csv";
        
        
        public async Task<StookRequestReponse> GetStockInfoByCodeAsync(string code)
        {
            var url = ServiceUrl.Replace("{{stock_code}}", code);
            using (var httpClient = new HttpClient())
            using (var response = await httpClient.GetAsync(url))
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                try
                {
                    csv.Configuration.RegisterClassMap<StookRequestReponseObjectMap>();
                    csv.Configuration.Delimiter = ",";
                    var stock = csv.GetRecords<StookRequestReponse>()
                        .ToList().FirstOrDefault();
                    return stock;
                }
                catch
                {
                    return null;
                }
            }
        }
    }

    public sealed class StookRequestReponseObjectMap : ClassMap<StookRequestReponse>
    {
        public StookRequestReponseObjectMap()
        {
            Map(m => m.Symbol).Name("Symbol");
            Map(m => m.Date).Name("Date");
            Map(m => m.Time).Name("Time");
            Map(m => m.Open).Name("Open");
            Map(m => m.High).Name("High");
            Map(m => m.Low).Name("Low");
            Map(m => m.Close).Name("Close");
            Map(m => m.Volume).Name("Volume");
        }
    }
}
