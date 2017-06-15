app.factory("signalR", function ($rootScope) {
    
    var connection = $.connection.hub.url='http://192.168.1.4:8088/signalR';
    var $hub = $.connection.TCPServerHub;
    var signalR = {
        startHub: function () {
            console.log("started");
            connection = $.connection.hub.start();
        },
     
        ////////////////////// CLIENT METHODS////////////////////            
        
        notifyStatus: function (callback) {
            $hub.client.notifyStatus = callback;
        }



    }
    return signalR;
});