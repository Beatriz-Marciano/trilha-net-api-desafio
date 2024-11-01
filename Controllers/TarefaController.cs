using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // Buscar o Id no banco utilizando o EF
            //implementado!!!
            var tarefa = _context.Tarefas.Find(id);
            // Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            //implementado!!!
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // Buscar todas as tarefas no banco utilizando o EF
            //implementado!!!
            var tarefa = _context.Tarefas.ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
             // Buscar as tarefas no banco utilizando o EF, que contenha o título recebido por parâmetro
             //implementado!!!
            var tarefa = _context.Tarefas.Where(x => x.Titulo.ToUpper().Contains(titulo.ToUpper())).ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
             // Buscar as tarefas por data no banco utilizando o EF
             //implementado!!!
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
             // Buscar as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
             //implementado!!!
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });
             // Adicionar a tarefa recebida no EF e salvar as mudanças
             //implementado!!!
            _context.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
            
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            //implementado!!!
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Titulo = tarefa.Titulo;
        // Atualizar a variável tarefaBanco no EF e salvar as mudanças
        //implementado!!!
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return Ok(tarefa);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();
        // Remover a tarefa encontrada através do EF e salvar as mudanças
        //implementado!!!
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}