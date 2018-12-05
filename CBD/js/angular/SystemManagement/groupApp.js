//var app = angular.module("app", ['trNgGrid'])
    app.directive('ckEditor', function () {
        return {
            require: '?ngModel',
            link: function (scope, elm, attr, ngModel) {
                var ck = CKEDITOR.replace(elm[0]);

                if (!ngModel) return;

                ck.on('pasteState', function () {
                    scope.$apply(function () {
                        ngModel.$setViewValue(ck.getData());
                    });
                });

                ngModel.$render = function (value) {
                    ck.setData(ngModel.$viewValue);
                };
            }
        };
    });
app.controller("groupController", groupController);
app.$inject = ["$scope", "cbdService"];

function groupController($scope, cbdService) {
    $scope.pageNumber = 0;
    $scope.pageSize = 10;
    $scope.orderby = "";
    $scope.isdesc = false;

    $scope.Items = [];
    $scope.Total = 0;
    $scope.IsEdit = false;

    $scope.Status = [];

    $scope.init = function () {
        $scope.UsedState();
        $scope.clear(false);
    }

    $scope.clear = function (isreset) {
        $scope.sCode = '';
        $scope.sName = '';
        $scope.sOrder = '';
        if (isreset === true) {
            $scope.sStatus = undefined;
        }

        $scope.code = "";
        $scope.name = "";
        $scope.order = '';
        $scope.status = '';
    }

    $scope.changeCode = function (code) {
        $scope.code = code.toUpperCase();
    }

    $scope.setPageSize = function (pageSize) {
        $scope.pageSize = pageSize;
    }

    $scope.addModel = function () {
        $scope.clear();
        $('#dialogModal').modal('show');
        $scope.IsEdit = false;
    }

    $scope.UsedState = function () {
        $scope.Status.push({ Value: 1, Name: 'Actived' });
        $scope.Status.push({ Value: 2, Name: 'Inactived' });
    }

    //Called from on-data-required directive.
    $scope.GenerateData = function (currentPage, pageItems) {
        $scope.pageNumber = currentPage;
        $scope.getData();
    }

    $scope.search = function () {
        $scope.getData();
    }

    $scope.getData = function () {
        ShowLoader();
        let options = {
            CODE: $scope.sCode, NAME: $scope.sName, ORDER: $scope.sOrder, USED_STATE: $scope.sStatus === null || $scope.sStatus === undefined ? 0 : $scope.sStatus, PageSize: $scope.pageSize, PageNumber: $scope.pageNumber, OrderBy: $scope.orderby, IsDesc: $scope.isdesc
        };
        cbdService.getData(options).then(function (d) {
            $scope.Items = d.data.Data;
            $scope.Total = d.data.Total;
            // Success
            HideLoader();
        }, function (status) {
            HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.editRecord = function (record) {
        $scope.IsEdit = true;
        $scope.id = record.ID;
        $scope.code = record.CODE;
        $scope.name = record.NAME;
        $scope.order = record.ORDER;
        $scope.status = record.USED_STATE;
        $scope.description = record.DESCRIPTION;
        //CKEDITOR.instances.description.insertHtml($scope.description);
        CKEDITOR.instances.description.setData($scope.description);

        $('#dialogModal').modal('show');
    }

    $scope.addOrUpdate = function () {
        $scope.description = CKEDITOR.instances.description.getData();
        let record = {
            ID: $scope.IsEdit === true ? $scope.id : 0,
            CODE: $scope.code,
            NAME: $scope.name,
            ORDER: $scope.order,
            USED_STATE: $scope.status,
            USEDSTATE_NAME: $scope.status === 1 ? 'Actived' : 'Inactived',
            DESCRIPTION: $scope.description,
        }

        if ($scope.IsEdit) {
            $scope.update(record);
        }
        else {
            $scope.add(record);
        }
        $scope.clear();
    };

    $scope.add = function (record) {
        ShowLoader();
        cbdService.AddRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                $scope.Items.unshift(d.data.Data);
                $('#dialogModal').modal('hide');
                //$scope.getData();
                DisplayMessage('Success', 'Group has been added successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.update = function (record) {
        ShowLoader();
        cbdService.UpdateRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                var currentRecord = _.first(_.where($scope.Items, { "ID": record.ID }));
                var index = $scope.Items.indexOf(currentRecord);
                $scope.Items[index] = angular.copy(record);
                $('#dialogModal').modal('hide');
                //$scope.getData();
                DisplayMessage('Success', 'Group has been updated successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.deleteRecord = function (record) {
        ShowLoader();
        cbdService.DeleteRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                var index = $scope.Items.indexOf(record);
                if (index > -1) {
                    $scope.Items.splice(index, 1);
                }
                //$scope.getData();
                DisplayMessage('Success', 'Group has been deleted successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }
}

app.factory('cbdService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.getData = function (options) {
        return $http.get(baseUrl + '/api/Group/Search?CODE=' + options.CODE + '&NAME=' + options.NAME + '&ORDER=' + options.ORDER + '&USED_STATE=' + options.USED_STATE + '&PageSize=' + options.PageSize + '&PageNumber=' + options.PageNumber + '&OrderBy=' + options.OrderBy + '&IsDesc=' + options.IsDesc);
    }
    fac.AddRecord = function (record) {
        return $http.post(baseUrl + '/api/Group/Add', record);
    }
    fac.UpdateRecord = function (record) {
        return $http.post(baseUrl + '/api/Group/Update', record);
    }
    fac.DeleteRecord = function (record) {
        return $http.post(baseUrl + '/api/Group/Delete', record);
    }
    return fac;
}]);
