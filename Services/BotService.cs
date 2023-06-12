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
            ChromeOptions ChromeOptions = new ChromeOptions();

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

                ChromeOptions.AddUserProfilePreference("download.default_directory", _downloadDirectory);
                ChromeOptions.AddUserProfilePreference("download.prompt_for_download", false);

                //ChromeOptions.AddArguments("--headless");
                ChromeOptions.AddArguments("--incognito");
                ChromeOptions.AddArgument("--disable-extensions");
                ChromeOptions.AddArgument("--disable-gpu");


                using (var webDriver = new ChromeDriver(ChromeOptions))
                {
                    webDriver.Manage().Window.Maximize();

                    //webDriver.Manage().Timeouts().ImplicitWait =  TimeSpan.FromSeconds(30);

                    WebDriverWait wait = new(webDriver, TimeSpan.FromSeconds(50));

                    webDriver.Navigate().GoToUrl("https://www.corsan.com.br/");

                    //webDriver.FindElement(By.Id("matriz2-cookie-confirmation-button")).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("matriz2-cookie-confirmation-button"))).Click();
                    Thread.Sleep(1000);

                    //webDriver.FindElement(By.XPath("/html/body/div[6]/div[1]/div/div[2]/section/div[2]/div/div[1]/div/a")).Click();
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[6]/div[1]/div/div[2]/section/div[2]/div/div[1]/div/a"))).Click();
                    Thread.Sleep(1000);

                    //webDriver.FindElement(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[1]/campos/div/input")).SendKeys(cpf.Trim());
                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[1]/campos/div/input"))).SendKeys(cpf.Trim());
                    Thread.Sleep(1000);

                    wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[2]/campos/div/div/div[3]/input"))).SendKeys(codigoImovel.Trim());

                    WebDriverWait waitButton = new(webDriver, TimeSpan.FromSeconds(60));
                    waitButton.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\'page-top\']/div[2]/div/div/div/div[1]/form/div[3]/button"))).Click();
                    Thread.Sleep(1000);

                    //var table  = webDriver.FindElement(By.XPath("//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table"));
                    var table = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table")));
                    Thread.Sleep(2000);

                    //var rows = table.FindElements(By.XPath("//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr"));
                    var rows = table.FindElements(By.XPath("//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr"));
                    Thread.Sleep(1000);

                    for (int i = 1; i < 4; i++)
                    {
                        Thread.Sleep(1000);
                        var fileName = $"{wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[2]"))).Text}.pdf";
                        //var fileName = $"{webDriver.FindElement(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[2]")).Text}.pdf";

                        Thread.Sleep(1000);
                        var status = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[5]"))).Text;
                        //var status = webDriver.FindElement(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[5]")).Text;

                        Thread.Sleep(1000);
                        var locator = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[6]/i")));
                        //var locator = webDriver.FindElement(By.XPath($"//*[@id=\"page-top\"]/div[2]/div/div/div/div[1]/form/div[1]/div[2]/div/table/tbody/tr[{i}]/td[6]/i"));

                        Thread.Sleep(1000);
                        wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
                        //locator.Click();

                        Thread.Sleep(1000);
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[7]/div/div[4]/div[2]/button"))).Click();
                        //webDriver.FindElement(By.XPath("/html/body/div[7]/div/div[4]/div[2]/button")).Click();

                        var oldFilePath = System.IO.Path.Combine(_downloadDirectory, "arquivo.pdf");

                        var newFilePath = System.IO.Path.Combine($"{_downloadDirectory}{fileName}");

                        if (File.Exists(System.IO.Path.Combine(_downloadDirectory, fileName)))
                        {
                            File.Delete(System.IO.Path.Combine(_downloadDirectory, fileName));
                        }

                        Thread.Sleep(1000);
                        File.Move(oldFilePath, newFilePath);

                        WebDriverWait waitButton2 = new(webDriver, TimeSpan.FromSeconds(60));
                        wait.Until(ExpectedConditions.ElementExists(By.XPath("/html/body/div[6]/div[1]/div/div[2]/section/div[2]/div/div[1]/div/a")));
                        waitButton2.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[6]/div[1]/div/div[2]/section/div[2]/div/div[1]/div/a"))).Click();

                        WebDriverWait waitTxtCpf = new(webDriver, TimeSpan.FromSeconds(60));
                        waitTxtCpf.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[1]/campos/div/input"))).SendKeys(cpf.Trim());


                        WebDriverWait waitTxtCodigoImovel = new(webDriver, TimeSpan.FromSeconds(60));
                        waitTxtCodigoImovel.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[1]/div/div[2]/campos/div/div/div[3]/input"))).SendKeys(codigoImovel.Trim());


                        WebDriverWait waitButton1 = new(webDriver, TimeSpan.FromSeconds(60));
                        waitButton1.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div[2]/div/div/div/div[1]/form/div[3]/button"))).Click();
                        Thread.Sleep(1000);

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

                statusExec = true;
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

