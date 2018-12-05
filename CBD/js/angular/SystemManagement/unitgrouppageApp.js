//var app = angular.module("app", ['trNgGrid']);
app.controller("unitgrouppageController", unitgrouppageController);
app.$inject = ["$scope", "cbdService"];

function unitgrouppageController($scope, cbdService) {
    $scope.pageNumber = 0;
    $scope.pageSize = 10;
    $scope.orderby = "";
    $scope.isdesc = false;

    $scope.Items = [];
    $scope.Total = 0;
    $scope.IsEdit = false;

    $scope.Status = [];
    $scope.Units = [];

    $scope.init = function () {
        $scope.UsedState();
        $scope.getAllUnits('');
        $scope.clear(false);
    }

    $scope.clear = function (isreset) {
        $scope.sCode = '';
        $scope.sName = '';
        $scope.sOrder = '';
        if (isreset === true) {
            $scope.sParentid = undefined;
            $scope.sStatus = undefined;
        }

        $scope.parentid = '';
        $scope.code = "";
        $scope.name = "";
        $scope.namevi = "";
        $scope.nameen = "";
        $scope.url = "";
        $scope.icon = "";
        $scope.order = "";
        $scope.status = "";
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
            CODE: $scope.sCode, NAME: $scope.sName, PARENT_ID: $scope.sParentid === null || $scope.sParentid === undefined ? null : $scope.sParentid, USED_STATE: $scope.sStatus === null || $scope.sStatus === undefined ? 0 : $scope.sStatus, PageSize: $scope.pageSize, PageNumber: $scope.pageNumber, OrderBy: $scope.orderby, IsDesc: $scope.isdesc
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
        ShowLoader();
        $scope.getAllUnits(record.ID);
        HideLoader();
        $scope.id = record.ID;
        $scope.code = record.CODE;
        $scope.name = record.NAME;
        $scope.parentid = record.PARENT_ID;
        $scope.status = record.USED_STATE;

        $('#dialogModal').modal('show');
    }

    $scope.addOrUpdate = function () {
        let record = {
            ID: $scope.IsEdit === true ? $scope.id : 0,
            CODE: $scope.code,
            NAME: $scope.name,
            PARENT_ID: $scope.parentid,
            USED_STATE: $scope.status,
        }

        if ($scope.IsEdit) {
            $scope.update(record);
        }
        else {
            $scope.add(record);
        }
        $scope.getAllUnits();
        $scope.clear();
    };

    $scope.add = function (record) {

        ShowLoader();
        cbdService.AddRecord(record).then(function (d) {
            // Success
            HideLoader();
            if (d.data.Code === 200) {
                //$scope.Items.unshift(d.data.Data);
                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'Unit has been added successfully.'); // Success
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
                //var currentRecord = _.first(_.where($scope.Items, { "ID": record.ID }));
                //var index = $scope.Items.indexOf(currentRecord);
                //$scope.Items[index] = angular.copy(record);
                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'Unit has been updated successfully.'); // Success
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
                //var index = $scope.Items.indexOf(record);
                //if (index > -1) {
                //    $scope.Items.splice(index, 1);
                //}
                $scope.getData();
                $scope.getAllUnits();
                DisplayMessage('Success', 'Unit has been deleted successfully.'); // Success
            } else {
                DisplayServerErrorMessage(d.data.Message); // Failed
            }
        }, function (status) {
            HideLoader();
            DisplayServerErrorMessage(status.data); // Failed
        });
    }

    $scope.getAllUnits = function (record) {
        //ShowLoader();
        cbdService.GetAllUnits(record).then(function (d) {
            // Success
            //HideLoader();
            $scope.Units = d.data.Data;
        }, function (status) {
            //HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }
}

app.factory('cbdService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.getData = function (options) {
        return $http.get(baseUrl + '/api/Unit/Search?CODE=' + options.CODE + '&NAME=' + options.NAME + '&PARENT_ID=' + options.PARENT_ID + '&USED_STATE=' + options.USED_STATE + '&PageSize=' + options.PageSize + '&PageNumber=' + options.PageNumber + '&OrderBy=' + options.OrderBy + '&IsDesc=' + options.IsDesc);
    }
    fac.AddRecord = function (record) {
        return $http.post(baseUrl + '/api/Unit/Add', record);
    }
    fac.UpdateRecord = function (record) {
        return $http.post(baseUrl + '/api/Unit/Update', record);
    }
    fac.DeleteRecord = function (record) {
        return $http.post(baseUrl + '/api/Unit/Delete', record);
    }
    fac.GetAllUnits = function (UnitId) {
        return $http.get(baseUrl + '/api/Unit/GetAllUnits?UnitId=' + UnitId);
    }
    return fac;
}]);
