function solve() {

    document.getElementById('send').addEventListener('click', addMessage);
 
    function addMessage() {
       let text = document.getElementById('chat_input').value;
       let messageBox = document.getElementById('chat_messages');
       let newMessage = document.createElement('div');
       document.getElementById('chat_input').value = '';
       newMessage.textContent = text;
       newMessage.setAttribute('class', 'message my-message')
       messageBox.appendChild(newMessage);
    }
 }