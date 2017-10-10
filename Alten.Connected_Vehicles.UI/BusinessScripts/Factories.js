app.factory("signalR", function ($rootScope) {
    
    var connection = $.connection.hub.url = 'http://10.46.12.108:8088/signalR';
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