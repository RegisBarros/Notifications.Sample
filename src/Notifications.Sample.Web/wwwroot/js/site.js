$(document).ready(function () {
    $('#btn-do-something').click(function () {
        $.post('do-somethig');
    });

    var transport = signalR.TransportType.WebSockets;

    var connection = new signalR.HubConnection(`http://${document.location.host}/notify`, { transport: transport });

    connection.on('showNotifications', (message) => {
        showMessage(message);
    });

    //button.addEventListener("click", event => {
    //    connection.invoke('send', name, messageInput.value);
    //    messageInput.value = '';
    //    messageInput.focus();
    //});

    var connectionStream = new signalR.HubConnection(`http://${document.location.host}/streaming`, { transport: transport });

    connectionStream.on("streamStarted", function () {
        startStreaming(connectionStream);
    });

    $('#btn-streaming').click(function () {
        connectionStream.invoke("sendStreamInit");
    });

    connectionStream.start();
});

function showMessage(message) {
    $.toast({
        heading: 'Information',
        text: message,
        position: 'top-right',
        stack: false,
        icon: 'success',
        loaderBg: '#9EC600',
        hideAfter: false, 
    })
}

function startStreaming(connectionStream) {
    connectionStream.stream("StartStreaming").subscribe({
        next: onStreamReceived,
        err: function (err) {
            console.log(err);
        },
        complete: function () {
            console.log("finished streaming");
        }
    });
}

function onStreamReceived(data) {
    console.log("received: " + data);
}

