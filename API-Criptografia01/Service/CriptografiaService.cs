
using System.Text;
using System.Security.Cryptography;

namespace TesteCriptografia
{
    public class CriptografiaService
    {
        #region RSA
        //private RSA CriarInstanciaRsa(
        //    string chave, string vetorInicializacao)
        //{
        //    var teste = RSA.Create();
        //    teste.Encrypt(Encoding.ASCII.GetBytes(chave), RSAEncryptionPadding.OaepSHA512);

        //    return teste;
        //}
        //public string EncriptarRsa(
        //    string chave,
        //    string vetorInicializacao,
        //    string textoNormal)
        //{
        //    if (String.IsNullOrWhiteSpace(textoNormal))
        //    {
        //        throw new Exception(
        //            "O conteúdo a ser encriptado não pode " +
        //            "ser uma string vazia.");
        //    }

        //    using (var algoritmo = CriarInstanciaRsa(
        //        chave, vetorInicializacao))
        //    {

        //        using (MemoryStream streamResultado =
        //               new MemoryStream())
        //        {
        //            using (CryptoStream csStream = new CryptoStream(
        //                streamResultado, encryptor,
        //                CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter writer =
        //                    new StreamWriter(csStream))
        //                {
        //                    writer.Write(textoNormal);
        //                }
        //            }

        //            return ArrayBytesToHexString(
        //                streamResultado.ToArray());
        //        }
        //    }
        //}

        #endregion

        #region Base64
        public string Encriptar64(
            string chave,
            string vetorInicializacao,
            string textoNormal)
        {
            var teste = Encoding.UTF8.GetBytes(textoNormal);
            var teste2 = Convert.ToBase64String(teste);

            var teste3 = Encoding.UTF8.GetBytes(teste2);
            var teste4 = Convert.ToBase64String(teste3);

            return teste4;
        }
        public string Decriptar64(
            string chave,
            string vetorInicializacao,
            string textoNormal)
        {
            var teste = Convert.FromBase64String(textoNormal);

            var teste2 = Encoding.UTF8.GetString(teste);

            return teste2;
        }


        #endregion

        #region Aes Encript
        private Aes CriarInstanciaAes(
            string chave, string vetorInicializacao)
        {
            if (!(chave != null &&
                  (chave.Length == 16 ||
                   chave.Length == 24 ||
                   chave.Length == 32)))
            {
                throw new Exception(
                    "A chave de criptografia deve possuir " +
                    "16, 24 ou 32 caracteres.");
            }

            if (vetorInicializacao == null ||
                vetorInicializacao.Length != 16)
            {
                throw new Exception(
                    "O vetor de inicialização deve possuir " +
                    "16 caracteres.");
            }

            Aes algoritmo = Aes.Create();
            algoritmo.Key =
                Encoding.ASCII.GetBytes(chave);
            algoritmo.IV =
                Encoding.ASCII.GetBytes(vetorInicializacao);

            return algoritmo;
        }

        public string Encriptar(
            string chave,
            string vetorInicializacao,
            string textoNormal)
        {
            if (String.IsNullOrWhiteSpace(textoNormal))
            {
                throw new Exception(
                    "O conteúdo a ser encriptado não pode " +
                    "ser uma string vazia.");
            }

            using (Aes algoritmo = CriarInstanciaAes(
                chave, vetorInicializacao))
            {
                ICryptoTransform encryptor =
                    algoritmo.CreateEncryptor(
                        algoritmo.Key, algoritmo.IV);

                using (MemoryStream streamResultado =
                       new MemoryStream())
                {
                    using (CryptoStream csStream = new CryptoStream(
                        streamResultado, encryptor,
                        CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer =
                            new StreamWriter(csStream))
                        {
                            writer.Write(textoNormal);
                        }
                    }

                    return ArrayBytesToHexString(
                        streamResultado.ToArray());
                }
            }
        }

        private static string ArrayBytesToHexString(byte[] conteudo)
        {
            string[] arrayHex = Array.ConvertAll(
                conteudo, b => b.ToString("X2"));
            return string.Concat(arrayHex);
        }

        public string Decriptar(
            string chave,
            string vetorInicializacao,
            string textoEncriptado)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(textoEncriptado))
                {
                    throw new Exception(
                        "O conteúdo a ser decriptado não pode " +
                        "ser uma string vazia.");
                }

                if (textoEncriptado.Length % 2 != 0)
                {
                    throw new Exception(
                        "O conteúdo a ser decriptado é inválido.");
                }

                using (Aes algoritmo = CriarInstanciaAes(
                    chave, vetorInicializacao))
                {
                    ICryptoTransform decryptor =
                        algoritmo.CreateDecryptor(
                            algoritmo.Key, algoritmo.IV);

                    string textoDecriptografado = "";
                    using (MemoryStream streamTextoEncriptado =
                        new MemoryStream(
                            HexStringToArrayBytes(textoEncriptado)))
                    {
                        using (CryptoStream csStream = new CryptoStream(
                            streamTextoEncriptado, decryptor,
                            CryptoStreamMode.Read))
                        {
                            using (StreamReader reader =
                                new StreamReader(csStream))
                            {
                                textoDecriptografado =
                                    reader.ReadToEnd();
                            }
                        }
                    }

                    return textoDecriptografado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static byte[] HexStringToArrayBytes(string conteudo)
        {
            int qtdeBytesEncriptados =
                conteudo.Length / 2;
            byte[] arrayConteudoEncriptado =
                new byte[qtdeBytesEncriptados];
            for (int i = 0; i < qtdeBytesEncriptados; i++)
            {
                arrayConteudoEncriptado[i] = Convert.ToByte(
                    conteudo.Substring(i * 2, 2), 16);
            }

            return arrayConteudoEncriptado;
        }
        #endregion
    }
}