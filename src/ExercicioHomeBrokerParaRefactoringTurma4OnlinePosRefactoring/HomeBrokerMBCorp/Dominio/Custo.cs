using System;

namespace HomeBrokerMBCorp.Dominio
{

    public interface Imposto
    {
        double CalcularImposto(double valorOperacao);        
    }


    public class CustoEmolumento : Imposto
    {
        public double CalcularImposto(double valorOperacao)
        {
            return valorOperacao * 0.000325;
        }
        
    }

    public class CustoIR : Imposto
    {
        public double CalcularImposto(double valorOperacao)
        {
            return valorOperacao * 0.15;
        }
        
    }

    public class CustoIRDayTrade : Imposto
    {
        public double CalcularImposto(double valorOperacao)
        {
            return valorOperacao * .20;
        }
     
    }

    public interface IEstrategiaCorretagem
    {
        //Calcula
        double CalcularCorretagem(double valorOperacao);
    }

    public class EstrategiaCustoCorretagemItau : IEstrategiaCorretagem
    {
        public double CalcularCorretagem(double valorOperacao)
        {
            return (valorOperacao * 0.03) + 10;
        }
    }

    public class EstrategiaCustoCorretagemXPInvestimento : IEstrategiaCorretagem
    {
        public double CalcularCorretagem(double valorOperacao)
        {
            return valorOperacao * 14.90;
        }
    }

    public class EstrategiaCustoCorretagemAgora : IEstrategiaCorretagem
    {
        public double CalcularCorretagem(double valorOperacao)
        {
            return 20;
        }
    }

    public class EstrategiaCustoCorretagemCitiBank : IEstrategiaCorretagem
    {
        public  double CalcularCorretagem(double valorOperacao)
        {
            if (valorOperacao < 100000)
                return 15;

            return CalcularCustoParaOperacaoAcimaDeCemMilReais(valorOperacao);
        }

        private double CalcularCustoParaOperacaoAcimaDeCemMilReais(double valorOperacao)
        {
            return valorOperacao * 0.02 + 15;
        }

    }

    public class EstrategiaCustoCorretagemBancoDoBrasil : IEstrategiaCorretagem
    {
        public  double CalcularCorretagem(double valorOperacao)
        {
            return 10;
        }    
    }

    public class EstrategiaCustoCorretagemAlpes:IEstrategiaCorretagem
    {
        public double CalcularCorretagem(double valorOperacao)
        {
            return 20;
        }
    }


    public class EstrategiaCustoCorretagemBarinsul : IEstrategiaCorretagem
    {
        public bool ehClienteEspecial { get; set; }

        public  double CalcularCorretagem(double valorOperacao)
        {
            if(ehClienteEspecial)
                return 5;

            return 14;
        }
    }

    public class EstrategiaCorretagemFactory
    {
        public IEstrategiaCorretagem CriarEstrategiaCorretagem(TipoCorretora tipoCorretora)
        {
            switch (tipoCorretora)
            {
                case TipoCorretora.ITAU:return new EstrategiaCustoCorretagemItau();
                case TipoCorretora.XP: return new EstrategiaCustoCorretagemXPInvestimento();
                case TipoCorretora.AGORA: return new EstrategiaCustoCorretagemAgora();
                default: throw new Exception(string.Format("Estratégia de corretagem {0} inválida",tipoCorretora));
            }
        }
    }

    public enum TipoCorretora
    {
        ITAU,
        XP,
        AGORA
    }
}
