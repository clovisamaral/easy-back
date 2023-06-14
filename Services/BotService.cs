using Azure.Storage.Blobs;
using EasyInvoice.API.Entities.Invoices;
using EasyInvoice.API.Repositories.Data;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;


namespace EasyInvoice.API.Services
{
    public class BotService
    {
        List<Invoice> lista = new List<Invoice>();
        public BotService()
        { }

        public bool RunTask(string codigoImovel, string cpf)
        {
            bool statusExec = false;
            string containerNamePrincipal = "faturas";

            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=easyinvoicesblob;AccountKey=eliDk5eEnMGdEERJ5YZUxhbrJGIxNEmR5lk7d/8DQJBQVPW8PX4tFuUrTouF6Eu4/95z0r9h7JH9+AStDxTfNg==;EndpointSuffix=core.windows.net");
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerNamePrincipal);
                containerClient.CreateIfNotExists();

                var _downloadDirectory = System.IO.Path.Combine($"C:\\Temp\\EasyInvoices\\Downloads\\{codigoImovel}\\");

                if (!Directory.Exists(_downloadDirectory))
                {
                    Directory.CreateDirectory(_downloadDirectory);
                }

                new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.Latest);

                var options = new ChromeOptions();

                options.AddUserProfilePreference("download.default_directory", _downloadDirectory);
                options.AddUserProfilePreference("download.prompt_for_download", false);

                //options.AddArgument("--headless");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-gpu");

                ChromeDriverService service = ChromeDriverService.CreateDefaultService(); ChromeDriver driver = new ChromeDriver(service, options);

                using (var webDriver = new ChromeDriver(service, options))
                {
                    webDriver.Manage().Window.Maximize();

                    WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(30));

                    webDriver.Navigate().GoToUrl("https://www.corsan.com.br/inicial");

                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("matriz2-cookie-confirmation-button"))).Click();

                    Thread.Sleep(3000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"bodyPrincipal\"]/div[6]/div[1]/div/div[2]/section/div[2]/div/div[1]/div/h2/a"))).Click();

                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[1]/campos/div/input"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[1]/campos/div/input"))).SendKeys(cpf.Trim());

                    Thread.Sleep(2000);
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[2]/campos/div/div/div[3]/input"))).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[2]/campos/div/div/div[3]/input"))).SendKeys(codigoImovel.Trim());

                    Thread.Sleep(2000);
                    IWebElement button = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("btn-lg")));
                    IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
                    js.ExecuteScript("arguments[0].click();", button);
                    //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\'page-top\']/div[2]/div/div/div/div[1]/form/div[3]/button"))).SendKeys(Keys.Enter);

                    Thread.Sleep(3000);
                    var table = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table")));
                    var rows = table.FindElements(By.XPath("//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr"));

                    for (int i = 1; i < 3; i++)
                    {
                        var fileName = $"{wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[2]"))).Text}.pdf";
                        var status = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[5]"))).Text;
                        var locator = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[6]/i")));
                        wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[7]/div/div[4]/div[2]/button"))).Click();

                        var oldFilePath = System.IO.Path.Combine(_downloadDirectory, "arquivo.pdf");
                        var newFilePath = System.IO.Path.Combine($"{_downloadDirectory}{fileName}");

                        if (File.Exists(System.IO.Path.Combine(_downloadDirectory, fileName)))
                        {
                            File.Delete(System.IO.Path.Combine(_downloadDirectory, fileName));
                        }

                        File.Move(oldFilePath, newFilePath);

                        containerClient.CreateIfNotExists();

                        BlobClient blobClient = containerClient.GetBlobClient(codigoImovel + "/" + fileName);

                        if (blobClient.Exists())
                        {
                            blobClient.DeleteIfExists();
                        }

                        ExtractAndDataInvoice(System.IO.Path.Combine(_downloadDirectory, fileName));

                        using FileStream fileStream = File.OpenRead(System.IO.Path.Combine(_downloadDirectory, fileName));
                        blobClient.Upload(fileStream, true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return statusExec;
        }

        public List<Invoice> GetInvoicesList()
        {
            return lista.ToList();
        }

        public void AddInvoices(Invoice invoice)
        {
            {
                lista.Add(new Invoice
                {
                    Address = invoice.Address,
                    Amount = invoice.Amount,
                    ClientName = invoice.ClientName,
                    Competence = invoice.Competence,
                    Consumption = invoice.Consumption,
                    ContractCode = invoice.ContractCode,
                    DateIssue = invoice.DateIssue,
                    DueDate = invoice.DueDate,
                    InvoiceNumber = invoice.InvoiceNumber,
                });
            };
        }

        public void ExtractAndDataInvoice(string caminhoArquivoPDF)
        {
            Invoice invoice = new Invoice();

            try
            {
                using (PdfReader reader = new PdfReader(caminhoArquivoPDF))
                {
                    string conteudoPDF = string.Empty;

                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        string paginaAtual = PdfTextExtractor.GetTextFromPage(reader, i);
                        conteudoPDF += paginaAtual;
                    }

                    var schema = CorsanLayoutRegex.GetLayoutInvoice();

                    foreach (var item in schema)
                    {
                        Match match = Regex.Match(conteudoPDF, item.Value);

                        if (match.Success)
                        {
                            switch (item.Key)
                            {
                                case "Competence":
                                    invoice.Competence = match.Value;
                                    break;
                                case "DateIssue":
                                    invoice.DateIssue = DateTime.Parse(match.Value);
                                    break;
                                case "InvoiceNumber":
                                    invoice.InvoiceNumber = match.Value;
                                    break;
                                case "ClientName":
                                    invoice.ClientName = match.Value;
                                    break;
                                case "Address":
                                    invoice.Address = match.Value;
                                    break;
                                case "ContractCode":
                                    invoice.ContractCode = match.Value;
                                    break;
                                case "Consumption":
                                    invoice.Consumption = match.Value;
                                    break;
                                case "DueDate":
                                    invoice.DueDate = DateTime.Parse(match.Value);
                                    break;
                                case "Amount":
                                    invoice.Amount = match.Value;
                                    break;
                            }
                        }
                    }
                    AddInvoices(invoice);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu na operação de extração:{ex.Message} ");
            }
        }
    }
}

