using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Boleto2Net;
using BoletoGen2.Models;
using System.Configuration;
using BoletoGen2.Models.Interface;
using BoletoGen2.Models.Class;

namespace BoletoGen2
{
    public class BoletoGenService : IBoletoGenService
    {
        private readonly InstitutionRepository _institutionRepository = new InstitutionRepository();
        private readonly UserRepository _userRepository = new UserRepository();
        public BoletoGenService() { }
        public string BoletoGenGet(Guid InstitutionId, Guid userId)
        {
            var inst = _institutionRepository.SelectInstitution(InstitutionId);
            var user = _userRepository.SelectUser(userId);

            //Cedente
            string path = $"C:\\Users\\caiot\\Documents";
            var objBoletos = new Boletos();
            objBoletos.Banco = Banco.Instancia(inst.BankCode);
            objBoletos.Banco.Cedente = new Cedente();
            objBoletos.Banco.Cedente.CPFCNPJ = inst.CNPJ.Replace(" ", "");
            objBoletos.Banco.Cedente.Nome = inst.Name;
            //objBoletos.Banco.Cedente.Observacoes = "Pague isso logo cara!";

            var conta = new ContaBancaria();
            conta.Agencia = inst.BankAgency.ToString();
            conta.DigitoAgencia = inst.BankAgencyDigit.ToString();
            conta.OperacaoConta = string.Empty;
            conta.DigitoConta = inst.BankAccountDigit.ToString();
            switch (inst.BankCode)
            {
                case 041:
                    conta.CarteiraPadrao = "1";
                    break;
                case 237:
                    conta.CarteiraPadrao = "09";
                    break;
                case 104:
                    conta.CarteiraPadrao = "SIG14";
                    break;
                case 341:
                    conta.CarteiraPadrao = "109";
                    conta.VariacaoCarteiraPadrao = "112";
                    break;
                case 422:
                    conta.CarteiraPadrao = "1";
                    break;
                case 033:
                    conta.CarteiraPadrao = "101";
                    break;
                case 756:
                    conta.CarteiraPadrao = "1";
                    conta.VariacaoCarteiraPadrao = "01";
                    break;
                case 748:
                    conta.CarteiraPadrao = "1";
                    conta.VariacaoCarteiraPadrao = "A";
                    break;
            }
            conta.TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples;
            conta.TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro;
            conta.TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa;
            conta.TipoDocumento = TipoDocumento.Tradicional;

            var ender = new Endereco();
            ender.LogradouroEndereco = inst.Adress;
            ender.LogradouroComplemento = inst.AdressComplement;
            ender.Bairro = inst.Neighborhood;
            ender.Cidade = inst.City;
            ender.UF = "SP";
            ender.CEP = inst.ZIP;

            objBoletos.Banco.Cedente.Codigo = "60063";
            objBoletos.Banco.Cedente.CodigoDV = "6";
            objBoletos.Banco.Cedente.CodigoTransmissao = "000000";
            objBoletos.Banco.Cedente.ContaBancaria = conta;
            objBoletos.Banco.Cedente.Endereco = ender;

            objBoletos.Banco.FormataCedente();


            var Titulo = new Boleto(objBoletos.Banco);
            Titulo.Sacado = new Sacado()
            {
                CPFCNPJ = "03861018250",
                Endereco = new Endereco()
                {
                    Bairro = inst.Neighborhood,
                    CEP = inst.ZIP,
                    Cidade = inst.City,
                    LogradouroEndereco = inst.Adress,
                    LogradouroNumero = "596",
                    LogradouroComplemento = inst.AdressComplement,
                    UF = "SP"
                },
                Nome = user.Name,
                Observacoes = "Pagar com urgênica para não ser protestado"
            };
            Titulo.CodigoOcorrencia = "01";
            Titulo.DescricaoOcorrencia = "Remessa Registrar";
            Titulo.NossoNumero = 1.ToString();
            Titulo.NumeroControleParticipante = "12";
            Titulo.NossoNumero = "123456" + 1.ToString();
            Titulo.DataEmissao = DateTime.Now;
            Titulo.DataVencimento = DateTime.Now.AddDays(15);
            Titulo.ValorTitulo = 200.0M;
            Titulo.Aceite = "N";
            Titulo.EspecieDocumento = TipoEspecieDocumento.DM;
            Titulo.ValorDesconto = 45;

            //multa
            Titulo.DataMulta = DateTime.Now.AddDays(15);
            Titulo.PercentualMulta = 2;
            Titulo.ValorMulta = Titulo.ValorTitulo * (Titulo.PercentualMulta / 100);
            Titulo.MensagemInstrucoesCaixa = $"Cobrar multa de {Titulo.ValorMulta}após a data de vencimento";

            //Juros
            Titulo.DataJuros = DateTime.Now.AddDays(15);
            Titulo.PercentualJurosDia = 10 / 30;
            Titulo.ValorJurosDia = Titulo.ValorTitulo * (Titulo.PercentualJurosDia / 100);
            string instrucoes = $"Cobrar Juros de {Titulo.PercentualJurosDia} por dia";
            if (Titulo.MensagemInstrucoesCaixa == null)
                Titulo.MensagemInstrucoesCaixa = instrucoes;
            else
                Titulo.MensagemInstrucoesCaixa += Environment.NewLine + instrucoes;

            Titulo.CodigoProtesto = TipoCodigoProtesto.NaoProtestar;
            Titulo.CodigoProtesto = 0;
            Titulo.ValidarDados();
            objBoletos.Add(Titulo);


            if (File.Exists(path + "\\remessa.txt"))
                File.Delete(path + "\\remessa.txt");

            //gerar arquivo de remessa
            var st = new MemoryStream();

            var remessa = new ArquivoRemessa(objBoletos.Banco, TipoArquivo.CNAB240, 1);
            remessa.GerarArquivoRemessa(objBoletos, st);

            byte[] fileInBytes;
            using (st)
            {
                fileInBytes = st.ToArray();
            }
           
            var arquivo = new FileStream(path + "\\remessa.txt", FileMode.Create, FileAccess.ReadWrite);
            arquivo.Write(fileInBytes, 0, fileInBytes.Length);

            arquivo.Close();
            st.Close();

            var lerArquivo = new StreamReader(path + "\\remessa.txt");

            var RefazArquivo = new StreamWriter(path + "\\remessa2.txt");
            string strTexto = string.Empty;
            int conta1 = 0;
            while (lerArquivo.Peek() != -1)
            {
                strTexto = lerArquivo.ReadLine();
                conta1 = strTexto.Length;
                if (conta1 < 240)
                {
                    conta1 = 240 - conta1;
                    string strEspaco = string.Empty;
                    for (int i = 0; i < conta1; i++)
                    {
                        strEspaco = strEspaco + " ";
                    }
                    RefazArquivo.WriteLine(strTexto + strEspaco);
                }
                else
                    RefazArquivo.WriteLine(strTexto);
            }
            RefazArquivo.Close();
            lerArquivo.Close();
            //GeraBoleto
            int numBoletos = 0;
            string pdfBase64;
            foreach (var linha in objBoletos)
            {
                numBoletos += 1;
                var novoBoleto = new BoletoBancario();
                novoBoleto.Boleto = linha;
                var pdf = novoBoleto.MontaBytesPDF(false);
                pdfBase64 = Convert.ToBase64String(pdf);
                return pdfBase64;
            }
             return null;

  
        }   }
}