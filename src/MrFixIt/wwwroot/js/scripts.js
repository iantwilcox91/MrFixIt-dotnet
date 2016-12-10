var test = "it works";


$(".claim-job").submit(function (event) {
    event.preventDefault();
    $("hideButton").hide();
    $.ajax({
        url: "/Jobs/Claim",
        //url: '@Url.Action("Claim")',
        type: 'POST',
        success: function (result) {
            $('#ClaimedJob').html(result);
        } //this should show "You've claimed this job" on the page but has errors
    });
});

$('.AddButton').click(function () {
    $("hideButton").show();
});