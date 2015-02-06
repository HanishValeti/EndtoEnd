var myModuleJS = new function () {
    _onDeleteSuccess = function(response, status, data) {
        //debugger;
        var id = "#sec-id-" + response.RecordsAffected;
        $(id).remove();
    },
    _onDeleteFailure = function (context) {
        alert("Something wrong happenned with delete operation");
    };
    _onConfirmDone = function() {
        return confirm("Are you sure you want to delete?");
    };

    return {
        onDeleteSuccess: _onDeleteSuccess,
        onDeleteFailure: _onDeleteFailure,
        onConfirmDone: _onConfirmDone
    };

}();