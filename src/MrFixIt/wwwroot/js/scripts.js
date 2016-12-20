var test = "it works";

$(function () {

    //claim a job
    $(".claimjob").click(function () {
       // alert("clicked");
        var jobid = $(this).siblings('.ThisJobId').val();
        var username = $('.ThisUserName-' + jobid).val();
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



    //mark active
    $(".markActive").click(function () {
        var jobid = $(this).siblings('.ThisJobId').val();
        var title = $('.Title-' + jobid).val();
        $(".ActiveHide-" + jobid).hide();
        //alert("clicked " + jobid + " mark as Active");
        $.ajax({
            type: 'GET',
            data: { jobid: jobid, title: title },
            url: '/Workers/MarkAsActive',
            success: function (result) {
                $('.ThisIsYourActiveJob').html(result);
            }
        });
    });



    //mark compleated
    $(".markCompleated").click(function () {
        var jobid = $(this).siblings('.ThisJobId').val();
        var username = $('.ThisUserName-' + jobid).val();
        var title = $('.Title-' + jobid).val();
        $(".CompleatedHide-" + jobid).hide();
        $(".CompletedJobs").append('<h5> ' + title + ' </h5>');
        //alert("clicked " + title + " mark as compleated");
        $.ajax({
            type: 'GET',
            data: { jobid: jobid },
            url: '/Workers/MarkAsCompleated',
            success: function (result) {
                $('.hiddenDiv').html(result);
            }

        });

    });

});