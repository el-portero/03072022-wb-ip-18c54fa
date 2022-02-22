using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ArchitectInterviewProject.Models;

namespace ArchitectInterviewProject
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var program = new Program();

            if (args.Length < 2)
                program.PrintUsageAndExit();

            try
            {
                await program.DriveAsync(args[0], args[1]);
                program.WaitForKeyPressAndExit(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                program.WaitForKeyPressAndExit(-1);
            }
        }

        private string Usage => $@"
Drives various requests against the architecture interview project API.

ArchitectInterviewProject [base-uri] [bearer-token]

    base-uri        The base URI of the vendor created API.
    bearer-token    A bearer token to use for authenticated requests.

Example: ArchitectInterviewProject https://api.supplylogix.com cGFzc3dvcmQ=
";

        private static HashSet<DriverSite> Sites = new HashSet<DriverSite>
        {
            new DriverSite(
                new Site { NPI = "0000000001", SiteName = "Healthy Drug #01"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4522,
                    TotalInventoryValue = 1722121494.14M,
                    HighestUnitCostSiteItem = new SiteItem("00000004974", 1987, 504.89M, 1003216.43M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000001948", 2998, 326.50M, 978847.00M),
                    HighestInventoryValueSiteItem = new SiteItem("00000004420", 2991, 498.65M, 1491462.15M),
                    LowestUnitCostSiteItem = new SiteItem("00000000169", 1069, 5.24M, 5601.56M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000003248", 0, 500.45M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000003248", 0, 500.45M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000002", SiteName = "Healthy Drug #02"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4496,
                    TotalInventoryValue = 1766151584.58M,
                    HighestUnitCostSiteItem = new SiteItem("00000000886", 2630, 504.66M, 1327255.80M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000003199", 2999, 213.95M, 641636.05M),
                    HighestInventoryValueSiteItem = new SiteItem("00000002241", 2975, 498.21M, 1482174.75M),
                    LowestUnitCostSiteItem = new SiteItem("00000001600", 1877, 5.41M, 10154.57M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000000806", 0, 366.50M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000000806", 0, 366.50M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000003", SiteName = "Healthy Drug #03"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4526,
                    TotalInventoryValue = 1716133021.53M,
                    HighestUnitCostSiteItem = new SiteItem("00000000043", 62, 504.97M, 31308.14M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000001556", 2999, 398.98M, 1196541.02M),
                    HighestInventoryValueSiteItem = new SiteItem("00000002515", 2974, 504.35M, 1499936.90M),
                    LowestUnitCostSiteItem = new SiteItem("00000001140", 1180, 5.02M, 5923.60M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000001366", 0, 299.99M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000001366", 0, 299.99M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000004", SiteName = "Healthy Drug #04"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4511,
                    TotalInventoryValue = 1720858978.26M,
                    HighestUnitCostSiteItem = new SiteItem("00000001586", 2936, 504.95M, 1482533.20M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000003915", 2999, 124.98M, 374815.02M),
                    HighestInventoryValueSiteItem = new SiteItem("00000001586", 2936, 504.95M, 1482533.20M),
                    LowestUnitCostSiteItem = new SiteItem("00000000004", 791, 5.17M, 4089.47M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000000008", 0, 187.48M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000000008", 0, 187.48M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000005", SiteName = "Healthy Drug #05"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4519,
                    TotalInventoryValue = 1738964839.92M,
                    HighestUnitCostSiteItem = new SiteItem("00000003480", 405, 504.86M, 204468.30M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000001611", 2999, 338.80M, 1016061.20M),
                    HighestInventoryValueSiteItem = new SiteItem("00000001440", 2928, 504.69M, 1477732.32M),
                    LowestUnitCostSiteItem = new SiteItem("00000004971", 610, 5.04M, 3074.40M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000000771", 0, 417.62M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000000771", 0, 417.62M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000006", SiteName = "Healthy Drug #06"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4470,
                    TotalInventoryValue = 1714187676.32M,
                    HighestUnitCostSiteItem = new SiteItem("00000003074", 945, 504.78M, 477017.10M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000003713", 2999, 351.07M, 1052858.93M),
                    HighestInventoryValueSiteItem = new SiteItem("00000002014", 2954, 499.52M, 1475582.08M),
                    LowestUnitCostSiteItem = new SiteItem("00000002924", 1975, 5.00M, 9875.00M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000002950", 0, 270.57M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000002950", 0, 270.57M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000007", SiteName = "Healthy Drug #07"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4492,
                    TotalInventoryValue = 1729128973.70M,
                    HighestUnitCostSiteItem = new SiteItem("00000004164", 2221, 504.96M, 1121516.16M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000004592", 2999, 111.58M, 334628.42M),
                    HighestInventoryValueSiteItem = new SiteItem("00000001916", 2965, 503.58M, 1493114.70M),
                    LowestUnitCostSiteItem = new SiteItem("00000004287", 2147, 5.01M, 10756.47M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000001909", 1, 140.83M, 140.83M),
                    LowestInventoryValueSiteItem = new SiteItem("00000003603", 1, 26.67M, 26.67M),
                }),
            new DriverSite(
                new Site { NPI = "0000000008", SiteName = "Healthy Drug #08"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4496,
                    TotalInventoryValue = 1702208014.90M,
                    HighestUnitCostSiteItem = new SiteItem("00000004876", 1336, 504.93M, 674586.48M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000002587", 2999, 118.09M, 354151.91M),
                    HighestInventoryValueSiteItem = new SiteItem("00000004271", 2971, 499.10M, 1482826.10M),
                    LowestUnitCostSiteItem = new SiteItem("00000000144", 301, 5.09M, 1532.09M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000002690", 0, 491.71M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000002690", 0, 491.71M, 0.00M),
                }),
            new DriverSite(
                new Site { NPI = "0000000009", SiteName = "Healthy Drug #09"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4463,
                    TotalInventoryValue = 1717560909.76M,
                    HighestUnitCostSiteItem = new SiteItem("00000002705", 430, 504.79M, 217059.70M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000004638", 2999, 406.04M, 1217713.96M),
                    HighestInventoryValueSiteItem = new SiteItem("00000000787", 2944, 501.96M, 1477770.24M),
                    LowestUnitCostSiteItem = new SiteItem("00000000325", 73, 5.04M, 367.92M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000001572", 1, 155.09M, 155.09M),
                    LowestInventoryValueSiteItem = new SiteItem("00000001613", 1, 117.43M, 117.43M),
                }),
            new DriverSite(
                new Site { NPI = "0000000010", SiteName = "Healthy Drug #10"},
                new InventorySummary 
                { 
                    UniqueDrugCount = 4534,
                    TotalInventoryValue = 1756958855.55M,
                    HighestUnitCostSiteItem = new SiteItem("00000002898", 1244, 504.80M, 627971.20M),
                    HighestInventoryUnitsSiteItem = new SiteItem("00000002814", 2998, 159.96M, 479560.08M),
                    HighestInventoryValueSiteItem = new SiteItem("00000002290", 2998, 500.82M, 1501458.36M),
                    LowestUnitCostSiteItem = new SiteItem("00000000363", 113, 5.28M, 596.64M),
                    LowestInventoryUnitsSiteItem = new SiteItem("00000000815", 0, 302.19M, 0.00M),
                    LowestInventoryValueSiteItem = new SiteItem("00000000815", 0, 302.19M, 0.00M),
                }),
        };

        public void PrintUsageAndExit()
        {
            PrintMsgAndExit(Usage, -1);
        }

        public void PrintMsgAndExit(string msg, int exitCode)
        {
            Console.WriteLine(msg);
            WaitForKeyPressAndExit(exitCode);
        }

        public void WaitForKeyPressAndExit(int exitCode)
        {
            Console.WriteLine($"{Environment.NewLine}Press enter to continue...");
            Console.ReadLine();
        }

        public async Task DriveAsync(string baseUriStr, string bearerToken)
        {
            if (!Uri.TryCreate(baseUriStr, UriKind.Absolute, out Uri baseUri))
            {
                PrintUsageAndExit();
                return;
            }

            using (var handler = new HttpClientHandler())
            {
                // accept invalid certificates
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                using (var client = new HttpClient(handler))
                {
                    client.BaseAddress = baseUri;

                    // give the api a chance to setup/cleanup before running
                    await InitAsync(client, bearerToken);

                    // create the initial sites
                    await CreateSitesAsync(client, bearerToken, Sites.Select(x => x.Site));

                    // check site details
                    await SiteDetailsNoAuthAsync(client);
                    await Task.WhenAll(Sites.Select(site => SiteDetailsAsync(client, bearerToken, site.Site)));

                    // upload a test qoh file and wait for it to finish processing
                    var fileId = await UploadFileAsync(client, bearerToken);
                    await FileDetailsAsync(client, bearerToken, fileId);

                    // check inventory summary details
                    await Task.WhenAll(Sites.Select(site => SiteInventorySummary(client, bearerToken, site)));
                }
            }
        }

        private async Task InitAsync(HttpClient client, string bearerToken)
        {
            var res = await PerformRequestAsync(client, bearerToken, () => new HttpRequestMessage(HttpMethod.Post, "api/init"));

            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);
        }

        private async Task CreateSitesAsync(HttpClient client, string bearerToken, IEnumerable<Site> sites)
        {
            await Task.WhenAll(sites.Select(async site =>
            {
                var res = await PerformRequestAsync(client, bearerToken, () =>
                {
                    var req = new HttpRequestMessage(HttpMethod.Post, "/api/sites");

                    req.Content = new StringContent(JsonConvert.SerializeObject(site), Encoding.UTF8, "application/json");

                    return req;
                });

                Assert.AreEqual(HttpStatusCode.Created, res.StatusCode);
            }));
        }

        private async Task SiteDetailsNoAuthAsync(HttpClient client)
        {
            var res = await client.GetAsync($"api/sites/{Sites.First().Site.NPI}");

            Assert.AreEqual(HttpStatusCode.Unauthorized, res.StatusCode);
        }

        private async Task SiteDetailsAsync(HttpClient client, string bearerToken, Site site)
        {
            using (var req = new HttpRequestMessage(HttpMethod.Get, $"api/sites/{site.NPI}"))
            {
                req.Headers.Authorization = GetAuthHeader(bearerToken);

                var res = await client.SendAsync(req);

                Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

                var res1 = await UnmarshalResponseAsync<Site>(res);

                Assert.NotNull(res1);
                Assert.AreEqual(site, res1);
            }
        }

        private async Task<Guid> UploadFileAsync(HttpClient client, string bearerToken)
        {
            var fileName = "qoh-file.txt";
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

            using (var req = new HttpRequestMessage(HttpMethod.Post, "api/files/qoh"))
            using (var content = new MultipartFormDataContent())
            using (var qohContent = new ByteArrayContent(File.ReadAllBytes(Path.Combine(path, "Resources", fileName))))
            {
                req.Headers.Authorization = GetAuthHeader(bearerToken);

                content.Add(qohContent, "qoh-file", fileName);

                req.Content = content;

                var res = await client.SendAsync(req);

                Assert.AreEqual(HttpStatusCode.Created, res.StatusCode);

                var res1 = await UnmarshalResponseAsync<UploadFileResponse>(res);

                Assert.NotNull(res1);
                Assert.AreEqual(fileName, res1.FileName);
                Assert.AreEqual(FileStatus.Processing, res1.Status);

                return res1.Id;
            }
        }

        private async Task FileDetailsAsync(HttpClient client, string bearerToken, Guid id)
        {
            var maxDepth = 5;

            async Task<FileDetail> RunAsync(int depth)
            {
                var res = await PerformRequestAsync(client, bearerToken, () => new HttpRequestMessage(HttpMethod.Get, $"api/files/qoh/{id}"));

                Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

                var res1 = await UnmarshalResponseAsync<FileDetail>(res);

                if (res1.Status == FileStatus.Completed)
                    return res1;

                if (depth >= maxDepth)
                    return null;

                await Task.Delay(TimeSpan.FromSeconds(1));

                return await RunAsync(depth + 1);
            }

            var fileDetails = await RunAsync(0);

            Assert.NotNull(fileDetails);
            Assert.AreEqual(id, fileDetails.Id);
            Assert.AreEqual(FileStatus.Completed, fileDetails.Status);
            Assert.AreEqual(2, fileDetails.InvalidRecordCount);
            Assert.AreEqual(45031, fileDetails.TotalRecordCount);
            Assert.AreEqual(45029, fileDetails.ValidRecordCount);
        }

        private async Task SiteInventorySummary(HttpClient client, string bearerToken, DriverSite site)
        {
            var res = await PerformRequestAsync(client, bearerToken, () => new HttpRequestMessage(HttpMethod.Get, $"api/sites/{site.Site.NPI}/inventory-summary"));

            Assert.AreEqual(HttpStatusCode.OK, res.StatusCode);

            var summary = await UnmarshalResponseAsync<InventorySummary>(res);

            Assert.NotNull(summary);
            Assert.AreEqual(site.InventorySummary, summary);
        }

        private async Task<HttpResponseMessage> PerformRequestAsync(HttpClient client, string bearerToken, Func<HttpRequestMessage> requestGenerator)
        {
            using (var req = requestGenerator())
            {
                req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                req.Headers.Authorization = GetAuthHeader(bearerToken);

                var res = await client.SendAsync(req);

                return res;
            }
        }

        private AuthenticationHeaderValue GetAuthHeader(string bearerToken)
        {
            return new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        private async Task<T> UnmarshalResponseAsync<T>(HttpResponseMessage res)
        {
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
        }
    }
}
