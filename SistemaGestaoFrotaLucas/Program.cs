using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoFrotaLucas
{
    internal class Program
    {



        static List<Veiculo> frota = new List<Veiculo>();
        private static object veiculo;

        static void Main()
        {
            int opcao = 0;

            do
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("================================");
                Console.WriteLine("    SISTEMA DE GESTAO DE FROTA");
                Console.WriteLine("================================");
                Console.ResetColor();

                Console.WriteLine("1 - Adicionar veiculo");
                Console.WriteLine("2 - Consultar veiculos");
                Console.WriteLine("3 - Abastecer veiculo");
                Console.WriteLine("4 - Realizar viagem");
                Console.WriteLine("5 - Sair");

                Console.Write("\nEscolha uma opcao: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opcao invalida!");
                    Console.ReadKey();
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        AdicionarVeiculo(veiculo);
                        break;

                    case 2:
                        ConsultarVeiculos();
                        break;

                    case 3:
                        AbastecerVeiculo();
                        break;

                    case 4:
                        RealizarViagem();
                        break;

                    case 5:
                        Console.WriteLine("Programa terminado!");
                        break;

                    default:
                        Console.WriteLine("Opcao invalida!");
                        break;
                }

                if (opcao != 5)
                {
                    Console.WriteLine("\nPrima uma tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcao != 5);
        }

       
           private static void AdicionarVeiculo(object veiculo)
        {
            try
            {
                Console.Write("Matricula: ");
                string matricula = Console.ReadLine();

                Console.Write("Capacidade do deposito: ");
                int capacidade = int.Parse(Console.ReadLine());

                Veiculo novoVeiculo = new Veiculo(matricula, capacidade);

                novoVeiculo.ReservaAtingida += AvisarReserva;

                frota.Add(novoVeiculo);

                Console.WriteLine("Veiculo adicionado com sucesso!");
                Console.WriteLine("Quilometragem inicial: " + novoVeiculo.Quilometragem + " km");
                Console.WriteLine("Combustivel inicial: " + novoVeiculo.NivelCombustivel + " litros");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
            }
        }
        

        static void AdicionarVeiculo(Veiculo veiculo1)
        {
            try
            {
                Console.Write("Matricula: ");
                string matricula = Console.ReadLine();

                Console.Write("Capacidade do deposito: ");

                if (!int.TryParse(Console.ReadLine(), out int capacidade))
                {
                    Console.WriteLine("Capacidade invalida!");
                    return;
                }
                frota.Add(veiculo1);
                Console.WriteLine("Veiculo adicionado com sucesso!");
                Console.WriteLine("Quilometragem inicial: " + veiculo1.Quilometragem + " km");

                Veiculo veiculo = new Veiculo(matricula, capacidade);

                veiculo.ReservaAtingida += AvisarReserva;

                frota.Add(veiculo);

                Console.WriteLine("Veiculo adicionado com sucesso!");
            }
            catch (Exception erro)
            {
                Console.WriteLine("Erro: " + erro.Message);
            }
        }

        static void ConsultarVeiculos()
        {
            if (frota.Count == 0)
            {
                Console.WriteLine("Nao existem veiculos registados.");
                return;
            }

            foreach (Veiculo veiculo in frota)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Matricula: " + veiculo.Matricula);
                Console.WriteLine("Quilometragem: " + veiculo.Quilometragem + " km");
                Console.WriteLine("Combustivel: " + veiculo.NivelCombustivel.ToString("F2") + " litros");
                Console.WriteLine("Capacidade: " + veiculo.CapacidadeDeposito + " litros");
            }

            Console.WriteLine("------------------------");
        }

        static Veiculo ProcurarVeiculo()
        {
            Console.Write("Introduza a matricula: ");
            string matricula = Console.ReadLine();

            foreach (Veiculo veiculo in frota)
            {
                if (veiculo.Matricula.Equals(
                    matricula, StringComparison.OrdinalIgnoreCase))
                {
                    return veiculo;
                }
            }

            Console.WriteLine("Veiculo nao encontrado!");
            return null;
        }

        static void AbastecerVeiculo()
        {
            Veiculo veiculo = ProcurarVeiculo();

            if (veiculo == null)
                return;

            Console.Write("Quantos litros pretende abastecer? ");

            if (double.TryParse(Console.ReadLine(), out double litros))
            {
                veiculo.Abastecer(litros);
            }
            else
            {
                Console.WriteLine("Quantidade invalida!");
            }
        }

        static void RealizarViagem()
        {
            Veiculo veiculo = ProcurarVeiculo();

            if (veiculo == null)
                return;

            Console.Write("Distancia da viagem em km: ");

            if (int.TryParse(Console.ReadLine(), out int km))
            {
                veiculo.RealizarViagem(km);
            }
            else
            {
                Console.WriteLine("Distancia invalida!");
            }
        }

        static void AvisarReserva(object sender, EventArgs e)
        {
            Veiculo veiculo = (Veiculo)sender;

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n!!! ALERTA DE RESERVA !!!");
            Console.WriteLine("Veiculo: " + veiculo.Matricula);
            Console.WriteLine("Combustivel abaixo de 10%!");

            Console.ResetColor();
        }
    }
}
    

