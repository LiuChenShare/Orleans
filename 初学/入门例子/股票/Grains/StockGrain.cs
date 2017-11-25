using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Grains
{
    public class StockGrain : Orleans.Grain, IGrains.IStockGrain
    {
        string price;
        string graphData;
        public override async Task OnActivateAsync()
        {
            string stock;
            this.GetPrimaryKey(out stock);
            await UpdatePrice(stock);

            var timer = RegisterTimer(
                UpdatePrice,
                stock,
                TimeSpan.FromMinutes(1),
                TimeSpan.FromMinutes(1));

            await base.OnActivateAsync();
        }

        async Task UpdatePrice(object stock)
        {
            // 无需等待就收集任务变量
            var priceTask = GetPriceFromYahoo(stock as string);
            var graphDataTask = GetYahooGraphData(stock as string);

            // 等待的任务
            await Task.WhenAll(priceTask, graphDataTask);

            // 读取结果
            price = priceTask.Result;
            graphData = graphDataTask.Result;
            Console.WriteLine(price);
            Console.WriteLine(graphData);
        }

        public Task<string> GetPriceFromYahoo(string stock)
        {
            var uri = "获取的关于" + stock + "的时间是：" + DateTime.Now.ToString("t");
            //using (var http = new HttpClient())
            //using (var resp = await http.GetAsync(uri))
            //{
            //    return await resp.Content.ReadAsStringAsync();
            //}
            return Task.FromResult(uri);
        }

        public Task<string> GetYahooGraphData(string stock)
        {
            // retrieve the graph data from Yahoo finance
            //var uri = string.Format(
            //    "http://chartapi.finance.yahoo.com/instrument/1.0/{0}/chartdata;type=quote;range=1d/csv/", stock);
            //using (var http = new HttpClient())
            //using (var resp = await http.GetAsync(uri))
            //{
            //    return await resp.Content.ReadAsStringAsync();
            //}
            var uri = "获取的关于" + stock + "的第二个时间是：" + DateTime.Now.ToString("t");
            return Task.FromResult(uri);
        }

        public Task<string> GetPrice()
        {
            return Task.FromResult(price);
        }
    }
}
