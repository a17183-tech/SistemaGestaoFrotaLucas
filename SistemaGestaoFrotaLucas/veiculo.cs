using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestaoFrotaLucas
{
  

public class Veiculo
    {
        private int _quilometragem;
        private double _nivelCombustivel;

        public string Matricula { get; set; }
        public int CapacidadeDeposito { get; set; }

        public int Quilometragem
        {
            get { return _quilometragem; }
            set
            {
                if (value < _quilometragem)
                {
                    throw new ArgumentException("A quilometragem não pode diminuir!");
                }

                _quilometragem = value;
            }
        }

        public double NivelCombustivel
        {
            get { return _nivelCombustivel; }
            set
            {
                if (value < 0 || value > CapacidadeDeposito)
                {
                    throw new ArgumentException("Nível de combustível inválido!");
                }

                _nivelCombustivel = value;

                if (_nivelCombustivel < CapacidadeDeposito * 0.10)
                {
                    ReservaAtingida?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler ReservaAtingida;

        public Veiculo(string matricula, int capacidadeDeposito)
        {
            if (string.IsNullOrWhiteSpace(matricula))
                throw new ArgumentException("Matrícula inválida!");

            if (capacidadeDeposito <= 0)
                throw new ArgumentException("Capacidade inválida!");

            Matricula = matricula;
            CapacidadeDeposito = capacidadeDeposito;
            _quilometragem = 0;
            _nivelCombustivel = capacidadeDeposito;
        }

        public Veiculo()
        {
        }

        public void Abastecer(double litros)
        {
            if (litros <= 0)
            {
                Console.WriteLine("Quantidade de combustível inválida!");
                return;
            }

            double novoNivel = NivelCombustivel + litros;

            if (novoNivel > CapacidadeDeposito)
            {
                Console.WriteLine("Aviso: O depósito ficou totalmente cheio!");
                NivelCombustivel = CapacidadeDeposito;
            }
            else
            {
                NivelCombustivel = novoNivel;
            }
        }

        public bool RealizarViagem(int kmDistancia)
        {
            if (kmDistancia <= 0)
            {
                Console.WriteLine("Distância inválida!");
                return false;
            }

            double combustivelNecessario = kmDistancia * 0.07;

            if (NivelCombustivel < combustivelNecessario)
            {
                Console.WriteLine("Combustível insuficiente para realizar a viagem!");
                return false;
            }

            NivelCombustivel -= combustivelNecessario;
            Quilometragem += kmDistancia;

            Console.WriteLine("Viagem realizada com sucesso!");

            return true;
        }
    }

}

