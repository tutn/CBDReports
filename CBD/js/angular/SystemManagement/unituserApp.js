//var app = angular.module("app", ['trNgGrid']);
app.controller("unituserController", unituserController);
app.$inject = ["$scope", "cbdService"];

function unituserController($scope, cbdService) {
    $scope.pageNumber = 0;
    $scope.pageSize = 10;
    $scope.orderby = "";
    $scope.isdesc = false;

    $scope.Items = [];
    $scope.Total = 0;
    $scope.IsEdit = false;

    $scope.UserItems = [];
    $scope.UserTotal = 0;
    $scope.pageNumberU = 0;
    $scope.pageSizeU = 10;

    $scope.Status = [];
    $scope.Units = [];
    $scope.UnitNodes = [];
    
    $scope.CustomCallback = function (item, selectedItems) {
        if (selectedItems !== undefined && selectedItems.length >= 80) {
            return false;
        } else {
            return true;
        }
    };


    $scope.init = function () {
        $scope.UsedState();
        $scope.getUnitNodes();
        $scope.getUsers()
        $scope.clear(false);
    };

    $scope.clear = function (isreset) {
        $scope.sUserName = '';
        $scope.sUnitId = '';
        if (isreset === true) {
            $scope.sStatus = undefined;
        }

        $scope.username = "";
        $scope.password = "";
        $scope.fullname = "";
        $scope.email = "";
        $scope.files = "";
        $scope.status = '';
    }

    $scope.setPageSize = function (pageSize) {
        $scope.pageSize = pageSize;
    }

    $scope.setPageSizeU = function (pageSize) {
        $scope.pageSizeU = pageSize;
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

    $scope.GenerateUserData = function (currentPage, pageItems) {
        $scope.pageNumberU = currentPage;
        $scope.getUsers();
    }

    $scope.search = function () {
        $scope.getData();
    }

    $scope.getData = function () {
        ShowLoader();
        let options = {
            USER_NAME: $scope.sUserName, UNIT_ID: $scope.sUnitId, USED_STATE: $scope.sStatus === null || $scope.sStatus === undefined ? 0 : $scope.sStatus, PageSize: $scope.pageSize, PageNumber: $scope.pageNumber, OrderBy: $scope.orderby, IsDesc: $scope.isdesc
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

    $scope.getUsers = function () {
        ShowLoader();
        let options = {
            USER_NAME: "", FULL_NAME: "", EMAIL: "", USED_STATE: '', PageSize: $scope.pageSizeU, PageNumber: $scope.pageNumberU, OrderBy: $scope.orderby, IsDesc: $scope.isdesc
        };
        cbdService.GetUsers(options).then(function (d) {
            $scope.UserItems = d.data.Data;
            $scope.UserTotal = d.data.Total;
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
        $scope.username = record.USER_NAME;
        $scope.password = record.PASSWORD;
        $scope.fullname = record.FULL_NAME;
        $scope.email = record.EMAIL;
        $scope.files = record.AVATAR;
        $scope.status = record.USED_STATE;

        $('#dialogModal').modal('show');
    }

    $scope.submit = function () {
        let record = {
            ID: $scope.IsEdit === true ? $scope.id : 0,
            USER_NAME: $scope.username,
            PASSWORD: $scope.password,
            FULL_NAME: $scope.fullname,
            EMAIL: $scope.email,
            AVATAR: $scope.files,
            USED_STATE: $scope.status,
        }

        if ($scope.files) {
            var ext = $scope.files.name.toLowerCase().split('.').pop();
            if (ext === $scope.files.name) return;
            if (ext === 'png' || ext === 'jpg' || ext === 'jpeg') {
                Upload.upload({
                    url: '/api/User/Upload',
                    data: { file: $scope.files, username: $scope.username },
                })
                    .then(function (response) {
                        record.AVATAR = response.data.Data;
                        $scope.addOrUpdate(record);
                        //DisplayMessage('Success', 'File Uploaded successfully.'); // Success
                    }, function (err) {
                        //HideLoader();
                        //DisplayMessage('Warring', 'File Uploaded unsuccessfully.'); // UnSuccess
                    });
            } else {
                alert("Chỉ upload file có định dạng png/ jpg hoặc jpeg. Hãy kiểm tra lại")
            }

        } else {
            $scope.addOrUpdate(record);
        }
    }
    $scope.addOrUpdate = function (record) {
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


                $('#dialogModal').modal('hide');
                $scope.getData();
                DisplayMessage('Success', 'User has been added successfully.'); // Success
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
                DisplayMessage('Success', 'User has been updated successfully.'); // Success
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
                DisplayMessage('Success', 'User has been deleted successfully.'); // Success
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

    $scope.getUnitNodes = function (record) {
        //ShowLoader();
        cbdService.GetUnitNodes(record).then(function (d) {
            // Success
            //HideLoader();
            $scope.UnitNodes = angular.copy(d.data.Data);
            //data1 = d.data.Data;
        }, function (status) {
            //HideLoader();
            //DisplayServerErrorMessage(status.data); // Failed
        });
    }

}

app.factory('cbdService', ["$http", "baseUrl", function ($http, baseUrl) {
    var fac = {};
    fac.getData = function (options) {
        return $http.get(baseUrl + '/api/User/Search?USER_NAME=' + options.USER_NAME + '&FULL_NAME=' + options.FULL_NAME + '&EMAIL=' + options.EMAIL + '&USED_STATE=' + options.USED_STATE + '&PageSize=' + options.PageSize + '&PageNumber=' + options.PageNumber + '&OrderBy=' + options.OrderBy + '&IsDesc=' + options.IsDesc);
    }
    fac.AddRecord = function (record) {
        return $http.post(baseUrl + '/api/User/Add', record);
    }
    fac.UpdateRecord = function (record) {
        return $http.post(baseUrl + '/api/User/Update', record);
    }
    fac.DeleteRecord = function (record) {
        return $http.post(baseUrl + '/api/User/Delete', record);
    }
    fac.GetAllUnits = function (UnitId) {
        return $http.get(baseUrl + '/api/Unit/GetAllUnits?UnitId=' + UnitId);
    }
    fac.GetUnitNodes = function (UnitId) {
        return $http.get(baseUrl + '/api/Unit/GetUnitNodes');
    }
    fac.GetUsers = function (options) {
        return $http.get(baseUrl + '/api/User/Search?USER_NAME=' + options.USER_NAME + '&FULL_NAME=' + options.FULL_NAME + '&EMAIL=' + options.EMAIL + '&USED_STATE=' + options.USED_STATE + '&PageSize=' + options.PageSize + '&PageNumber=' + options.PageNumber + '&OrderBy=' + options.OrderBy + '&IsDesc=' + options.IsDesc);
    }
    return fac;
}]);
