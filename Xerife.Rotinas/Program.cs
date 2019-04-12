using System;
using Xerife.Business;

namespace Xerife.Rotinas
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            if (args.Length == 0)
            {
                Console.WriteLine("####                 MENU                   ####");
                Console.WriteLine("#### 1 - Remover Acessos Temporarios da VPN ####");
                Console.WriteLine("#### 2 - Executar Integração Channel x TFS  ####");
                Console.WriteLine("################################################");
                input = Console.ReadLine();
            }
            else
            {
                input = args[0];
            }
            switch (input)
            {
                case "1":
                    var uvb = new UsuarioVpnBus();
                    uvb.RemoverAcessosTemporariosVpn();
                    break;
                case "2":
                    var pb = new ProjetoBus();
                    pb.ExecutarIntegracaoChannelTfs();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Aperte qualquer tecla para sair.");
                    Console.Read();
                    break;
            }
        }
    }
}
