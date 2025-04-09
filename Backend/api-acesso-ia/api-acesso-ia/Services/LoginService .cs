using api_acesso_ia.Models;
using api_acesso_ia.Repositories;
using api_acesso_ia.Repositories.Interfaces;
using api_acesso_ia.Services.Interfaces;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Security.Cryptography;
using System.Text;


namespace api_acesso_ia.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<LoginUsuario> AutenticarService(string login, string senha)
        {
            var senhaHash = CriptografarSenha(senha);
            return await _loginRepository.Autenticar(login, senhaHash);
        }


        public async Task<LoginUsuario> BuscarPorEmailService(string email)
        {
            return await _loginRepository.BuscarPorEmail(email);
        }

        public async Task<bool> ResetarSenhaService(int idUsuario , string novaSenha)
        {
            var usuarioLogin = await _loginRepository.BuscarPorId(idUsuario);
            if (usuarioLogin == null) return false;

            usuarioLogin.Senha = CriptografarSenha(novaSenha);

            await _loginRepository.Atualizar(usuarioLogin);

            await this.EnviarEmailAsync(usuarioLogin.Email,
            "Sua senha foi resetada",
            $"Olá {usuarioLogin.Nome}, sua senha foi redefinida com sucesso!");

            return true;
        }

        public async Task<bool> EnviarEmailAsync(string destinatario, string assunto, string mensagem)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Meu sistema", "danielemarquespessoal@gmail.com"));
            email.To.Add(MailboxAddress.Parse(destinatario));
            email.Subject = assunto;

            email.Body = new TextPart("plain") { Text = mensagem };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync("danielemarquespessoal@gmail.com", "gxbb qoaz yvvs bfsj"); // App Password
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                return false;
            }

        }
        public async Task<LoginUsuario> CadastrarService(LoginUsuario dados)
        {
            return await _loginRepository.Cadastrar(dados);
        }

      

        public async Task<bool> CpfJaCadastradoService(string cpf)
        {
            return await _loginRepository.CpfJaCadastrado(cpf);
        }

        private string CriptografarSenha(string senha)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(senha);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        string ILoginService.CriptografarSenha(string senha)
        {
            return CriptografarSenha(senha);
        }

        public Task<bool> ResertarService(LoginUsuario dados)
        {
            throw new NotImplementedException();
        }
    }
}
