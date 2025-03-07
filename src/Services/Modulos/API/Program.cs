using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


    }

    //using Microsoft.AspNetCore.Hosting;
    //using Microsoft.Extensions.Hosting;
    //using System.Net.Http;
    //using System.Security.Cryptography.X509Certificates;

    //namespace API
    //{
    //    public class Program
    //    {
    //        public static void Main(string[] args)
    //        {
    //            // Carregar o certificado
    //            var certificado = CarregarCertificado();

    //            // Configurar o HttpClient para usar o certificado
    //            var handler = new HttpClientHandler();
    //            handler.ClientCertificates.Add(certificado);
    //            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true; // Ignorar erros de validação do servidor (não recomendado para produção)

    //            // Criar o HttpClient
    //            var httpClient = new HttpClient(handler);

    //            // Iniciar a API
    //            CreateHostBuilder(args).Build().Run();
    //        }

    //        public static IHostBuilder CreateHostBuilder(string[] args) =>
    //            Host.CreateDefaultBuilder(args)
    //                .ConfigureWebHostDefaults(webBuilder =>
    //                {
    //                    webBuilder.UseStartup<Startup>();
    //                });

    //        private static X509Certificate2 CarregarCertificado()
    //        {
    //            // Thumbprint do certificado (substitua pelo thumbprint do seu certificado)
    //            string thumbprint = "c113f52c759e4d4edef985f6c01bbd73a591032c";

    //            // Carregar o certificado do repositório do Windows
    //            using (X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
    //            {
    //                store.Open(OpenFlags.ReadOnly);
    //                X509Certificate2Collection certificados = store.Certificates.Find(
    //                    X509FindType.FindByThumbprint,
    //                    thumbprint,
    //                    validOnly: false
    //                );
    //                return certificados.Count > 0 ? certificados[0] : null;
    //            }
    //        }
    //    }
    //}
}
