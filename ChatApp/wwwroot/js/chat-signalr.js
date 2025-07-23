var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

var _connectionId = '';

connection.on("ReceiveMessage", function (data) {
    var message = document.createElement("div");
    message.classList.add("message");

    var header = document.createElement("header");
    header.appendChild(document.createTextNode(data.name));

    var p = document.createElement("p");
    p.appendChild(document.createTextNode(data.content));

    var footer = document.createElement("footer");
    footer.appendChild(document.createTextNode(data.TimeStamp));

    message.appendChild(header);
    message.appendChild(p);
    message.appendChild(footer);

    document.querySelector(".chat-body").append(message);
}); 

var joinGroup = function () {
    var url = '/Chats/JoinGroup/' + _connectionId + '/' + window.chatId;
    axios.post(url)
        .then(function (response) {
            console.log("Joined group successfully!");
        })
        .catch(function (error) {
            console.error("Error joining group:", error);
        });
}

connection.start()
    .then(function () {
        connection.invoke('getConnectionId')
            .then(function (connectionId) {
                _connectionId = connectionId;
                joinGroup();
            });
    })
    .catch(function (err) {
        console.log(err)
    });

window.addEventListener('onunload', function () {
    connection.invoke('leaveGroup', '@Model.Id');
});

var sendMessage = function (event) {
    event.preventDefault();
    var data = new FormData(event.target);
    document.getElementById('message-input').value = '';
    axios.post('/Chats/SendMessage', data)
        .then(res => {
            console.log("Message Sent!")
        })
        .catch(err => {
            console.log("Failed to send message!")
        })
}