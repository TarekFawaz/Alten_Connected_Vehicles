(function () {
    'use strict'
    app.controller("VehiclesController", function ($scope, $rootScope, signalR, Flash, $http) {
       
      
        $scope.customers = [];
        $scope.Listcustomers = [];
        $scope.selectedCustomerID = "-1";
        $scope.loadCustomers = function () {
            $http.get('http://localhost:51889/api/Customers').then(function (response) {
                $scope.Listcustomers = response.data;
                if ($scope.selectedCustomerID == -1) {
                    $scope.customers = response.data;
                }
                else
                {
                    $.each(response.data, function (i) {
                        if(response.data[i].ID==$scope.selectedCustomerID)
                        {
                            $scope.customers.length = 0;
                            $scope.customers.push(response.data[i]);
                        }
                    }
                    
                    );
                }
               $scope.$apply();
                console.log(response.data);
                console.log($scope.customers);
            });
        };

        $scope.loadCustomers();
        
         signalR.startHub();


        signalR.notifyStatus(function (RegNo, status) {
           $.each($scope.customers, function (i) {
                $.each($scope.customers[i].Vehicles, function (d) {
                   if ($scope.customers[i].Vehicles[d].RegNo === RegNo) {
                        $scope.customers[i].Vehicles[d].ActiveStatus = status;
                     $scope.$apply();
                    }
                });
            });
           
            

        });
        
    });

    
})();