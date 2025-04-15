using api_acesso_ia.Models;
using api_acesso_ia.Repositories.Interfaces;
using api_acesso_ia.Services.Interfaces;
using MailKit.Security;
using MimeKit;

namespace api_acesso_ia.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IList<Usuario>> ListarTodosService()
        {
            return await _usuarioRepository.ListarTodos();
        }
        public async Task<Usuario> BuscarPorIdService(int id)
        {
            return await _usuarioRepository.BuscarPorId(id);
        }

        public async Task<Usuario> CriarService(Usuario dados)
        {
            var usuarioCriado = await _usuarioRepository.Criar(dados);

            if (usuarioCriado != null && !string.IsNullOrEmpty(usuarioCriado.Email))
            {
                string assunto = "Usuario cadastrado com sucesso ";
                string mensagem = $"Bem vindo {usuarioCriado.Nome},\n\nSeu cadastro foi criado com sucesso!";

                await EnviarEmailAsync(usuarioCriado.Email, assunto, mensagem);
            }

            return usuarioCriado;
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
        public async Task<bool> AtualizarService(Usuario dados)
        {
            return await _usuarioRepository.Atualizar(dados);
        }
        public async Task<bool> DeletarService(int id)
        {
            return await _usuarioRepository.Deletar(id);
        }

        public async Task<bool> CpfJaCadastradoService(string cpf)
        {
             var possui = await _usuarioRepository.CpfJaCadastrado(cpf);
            
            if (possui)
            {
                return true;
            }
            return false;
        }
    }
}
