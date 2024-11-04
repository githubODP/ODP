namespace CGEODP.Core.Data
{
    public static class CpfUtils
    {
        /// <summary>
        /// Método que mascara o CPF no formato XXX.XXX.XXX-XX.
        /// </summary>
        /// <param name="cpf">O CPF no formato XXX.XXX.XXX-XX.</param>
        /// <returns>O CPF mascarado com "xxx" nos 3 primeiros dígitos e "xx" nos 2 últimos.</returns>
        public static string MascaraCpf(string cpf)
        {
            // Certifique-se de que o CPF tem o tamanho esperado de 14 caracteres (incluindo pontos e traço)
            if (cpf.Length != 14)
            {
                throw new ArgumentException("O CPF precisa ter 14 caracteres, incluindo pontos e traço.");
            }

            // Substituir os 3 primeiros caracteres por "xxx" e os 2 últimos por "xx", mantendo os pontos e traço no lugar
            string mascarado = $"xxx.{cpf.Substring(4, 3)}.{cpf.Substring(8, 3)}-xx";

            return mascarado;
        }





        public static string ReplaceCpf(string cpf)
        {
            // Certifique-se de que o CPF tem o tamanho esperado de 14 caracteres (incluindo pontos e traço)
            if (cpf.Length != 14)
            {
                throw new ArgumentException("O CPF precisa ter 14 caracteres, incluindo pontos e traço.");
            }

            // Mascarar o CPF mantendo os 3 primeiros e os 2 últimos dígitos, substituindo o meio por "X"
            string mascarado = $"{cpf.Substring(0, 3)}.XXX.XXX-{cpf.Substring(12, 2)}";

            return mascarado;
        }


        public static string SubstituiCpf(string cpf)
        {
            // Certifique-se de que o CPF tem o tamanho esperado de 14 caracteres (incluindo pontos e traço)
            if (cpf.Length != 14)
            {
                throw new ArgumentException("O CPF precisa ter 14 caracteres, incluindo pontos e traço.");
            }

            // Substituir os 3 primeiros caracteres por "xxx" e os 2 últimos por "xx", mantendo os pontos e traço no lugar
            string mascarado = $"***.{cpf.Substring(4, 3)}.{cpf.Substring(8, 3)}-**";

            return mascarado;
        }

    }

}
