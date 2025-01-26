fetch('/consumos/create')
    .then(response => {
        if (!response.ok) {
            return response.json(); // Retorna o JSON contendo a mensagem de erro
        }
        return response.json(); // Se a resposta for bem-sucedida
    })
    .then(data => {
        if (data.message && window.location.pathname === '/consumos/index') { // Verifica se está na página correta
            // Exibe a mensagem de erro como um flash message
            showFlashMessage(data.message);
        }
    })
    .catch(error => {
        console.error('Erro na requisição:', error);
    });

// Função para exibir a mensagem
function showFlashMessage(message) {
    const flashMessage = document.createElement('div');
    flashMessage.classList.add('flash-message');
    flashMessage.textContent = message;

    // Adiciona a mensagem à página
    document.body.appendChild(flashMessage);

    // Adiciona um estilo para a mensagem, pode ser ajustado conforme necessário
    flashMessage.style.position = 'fixed';
    flashMessage.style.bottom = '20px';
    flashMessage.style.left = '50%';
    flashMessage.style.transform = 'translateX(-50%)';
    flashMessage.style.backgroundColor = 'red';
    flashMessage.style.color = 'white';
    flashMessage.style.padding = '10px';
    flashMessage.style.borderRadius = '5px';

    // Remover após alguns segundos
    setTimeout(() => {
        flashMessage.remove();
    }, 5000);
}