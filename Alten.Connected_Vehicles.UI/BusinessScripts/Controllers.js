(function () {
    'use strict'
    app.controller("VehiclesController", function ($scope, $rootScope, signalR, Flash, $http) {
       
      
        $scope.customers = [];
        $scope.loadCustomers = function () {
            $http.get('http://localhost:51889/api/Customers').then(function (response) {
                $scope.customers = response.data;
                $scope.$apply();
                console.log(response.data);
                console.log($scope.customers);
            });
        };

        $scope.loadCustomers();
        

        signalR.startHub();


        signalR.notifyStatus(function (RegNo, status) {
            /*$.each($scope.customers, function (i) {
                $.each($scope.customers.Vehicles, function (d) {
                    if ($scope.customers[i].Vehicles[d].RegNo === RegNo) {
                        $scope.customers[i].Vehicles[d].ActiveStatus = status;
                        $scope.$apply();
                    }
                });
            });*/
            $scope.loadCustomers();
            

        });
        
    });

    
})();