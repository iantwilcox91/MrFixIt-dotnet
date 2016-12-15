var test = "it works";

$(function () {


$(".claimjob").click(function () {
    //alert("clicked");
    var jobid = $(this).siblings('.ThisJobId').val();
    //alert(jobid)
    var username = $('.ThisUserName-' + jobid).val();
    //alert(username)
    $(".HideAfterClick-"+jobid).hide();

    $.ajax({
        url: "/Jobs/Claim",
        data: { jobId: jobid, userName: username },
        type: 'GET',
        success: function (result) {
            $('.ClaimedJob-' + jobid).html(result);
        }
    });
});

});