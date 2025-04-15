using api_acesso_ia.Models;
using api_acesso_ia.Dtos;
using api_acesso_ia.Repositories.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using api_acesso_ia.Services.Interfaces;
using api_acesso_ia.Request;

namespace api_acesso_ia.Services
{
    public class AcessoService : IAcessoService
    {
        private readonly IAcessoRepository _acessoRepository;

        public AcessoService(IAcessoRepository acessoRepository)
        {
            _acessoRepository = acessoRepository;
        }

        public Task<AcessoResponse> BuscarPorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AcessoResponse> BuscarPorEmailService(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AcessoResponse>> ListarTodos()
        {
            return _acessoRepository.ListarTodos();
        }

        public async Task<bool> Registrar(int IdUsuario, DateTime DataHoraAcesso)
        {
            
            var sucesso = await _acessoRepository.Registrar(IdUsuario, DataHoraAcesso);

            var usuarioExists = await _acessoRepository.BuscarPorId(IdUsuario);

            if (sucesso && !string.IsNullOrEmpty(usuarioExists.Email))
            {
                string assunto = "Registro de Acesso cadastrado com sucesso";
                string dataHora = DataHoraAcesso.ToString("dd/MM/yyyy HH:mm");
                string mensagem = $"Olá {usuarioExists.Nome},\n\n" +
                                  $"Seu registro de acesso foi realizado com sucesso em {dataHora}.";

                await EnviarEmailAsync(usuarioExists.Email, assunto, mensagem);
            }

            return sucesso;
        }

        public async Task<bool> EnviarEmailAsync(string destinatario, string assunto, string mensagem)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Meu sistema", "danielemarquespessoal@gmail.com"));
            email.To.Add(MailboxAddress.Parse(destinatario));
            email.Subject = assunto;
            email.Body = new TextPart("plain") { Text = mensagem };

            using var smtp = new SmtpClient();
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

        Task IAcessoService.BuscarPorEmail(string email)
        {
            return BuscarPorEmail(email);
        }

        Task IAcessoService.BuscarPorEmailService(string email)
        {
            return BuscarPorEmailService(email);
        }
    }
}
