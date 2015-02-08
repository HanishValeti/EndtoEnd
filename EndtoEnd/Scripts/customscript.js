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

    _oncsecClick = function () {
        debugger;
        $("#dialog").dialog({
            autoOpen: false
        });
        //alert("ba");
        BootstrapDialog.alert('I want banana!');
        $("#dialog").dialog("open");
    };

    return {
        onDeleteSuccess: _onDeleteSuccess,
        onDeleteFailure: _onDeleteFailure,
        onConfirmDone: _onConfirmDone,
        oncsecClick: _oncsecClick
    };

}();

$(".btn-primary").on("click", function () {
    if (($(this).find("input").attr("id") === "IsModal")) {
        $("#regdiv").replaceWith("<div id=\"modaldiv\"><p><button type=\"button\" id=\"csec\" class=\"btn btn-primary btn-sn\" data-toggle=\"modal\" data-target=\"#myModal\">Create New</button></p></div>");
    } else {
        $("#modaldiv").replaceWith("<div id=\"regdiv\"><p><button type=\"button\" id=\"csec\" class=\"btn btn-primary btn-sn\" onclick=\"window.location='/SecurityMfMvc/Create'\">Create New</button></p></div>");
    }

});

$("#myModal").on("show.bs.modal", function (event) {
    //var button = $(event.relatedTarget); // Button that triggered the modal
    //event.preventDefault();
    var modal = $(this);
    $.ajax({
        cache: false,
        url: "/SecurityMfMvc/Create",
        type: "GET",
        dataType: "html",
        //data: { id: id },
        success: function (response, status, data) {
            modal.find(".modal-body p").html(response);
        },
        error: function (e) {
            modal.load(e);
        }
    });
    //var recipient = button.on("click", function (e) {
    //    //var id = $(this).data('id');
    //});
});

// Initialize numeric spinner input boxes
//$(".numeric-spinner").spinedit();
// Initialize modal dialog
// attach modal-container bootstrap attributes to links with .modal-link class.
// when a link is clicked with these attributes, bootstrap will display the href content in a modal dialog.
$("body").on("click", ".modal-link", function (e) {
    e.preventDefault();
    $(this).attr("data-target", "#myModal");
    $(this).attr("data-toggle", "modal");
});
// Attach listener to .modal-close-btn's so that when the button is pressed the modal dialog disappears
//$("body").on("click", ".close", function () {
//    $("#myModal").modal("hide");
//});
////clear modal cache, so that new content can be loaded
//$("#myModal").on("hidden.bs.modal", function () {
//    $(this).removeData("bs.modal");
//});
//$("#CancelModal").on("click", function () {
//    return false;
//});